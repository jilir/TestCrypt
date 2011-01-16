using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TestCrypt.Pages
{
    public partial class SelectVolumePage : WizardPage
    {
        #region Attributes
        private PageContext context;
        #endregion

        #region Constructors
        public SelectVolumePage(PageContext context)
        {
            InitializeComponent();

            this.context = context;
        }
        #endregion

        #region Events
        private void SelectVolumePage_Load(object sender, EventArgs e)
        {
            foreach (PhysicalDrive.DriveInfo drive in PhysicalDrive.Drives)
            {
                ListViewItem item = new ListViewItem(drive.Volume);
                item.Tag = drive;

                item.SubItems.Add(drive.Geometry.MediaType.ToString());
                item.SubItems.Add(PhysicalDrive.GetAsBestFitSizeUnit(drive.Size));
                item.SubItems.Add(drive.Size.ToString());
                item.SubItems.Add((drive.Size / drive.Geometry.BytesPerSector).ToString());
                item.SubItems.Add(string.Format("{0}/{1}/{2}", drive.Geometry.Cylinders, drive.Geometry.TracksPerCylinder, drive.Geometry.SectorsPerTrack));
                lsvPhysicalDrives.Items.Add(item);
            }
            lsvPhysicalDrives.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void lsvPhysicalDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this wizard page is ready when a physical drive has been selected
            if (ready != (lsvPhysicalDrives.SelectedItems.Count > 0))
            {
                ready = (lsvPhysicalDrives.SelectedItems.Count > 0);
                OnReadyChanged(new EventArgs());
            }
            
            // store the selected physical drive inside the page context
            context.Drive = (lsvPhysicalDrives.SelectedItems.Count > 0) ? (PhysicalDrive.DriveInfo)lsvPhysicalDrives.SelectedItems[0].Tag : null;            
        }
        #endregion
    }
}
