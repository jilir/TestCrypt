namespace TestCrypt.Pages
{
    partial class PageDisk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageDisk));
            this.mnuDisk = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.lsvPhysicalDrives = new System.Windows.Forms.ListView();
            this.lsvPhysicalDrivesColDisk = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvPhysicalDrivesColType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvPhysicalDrivesColSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvPhysicalDrivesColSectors = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvPhysicalDrivesColGeometry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuDisk.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuDisk
            // 
            resources.ApplyResources(this.mnuDisk, "mnuDisk");
            this.mnuDisk.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRefresh});
            this.mnuDisk.Name = "mnuDisk";
            // 
            // mnuRefresh
            // 
            resources.ApplyResources(this.mnuRefresh, "mnuRefresh");
            this.mnuRefresh.Name = "mnuRefresh";
            this.mnuRefresh.Click += new System.EventHandler(this.mnuRefresh_Click);
            // 
            // lsvPhysicalDrives
            // 
            resources.ApplyResources(this.lsvPhysicalDrives, "lsvPhysicalDrives");
            this.lsvPhysicalDrives.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvPhysicalDrivesColDisk,
            this.lsvPhysicalDrivesColType,
            this.lsvPhysicalDrivesColSize,
            this.lsvPhysicalDrivesColSectors,
            this.lsvPhysicalDrivesColGeometry});
            this.lsvPhysicalDrives.ContextMenuStrip = this.mnuDisk;
            this.lsvPhysicalDrives.FullRowSelect = true;
            this.lsvPhysicalDrives.HideSelection = false;
            this.lsvPhysicalDrives.MultiSelect = false;
            this.lsvPhysicalDrives.Name = "lsvPhysicalDrives";
            this.lsvPhysicalDrives.UseCompatibleStateImageBehavior = false;
            this.lsvPhysicalDrives.View = System.Windows.Forms.View.Details;
            this.lsvPhysicalDrives.SelectedIndexChanged += new System.EventHandler(this.lsvPhysicalDrives_SelectedIndexChanged);
            // 
            // lsvPhysicalDrivesColDisk
            // 
            resources.ApplyResources(this.lsvPhysicalDrivesColDisk, "lsvPhysicalDrivesColDisk");
            // 
            // lsvPhysicalDrivesColType
            // 
            resources.ApplyResources(this.lsvPhysicalDrivesColType, "lsvPhysicalDrivesColType");
            // 
            // lsvPhysicalDrivesColSize
            // 
            resources.ApplyResources(this.lsvPhysicalDrivesColSize, "lsvPhysicalDrivesColSize");
            // 
            // lsvPhysicalDrivesColSectors
            // 
            resources.ApplyResources(this.lsvPhysicalDrivesColSectors, "lsvPhysicalDrivesColSectors");
            // 
            // lsvPhysicalDrivesColGeometry
            // 
            resources.ApplyResources(this.lsvPhysicalDrivesColGeometry, "lsvPhysicalDrivesColGeometry");
            // 
            // PageDisk
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvPhysicalDrives);
            this.Name = "PageDisk";
            this.PageActivated += new System.EventHandler<System.EventArgs>(this.PageDisk_PageActivated);
            this.PageNext += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.PageDisk_PageNext);
            this.PageBack += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.PageDisk_PageBack);
            this.mnuDisk.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvPhysicalDrives;
        private System.Windows.Forms.ColumnHeader lsvPhysicalDrivesColDisk;
        private System.Windows.Forms.ColumnHeader lsvPhysicalDrivesColType;
        private System.Windows.Forms.ColumnHeader lsvPhysicalDrivesColSize;
        private System.Windows.Forms.ColumnHeader lsvPhysicalDrivesColSectors;
        private System.Windows.Forms.ColumnHeader lsvPhysicalDrivesColGeometry;
        private System.Windows.Forms.ContextMenuStrip mnuDisk;
        private System.Windows.Forms.ToolStripMenuItem mnuRefresh;

    }
}
