namespace TestCrypt.Pages
{
    partial class SelectVolumePage
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
            this.lsvPhysicalDrives = new System.Windows.Forms.ListView();
            this.lsvPhysicalDrivesColVolume = new System.Windows.Forms.ColumnHeader();
            this.lsvPhysicalDrivesColType = new System.Windows.Forms.ColumnHeader();
            this.lsvPhysicalDrivesColSize = new System.Windows.Forms.ColumnHeader();
            this.lsvPhysicalDrivesColSizeInBytes = new System.Windows.Forms.ColumnHeader();
            this.lsvPhysicalDrivesColSectors = new System.Windows.Forms.ColumnHeader();
            this.lsvPhysicalDrivesColGeometry = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lsvPhysicalDrives
            // 
            this.lsvPhysicalDrives.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvPhysicalDrivesColVolume,
            this.lsvPhysicalDrivesColType,
            this.lsvPhysicalDrivesColSize,
            this.lsvPhysicalDrivesColSizeInBytes,
            this.lsvPhysicalDrivesColSectors,
            this.lsvPhysicalDrivesColGeometry});
            this.lsvPhysicalDrives.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvPhysicalDrives.FullRowSelect = true;
            this.lsvPhysicalDrives.HideSelection = false;
            this.lsvPhysicalDrives.Location = new System.Drawing.Point(0, 0);
            this.lsvPhysicalDrives.MultiSelect = false;
            this.lsvPhysicalDrives.Name = "lsvPhysicalDrives";
            this.lsvPhysicalDrives.Size = new System.Drawing.Size(675, 333);
            this.lsvPhysicalDrives.TabIndex = 14;
            this.lsvPhysicalDrives.UseCompatibleStateImageBehavior = false;
            this.lsvPhysicalDrives.View = System.Windows.Forms.View.Details;
            this.lsvPhysicalDrives.SelectedIndexChanged += new System.EventHandler(this.lsvPhysicalDrives_SelectedIndexChanged);
            // 
            // lsvPhysicalDrivesColVolume
            // 
            this.lsvPhysicalDrivesColVolume.Text = "Volume";
            this.lsvPhysicalDrivesColVolume.Width = 111;
            // 
            // lsvPhysicalDrivesColType
            // 
            this.lsvPhysicalDrivesColType.Text = "Type";
            this.lsvPhysicalDrivesColType.Width = 111;
            // 
            // lsvPhysicalDrivesColSize
            // 
            this.lsvPhysicalDrivesColSize.Text = "Size";
            this.lsvPhysicalDrivesColSize.Width = 111;
            // 
            // lsvPhysicalDrivesColSizeInBytes
            // 
            this.lsvPhysicalDrivesColSizeInBytes.Text = "Size in Bytes";
            this.lsvPhysicalDrivesColSizeInBytes.Width = 111;
            // 
            // lsvPhysicalDrivesColSectors
            // 
            this.lsvPhysicalDrivesColSectors.Text = "Sectors";
            this.lsvPhysicalDrivesColSectors.Width = 111;
            // 
            // lsvPhysicalDrivesColGeometry
            // 
            this.lsvPhysicalDrivesColGeometry.Text = "Geometry (C/H/S)";
            this.lsvPhysicalDrivesColGeometry.Width = 111;
            // 
            // SelectVolumePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvPhysicalDrives);
            this.Name = "SelectVolumePage";
            this.Size = new System.Drawing.Size(675, 333);
            this.Subtitle = "Select the physical drive to search for TrueCrypt volumes";
            this.Title = "Select Volume";
            this.Load += new System.EventHandler(this.SelectVolumePage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvPhysicalDrives;
        private System.Windows.Forms.ColumnHeader lsvPhysicalDrivesColVolume;
        private System.Windows.Forms.ColumnHeader lsvPhysicalDrivesColType;
        private System.Windows.Forms.ColumnHeader lsvPhysicalDrivesColSize;
        private System.Windows.Forms.ColumnHeader lsvPhysicalDrivesColSizeInBytes;
        private System.Windows.Forms.ColumnHeader lsvPhysicalDrivesColSectors;
        private System.Windows.Forms.ColumnHeader lsvPhysicalDrivesColGeometry;

    }
}
