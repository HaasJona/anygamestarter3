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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Jonha.TS3.Utils;
using System.Globalization;

namespace Jonha.TS3.AnyGameStarter {
    public partial class AddProfileDialog : Form {
        public StarterProfile Profile { get; private set; }

        public static class Languages {
            public class Language : IComparable<Language> {
                public string SimsIdentifier {
                    get;
                    private set;
                }
                public string IetfIdentifier {
                    get;
                    private set;
                }

                public override string ToString() {
                    try {
                        return (new CultureInfo(IetfIdentifier)).DisplayName;
                    }
                    catch {
                        return "? (" + IetfIdentifier + ")";
                    }
                }

                public Language(string simsIdentifier, string ietfIdentifier) {
                    this.SimsIdentifier = simsIdentifier;
                    this.IetfIdentifier = ietfIdentifier;
                }

                public int CompareTo(Language other) {
                    return this.ToString().CompareTo(other.ToString());
                }
            }

            public static Language Lang(string simsIdentifier, string ietfIdentifier) {
                return new Language(simsIdentifier, ietfIdentifier);
            }

            public static readonly Language English = Lang("en-US", "en");
            public static readonly Language ChineseTraditional = Lang("zh-CN", "zh-CN");
            public static readonly Language ChineseTaiwan = Lang("zh-TW", "zh-TW");
            public static readonly Language Czech = Lang("cs-CZ", "cs");
            public static readonly Language Danish = Lang("da-DK", "da");
            public static readonly Language Dutch = Lang("nl-NL", "nl");
            public static readonly Language Finnish = Lang("fi-FI", "fi");
            public static readonly Language French = Lang("fr-FR", "fr");
            public static readonly Language German = Lang("de-DE", "de");
            public static readonly Language Greek = Lang("el-GR", "el");
            public static readonly Language Hungarian = Lang("hu-HU", "hu");
            public static readonly Language Italian = Lang("it-IT", "it");
            public static readonly Language Japanese = Lang("ja-JP", "ja");
            public static readonly Language Korean = Lang("ko-KR", "ko");
            public static readonly Language Norwegian = Lang("no-NO", "no");
            public static readonly Language Polish = Lang("pl-PL", "pl");
            public static readonly Language Portuguese = Lang("pt-PT", "pt");
            public static readonly Language PortugueseBrazilian = Lang("pt-BR", "pt-BR");
            public static readonly Language Russian = Lang("ru-RU", "ru");
            public static readonly Language Spanish = Lang("es-ES", "es");
            public static readonly Language SpanishMexican = Lang("es-MX", "es-MX");
            public static readonly Language Swedish = Lang("sv-SE", "sv");
            public static readonly Language Thai = Lang("th-TH", "th");

            public static List<Language> Sku1Languages = new List<Language>()
            {
                English, French, Spanish
            };

            public static List<Language> Sku2Languages = new List<Language>()
            {
                English, Czech, Danish, Dutch, Finnish, French, German, Greek, Hungarian, Italian, Norwegian, Polish, Portuguese, PortugueseBrazilian, Russian, Spanish, Swedish
            };

            public static List<Language> Sku3Languages = new List<Language>()
            {
                English, ChineseTaiwan, Korean
            };

            public static List<Language> Sku5Languages = new List<Language>()
            {
                English, Japanese
            };

            public static List<Language> Sku7Languages = new List<Language>()
            {
                English, Czech, Danish, Dutch, Finnish, French, German, Greek, Hungarian, Italian, Norwegian, Polish, Portuguese, PortugueseBrazilian, Russian, Spanish, Swedish, Japanese, ChineseTaiwan, ChineseTraditional, Korean, Thai
            };
        }

        public AddProfileDialog() {
            init();
        }

        private bool _dontChangeExecutable = false;
        public AddProfileDialog(StarterProfile p) {
            if (p == null) {
                init();
            }
            else {
                init(p);
            }
        }

        private void init() {
            InitializeComponent();
            AdvancedCheck.Checked = false;
            Translator.TranslateForm(this);
            InitLocales();
            initGames();
            int i = 1;
        b:
            string name = "AnyGame " + i;
            foreach (var profile in StarterProfile.Profiles) {
                if (profile.Name == name) {
                    i++;
                    goto b;
                }
            }
            nameBox.Text = name;
            if (GameUtils.UnifiedExe != null) {
                executableBox.Text = GameUtils.UnifiedExe;
            }
            else {
                AdvancedCheck.Checked = true;
            }
            languageBox.Text = GameUtils.Locale;
        }

        private void init(StarterProfile p) {
            InitializeComponent();
            Translator.TranslateForm(this);
            InitLocales();
            AnyGameStarter.Program.TryRestorePermissions();
            GameUtils.RedetectGames();
            initGames();
            nameBox.Enabled = false;
            niceLanguageBox.Enabled = false;
            languageBox.Enabled = false;
            locationBox.Enabled = false;
            _dontChangeExecutable = true;
            foreach (string game in p.InstalledGames) {
                if (expansionBox.Items.ContainsKey(game))
                    expansionBox.Items[game].Checked = expansionBox.CheckBoxes;
            }
            nameBox.Text = p.Name;
            executableBox.Text = p.Executable;
            languageBox.Text = p.Locale;
            Text = Translator.GetText("EditProfileDialog");
            AdvancedCheck.Checked = false;
        }

        private void initGames() {
            foreach (var game in GameUtils.InstalledGames) {
                if (game.Name != "The Sims 3")
                    AddGame(game.DisplayName, game.Name, game.Directory);
            }
            if (GameUtils.UnifiedExe == null) {
                expansionBox.Items.Add(Translator.GetText("NoGame"), "Empty");
                expansionBox.CheckBoxes = false;
            }
            if (GameUtils.IsUsingSteam) {
                expansionBox.Items.Add(Translator.GetText("SteamMode"), "Steam");
                expansionBox.CheckBoxes = false;
            }
            if (expansionBox.Items.Count == 0) {
                expansionBox.Items.Add(Translator.GetText("NoExpansions"), "Empty");
                expansionBox.CheckBoxes = false;
            }
        }

        public AddProfileDialog(bool b) {
            InitializeComponent();
            Translator.TranslateForm(this);
            InitLocales();
            initGames();
            nameBox.Text = "Default";
            locationBox.Text = "";
            executableBox.Text = GameUtils.UnifiedExe;
            languageBox.Text = GameUtils.Locale;
            nameBox.Enabled = false;
            locationBox.Enabled = false;
            executableBox.Enabled = false;
            expansionBox.Enabled = false;
            foreach (ListViewItem i in expansionBox.Items) {
                i.Checked = expansionBox.CheckBoxes;
            }
            Text = Translator.GetText("DefaultProfileDialog");
            AdvancedCheck.Checked = false;
        }

        private void AddGame(string displayName, string internalName, string installDir) {
            var folder = new DirectoryInfo(Path.Combine(Path.Combine(installDir, "Game"), "Bin"));
            var item = new ListViewItem(displayName, getIcon(folder));
            item.SubItems.Add(installDir);
            item.Name = internalName;
            if (!expansionBox.Items.ContainsKey(installDir))
                expansionBox.Items.Add(item);
        }

        private string getIcon(DirectoryInfo folder) {

            var icons = folder.GetFiles("Sims3*.ico");
            if (icons.Length > 0) {
                Icon image = new Icon(icons[0].FullName, 32, 32);
                if (IconList.Images.ContainsKey(icons[0].FullName))
                    return icons[0].FullName;
                IconList.Images.Add(icons[0].FullName, image);
                return icons[0].FullName;
            }
            return "Default";
        }

        private void NameBox_TextChanged(object sender, EventArgs e) {
            try {
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folder = Path.Combine(folder, "AnyGameStarter3");
                folder = Path.Combine(folder, "Games");
                folder = Path.Combine(folder, nameBox.Text);
                locationBox.Text = folder;
            }
            catch (Exception) { }
        }

        private void NameBox_Validating(object sender, CancelEventArgs e) {
            nameBox.Text = nameBox.Text.Trim();
            OKButton.Enabled = !String.IsNullOrEmpty(nameBox.Text);
            if (nameBox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || nameBox.Text.IndexOfAny(Path.GetInvalidPathChars()) >= 0 || nameBox.Text.IndexOfAny(new Char[] { '<', '>' }) >= 0) {
                MessageBox.Show(Translator.GetText("NoSpecialChars") + "\n\":,*?<>/\\");
                e.Cancel = true;
                return;
            }
            foreach (var profile in StarterProfile.Profiles) {
                if (profile.Name == nameBox.Text) {
                    MessageBox.Show(Translator.GetText("ProfileAlreadyExists"));
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void NameBox_Validated(object sender, EventArgs e) {
            try {
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                folder = Path.Combine(folder, "AnyGameStarter3");
                folder = Path.Combine(folder, "Games");
                folder = Path.Combine(folder, nameBox.Text);
                locationBox.Text = folder;
            }
            catch (Exception) { }
        }

        private void button1_Click(object sender, EventArgs e) {
            if (expansionBox.Enabled == true) {
                if (!File.Exists(executableBox.Text)) {
                    MessageBox.Show(Translator.GetText("ExecutableNotFound"));
                    DialogResult = System.Windows.Forms.DialogResult.None;
                    AdvancedCheck.Checked = true;
                    return;
                }
                var games = new List<string>();
                foreach (ListViewItem item in expansionBox.CheckedItems) {
                    games.Add(item.Name);
                }
                if (locationBox.Enabled == true) {
                target:
                    try {
                        var directory = new DirectoryInfo(locationBox.Text);
                        if (directory.Exists) {
                            var result = MessageBox.Show(Translator.GetText("DirectoryAlreadyExists"), "Any Game Starter 3", MessageBoxButtons.AbortRetryIgnore);
                            if (result == System.Windows.Forms.DialogResult.Retry) {
                                goto target;
                            }
                            if (result == System.Windows.Forms.DialogResult.Abort) {
                                DialogResult = System.Windows.Forms.DialogResult.None;
                                return;
                            }
                        }
                        else {
                            directory.Create();
                        }
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                        DialogResult = System.Windows.Forms.DialogResult.None;
                        return;
                    }
                }
                Profile = new StarterProfile(executableBox.Text, locationBox.Text, games, nameBox.Text, languageBox.Text);
                Profile.Save();
                Hide();
            }
            else {
                GameUtils.Locale = languageBox.Text;
                Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = Translator.GetText("Sims3Exe") + " (TS3*.exe)|TS3*.exe";
            ofd.FileName = Path.GetFileName(executableBox.Text);
            try {
                ofd.InitialDirectory = Path.GetDirectoryName(executableBox.Text);
            }
            catch {
                ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            }
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                executableBox.Text = ofd.FileName;
            ofd.Dispose();
        }

        private void button3_Click(object sender, EventArgs e) {
            Close();
        }

        private void InitLocales() {
            languageBox.Items.Clear();
            var languages = Languages.Sku7Languages;
            if (GameUtils.BaseGame != null) {
                switch (GameUtils.BaseGame.Sku) {
                    case 1:
                        languages = Languages.Sku1Languages;
                        break;
                    case 2:
                        languages = Languages.Sku2Languages;
                        break;
                    case 3:
                        languages = Languages.Sku3Languages;
                        break;
                    case 5:
                        languages = Languages.Sku5Languages;
                        break;
                }
            }
            languages.Sort();
            foreach (var lang in languages) {
                languageBox.Items.Add(lang.SimsIdentifier);
                niceLanguageBox.Items.Add(lang);
            }
        }

        private void niceLanguageBox_SelectedIndexChanged(object sender, EventArgs e) {
            languageBox.SelectedIndex = niceLanguageBox.SelectedIndex;
        }

        private void languageBox_SelectedIndexChanged(object sender, EventArgs e) {
            niceLanguageBox.SelectedIndex = languageBox.SelectedIndex;
        }

        private void AdvancedCheck_CheckedChanged(object sender, EventArgs e) {
            int delta = advancedGroup.Height + 6;
            SuspendLayout();
            if (AdvancedCheck.Checked) {
                advancedGroup.Visible = true;
                expansionGroup.Top += delta;
                expansionGroup.Height -= delta;
                languageBox.Visible = true;
            }
            else {
                advancedGroup.Visible = false;
                expansionGroup.Top -= delta;
                expansionGroup.Height += delta;
                languageBox.Visible = false;
            }
            ResumeLayout();
        }

        private void expansionBox_ItemChecked(object sender, ItemCheckedEventArgs e) {
            if (_dontChangeExecutable)
                return;
            //find latest ep
            var expansions = new List<string>();
            foreach (ListViewItem item in expansionBox.Items) {
                if (item.Checked) {
                    expansions.Add(item.Name);
                }
            }
            var games = new List<Game>();
            foreach (Game game in GameUtils.InstalledGames) {
                if (expansions.Contains(game.Name)) {
                    games.Add(game);
                }
            }
            executableBox.Text = GameUtils.UnifiedExe;
        }

        private void AddProfileDialog_Load(object sender, EventArgs e) {
            _dontChangeExecutable = false;
        }

        private void expansionBox_SelectedIndexChanged(object sender, EventArgs e) {

        }

        public AddProfileDialog(StarterProfile source, bool copy)
            : this(source) {
            if (source == null) {
                nameBox.Text = Translator.GetText("DefaultProfile") + " - " + Translator.GetText("Copy");
                foreach (ListViewItem i in expansionBox.Items) {
                    i.Checked = true;
                }
            }
            else {
                nameBox.Text = source.Name + " - " + Translator.GetText("Copy");
            }
            nameBox.Enabled = true;
            niceLanguageBox.Enabled = false;
            languageBox.Enabled = false;
            locationBox.Enabled = true;
            Text = Translator.GetText("CopyProfileDialog");
        }
    }
}
