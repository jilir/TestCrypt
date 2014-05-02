namespace TestCrypt.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChsLbaConverter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGerman = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.rtfSubtitle = new System.Windows.Forms.RichTextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDismountAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDismountDummy = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFinish = new System.Windows.Forms.Button();
            this.etchedLine2 = new TestCrypt.EtchedLine();
            this.etchedLine1 = new TestCrypt.EtchedLine();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStripNotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuHelp});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            resources.ApplyResources(this.mnuFile, "mnuFile");
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            resources.ApplyResources(this.mnuExit, "mnuExit");
            this.mnuExit.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChsLbaConverter,
            this.mnuLanguage});
            this.mnuTools.Name = "mnuTools";
            resources.ApplyResources(this.mnuTools, "mnuTools");
            // 
            // mnuChsLbaConverter
            // 
            this.mnuChsLbaConverter.Name = "mnuChsLbaConverter";
            resources.ApplyResources(this.mnuChsLbaConverter, "mnuChsLbaConverter");
            this.mnuChsLbaConverter.Click += new System.EventHandler(this.mnuChsLbaConverter_Click);
            // 
            // mnuLanguage
            // 
            this.mnuLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEnglish,
            this.mnuGerman});
            this.mnuLanguage.Name = "mnuLanguage";
            resources.ApplyResources(this.mnuLanguage, "mnuLanguage");
            // 
            // mnuEnglish
            // 
            this.mnuEnglish.Name = "mnuEnglish";
            resources.ApplyResources(this.mnuEnglish, "mnuEnglish");
            this.mnuEnglish.Tag = "en";
            this.mnuEnglish.Click += new System.EventHandler(this.mnuLanguage_Click);
            // 
            // mnuGerman
            // 
            this.mnuGerman.Name = "mnuGerman";
            resources.ApplyResources(this.mnuGerman, "mnuGerman");
            this.mnuGerman.Tag = "de";
            this.mnuGerman.Click += new System.EventHandler(this.mnuLanguage_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            resources.ApplyResources(this.mnuHelp, "mnuHelp");
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            resources.ApplyResources(this.mnuAbout, "mnuAbout");
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            resources.ApplyResources(this.btnBack, "btnBack");
            this.btnBack.Name = "btnBack";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pagePanel
            // 
            resources.ApplyResources(this.pagePanel, "pagePanel");
            this.pagePanel.BackColor = System.Drawing.SystemColors.Control;
            this.pagePanel.Name = "pagePanel";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TestCrypt.Properties.Resources.Unlock_icon;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // rtfSubtitle
            // 
            resources.ApplyResources(this.rtfSubtitle, "rtfSubtitle");
            this.rtfSubtitle.BackColor = System.Drawing.SystemColors.Control;
            this.rtfSubtitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtfSubtitle.Name = "rtfSubtitle";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripNotifyIcon;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.BalloonTipClosed += new System.EventHandler(this.notifyIcon_BalloonTipClosed);
            // 
            // contextMenuStripNotifyIcon
            // 
            this.contextMenuStripNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDismountAll,
            this.toolStripSeparator1,
            this.mnuDismountDummy});
            this.contextMenuStripNotifyIcon.Name = "contextMenuStripNotifyIcon";
            resources.ApplyResources(this.contextMenuStripNotifyIcon, "contextMenuStripNotifyIcon");
            // 
            // mnuDismountAll
            // 
            this.mnuDismountAll.Name = "mnuDismountAll";
            resources.ApplyResources(this.mnuDismountAll, "mnuDismountAll");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // mnuDismountDummy
            // 
            this.mnuDismountDummy.Name = "mnuDismountDummy";
            resources.ApplyResources(this.mnuDismountDummy, "mnuDismountDummy");
            // 
            // btnFinish
            // 
            resources.ApplyResources(this.btnFinish, "btnFinish");
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // etchedLine2
            // 
            resources.ApplyResources(this.etchedLine2, "etchedLine2");
            this.etchedLine2.DarkColor = System.Drawing.SystemColors.ControlDark;
            this.etchedLine2.LightColor = System.Drawing.SystemColors.ControlLightLight;
            this.etchedLine2.Name = "etchedLine2";
            // 
            // etchedLine1
            // 
            resources.ApplyResources(this.etchedLine1, "etchedLine1");
            this.etchedLine1.DarkColor = System.Drawing.SystemColors.ControlDark;
            this.etchedLine1.LightColor = System.Drawing.SystemColors.ControlLightLight;
            this.etchedLine1.Name = "etchedLine1";
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnNext;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.rtfSubtitle);
            this.Controls.Add(this.etchedLine2);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.etchedLine1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.menuStrip);
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStripNotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EtchedLine etchedLine1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuLanguage;
        private System.Windows.Forms.ToolStripMenuItem mnuEnglish;
        private System.Windows.Forms.ToolStripMenuItem mnuGerman;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pagePanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private EtchedLine etchedLine2;
        private System.Windows.Forms.RichTextBox rtfSubtitle;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem mnuDismountAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuDismountDummy;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.ToolStripMenuItem mnuChsLbaConverter;

    }
}

