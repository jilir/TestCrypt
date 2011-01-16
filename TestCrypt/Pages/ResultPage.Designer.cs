namespace TestCrypt.Pages
{
    partial class ResultPage
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
            this.lsvHeader = new System.Windows.Forms.ListView();
            this.lsvHeaderColSector = new System.Windows.Forms.ColumnHeader();
            this.lsvHeaderColVolumeSize = new System.Windows.Forms.ColumnHeader();
            this.lsvHeaderColHidden = new System.Windows.Forms.ColumnHeader();
            this.lsvHeaderColVersion = new System.Windows.Forms.ColumnHeader();
            this.lsvHeaderColSupported = new System.Windows.Forms.ColumnHeader();
            this.lsvHeaderColNormalHeader = new System.Windows.Forms.ColumnHeader();
            this.lsvHeaderColBackupHeader = new System.Windows.Forms.ColumnHeader();
            this.mnuHeader = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuMountAsNormalHeader = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMountAsBackupHeader = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDismountAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvHeader
            // 
            this.lsvHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvHeader.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvHeaderColSector,
            this.lsvHeaderColVolumeSize,
            this.lsvHeaderColHidden,
            this.lsvHeaderColVersion,
            this.lsvHeaderColSupported,
            this.lsvHeaderColNormalHeader,
            this.lsvHeaderColBackupHeader});
            this.lsvHeader.ContextMenuStrip = this.mnuHeader;
            this.lsvHeader.FullRowSelect = true;
            this.lsvHeader.HideSelection = false;
            this.lsvHeader.Location = new System.Drawing.Point(3, 3);
            this.lsvHeader.MultiSelect = false;
            this.lsvHeader.Name = "lsvHeader";
            this.lsvHeader.Size = new System.Drawing.Size(669, 327);
            this.lsvHeader.TabIndex = 0;
            this.lsvHeader.UseCompatibleStateImageBehavior = false;
            this.lsvHeader.View = System.Windows.Forms.View.Details;
            this.lsvHeader.SelectedIndexChanged += new System.EventHandler(this.lsvHeader_SelectedIndexChanged);
            // 
            // lsvHeaderColSector
            // 
            this.lsvHeaderColSector.Text = "Sector";
            // 
            // lsvHeaderColVolumeSize
            // 
            this.lsvHeaderColVolumeSize.Text = "Volume Size";
            this.lsvHeaderColVolumeSize.Width = 85;
            // 
            // lsvHeaderColHidden
            // 
            this.lsvHeaderColHidden.Text = "Hidden";
            // 
            // lsvHeaderColVersion
            // 
            this.lsvHeaderColVersion.Text = "Version";
            // 
            // lsvHeaderColSupported
            // 
            this.lsvHeaderColSupported.Text = "Supported";
            // 
            // lsvHeaderColNormalHeader
            // 
            this.lsvHeaderColNormalHeader.Text = "Normal Header";
            // 
            // lsvHeaderColBackupHeader
            // 
            this.lsvHeaderColBackupHeader.Text = "Embedded Backup Header";
            // 
            // mnuHeader
            // 
            this.mnuHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMountAsNormalHeader,
            this.mnuMountAsBackupHeader,
            this.toolStripMenuItem2,
            this.mnuDismountAll});
            this.mnuHeader.Name = "mnuHeader";
            this.mnuHeader.Size = new System.Drawing.Size(209, 98);
            // 
            // mnuMountAsNormalHeader
            // 
            this.mnuMountAsNormalHeader.Name = "mnuMountAsNormalHeader";
            this.mnuMountAsNormalHeader.Size = new System.Drawing.Size(208, 22);
            this.mnuMountAsNormalHeader.Text = "Mount as &Normal Header";
            this.mnuMountAsNormalHeader.Click += new System.EventHandler(this.mnuMountAsNormalHeader_Click);
            // 
            // mnuMountAsBackupHeader
            // 
            this.mnuMountAsBackupHeader.Name = "mnuMountAsBackupHeader";
            this.mnuMountAsBackupHeader.Size = new System.Drawing.Size(208, 22);
            this.mnuMountAsBackupHeader.Text = "Mount as &Backup Header";
            this.mnuMountAsBackupHeader.Click += new System.EventHandler(this.mnuMountAsBackupHeader_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(205, 6);
            // 
            // mnuDismountAll
            // 
            this.mnuDismountAll.Name = "mnuDismountAll";
            this.mnuDismountAll.Size = new System.Drawing.Size(208, 22);
            this.mnuDismountAll.Text = "&Dismount All";
            this.mnuDismountAll.Click += new System.EventHandler(this.mnuDismountAll_Click);
            // 
            // ResultPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvHeader);
            this.Name = "ResultPage";
            this.Size = new System.Drawing.Size(675, 333);
            this.Subtitle = "Presents the possible TrueCrypt volumes found by the analyzer";
            this.Title = "Analyzer Result";
            this.PageActivated += new System.EventHandler<System.EventArgs>(this.ResultPage_PageActivated);
            this.PageBack += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.ResultPage_PageBack);
            this.mnuHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvHeader;
        private System.Windows.Forms.ColumnHeader lsvHeaderColSector;
        private System.Windows.Forms.ColumnHeader lsvHeaderColVolumeSize;
        private System.Windows.Forms.ColumnHeader lsvHeaderColHidden;
        private System.Windows.Forms.ColumnHeader lsvHeaderColSupported;
        private System.Windows.Forms.ColumnHeader lsvHeaderColVersion;
        private System.Windows.Forms.ColumnHeader lsvHeaderColNormalHeader;
        private System.Windows.Forms.ColumnHeader lsvHeaderColBackupHeader;
        private System.Windows.Forms.ContextMenuStrip mnuHeader;
        private System.Windows.Forms.ToolStripMenuItem mnuMountAsNormalHeader;
        private System.Windows.Forms.ToolStripMenuItem mnuMountAsBackupHeader;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuDismountAll;
    }
}
