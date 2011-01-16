using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TestCrypt.Pages;

namespace TestCrypt
{
    public partial class FormMain : Form
    {
        #region Attributes
        private WizardPage[] pages;

        private int activePage;

        private bool silentExit;
        #endregion

        #region Constructors
        public FormMain()
        {
            InitializeComponent();

            PageContext context = new PageContext();
            pages = new WizardPage[] { new SelectVolumePage(context), 
                                       new PartitionAnalyzerPage(context), 
                                       new VolumeAnalyzerPage(context), 
                                       new TrueCryptPage(context), 
                                       new SummaryPage(context), 
                                       new AnalyzePage(context),
                                       new ResultPage(context)
                                     };

            // adjust the size of the wizard to the size of the biggest wizard page
            AdjustWizardSize();

            // activate the first wizard page
            ActivatePage(0);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adjust the size of the wizard to the size of the biggest wizard page.
        /// </summary>
        private void AdjustWizardSize()
        {   
            foreach (WizardPage page in pages)
            {
                // adjust width of the wizard to the current wizard page
                if (page.Size.Width > pagePanel.Size.Width)
                {
                    Size = new Size(Size.Width + (page.Size.Width - pagePanel.Size.Width), Size.Height);
                }

                // adjust height of the wizard to the current wizard page
                if (page.Size.Height > pagePanel.Size.Height)
                {
                    Size = new Size(Size.Width, Size.Height + (page.Size.Height - pagePanel.Size.Height));
                }
            }
        }

        /// <summary>
        /// Activates the given page of the wizard.
        /// </summary>
        /// <param name="page">The page of the wizard that should be activated.</param>
        public void ActivatePage(int page) 
        {
            if (page < 0)
            {
                page = 0;
            }
            else if (page >= pages.Length)
            {
                page = pages.Length - 1;
            }

            // add wizard page to the wizard if not done already
            if (!pagePanel.Controls.Contains(pages[page]))
            {
                pages[page].Size = pagePanel.Size;
                pagePanel.Controls.Add(pages[page]);
                pages[page].ReadyChanged += new EventHandler<EventArgs>(Page_ReadyChanged);
            }

            // update visibility of the wizard pages
            pages[activePage].Visible = false;
            pages[page].Visible = true;

            // set the title and subtitle of the wizard page
            lblTitle.Text = pages[page].Title;
            lblSubtitle.Text = pages[page].Subtitle;

            // update the buttons of the wizard
            btnBack.Enabled = (page != 0);
            btnNext.Text = ((page + 1) == pages.Length) ? "&Finish" : "&Next >";

            // finalize activating the wizard page
            pages[page].OnPageActivated(new EventArgs());
            btnNext.Enabled = pages[page].Ready;
            activePage = page;            
        }
        #endregion

        #region Events
        private void Page_ReadyChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = pages[activePage].Ready;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            WizardPage.PageTransitionEventArgs args = new WizardPage.PageTransitionEventArgs();
            pages[activePage].OnPageNext(args);
            if (!args.Cancel)
            {
                // the "Next >" button will become the "Finish" button on the last page
                if ((activePage + 1) < pages.Length)
                {
                    // activate the next page
                    ActivatePage((int)(activePage + args.PageCount));
                }
                else
                {
                    // the "Finish" button has been pressed, close the wizard
                    Close();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            WizardPage.PageTransitionEventArgs args = new WizardPage.PageTransitionEventArgs();
            pages[activePage].OnPageBack(args);
            if (!args.Cancel)
            {
                ActivatePage((int)(activePage - args.PageCount));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // start the encryption thread pool to speed up the analyzer - this is also the first access to the native 
            // "TrueCrypt.dll" and therefore an exception may occur when the library cannot be loaded
            try
            {
                TrueCrypt.EncryptionThreadPoolStart(Environment.ProcessorCount);
            }
            catch (DllNotFoundException)
            {
                MessageBox.Show(this, "Unable to load DLL \"TrueCrypt.dll\": This could be caused by a missing Microsoft Visual C++ 2010 Redistributable Package (x86).", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                silentExit = true;
                Close();
            }

            // inform the user that some 64-bit operating systems are not supported due to driver signing
            if (Wow.Is64BitOperatingSystem && Wow.IsOSAtLeast(Wow.OSVersion.WIN_VISTA))
            {
                MessageBox.Show(this, "A 64-bit operating system which requires signed drivers (Windows® Vista or newer) has been detected. Mounting a volume is not supported and will fail unless you have signed the driver on your own.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!silentExit)
            {
                // it is a little bit to easy to close the wizard unintentionally: let the user confirm that he wants to close the wizard
                e.Cancel = MessageBox.Show(this, "Do you really want to exit TestCrypt", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes; 
            }
        }
        #endregion
    }
}
