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

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Jonha.TS3.Utils;

namespace Jonha.TS3.AnyGameStarter
{
    [Serializable()]
    public class StarterProfile
    {
        public DateTime CreationTime { get; private set; }
        public List<string> InstalledGames { get; private set; }
        public string Executable { get; private set; }
        public string VirtualAppDataFolder { get; private set; }
        public string Name { get; private set; }
        public string Locale { get; private set; }

        public static IEnumerable<StarterProfile> Profiles
        {
            get
            {
                var folder = GetAnyGameFolder();
                var d = new DirectoryInfo(folder);
                var list = new List<StarterProfile>();
                foreach (var file in d.GetFiles("*.profile"))
                {
                    try
                    {
                        list.Add(new StarterProfile(file.Name.Substring(0, file.Name.Length - file.Extension.Length)));
                    }
                    catch (ArgumentException)
                    {
                        //TODO: Error code
                    }
                }
                return list;
            }
        }

        public StarterProfile(string executable, string appdatafolder, List<string> games, string name, string locale)
        {
            this.InstalledGames = games;
            this.Executable = executable;
            this.VirtualAppDataFolder = appdatafolder;
            this.Name = name;
            this.Locale = locale;
            this.CreationTime = DateTime.Now;
        }

        public void Save()
        {
            var folder = GetAnyGameFolder();
            Directory.CreateDirectory(folder);
            using (var writer = new StreamWriter(Path.Combine(folder, Name + ".profile"), false))
            {
                writer.WriteLine("Version 2");
                writer.WriteLine(Locale);
                writer.WriteLine(Executable);
                writer.WriteLine(VirtualAppDataFolder);
                foreach (var game in InstalledGames)
                {
                    writer.WriteLine(game);
                }
            }
        }

        public StarterProfile(string name)
        {
            var folder = GetAnyGameFolder();
            Directory.CreateDirectory(folder);
            var fileName = Path.Combine(folder, name + ".profile");
            using (var reader = new StreamReader(fileName))
            {
                var version = reader.ReadLine();
                if (version == "Version 1") {
                    this.Locale = reader.ReadLine();
                    var line = reader.ReadLine();
                        this.Executable = GameUtils.UnifiedExe;
                    this.VirtualAppDataFolder = reader.ReadLine();
                    this.InstalledGames = new List<string>();
                    while (!reader.EndOfStream) {
                        InstalledGames.Add(reader.ReadLine());
                    }
                }
                else if (version == "Version 2") {
                    this.Locale = reader.ReadLine();
                    this.Executable = reader.ReadLine();
                    this.VirtualAppDataFolder = reader.ReadLine();
                    this.InstalledGames = new List<string>();
                    while (!reader.EndOfStream) {
                        InstalledGames.Add(reader.ReadLine());
                    }
                }
                else {
                    throw new ArgumentException("Profile has illegal version");
                }
            }
            this.Name = name;
            this.CreationTime = new FileInfo(fileName).CreationTime;
        }

        public static void rename(String oldName, String newName) {
            var folder = GetAnyGameFolder();
            Directory.CreateDirectory(folder);
            var oldFileName = Path.Combine(folder, oldName + ".profile");
            var newFileName = Path.Combine(folder, newName + ".profile");
            File.Move(oldFileName, newFileName);
        }

        public static string GetAnyGameFolder()
        {
            var folder = GetAnyGameFolderName();
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static string GetAnyGameFolderName()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folder = Path.Combine(folder, "AnyGameStarter3");
            folder = Path.Combine(folder, "Profiles");
            return folder;
        }

        public static bool IsFirstRun()
        {
            return (!Directory.Exists(GetAnyGameFolderName()));
        }

        public void Delete()
        {
            var folder = GetAnyGameFolder();
            if (MessageBox.Show(Translator.GetText("DeleteProfileConfirm", Name), "Any Game Starter 3", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                File.Delete(Path.Combine(folder, Name + ".profile"));
            else
                return;
            if (!Directory.Exists(VirtualAppDataFolder))
                return;
            if (MessageBox.Show(Translator.GetText("DeleteProfileFolder1") +  "\n" + Translator.GetText("DeleteProfileFolder2") + "\n" + VirtualAppDataFolder, "Any Game Starter 3", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                try {
                    Directory.Delete(VirtualAppDataFolder, true);
                }
                catch (Exception) {
                    try {
                        Directory.Delete(VirtualAppDataFolder, true);
                    }
                    catch (Exception) {
                    }
                }

        }
    }
}
