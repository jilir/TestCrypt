using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TestCrypt.Pages
{
    public partial class AnalyzePage : WizardPage
    {
        #region Constants
        private const int SCAN_BUFFER_SIZE_IN_BYTES = 1024 * 1024;
        #endregion

        #region Attributes
        private PageContext context;

        /// <summary>
        /// Synchronization of the background worker with the main thread on cancellation.
        /// </summary>
        private AutoResetEvent resetEvent = new AutoResetEvent(false);

        /// <summary>
        /// Synchronization primitive to synchronize updating the current progress.
        /// </summary>
        private Mutex progressMutex = new Mutex();

        /// <summary>
        /// Stores the task which is currently performed by the analyzer.
        /// </summary>
        PageContext.ScanRange task;

        /// <summary>
        /// Stores the progress of the current task in percent.
        /// </summary>
        private double progressTask;

        /// <summary>
        /// Stores the total progress in percent.
        /// </summary>
        private double progressTotal;

        /// <summary>
        /// Stores the estimated remaining time required by the analyzer.
        /// </summary>
        private TimeSpan remainingTimeTotal;

        /// <summary>
        /// Stores whether the analyzer has been canceled by the user.
        /// </summary>
        private bool canceled;
        #endregion
        
        #region Constructors
        public AnalyzePage(PageContext context)
        {
            InitializeComponent();

            this.context = context;
        }
        #endregion

        #region Events
        private void AnalyzePage_PageActivated(object sender, EventArgs e)
        {
            // clear the list of TrueCrypt headers which have been found in previous scans
            context.HeaderList.Clear();

            // initialize the progress information
            lblTask.Text = "";
            prgTask.Value = 0;
            lblPercentageTask.Text = string.Format("{0:0.00}%", 0);
            prgTotal.Value = 0;
            lblPercentageTotal.Text = string.Format("{0:0.00}%", 0);
            lblTotalEstimatedTimeRemaining.Text = "Estimating...";

            // start a background worker to scan for TrueCrypt volumes
            ready = false;
            canceled = false;
            backgroundWorker.RunWorkerAsync();
        }

        private void AnalyzePage_PageBack(object sender, PageTransitionEventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                const string message = "The analyzer is still in progress. If you go back the analyzer will be canceled. Do you really want to continue?";
                if (MessageBox.Show(this, message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                     // cancel the background worker and wait for cancellation
                    canceled = true;
                    backgroundWorker.CancelAsync();
                    resetEvent.WaitOne();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] data = new byte[SCAN_BUFFER_SIZE_IN_BYTES];
            List<PageContext.ScanRange> rangeList = context.GetOptimizedScanRanges();
            TrueCrypt.CRYPTO_INFO cryptoInfo = new TrueCrypt.CRYPTO_INFO();
            TrueCrypt.Password password = new TrueCrypt.Password(context.Password);

            // get the total number of sectors to analyze
            Int64 totalLbaCount = 0;
            Int64 totalCurrentLba = 0;
            foreach (PageContext.ScanRange range in rangeList)
            {
                totalLbaCount += (range.EndLba - range.StartLba) + 1;
            }

            // store the current time as the start time of the analyzer
            DateTime startTimeTotal = DateTime.Now;

            for (int i = 0; (i < rangeList.Count) && !backgroundWorker.CancellationPending; i++)
            {
                // get the range to scan
                PageContext.ScanRange range = rangeList[i];                

                Int64 curLba = range.StartLba;
                while ((curLba <= range.EndLba) && !backgroundWorker.CancellationPending)
                {
                    PhysicalDrive.Read(context.Drive.Volume, curLba * context.Drive.Geometry.BytesPerSector, TrueCrypt.TC_VOLUME_HEADER_SIZE, data);
                    if (0 == TrueCrypt.ReadVolumeHeader(false, data, ref password, IntPtr.Zero, ref cryptoInfo))
                    {
                        context.HeaderList.Add(new PageContext.Header(curLba, cryptoInfo));
                    }

                    if (progressMutex.WaitOne())
                    {
                        task = range;
                        progressTask = (((curLba + 1) - range.StartLba) * 100.0) / ((range.EndLba - range.StartLba) + 1);
                        progressTotal = (double)totalCurrentLba / totalLbaCount;
                        if (progressTotal > 0)
                        {
                            TimeSpan elapsed = DateTime.Now - startTimeTotal;
                            remainingTimeTotal = new TimeSpan((long)(elapsed.Ticks / progressTotal)) - elapsed;
                        }
                        progressTotal *= 100.0;
                        progressMutex.ReleaseMutex();
                        backgroundWorker.ReportProgress(0);
                    }

                    curLba++;
                    totalCurrentLba++;
                }                
            }

            resetEvent.Set();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressMutex.WaitOne())
            {
                PhysicalDrive.CylinderHeadSector chsStartOffset = PhysicalDrive.LbaToChs(task.StartLba, context.Drive.Geometry);
                PhysicalDrive.CylinderHeadSector chsEndOffset = PhysicalDrive.LbaToChs(task.EndLba, context.Drive.Geometry);
                lblTask.Text = string.Format("Analyze {1}/{2}/{3} ({0} LBA) - {5}/{6}/{7} ({4} LBA)",
                                             task.StartLba, chsStartOffset.Cylinders, chsStartOffset.TracksPerCylinder, chsStartOffset.SectorsPerTrack,
                                             task.EndLba, chsEndOffset.Cylinders, chsEndOffset.TracksPerCylinder, chsEndOffset.SectorsPerTrack);
                prgTask.Value = (int)progressTask;
                lblPercentageTask.Text = string.Format("{0:0.00}%", progressTask);
                prgTotal.Value = (int)progressTotal;
                lblPercentageTotal.Text = string.Format("{0:0.00}%", progressTotal);
                lblTotalEstimatedTimeRemaining.Text = string.Format("{0} Days {1} Hours {2} Minutes {3} Seconds", remainingTimeTotal.Days, remainingTimeTotal.Hours, remainingTimeTotal.Minutes, remainingTimeTotal.Seconds);
                progressMutex.ReleaseMutex();
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblTask.Text = "";
            prgTask.Value = 100;
            lblPercentageTask.Text = string.Format("{0:0.00}%", 100);
            prgTotal.Value = 100;
            lblPercentageTotal.Text = string.Format("{0:0.00}%", 100);
            lblTotalEstimatedTimeRemaining.Text = "Finished";


            if (context.HeaderList.Count > 0)
            {
                ready = true;
                OnReadyChanged(new EventArgs());
            }
            else if (!canceled)
            {
                MessageBox.Show(this, "No TrueCrypt header could be found.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
   } 
}
