namespace TestCrypt.Pages
{
    partial class PageDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageDevice));
            this.mnuDevice = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.lsvDevices = new System.Windows.Forms.ListView();
            this.lsvDevicesColDevice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvDevicesColDrive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvDevicesColSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvDevicesColLabel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuDevice.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuDevice
            // 
            this.mnuDevice.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRefresh});
            this.mnuDevice.Name = "mnuDisk";
            resources.ApplyResources(this.mnuDevice, "mnuDevice");
            // 
            // mnuRefresh
            // 
            this.mnuRefresh.Name = "mnuRefresh";
            resources.ApplyResources(this.mnuRefresh, "mnuRefresh");
            this.mnuRefresh.Click += new System.EventHandler(this.mnuRefresh_Click);
            // 
            // lsvDevices
            // 
            this.lsvDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvDevicesColDevice,
            this.lsvDevicesColDrive,
            this.lsvDevicesColSize,
            this.lsvDevicesColLabel});
            this.lsvDevices.ContextMenuStrip = this.mnuDevice;
            resources.ApplyResources(this.lsvDevices, "lsvDevices");
            this.lsvDevices.FullRowSelect = true;
            this.lsvDevices.HideSelection = false;
            this.lsvDevices.MultiSelect = false;
            this.lsvDevices.Name = "lsvDevices";
            this.lsvDevices.UseCompatibleStateImageBehavior = false;
            this.lsvDevices.View = System.Windows.Forms.View.Details;
            this.lsvDevices.SelectedIndexChanged += new System.EventHandler(this.lsvPhysicalDrives_SelectedIndexChanged);
            // 
            // lsvDevicesColDevice
            // 
            resources.ApplyResources(this.lsvDevicesColDevice, "lsvDevicesColDevice");
            // 
            // lsvDevicesColDrive
            // 
            resources.ApplyResources(this.lsvDevicesColDrive, "lsvDevicesColDrive");
            // 
            // lsvDevicesColSize
            // 
            resources.ApplyResources(this.lsvDevicesColSize, "lsvDevicesColSize");
            // 
            // lsvDevicesColLabel
            // 
            resources.ApplyResources(this.lsvDevicesColLabel, "lsvDevicesColLabel");
            // 
            // PageDevice
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvDevices);
            this.Name = "PageDevice";
            this.PageActivated += new System.EventHandler<System.EventArgs>(this.PageDevice_PageActivated);
            this.PageNext += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.PageDevice_PageNext);
            this.PageBack += new System.EventHandler<TestCrypt.WizardPage.PageTransitionEventArgs>(this.PageDevice_PageBack);
            this.mnuDevice.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvDevices;
        private System.Windows.Forms.ColumnHeader lsvDevicesColDevice;
        private System.Windows.Forms.ColumnHeader lsvDevicesColDrive;
        private System.Windows.Forms.ColumnHeader lsvDevicesColSize;
        private System.Windows.Forms.ColumnHeader lsvDevicesColLabel;
        private System.Windows.Forms.ContextMenuStrip mnuDevice;
        private System.Windows.Forms.ToolStripMenuItem mnuRefresh;

    }
}
