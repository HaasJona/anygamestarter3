/*
 * Copyright (c) 2010, Jonathan Haas
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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security;
using Microsoft.Win32;
using Jonha.TS3.AnyGameStarter;
using System.Text.RegularExpressions;
using System;

namespace Jonha.TS3.Utils
{
    public static class GameUtils
    {
        private static string datapath = Path.Combine(Path.Combine("GameData", "Shared"), "Packages");

        private static List<Game> games;

        private static Log log;

        public static string UnifiedExe { get; set; }

        public static string UnifiedLauncherExecutable { get; set; }

        public static bool IsUsingSteam { get; set; }

        public static ReadOnlyCollection<Game> InstalledGames
        {
            get
            {
                if (games == null)
                    DetectGames();
                return games.AsReadOnly();
            }
        }

        static public void RedetectGames()
        {
            games = null;
            DetectGames();
        }

        public static Game BaseGame
        {
            get
            {
                if (games == null)
                    RedetectGames();
                foreach (var game in games)
                {
                    if (game.Name == "The Sims 3")
                        return game;
                }
                return null;
            }
        }

        public static string Locale
        {
            get
            {
                try
                {
                    if (GameUtils.BaseGame != null && GameUtils.BaseGame.RegistryKey != null)
                    {
                        var key = Registry.LocalMachine.OpenSubKey(GameUtils.BaseGame.RegistryKey, false);
                        var s = (string)key.GetValue("Locale");
                        key.Close();
                        return s;
                    }
                }
                catch (SecurityException)
                {
                    return "en-US";
                }
                return "en-US";
            }
            set
            {
                if (value == Locale)
                    return;
                if (GameUtils.BaseGame != null)
                {
                    var key = Registry.LocalMachine.OpenSubKey(GameUtils.BaseGame.RegistryKey, true);
                    key.SetValue("Locale", value);
                    key.Close();
                }
            }
        }

        private static void DetectGames()
        {
            log = new Log();
            games = new List<Game>();
            string path32 = "Software\\Sims\\";
            string path64 = "Software\\Wow6432Node\\Sims\\";
            DetectGames(path32);
            DetectGames(path64);
            if (games.Count == 0) {
                string steamPath32 = "Software\\Valve\\Steam\\";
                string steamPath64 = "Software\\Wow6432Node\\Valve\\Steam\\";
                DetectSteam(steamPath32);
                DetectSteam(steamPath64);
            }
            games.Sort((a, b) => { return a.ProductID.CompareTo(b.ProductID); });
        }

        private static void DetectSteam(string path) {
            log.Put("Detecting steam games in " + path);
            var key = Registry.LocalMachine.OpenSubKey(path, false);
            if (key != null) {
                log.Put("Key found. Checking InstallPath.");
                string installPath = (string)key.GetValue("InstallPath");
                if (installPath != null) {
                    addSteamGame(Path.Combine(installPath , @"steamapps\common\the sims 3\"));
                }
            }
            else {
                log.Put("Key not found.");
            }
        }

        private static void addSteamGame(string installPath) {
            if (new DirectoryInfo(installPath).Exists) {
                AddGame("The Sims 3", "The Sims 3 (Steam)", installPath, null, -1, -1);
                IsUsingSteam = true;
            }
        }

        private static void DetectGames(string path)
        {
            log.Put("Detecting games in " + path);
            var key = Registry.LocalMachine.OpenSubKey(path, false);
            if (key != null) {
                log.Put("Key found. Checking Subkeys.");
                var subkeys = key.GetSubKeyNames();
                foreach (var item in subkeys) {
                    AddGame(key, item);
                }
            }
            else {
                log.Put("Key not found.");
            }
        }

        private static void AddGame(RegistryKey key, string item)
        {
            log.Put("Trying to add " + item);
            try
            {
                var subkey = key.OpenSubKey(item);
                log.Put("Permissions OK.");
                if (subkey != null)
                {
                    log.Put("Item found.");
                    string displayName = subkey.GetValue("DisplayName", item).ToString();
                    log.Put("DisplayName: " + displayName);
                    var installDir = subkey.GetValue("Install Dir");
                    var sku = subkey.GetValue("SKU", 0);
                    var id = subkey.GetValue("ProductID", 1);
                    if (installDir != null)
                    {
                        log.Put("InstallDir: " + installDir);
                        AddGame(item, displayName, installDir.ToString(), subkey.Name, (int)sku, (int)id);
                    }
                    else
                    {
                        log.Put("No InstallDir. Skipping...");
                    }
                }
            }
            catch (SecurityException)
            {
                var game = new Game(item, string.Empty, item, key.Name.Substring(19) + @"\" + item, 0, 0);
                games.Add(game);
            }
        }

        private static void AddGame(string name, string displayName, string installDir, string registryKey, int sku, int productId)
        {
            var f = Path.Combine(Path.Combine(installDir, "Game"), "Bin");
            log.Put("Checking Folder: " + f);
            var folder = new DirectoryInfo(f);
            if (!folder.Exists)
            {
                log.Put("Fail."); log.Put("");
                return;
            }
            if (folder.GetFiles("TS3CAP.exe").Length > 0) {
                log.Put("Create-A-Pet-Demo. Continue."); log.Put("");
                return;
            }
            log.Put("OK.");
            f = Path.Combine(installDir, datapath);
            log.Put("Checking Folder: " + f);
            var folder2 = new DirectoryInfo(f);
            if (!folder2.Exists)
            {
                log.Put("Fail."); log.Put("");
                return;
            }
            log.Put("OK.");

            log.Put("Searching Exe files");
            var exe = new List<FileInfo>(folder.GetFiles("TS3W.exe"));
            if (exe.Count == 1) {
                log.Put("OK. Using TS3W.exe: " + exe[0].Name);
                UnifiedExe = exe[0].FullName;
            }

            exe = new List<FileInfo>(folder.GetFiles("Sims3LauncherW.exe"));
            if (exe.Count == 1) {
                log.Put("OK. Using Sims3LauncherW.exe: " + exe[0].Name);
                UnifiedLauncherExecutable = exe[0].FullName;
            }

            var game = new Game(name, installDir, displayName, registryKey == null ? null : registryKey.Substring(19), sku, productId);
            games.Add(game);
            log.Put("Game added...");
            log.Put("");
        }

        public static string GetLog()
        {
            return log.Get();
        }
    }

    public class Game
    {
        public string Directory { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string RegistryKey { get; private set; }
        public int Sku { get; private set; }
        public int ProductID { get; private set; }

        public Game(string name, string directory, string displayName, string registryKey, int sku, int productID)
        {
            this.Directory = directory;
            this.DisplayName = displayName;
            this.RegistryKey = registryKey;
            this.Name = name;
            this.Sku = sku;
            this.ProductID = productID;
        }
    }
}
