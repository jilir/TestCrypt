using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestCrypt.Pages
{
    /// <summary>
    /// Class for the device selection page of the TestCrypt wizard.
    /// </summary>
    public partial class PageDevice : WizardPage
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

        /// <summary>
        /// The list of enumerated logical volumes.
        /// </summary>
        private volatile List<PhysicalDrive.VolumeInfo> volumes;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the currently selected device.
        /// </summary>
        private Device SelectedDevice
        {
            get { return (lsvDevices.SelectedItems.Count > 0) ? (Device)lsvDevices.SelectedItems[0].Tag : null; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="previousPage">The previous page of the wizard to return to in case of a "PageBack" event.</param>
        public PageDevice(WizardPage previousPage)
        {
            this.previousPage = previousPage;
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Refresh the disk selection by (re-)enumerating the available devices of the system.
        /// </summary>
        private void RefreshPage()
        {
            // store the currently selected device (if any)
            Device selectedDevice = SelectedDevice;

            // clear the devices already enumerated in the list-view
            lsvDevices.Items.Clear();

            // enumerating the devices can take quite a time if a harddisk has to spin up: use progress dialog
            FormProgress dlg = new FormProgress();
            dlg.DoWork += backgroundWorker_DoWork;
            dlg.ShowDialog(this);

            // the devices have been enumerated
            foreach (PhysicalDrive.DriveInfo drive in drives)
            {
                ListViewItem item = new ListViewItem(string.Format("{0} {1}:", PageContext.GetInstance().GetResourceString("Disk"), drive.DriveNumber));
                item.Font = new Font(item.Font, FontStyle.Bold);
                item.Tag = new Device(drive);
                item.SubItems.Add("");
                item.SubItems.Add(PhysicalDrive.GetAsBestFitSizeUnit(drive.Size));
                item.SubItems.Add("");
                lsvDevices.Items.Add(item);



                foreach (PhysicalDrive.PARTITION_INFORMATION_EX partition in drive.Partitions)
                {
                    PhysicalDrive.VolumeInfo volumeInfo = null;
                    foreach (PhysicalDrive.VolumeInfo volume in volumes)
                    {
                        if ((volume.DeviceInfo.DeviceNumber == drive.DriveNumber) && (volume.DeviceInfo.PartitionNumber == partition.PartitionNumber))
                        {
                            volumeInfo = volume;
                            break;
                        }
                        
                    }
                    item = new ListViewItem(string.Format(@"\Device\Harddisk{0}\Partition{1}", drive.DriveNumber, partition.PartitionNumber));
                    item.Tag = new Device(drive, partition.PartitionNumber);
                    item.SubItems.Add((volumeInfo != null) ? volumeInfo.RootPath : string.Empty);
                    item.SubItems.Add(PhysicalDrive.GetAsBestFitSizeUnit(partition.PartitionLength));
                    item.SubItems.Add((volumeInfo != null) ? volumeInfo.Label : string.Empty);
                    lsvDevices.Items.Add(item);

                    // re-select the previously selected device if it still exists
                    if ((selectedDevice != null) && selectedDevice.Partition.HasValue && 
                        (drive.DriveNumber == selectedDevice.Drive.DriveNumber) && (partition.PartitionNumber == selectedDevice.Partition.Value))
                    {
                        item.Focused = true;
                        item.Selected = true;
                    }
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
            // this wizard page is ready when a device has been selected
            bool isDeviceSelected = (null != SelectedDevice);
            if (ready != isDeviceSelected)
            {
                ready = isDeviceSelected;
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
            volumes = PhysicalDrive.Volumes;
        }

        /// <summary>
        /// "PageActivated" event handler of the wizard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        private void PageDevice_PageActivated(object sender, EventArgs e)
        {
            RefreshPage();
        }

        /// <summary>
        /// "PageNext" event handler of the wizard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:TestCrypt.PageTransitionEventArgs" /> instance containing the event data.</param>
        private void PageDevice_PageNext(object sender, PageTransitionEventArgs e)
        {
            // store the selected device inside the page context
            PageContext.GetInstance().Device = SelectedDevice;

            // continue with the TrueCrypt settings wizard page
            e.Page = new PageTrueCrypt(this);
        }

        /// <summary>
        /// "PageBack" event handler of the wizard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:TestCrypt.PageTransitionEventArgs" /> instance containing the event data.</param>
        private void PageDevice_PageBack(object sender, PageTransitionEventArgs e)
        {
            // return to the previous wizard page
            e.Page = previousPage;
        }
        #endregion
    }
}
