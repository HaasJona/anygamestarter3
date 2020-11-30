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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security;
using System.Windows.Forms;
using Jonha.TS3.Utils;
using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Collections.Specialized;
using Microsoft.VisualBasic.FileIO;

namespace Jonha.TS3.AnyGameStarter {
    public partial class MainForm : Form {
        private ListViewItem DefaultProfileItem;

        /// <summary>
        /// Gets the game icon from a game\bin folder
        /// </summary>
        /// <param name="folder">A complete path to the game\bin directory</param>
        /// <returns>Key in IconList for the added icon</returns>
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

        /// <summary>
        /// Main Form constructor
        /// </summary>
        public MainForm() {
            InitializeComponent();
        }

        /// <summary>
        /// Refreshes the profile list.
        /// </summary>
        private void ReloadProfiles() {
            Profiles.Items.Clear();
            if (GameUtils.BaseGame != null)
                Profiles.Items.Add(DefaultProfileItem);
            DefaultProfileItem.Group = Profiles.Groups[0];
            foreach (var p in StarterProfile.Profiles) {
                var i = Profiles.Items.Add(p.Name);
                i.SubItems.Add(string.Join(",", p.InstalledGames.ToArray()));
                i.ImageIndex = 0;
                i.Group = Profiles.Groups[1];
            }
            Profiles_SelectedIndexChanged(null, null);
        }

        private void exitButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void Profiles_MouseDown(object sender, MouseEventArgs e) {
            var item = Profiles.HitTest(e.Location).Item;
            addProfileToolStripMenuItem.Visible = item == null;
            propertiesToolStripMenuItem.Visible =
                sep1.Visible =
                sep2.Visible =
                launchItem.Visible =
                openLauncher.Visible =
                duplicateMenuItem.Visible =
                renameMenuItem.Visible = 
                propertiesToolStripMenuItem.Visible =
                openFolderItem.Visible =
                desktopShortcutItem.Visible =
                deleteItem.Visible = item != null;
            if (item == null)
                return;
            renameMenuItem.Enabled = deleteItem.Enabled = item != DefaultProfileItem;
            propertiesToolStripMenuItem.Enabled = deleteItem.Enabled || (Control.ModifierKeys & Keys.Control) != 0;
            openLauncher.Enabled = GameUtils.BaseGame != null;
        }

        private void addProfile_Click(object sender, EventArgs e) {
            AddProfileDialog diag = new AddProfileDialog();
            diag.ShowDialog();
            ReloadProfiles();
        }

        private void launchItem_Click(object sender, EventArgs e) {
            LaunchActiveItem();
        }

        private void LaunchActiveItem() {
            if (Profiles.SelectedItems.Count == 0) return;
            Enabled = false;
            UseWaitCursor = true;
            try {
                if (Profiles.SelectedItems[0].Group == Profiles.Groups[0])
                    StartDefaultGame();
                else
                    StartGame(Profiles.SelectedItems[0].Text);
            }
            finally {
                Enabled = true;
                UseWaitCursor = false;
            }
        }

        private void StartGame(string p) {
            HandleGameStart(AnyGameStarter.Program.StartGame(p));
        }

        /// <summary>
        /// Starts the Sims 3 game just by launching the main executable
        /// </summary>
        private static void StartDefaultGame() {
            Process p = new Process() {
                StartInfo = new ProcessStartInfo(GameUtils.UnifiedExe) {
                    UseShellExecute = false
                }
            };
            p.Start();
        }

        private static void StartDefaultLauncher() {
            Process p = new Process() {
                StartInfo = new ProcessStartInfo(GameUtils.UnifiedLauncherExecutable) {
                    UseShellExecute = false
                }
            };
            p.Start();
        }

        private void openFolderItem_Click(object sender, EventArgs e) {
            DirectoryInfo directory;
            if (Profiles.SelectedItems[0].Group == Profiles.Groups[1]) {
                directory = new DirectoryInfo(new StarterProfile(Profiles.SelectedItems[0].Text).VirtualAppDataFolder);
            }
            else {
                directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Electronic Arts"));
            }
            Directory.CreateDirectory(directory.FullName);
            retry:
            var d2 = FindSimsRoot(directory, 0);
            if (d2 != null)
                directory = d2;
            else {
                var result = MessageBox.Show(Translator.GetText("FirstRunProfile"), "Any Game Starter 3", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result == System.Windows.Forms.DialogResult.Retry) goto retry;
                if (result == System.Windows.Forms.DialogResult.Abort) return;
            }
            string path = directory.FullName;
            Process p = new Process() { StartInfo = new ProcessStartInfo(path) };
            p.Start();
            p.Dispose();
        }

        public static DirectoryInfo FindSimsRoot(DirectoryInfo directory, int iterations) {
            if (new FileInfo(Path.Combine(directory.FullName, "tslus.bin")).Exists)
                return directory;
            if (iterations > 6 || !directory.Exists)
                return null;
            var dirs = directory.GetDirectories();
            DirectoryInfo o = null;
            foreach (var dir in dirs) {
                DirectoryInfo d;
                if ((d = FindSimsRoot(dir, iterations + 1)) != null) {
                    if (o != null)
                        return directory;
                    o = d;
                }
            }
            return o;
        }

        private void deleteItem_Click(object sender, EventArgs e) {
            deleteSelectedProfile();
        }

        private void deleteSelectedProfile() {
            if (Profiles.SelectedItems.Count == 0) return;
            new StarterProfile(Profiles.SelectedItems[0].Text).Delete();
            ReloadProfiles();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Profiles.SelectedItems[0].Group == Profiles.Groups[0]) {
                AddProfileDialog diag = new AddProfileDialog(true);
                diag.ShowDialog();
            }
            else {
                AddProfileDialog diag = new AddProfileDialog(new StarterProfile(Profiles.SelectedItems[0].Text));
                diag.ShowDialog();
                ReloadProfiles();
            }
        }

        private void desktopShortcutItem_Click(object sender, EventArgs e)
        {
            if (Profiles.SelectedItems[0].Group == Profiles.Groups[0])
            {
                ShortcutUtil.Create(Environment.SpecialFolder.Desktop, GameUtils.UnifiedExe, Profiles.SelectedItems[0].Text, String.Empty, Profiles.SelectedItems[0].Text);
            }
            else {
                ShortcutUtil.Create(Environment.SpecialFolder.Desktop, Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Sims3AnyGameStarter.exe"), Profiles.SelectedItems[0].Text, "\"" + Profiles.SelectedItems[0].Text + "\"", Profiles.SelectedItems[0].Text);
            }
        }

        private void Profiles_ItemActivate(object sender, EventArgs e) {
            LaunchActiveItem();
        }

        /// <summary>
        /// Main initialization procedure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e) {
            if (GameUtils.BaseGame == null) {
                if (MessageBox.Show(Translator.GetText("NoSimsFound"), "Any Game Starter 3", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
                    Close();
                    Application.Exit();
                    return;
                }
            }
            //Program.TryRestorePermissions();
            Translator.TranslateForm(this);
            if (StarterProfile.IsFirstRun())
                new AboutBox().ShowDialog();
            DefaultProfileItem = new ListViewItem(Translator.GetText("DefaultProfile"));
            DefaultProfileItem.SubItems.Add("All");
            DefaultProfileItem.ImageIndex = 0;
            Profiles.Groups.Add("default", Translator.GetText("DefaultProfileSection"));
            Profiles.Groups.Add("anygame", Translator.GetText("AdditionalProfileSection"));
            ReloadProfiles();
        }

        private void openLauncher_Click(object sender, EventArgs e) {
            Enabled = false;
            UseWaitCursor = true;
            try {
                if (Profiles.SelectedItems[0].Group == Profiles.Groups[0])
                    StartDefaultLauncher();
                else
                    HandleGameStart(AnyGameStarter.Program.StartGame(Profiles.SelectedItems[0].Text, true));
            }
            finally {
                Enabled = true;
                UseWaitCursor = false;
            }
        }

        private void HandleGameStart(StartGameResult startGameResult) {
            this.Activate();
            if (startGameResult == StartGameResult.Success)
                return;
            using (var dialog = new StartGameErrorDialog(startGameResult)) {
                dialog.ShowDialog();
            }
        }

        private void about_Click(object sender, EventArgs e) {
            (new AboutBox()).ShowDialog();
        }

        private void Profiles_SelectedIndexChanged(object sender, EventArgs e) {
            bool b = (Profiles.SelectedIndices.Count == 1);
            foreach (Control c in panel1.Controls) {
                if (c is Label) {
                    c.Visible = b;
                }
            }
            if (b) {
                lblSelectionName.Text = Profiles.SelectedItems[0].Text;
                if (Profiles.SelectedItems[0].Group == Profiles.Groups[1]) {
                    var item = new StarterProfile(Profiles.SelectedItems[0].Text);
                    var directory = new DirectoryInfo(item.VirtualAppDataFolder);
                    var d2 = FindSimsRoot(directory, 0);
                    if (d2 != null)
                        directory = d2;
                    size.Text = FormatSize(DirSize(directory));
                    mods.Text = NumMods(new DirectoryInfo(Path.Combine(directory.FullName, "Mods"))).ToString();
                    savegames.Text = NumSafeGames(new DirectoryInfo(Path.Combine(directory.FullName, "Saves"))).ToString();
                    creation.Text = item.CreationTime.ToString();
                    language.Text = item.Locale;
                }
                else {
                    var directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Electronic Arts"));
                    var d2 = FindSimsRoot(directory, 0);
                    if (d2 != null)
                        directory = d2;
                    size.Text = FormatSize(DirSize(directory));
                    mods.Text = NumMods(new DirectoryInfo(Path.Combine(directory.FullName, "Mods"))).ToString();
                    savegames.Text = NumSafeGames(new DirectoryInfo(Path.Combine(directory.FullName, "Saves"))).ToString();
                    creation.Text = directory.CreationTime.ToString();
                    language.Text = GameUtils.Locale;
                }
            }
        }

        private string FormatMods(long p) {
            return Translator.GetText("Mods", p.ToString());
        }

        private string FormatSize(double p) {
            if (p > 1000 * 1000 * 1000)
                return Math.Round(p / (1000 * 1000 * 1000), 2).ToString() + " GB";
            if (p > 1000 * 1000)
                return Math.Round(p / (1000 * 1000), 2).ToString() + " MB";
            return Math.Round(p / (1000)).ToString() + " KB";
        }

        public static long DirSize(DirectoryInfo d) {
            long Size = 0;
            if (!d.Exists)
                return 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis) {
                Size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis) {
                Size += DirSize(di);
            }
            return (Size);
        }

        public static long NumMods(DirectoryInfo d) {
            long Size = 0;
            if (!d.Exists)
                return 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles("*.package");
            foreach (FileInfo fi in fis) {
                Size++;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis) {
                Size += NumMods(di);
            }
            return (Size);
        }

        public static long NumSafeGames(DirectoryInfo d) {
            long Size = 0;
            if (!d.Exists)
                return 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles("*.sims3");
            foreach (FileInfo fi in fis) {
                Size++;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories("*.sims3");
            foreach (DirectoryInfo di in dis) {
                Size++;
            }
            return (Size);
        }

        private void addProfileToolStripMenuItem_Click(object sender, EventArgs e) {
            AddProfileDialog diag = new AddProfileDialog();
            diag.ShowDialog();
            ReloadProfiles();
        }

        private void duplicateMenuItem_Click(object sender, EventArgs e) {
            if (Profiles.SelectedItems.Count == 1) {
                StarterProfile oldProfile;
                String oldFolder;
                String newProfileAddOn = "";
                if (Profiles.SelectedItems[0] == DefaultProfileItem) {
                    oldProfile = null;
                    var directory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Electronic Arts"));
                    var root = FindSimsRoot(directory, 6);
                    oldFolder = root.FullName;
                    newProfileAddOn = Path.Combine(root.Parent.Parent.Name, Path.Combine(root.Parent.Name, root.Name));
                }
                else {
                    oldProfile = new StarterProfile(Profiles.SelectedItems[0].Text);
                    oldFolder = oldProfile.VirtualAppDataFolder;
                }
                var diag = new AddProfileDialog(oldProfile, true);
                diag.ShowDialog();
                var newProfile = diag.Profile;
                if (newProfile == null) return;
                var newFolder = newProfile.VirtualAppDataFolder;
                if (newProfileAddOn != null) {
                    newFolder = Path.Combine(newFolder, newProfileAddOn);
                    new DirectoryInfo(newFolder).Create();
                }
                try {
                    new Computer().FileSystem.CopyDirectory(oldFolder, newFolder, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                    newProfile.Delete();
                }
                ReloadProfiles();
            }
        }
        private void Profiles_ItemDrag(object sender, ItemDragEventArgs e) {
            String file = null;

            if (Profiles.SelectedItems[0].Group == Profiles.Groups[0]) {
                file = ShortcutUtil.Create(SpecialDirectories.Temp, GameUtils.UnifiedExe, Profiles.SelectedItems[0].Text, String.Empty, Profiles.SelectedItems[0].Text);
            }
            else {
                file = ShortcutUtil.Create(SpecialDirectories.Temp, Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Sims3AnyGameStarter.exe"), Profiles.SelectedItems[0].Text, "\"" + Profiles.SelectedItems[0].Text + "\"", Profiles.SelectedItems[0].Text);
            }
            if (file == null) return;
            var data = new DataObject(DataFormats.UnicodeText, file);
            data.SetText(file);
            var str = new StringCollection();
            str.Add(file);
            data.SetFileDropList(str);
            Profiles.DoDragDrop(data, DragDropEffects.Move);
            var fileInfo = new FileInfo(file);
            if (fileInfo.Exists) {
                fileInfo.Delete();
            }
        }

        private void Profiles_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                deleteSelectedProfile();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.F2) {
                renameSelectedProfile();
                e.Handled = true;
            }
        }

        private void renameMenuItem_Click(object sender, EventArgs e) {
            renameSelectedProfile();
        }

        private void renameSelectedProfile() {
            if (Profiles.SelectedItems.Count == 1) {
                Profiles.SelectedItems[0].BeginEdit();
            }
        }
        private void Profiles_BeforeLabelEdit(object sender, LabelEditEventArgs e) {
            if (e.Item == DefaultProfileItem.Index) {
                e.CancelEdit = true;
            }
        }

        private void Profiles_AfterLabelEdit(object sender, LabelEditEventArgs e) {
            var label = e.Label;
            if (String.IsNullOrEmpty(label) || (label.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)) {
                e.CancelEdit = true;
                return;
            }
            label = label.Trim();
            try {
                StarterProfile.rename(Profiles.Items[e.Item].Text, label);
                lblSelectionName.Text = label;
            }
            catch (Exception ex) {
                e.CancelEdit = true;
                MessageBox.Show(ex.Message);
            }
        }
    }
}