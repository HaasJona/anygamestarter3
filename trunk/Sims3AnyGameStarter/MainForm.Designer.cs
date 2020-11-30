
namespace Jonha.TS3.AnyGameStarter
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this.ProfileContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLauncher = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.desktopShortcutItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.renameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.savegames = new System.Windows.Forms.Label();
            this.creation = new System.Windows.Forms.Label();
            this.mods = new System.Windows.Forms.Label();
            this.language = new System.Windows.Forms.Label();
            this.size = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSelectionName = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.about = new System.Windows.Forms.ToolStripButton();
            this.addProfile = new System.Windows.Forms.ToolStripButton();
            this.Profiles = new Jonha.TS3.AnyGameStarter.ListViewEx();
            this.ProfileContextMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // IconList
            // 
            this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
            this.IconList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconList.Images.SetKeyName(0, "agsicon.ico");
            // 
            // ProfileContextMenu
            // 
            this.ProfileContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProfileToolStripMenuItem,
            this.launchItem,
            this.openLauncher,
            this.openFolderItem,
            this.sep1,
            this.desktopShortcutItem,
            this.sep2,
            this.renameMenuItem,
            this.duplicateMenuItem,
            this.propertiesToolStripMenuItem,
            this.deleteItem});
            this.ProfileContextMenu.Name = "ProfileContextMenu";
            this.ProfileContextMenu.Size = new System.Drawing.Size(140, 214);
            // 
            // addProfileToolStripMenuItem
            // 
            this.addProfileToolStripMenuItem.Name = "addProfileToolStripMenuItem";
            this.addProfileToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.addProfileToolStripMenuItem.Text = "!AddProfile";
            this.addProfileToolStripMenuItem.Click += new System.EventHandler(this.addProfileToolStripMenuItem_Click);
            // 
            // launchItem
            // 
            this.launchItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.launchItem.Name = "launchItem";
            this.launchItem.ShortcutKeyDisplayString = "";
            this.launchItem.Size = new System.Drawing.Size(139, 22);
            this.launchItem.Text = "!Launch";
            this.launchItem.Click += new System.EventHandler(this.launchItem_Click);
            // 
            // openLauncher
            // 
            this.openLauncher.Name = "openLauncher";
            this.openLauncher.Size = new System.Drawing.Size(139, 22);
            this.openLauncher.Text = "!Launcher";
            this.openLauncher.Click += new System.EventHandler(this.openLauncher_Click);
            // 
            // openFolderItem
            // 
            this.openFolderItem.Name = "openFolderItem";
            this.openFolderItem.Size = new System.Drawing.Size(139, 22);
            this.openFolderItem.Text = "!OpenFolder";
            this.openFolderItem.Click += new System.EventHandler(this.openFolderItem_Click);
            // 
            // sep1
            // 
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(136, 6);
            // 
            // desktopShortcutItem
            // 
            this.desktopShortcutItem.Name = "desktopShortcutItem";
            this.desktopShortcutItem.Size = new System.Drawing.Size(139, 22);
            this.desktopShortcutItem.Text = "!Shortcut";
            this.desktopShortcutItem.Click += new System.EventHandler(this.desktopShortcutItem_Click);
            // 
            // sep2
            // 
            this.sep2.Name = "sep2";
            this.sep2.Size = new System.Drawing.Size(136, 6);
            // 
            // renameMenuItem
            // 
            this.renameMenuItem.Name = "renameMenuItem";
            this.renameMenuItem.Size = new System.Drawing.Size(139, 22);
            this.renameMenuItem.Text = "!Rename";
            this.renameMenuItem.Click += new System.EventHandler(this.renameMenuItem_Click);
            // 
            // duplicateMenuItem
            // 
            this.duplicateMenuItem.Name = "duplicateMenuItem";
            this.duplicateMenuItem.Size = new System.Drawing.Size(139, 22);
            this.duplicateMenuItem.Text = "!Duplicate";
            this.duplicateMenuItem.Click += new System.EventHandler(this.duplicateMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.propertiesToolStripMenuItem.Text = "!Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // deleteItem
            // 
            this.deleteItem.Name = "deleteItem";
            this.deleteItem.Size = new System.Drawing.Size(139, 22);
            this.deleteItem.Text = "!Delete";
            this.deleteItem.Click += new System.EventHandler(this.deleteItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.savegames);
            this.panel1.Controls.Add(this.creation);
            this.panel1.Controls.Add(this.mods);
            this.panel1.Controls.Add(this.language);
            this.panel1.Controls.Add(this.size);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblSelectionName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 325);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(6);
            this.panel1.Size = new System.Drawing.Size(477, 64);
            this.panel1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Location = new System.Drawing.Point(455, 43);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(22, 22);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label4.Location = new System.Drawing.Point(84, 43);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "!SaveGames";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label5.Location = new System.Drawing.Point(84, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "!Mods";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label5.Visible = false;
            // 
            // savegames
            // 
            this.savegames.AutoSize = true;
            this.savegames.BackColor = System.Drawing.Color.Transparent;
            this.savegames.Location = new System.Drawing.Point(166, 43);
            this.savegames.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.savegames.Name = "savegames";
            this.savegames.Size = new System.Drawing.Size(62, 13);
            this.savegames.TabIndex = 8;
            this.savegames.Text = "savegames";
            this.savegames.Visible = false;
            // 
            // creation
            // 
            this.creation.AutoSize = true;
            this.creation.BackColor = System.Drawing.Color.Transparent;
            this.creation.Location = new System.Drawing.Point(293, 43);
            this.creation.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.creation.Name = "creation";
            this.creation.Size = new System.Drawing.Size(49, 13);
            this.creation.TabIndex = 6;
            this.creation.Text = "location";
            this.creation.Visible = false;
            // 
            // mods
            // 
            this.mods.AutoSize = true;
            this.mods.BackColor = System.Drawing.Color.Transparent;
            this.mods.Location = new System.Drawing.Point(166, 24);
            this.mods.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.mods.Name = "mods";
            this.mods.Size = new System.Drawing.Size(59, 13);
            this.mods.TabIndex = 7;
            this.mods.Text = "numMods";
            this.mods.Visible = false;
            // 
            // language
            // 
            this.language.AutoSize = true;
            this.language.BackColor = System.Drawing.Color.Transparent;
            this.language.Location = new System.Drawing.Point(293, 24);
            this.language.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.language.Name = "language";
            this.language.Size = new System.Drawing.Size(56, 13);
            this.language.TabIndex = 5;
            this.language.Text = "language";
            this.language.Visible = false;
            // 
            // size
            // 
            this.size.AutoSize = true;
            this.size.BackColor = System.Drawing.Color.Transparent;
            this.size.Location = new System.Drawing.Point(293, 5);
            this.size.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.size.Name = "size";
            this.size.Size = new System.Drawing.Size(26, 13);
            this.size.TabIndex = 4;
            this.size.Text = "size";
            this.size.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(211, 43);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "!Created";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(211, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "!Language";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(231, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "!Size";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label1.Visible = false;
            // 
            // lblSelectionName
            // 
            this.lblSelectionName.AutoEllipsis = true;
            this.lblSelectionName.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectionName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectionName.Location = new System.Drawing.Point(9, 6);
            this.lblSelectionName.Name = "lblSelectionName";
            this.lblSelectionName.Size = new System.Drawing.Size(134, 25);
            this.lblSelectionName.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.about,
            this.addProfile});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(477, 50);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // about
            // 
            this.about.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.about.ForeColor = System.Drawing.Color.Black;
            this.about.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.about.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.about.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(47, 46);
            this.about.Text = "!About";
            this.about.Click += new System.EventHandler(this.about_Click);
            // 
            // addProfile
            // 
            this.addProfile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.addProfile.ForeColor = System.Drawing.Color.Black;
            this.addProfile.Image = ((System.Drawing.Image)(resources.GetObject("addProfile.Image")));
            this.addProfile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addProfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addProfile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.addProfile.Name = "addProfile";
            this.addProfile.Size = new System.Drawing.Size(106, 46);
            this.addProfile.Text = "!AddProfile";
            this.addProfile.Click += new System.EventHandler(this.addProfile_Click);
            // 
            // Profiles
            // 
            this.Profiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Profiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Profiles.ContextMenuStrip = this.ProfileContextMenu;
            this.Profiles.LabelEdit = true;
            this.Profiles.LargeImageList = this.IconList;
            this.Profiles.Location = new System.Drawing.Point(0, 48);
            this.Profiles.Margin = new System.Windows.Forms.Padding(0);
            this.Profiles.MultiSelect = false;
            this.Profiles.Name = "Profiles";
            this.Profiles.Size = new System.Drawing.Size(477, 277);
            this.Profiles.TabIndex = 1;
            this.Profiles.UseCompatibleStateImageBehavior = false;
            this.Profiles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.Profiles_AfterLabelEdit);
            this.Profiles.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.Profiles_BeforeLabelEdit);
            this.Profiles.ItemActivate += new System.EventHandler(this.Profiles_ItemActivate);
            this.Profiles.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.Profiles_ItemDrag);
            this.Profiles.SelectedIndexChanged += new System.EventHandler(this.Profiles_SelectedIndexChanged);
            this.Profiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Profiles_KeyDown);
            this.Profiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Profiles_MouseDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(54)))), ((int)(((byte)(99)))));
            this.ClientSize = new System.Drawing.Size(477, 389);
            this.Controls.Add(this.Profiles);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Any Game Starter 3";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ProfileContextMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList IconList;
        private ListViewEx Profiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip ProfileContextMenu;
        private System.Windows.Forms.ToolStripMenuItem launchItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderItem;
        private System.Windows.Forms.ToolStripSeparator sep2;
        private System.Windows.Forms.ToolStripMenuItem deleteItem;
        private System.Windows.Forms.ToolStripMenuItem desktopShortcutItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLauncher;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton about;
        private System.Windows.Forms.Label lblSelectionName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label creation;
        private System.Windows.Forms.Label language;
        private System.Windows.Forms.Label size;
        private System.Windows.Forms.ToolStripMenuItem addProfileToolStripMenuItem;
        private System.Windows.Forms.Label mods;
        private System.Windows.Forms.Label savegames;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton addProfile;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripMenuItem duplicateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameMenuItem;
    }
}

