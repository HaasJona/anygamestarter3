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

namespace Jonha.TS3.AnyGameStarter
{
    partial class AddProfileDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProfileDialog));
            this.OKButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.locationBox = new System.Windows.Forms.TextBox();
            this.executableBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.advancedGroup = new System.Windows.Forms.GroupBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.languageBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.expansionGroup = new System.Windows.Forms.GroupBox();
            this.expansionBox = new Jonha.TS3.AnyGameStarter.ListViewEx();
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this.cancelButton = new System.Windows.Forms.Button();
            this.niceLanguageBox = new System.Windows.Forms.ComboBox();
            this.AdvancedCheck = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.advancedGroup.SuspendLayout();
            this.expansionGroup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(261, 11);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(88, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "!OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "!Name";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(103, 12);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(123, 22);
            this.nameBox.TabIndex = 0;
            this.nameBox.TextChanged += new System.EventHandler(this.NameBox_TextChanged);
            this.nameBox.Validating += new System.ComponentModel.CancelEventHandler(this.NameBox_Validating);
            this.nameBox.Validated += new System.EventHandler(this.NameBox_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "!Location";
            // 
            // locationBox
            // 
            this.locationBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.locationBox.Location = new System.Drawing.Point(107, 19);
            this.locationBox.Name = "locationBox";
            this.locationBox.Size = new System.Drawing.Size(308, 22);
            this.locationBox.TabIndex = 0;
            // 
            // executableBox
            // 
            this.executableBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.executableBox.Location = new System.Drawing.Point(107, 47);
            this.executableBox.Name = "executableBox";
            this.executableBox.Size = new System.Drawing.Size(202, 22);
            this.executableBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "!Executable";
            // 
            // advancedGroup
            // 
            this.advancedGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advancedGroup.Controls.Add(this.browseButton);
            this.advancedGroup.Controls.Add(this.label3);
            this.advancedGroup.Controls.Add(this.executableBox);
            this.advancedGroup.Controls.Add(this.label2);
            this.advancedGroup.Controls.Add(this.locationBox);
            this.advancedGroup.Location = new System.Drawing.Point(12, 65);
            this.advancedGroup.Name = "advancedGroup";
            this.advancedGroup.Size = new System.Drawing.Size(421, 79);
            this.advancedGroup.TabIndex = 3;
            this.advancedGroup.TabStop = false;
            this.advancedGroup.Text = "!Advanced";
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(320, 46);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(95, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "!Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // languageBox
            // 
            this.languageBox.FormattingEnabled = true;
            this.languageBox.Items.AddRange(new object[] {
            "en-US",
            "zh-CN",
            "zh-TW",
            "cs-CZ",
            "da-DK",
            "nl-NL",
            "fi-FI",
            "fr-FR",
            "de-DE",
            "el-GR",
            "hu-HU",
            "it-IT",
            "ja-JP",
            "ko-KR",
            "no",
            "pl-PL",
            "pt-PT",
            "pt-BR",
            "ru-RU",
            "es-ES",
            "es-MX",
            "sv-SE",
            "th-TH"});
            this.languageBox.Location = new System.Drawing.Point(269, 38);
            this.languageBox.Name = "languageBox";
            this.languageBox.Size = new System.Drawing.Size(57, 21);
            this.languageBox.TabIndex = 2;
            this.languageBox.SelectedIndexChanged += new System.EventHandler(this.languageBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "!Language";
            // 
            // expansionGroup
            // 
            this.expansionGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expansionGroup.Controls.Add(this.expansionBox);
            this.expansionGroup.Location = new System.Drawing.Point(12, 150);
            this.expansionGroup.Name = "expansionGroup";
            this.expansionGroup.Size = new System.Drawing.Size(421, 108);
            this.expansionGroup.TabIndex = 4;
            this.expansionGroup.TabStop = false;
            this.expansionGroup.Text = "!Expansions";
            // 
            // expansionBox
            // 
            this.expansionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expansionBox.CheckBoxes = true;
            this.expansionBox.LargeImageList = this.IconList;
            this.expansionBox.Location = new System.Drawing.Point(6, 19);
            this.expansionBox.Name = "expansionBox";
            this.expansionBox.Size = new System.Drawing.Size(409, 83);
            this.expansionBox.SmallImageList = this.IconList;
            this.expansionBox.TabIndex = 0;
            this.expansionBox.UseCompatibleStateImageBehavior = false;
            this.expansionBox.View = System.Windows.Forms.View.List;
            this.expansionBox.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.expansionBox_ItemChecked);
            // 
            // IconList
            // 
            this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
            this.IconList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconList.Images.SetKeyName(0, "Default");
            this.IconList.Images.SetKeyName(1, "Steam");
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(355, 11);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(78, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "!Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // niceLanguageBox
            // 
            this.niceLanguageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.niceLanguageBox.FormattingEnabled = true;
            this.niceLanguageBox.Location = new System.Drawing.Point(103, 38);
            this.niceLanguageBox.Name = "niceLanguageBox";
            this.niceLanguageBox.Size = new System.Drawing.Size(157, 21);
            this.niceLanguageBox.TabIndex = 1;
            this.niceLanguageBox.SelectedIndexChanged += new System.EventHandler(this.niceLanguageBox_SelectedIndexChanged);
            // 
            // AdvancedCheck
            // 
            this.AdvancedCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AdvancedCheck.AutoSize = true;
            this.AdvancedCheck.Checked = true;
            this.AdvancedCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AdvancedCheck.Location = new System.Drawing.Point(12, 264);
            this.AdvancedCheck.Name = "AdvancedCheck";
            this.AdvancedCheck.Size = new System.Drawing.Size(121, 17);
            this.AdvancedCheck.TabIndex = 5;
            this.AdvancedCheck.Text = "!AdvancedOptions";
            this.AdvancedCheck.UseVisualStyleBackColor = true;
            this.AdvancedCheck.CheckedChanged += new System.EventHandler(this.AdvancedCheck_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 44);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(445, 1);
            this.panel2.TabIndex = 0;
            // 
            // AddProfileDialog
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(445, 331);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.AdvancedCheck);
            this.Controls.Add(this.niceLanguageBox);
            this.Controls.Add(this.expansionGroup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.advancedGroup);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.languageBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(451, 355);
            this.Name = "AddProfileDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "!AddProfileDialog";
            this.Load += new System.EventHandler(this.AddProfileDialog_Load);
            this.advancedGroup.ResumeLayout(false);
            this.advancedGroup.PerformLayout();
            this.expansionGroup.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox locationBox;
        private System.Windows.Forms.TextBox executableBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox advancedGroup;
        private System.Windows.Forms.ComboBox languageBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox expansionGroup;
        private ListViewEx expansionBox;
        private System.Windows.Forms.ImageList IconList;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox niceLanguageBox;
        private System.Windows.Forms.CheckBox AdvancedCheck;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}