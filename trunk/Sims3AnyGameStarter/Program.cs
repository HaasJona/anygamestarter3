/*
 * Copyright (c) 2010 , Jonathan Haas
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without 
 * modification, are permitted provided that the following conditions are met:
 *    * Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 *    * Redistributions in binary form must reproduce the above copyright 
 *      notice, this list of conditions and the following disclaimer in the 
 *      documentation and/or other materials provided with the distribution.
 *    
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE 
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE. 
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using Jonha.TS3.Utils;
using Microsoft.Win32;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;

namespace Jonha.TS3.AnyGameStarter
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            bool isNew;
            using (var mutex = new Mutex(true, "JonhaAnyGameStarter", out isNew))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (args.Length == 0)
                {
                    if (!isNew)
                    {
                        MessageBox.Show(Translator.GetText("AlreadyRunning"));
                        return;
                    }
                    Application.Run(new MainForm());
                }
                else if (args.Length == 1)
                {
                    if (!isNew) {
                        MessageBox.Show(Translator.GetText("AlreadyRunning"));
                        return;
                    }
                    StartGame(args[0]);
                }
                else if (args.Length == 3 && args[0] == "-restore")
                {
                    if (isNew) {
                        RestoreAfterCrash(args[1], args[2]);
                    }
                }
                else
                {
                    MessageBox.Show("Usage: AnyGameStarter.exe ProfileName", "Any Game Starter 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            throw (e.Exception);
        }

        private static void RestoreAfterCrash(string language, string myDocumentsPath)
        {
            TryRestorePermissions();
            GameUtils.Locale = language;
            if (AdjustMyDocuments(myDocumentsPath))
                MessageBox.Show("Sims 3 settings have been restored after a crash:\n\nLanguage: " + language + "\nDocuments path: " + myDocumentsPath, "Any Game Starter 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Sims 3 settings have been restored after a crash:\n\nLanguage: " + language, "Any Game Starter 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool TryRestorePermissions()
        {
            try
            {
                RestorePermissions();
            }
            catch (SecurityException)
            {
                if (MessageBox.Show(Translator.GetText("NeedToFixRegistryPermissions"), "Any Game Starter 3", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return false;
                }
                Process p = new Process()
                {
                    StartInfo = new ProcessStartInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "FixRegistryRights.exe"))
                    {
                        UseShellExecute = true,
                        Verb = "runas"
                    }
                };
                try
                {
                    p.Start();
                    p.WaitForExit();
                    AnyGameStarter.Program.RestorePermissions();
                }
                catch
                {
                    MessageBox.Show(Translator.GetText("NeedToFixRegistryPermissionsFail"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        public static StartGameResult StartGame(string profile)
        {
            return StartGame(profile, false);
        }

        public static StartGameResult StartGame(string profile, bool startLauncher)
        {
            return StartGame(profile, startLauncher, false);
        }

        public static StartGameResult StartGame(string profile, bool startLauncher, bool firstStart)
        {
            if (!TryRestorePermissions())
                return StartGameResult.PermissionsNeeded;
            GameUtils.RedetectGames();
            StarterProfile e = null;
            try
            {
                e = new StarterProfile(profile);
            }
            catch (Exception)
            {
            }

            if (e == null)
                return StartGameResult.ProfileNotFound;

            CreateSystemRestore();

            try
            {
                foreach (var game in GameUtils.InstalledGames)
                {
                    if (game != GameUtils.BaseGame && !e.InstalledGames.Contains(game.Name) && game.RegistryKey != null)
                    {
                        RemovePermissions(game.RegistryKey);
                    }
                }
            }
            catch (SecurityException)
            {
                return StartGameResult.PermissionsNeeded;
            }
            var oldlocale = GameUtils.Locale;

            try
            {
                GameUtils.Locale = e.Locale;
            }
            catch (SecurityException)
            {
                return StartGameResult.PermissionsNeeded;
            }

            string fname = e.Executable;
            if (startLauncher)
            {
                var games = new List<Game>();
                foreach (Game game in GameUtils.InstalledGames)
                {
                    if (e.InstalledGames.Contains(game.Name))
                    {
                        games.Add(game);
                    }
                }
                fname = GameUtils.UnifiedLauncherExecutable;
            }
            string oldMyDocuments;
            string folder;
            try {
                folder = AdjustMyDocuments(e.VirtualAppDataFolder, out oldMyDocuments);
            }
            catch (ArgumentException) {
                GameUtils.Locale = oldlocale;
                foreach (var game in GameUtils.InstalledGames) {
                    if (game.RegistryKey != null) {
                        RestorePermissions(game.RegistryKey);
                    }
                }
                RemoveSystemRestore();
                return StartGameResult.Success;
            }
            folder = folder.Replace("%USERPROFILE%", e.VirtualAppDataFolder);
            try
            {
                Directory.CreateDirectory(folder);
                Process p = new Process() { StartInfo = new ProcessStartInfo(fname) { UseShellExecute = false } };
                p.StartInfo.EnvironmentVariables["userprofile"] = e.VirtualAppDataFolder;
                p.StartInfo.WorkingDirectory = Path.GetDirectoryName(fname);
                p.Start();
                try {
                    p.WaitForInputIdle();
                    if (p.MainWindowTitle == "" || startLauncher)
                    {
                        if (firstStart && !startLauncher)
                            p.Kill();
                        else
                            p.WaitForExit();
                        p.Close();
                        return StartGameResult.Success;
                    }
                    else
                    {
                        p.WaitForExit();
                        p.Close();                        
                        return StartGameResult.EarlyGameExit;
                    }
                }
                catch (Exception)
                {
                    p.Close();
                    return StartGameResult.Success;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Translator.GetText("StartGameErrorDialog"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return StartGameResult.Failure;
            }
            finally
            {
                try {
                    AdjustMyDocuments(oldMyDocuments);
                }
                finally {
                    GameUtils.Locale = oldlocale;
                    foreach (var game in GameUtils.InstalledGames) {
                        if (game.RegistryKey != null) {
                            RestorePermissions(game.RegistryKey);
                        }
                    }
                    RemoveSystemRestore();
                }
            }
        }

        private static void CreateSystemRestore()
        {
            string personalFolder = (string)Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", false).GetValue("Personal", null, RegistryValueOptions.DoNotExpandEnvironmentNames);
            string locale = GameUtils.Locale;
            var runOnceKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\RunOnce", RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (runOnceKey.GetValue("Sims3AnyGameStarter.exe", null) != null)
                return;
            runOnceKey.SetValue("AnyGameStarterRestore", "\"" + Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Sims3AnyGameStarter.exe") + "\" -restore \"" + locale + "\" \"" + personalFolder + "\"", RegistryValueKind.String);
        }

        private static void RemoveSystemRestore()
        {
            var runOnceKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\RunOnce", true);
            runOnceKey.DeleteValue("AnyGameStarterRestore", false);
        }

        public static void RestorePermissions()
        {
            foreach (var game in GameUtils.InstalledGames)
            {
                if (game.RegistryKey != null) {
                    RestorePermissions(game.RegistryKey);
                }
            }
        }

        private static bool AdjustMyDocuments(string p)
        {
            var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", true);
            string o = (string)key.GetValue("Personal", null, RegistryValueOptions.DoNotExpandEnvironmentNames);
            if (o == p) return false;
            key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", true);
            key.SetValue("Personal", p, RegistryValueKind.ExpandString);
            return true;
        }

        private static string AdjustMyDocuments(string p, out string old)
        {
            var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", true);
            string o = (string)key.GetValue("Personal", null, RegistryValueOptions.DoNotExpandEnvironmentNames);
            old = o;
            if (o.StartsWith("%USERPROFILE%"))
            {
                return o;
            }
            if (o.StartsWith(Environment.GetEnvironmentVariable("userprofile")))
            {
                string newval = o.Replace(Environment.GetEnvironmentVariable("userprofile"), "%USERPROFILE%");
                //key.DeleteValue("Personal");
                key.SetValue("Personal", newval, RegistryValueKind.ExpandString);
                return newval;
            }
            var result = MessageBox.Show(Translator.GetText("DocumentPathModified"),"Any Game Starter 3", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK) {
                string newval = @"%USERPROFILE%\Documents";
                key.SetValue("Personal", newval, RegistryValueKind.ExpandString);
                return newval;
            }
            else {
                throw new ArgumentException();
            }
        }

        private static RegistryAccessRule _rule;

        private static RegistryAccessRule DenyAccessRule
        {
            get
            {
                if (_rule == null)
                    _rule = new RegistryAccessRule(new NTAccount(WindowsIdentity.GetCurrent().Name), System.Security.AccessControl.RegistryRights.QueryValues, System.Security.AccessControl.AccessControlType.Deny);
                return _rule;
            }
        }

        private static void RestorePermissions(string p)
        {
            //var key = Registry.LocalMachine.OpenSubKey(p, RegistryKeyPermissionCheck.ReadSubTree, RegistryRights.ReadPermissions);
            //var access = key.GetAccessControl();
            //if (access.RemoveAccessRule(DenyAccessRule))
            //{
            //    key.Close();
            //    return;
            //}
            //key.Close();

            var key = Registry.LocalMachine.OpenSubKey(p, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.ChangePermissions);
            var access = key.GetAccessControl();
            access.RemoveAccessRule(DenyAccessRule);
            key.SetAccessControl(access);
            key.Close();
        }

        private static void RemovePermissions(string p)
        {
            var key = Registry.LocalMachine.OpenSubKey(p, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.ChangePermissions);
            var access = key.GetAccessControl();
            access.AddAccessRule(DenyAccessRule);
            key.SetAccessControl(access);
            key.Close();
        }

        public static string T(string o)
        {
            return Translator.GetText(o);
        }
    }

    public enum StartGameResult
    {
        Success,
        PermissionsNeeded,
        ProfileNotFound,
        Failure,
        EarlyGameExit,
    }
}
