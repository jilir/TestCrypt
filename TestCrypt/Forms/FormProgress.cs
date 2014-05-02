using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TestCrypt
{
    public partial class FormProgress : Form
    {
        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public FormProgress()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when ShowDialog() is called.
        /// </summary>
        public event DoWorkEventHandler DoWork;

        /// <summary>
        /// "Shown" event handler of the dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        private void FormProgress_Shown(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// "DoWork" event handler of the background worker.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (DoWork != null)
            {
                DoWork(this, e);
            }
        }

        /// <summary>
        /// "RunWorkerCompleted" event handler of the background worker.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
