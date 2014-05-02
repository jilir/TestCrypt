using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace TestCrypt.Pages
{
    /// <summary>
    /// Class for the disk selection page of the TestCrypt wizard.
    /// </summary>
    public partial class PageDisk : WizardPage
    {
        #region Attributes
        /// <summary>
        /// The previous page of the wizard to return to in case of a "PageBack" event.
        /// </summary>
        private WizardPage previousPage;

        /// <summary>
        /// The list of enumerated physical drives.
        /// </summary>
        private volatile List<PhysicalDrive.DriveInfo> drives;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the currently selected physical drive.
        /// </summary>
        private Device SelectedDisk
        {
            get { return (lsvPhysicalDrives.SelectedItems.Count > 0) ? (Device)lsvPhysicalDrives.SelectedItems[0].Tag : null; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="previousPage">The previous page of the wizard to return to in case of a "PageBack" event.</param>
        public PageDisk(WizardPage previousPage)
        {
            this.previousPage = previousPage;
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Refresh the disk selection by (re-)enumerating the available disks of the system.
        /// </summary>
        private void RefreshPage()
        {
            // store the currently selected physical drive (if any)
            Device selectedDisk = SelectedDisk;

            // clear the physical drives already enumerated in the list-view
            lsvPhysicalDrives.Items.Clear();

            // enumerating the physical drives can take quite a time if a harddisk has to spin up: use progress dialog
            FormProgress dlg = new FormProgress();
            dlg.DoWork += backgroundWorker_DoWork;
            dlg.ShowDialog(this);

            // the physical drives have been enumerated
            foreach (PhysicalDrive.DriveInfo drive in drives)
            {
                ListViewItem item = new ListViewItem(string.Format("{0} {1}", PageContext.GetInstance().GetResourceString("Disk"), drive.DriveNumber));
                item.Tag = new Device(drive);

                string type;
                switch (drive.PartitionStyle)
                {
                    case PhysicalDrive.PARTITION_STYLE.PARTITION_STYLE_MBR:
                        type = "MBR";
                        break;
                    case PhysicalDrive.PARTITION_STYLE.PARTITION_STYLE_GPT:
                        type = "GPT";
                        break;
                    default:
                        type = "RAW";
                        break;
                }
                item.SubItems.Add(type);
                item.SubItems.Add(PhysicalDrive.GetAsBestFitSizeUnit(drive.Size));
                item.SubItems.Add((drive.Size / drive.Geometry.BytesPerSector).ToString());
                item.SubItems.Add(string.Format("{0}/{1}/{2}", drive.Geometry.Cylinders, drive.Geometry.TracksPerCylinder, drive.Geometry.SectorsPerTrack));
                lsvPhysicalDrives.Items.Add(item);

                // re-select the previously selected physical drive if it still exists
                if ((selectedDisk != null) && (drive.DriveNumber == selectedDisk.Drive.DriveNumber))
                {
                    item.Focused = true;
                    item.Selected = true;
                }
            }

            // the ready state of the wizard page might have changed
            lsvPhysicalDrives_SelectedIndexChanged(this, new EventArgs());
        }
        #endregion

        #region Events
        /// <summary>
        /// "SelectedIndexChanged" event handler of the list-view.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        private void lsvPhysicalDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this wizard page is ready when a physical drive has been selected
            bool isDiskSelected = (null != SelectedDisk);
            if (ready != isDiskSelected)
            {
                ready = isDiskSelected;
                OnReadyChanged(new EventArgs());
            }
        }

        /// <summary>
        /// "Click" event handler of the "Refresh" menu-item.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            RefreshPage();
        }

        /// <summary>
        /// "DoWork" event handler of the background worker.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            drives = PhysicalDrive.Drives;
        }

        /// <summary>
        /// "PageActivated" event handler of the wizard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        private void PageDisk_PageActivated(object sender, EventArgs e)
        {
            RefreshPage();
        }

        /// <summary>
        /// "PageNext" event handler of the wizard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:TestCrypt.PageTransitionEventArgs" /> instance containing the event data.</param>
        private void PageDisk_PageNext(object sender, PageTransitionEventArgs e)
        {
            // store the selected physical drive inside the page context
            PageContext.GetInstance().Device = SelectedDisk;

            // select the next page of the TestCrypt wizard
            switch (PageContext.GetInstance().Mode)
            {
                case Mode.SearchFragment:
                    e.Page = new PageFragment(this);
                    break;
                default:
                    e.Page = new PageTrueCrypt(this);
                    break;
            }
        }

        /// <summary>
        /// "PageBack" event handler of the wizard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:TestCrypt.PageTransitionEventArgs" /> instance containing the event data.</param>
        private void PageDisk_PageBack(object sender, PageTransitionEventArgs e)
        {
            // return to the previous wizard page
            e.Page = previousPage;
        }
        #endregion
    }
}
