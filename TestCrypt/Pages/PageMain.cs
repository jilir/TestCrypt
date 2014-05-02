using System;
using System.Windows.Forms;

namespace TestCrypt.Pages
{
    /// <summary>
    /// Class for the mode selection page of the TestCrypt wizard.
    /// </summary>
    public partial class PageMain : WizardPage
    {
        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public PageMain()
            : base(true, false)
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// "CheckChanged" event handler of all radio buttons.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        private void PageMain_CheckedChanged(object sender, EventArgs e)
        {
            // selecting an option is enough for the "main"-page of the wizard to become ready
            if (!ready)
            {
                ready = true;
                OnReadyChanged(new EventArgs());
            }
        }

        /// <summary>
        /// "PageNext" event handler of the wizard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:TestCrypt.PageTransitionEventArgs" /> instance containing the event data.</param>
        private void PageMain_PageNext(object sender, PageTransitionEventArgs e)
        {
            // get the mode selected by the user
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton opt = (RadioButton)control;
                    if (opt.Checked)
                    {
                        PageContext.GetInstance().Mode = (Mode)Enum.Parse(typeof(Mode), (string)opt.Tag);
                        break;
                    }
                }
            }

            // select the next page of the TestCrypt wizard
            switch (PageContext.GetInstance().Mode)
            {
                case Mode.MountInPlace:
                case Mode.MountRescueDisk:
                    // start with the device selection wizard page
                    e.Page = new PageDevice(this);
                    break;
                default:
                    // start with the disk selection wizard page
                    e.Page = new PageDisk(this);
                    break;
            }
        }
        #endregion
    }
}
