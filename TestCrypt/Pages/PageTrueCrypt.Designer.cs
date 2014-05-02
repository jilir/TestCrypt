namespace TestCrypt.Pages
{
    partial class PageTrueCrypt
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageTrueCrypt));
            this.mnuKeyfiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddPath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.lsvKeyfiles = new System.Windows.Forms.ListView();
            this.chkUsLayout = new System.Windows.Forms.CheckBox();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.chkDisplayPassword = new System.Windows.Forms.CheckBox();
            this.lblKeyfiles = new System.Windows.Forms.Label();
            this.mnuKeyfiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuKeyfiles
            // 
            resources.ApplyResources(this.mnuKeyfiles, "mnuKeyfiles");
            this.mnuKeyfiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddFiles,
            this.mnuAddPath,
            this.toolStripMenuItem1,
            this.mnuRemove,
            this.mnuRemoveAll});
            this.mnuKeyfiles.Name = "mnuKeyfiles";
            // 
            // mnuAddFiles
            // 
            resources.ApplyResources(this.mnuAddFiles, "mnuAddFiles");
            this.mnuAddFiles.Name = "mnuAddFiles";
            this.mnuAddFiles.Click += new System.EventHandler(this.mnuAddFiles_Click);
            // 
            // mnuAddPath
            // 
            resources.ApplyResources(this.mnuAddPath, "mnuAddPath");
            this.mnuAddPath.Name = "mnuAddPath";
            this.mnuAddPath.Click += new System.EventHandler(this.mnuAddPath_Click);
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // mnuRemove
            // 
            resources.ApplyResources(this.mnuRemove, "mnuRemove");
            this.mnuRemove.Name = "mnuRemove";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // mnuRemoveAll
            // 
            resources.ApplyResources(this.mnuRemoveAll, "mnuRemoveAll");
            this.mnuRemoveAll.Name = "mnuRemoveAll";
            this.mnuRemoveAll.Click += new System.EventHandler(this.mnuRemoveAll_Click);
            // 
            // dlgOpenFile
            // 
            resources.ApplyResources(this.dlgOpenFile, "dlgOpenFile");
            this.dlgOpenFile.Multiselect = true;
            // 
            // dlgFolderBrowser
            // 
            resources.ApplyResources(this.dlgFolderBrowser, "dlgFolderBrowser");
            // 
            // lsvKeyfiles
            // 
            resources.ApplyResources(this.lsvKeyfiles, "lsvKeyfiles");
            this.lsvKeyfiles.ContextMenuStrip = this.mnuKeyfiles;
            this.lsvKeyfiles.FullRowSelect = true;
            this.lsvKeyfiles.MultiSelect = false;
            this.lsvKeyfiles.Name = "lsvKeyfiles";
            this.lsvKeyfiles.UseCompatibleStateImageBehavior = false;
            this.lsvKeyfiles.View = System.Windows.Forms.View.List;
            this.lsvKeyfiles.SelectedIndexChanged += new System.EventHandler(this.lsvKeyfiles_SelectedIndexChanged);
            // 
            // chkUsLayout
            // 
            resources.ApplyResources(this.chkUsLayout, "chkUsLayout");
            this.chkUsLayout.Name = "chkUsLayout";
            this.chkUsLayout.UseVisualStyleBackColor = true;
            this.chkUsLayout.CheckedChanged += new System.EventHandler(this.chkUsLayout_CheckedChanged);
            // 
            // txtConfirm
            // 
            resources.ApplyResources(this.txtConfirm, "txtConfirm");
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.UseSystemPasswordChar = true;
            this.txtConfirm.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // lblConfirm
            // 
            resources.ApplyResources(this.lblConfirm, "lblConfirm");
            this.lblConfirm.Name = "lblConfirm";
            // 
            // lblPassword
            // 
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.Name = "lblPassword";
            // 
            // chkDisplayPassword
            // 
            resources.ApplyResources(this.chkDisplayPassword, "chkDisplayPassword");
            this.chkDisplayPassword.Name = "chkDisplayPassword";
            this.chkDisplayPassword.UseVisualStyleBackColor = true;
            this.chkDisplayPassword.CheckedChanged += new System.EventHandler(this.chkDisplayPassword_CheckedChanged);
            // 
            // lblKeyfiles
            // 
            resources.ApplyResources(this.lblKeyfiles, "lblKeyfiles");
            this.lblKeyfiles.Name = "lblKeyfiles";
            // 
            // PageTrueCrypt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvKeyfiles);
            this.Controls.Add(this.chkUsLayout);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.chkDisplayPassword);
            this.Controls.Add(this.lblKeyfiles);
            this.Name = "PageTrueCrypt";
            this.PageActivated += new System.EventHandler<System.EventArgs>(this.PageTrueCrypt_PageActivated);
            this.PageNext += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.PageTrueCrypt_PageNext);
            this.PageBack += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.PageTrueCrypt_PageBack);
            this.mnuKeyfiles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKeyfiles;
        private System.Windows.Forms.CheckBox chkUsLayout;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.CheckBox chkDisplayPassword;
        private System.Windows.Forms.ContextMenuStrip mnuKeyfiles;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFiles;
        private System.Windows.Forms.ToolStripMenuItem mnuAddPath;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveAll;
        private System.Windows.Forms.ListView lsvKeyfiles;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;

    }
}
