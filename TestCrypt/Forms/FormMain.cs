using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TestCrypt.Pages;
using Microsoft.Win32.SafeHandles;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Resources;
using System.Collections;

namespace TestCrypt.Forms
{
    public partial class FormMain : Form
    {
        #region Attributes
        private WizardPage currentPage;

        private bool silentExit;

        private FormChsLbaConverter formChsLbaConverter;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public FormMain()
        {
            // configure the language of TestCrypt according to the value stored in the registry (if any)
            if (Settings.Default.Culture != CultureInfo.InvariantCulture)
            {
                Thread.CurrentThread.CurrentUICulture = Settings.Default.Culture;
            }

            InitializeComponent();

            // disable the menu item with the currently active language
            UpdateLanguage(Thread.CurrentThread.CurrentUICulture);

            // activate the first wizard page
            ActivatePage(new PageMain());
        }
        #endregion

        #region Methods
        /// <summary>
        /// Activates the given page of the wizard.
        /// </summary>
        /// <param name="page">The page of the wizard that should be activated.</param>
        private void ActivatePage(WizardPage page)
        {
            // store a reference to the new page of the wizard
            currentPage = page;

            // clear the current page of the wizard first
            pagePanel.Controls.Clear();

            // add wizard page to the wizard
            pagePanel.Controls.Add(page);
            page.Size = pagePanel.Size;
            page.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            page.ReadyChanged -= new EventHandler<EventArgs>(Page_ReadyChanged);
            page.ReadyChanged += new EventHandler<EventArgs>(Page_ReadyChanged);

            // set the title and subtitle of the wizard page
            lblTitle.Text = page.Title;
            rtfSubtitle.Text = page.Subtitle;

            // update the buttons of the wizard
            btnBack.Enabled = !page.FirstPage;
            if (page.LastPage)
            {
                btnNext.Enabled = false;
                btnFinish.Enabled = page.Ready;
                AcceptButton = btnFinish;
            }
            else
            {
                btnFinish.Enabled = false;
                btnNext.Enabled = page.Ready;
                AcceptButton = btnNext;
            }

            // finalize activating the wizard page
            page.OnPageActivated(new EventArgs());
        }

        /// <summary>
        /// Updates the language of the wizard.
        /// </summary>
        /// <param name="culture">The language of the wizard.</param>
        private void UpdateLanguage(CultureInfo culture)
        {
            // enable all language menu items first
            foreach (ToolStripMenuItem item in mnuLanguage.DropDownItems)
            {
                item.Enabled = true;
            }

            // try to find a direct matching culture
            ToolStripMenuItem languageItem = null;
            foreach (ToolStripMenuItem item in mnuLanguage.DropDownItems)
            {
                if (culture.Equals(new CultureInfo((string)item.Tag)))
                {
                    languageItem = item;
                    break;
                }
            }

            // try to find a matching neutral culture if no direct match could be found
            if (languageItem == null)
            {
                while ((culture != null) && !culture.IsNeutralCulture)
                {
                    culture = culture.Parent;
                }
                if (culture != null)
                {
                    foreach (ToolStripMenuItem item in mnuLanguage.DropDownItems)
                    {
                        if (culture.Equals(new CultureInfo((string)item.Tag)))
                        {
                            languageItem = item;
                            break;
                        }
                    }
                }
            }

            // disable the language menu item of the currently selected language
            if (languageItem != null)
            {
                languageItem.Enabled = false;
            }
            else
            {
                mnuEnglish.Enabled = false;
            }
        }
        #endregion

        #region Events
        private void mnuLanguage_Click(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo((string)((ToolStripItem)sender).Tag);
            Settings.Default.Culture = culture;
            Settings.Default.Save();
            UpdateLanguage(culture);
            MessageBox.Show(this, PageContext.GetInstance().GetResourceString("LanguageChanged"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuChsLbaConverter_Click(object sender, EventArgs e)
        {
            if ((null == formChsLbaConverter) || !formChsLbaConverter.Visible)
            {
                formChsLbaConverter = new FormChsLbaConverter();
                formChsLbaConverter.Show();
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog(this);
        }

        private void Page_ReadyChanged(object sender, EventArgs e)
        {
            ((Button)AcceptButton).Enabled = currentPage.Ready;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // the "Next" button has been pressed, activate the next page
            WizardPage.PageTransitionEventArgs args = new WizardPage.PageTransitionEventArgs();
            currentPage.OnPageNext(args);
            if (args.Page != null)
            {
                ActivatePage(args.Page);
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            // the "Finish" button has been pressed, close the wizard
            Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            WizardPage.PageTransitionEventArgs args = new WizardPage.PageTransitionEventArgs();
            currentPage.OnPageBack(args);
            if (args.Page != null)
            {
                ActivatePage(args.Page);
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
                //TrueCrypt.EncryptionThreadPoolStart(Environment.ProcessorCount);
            }
            catch (DllNotFoundException)
            {
                MessageBox.Show(this, PageContext.GetInstance().GetResourceString("LoadTrueCryptDllFailed"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                silentExit = true;
                Close();
            }

            // try to open the TestCrypt driver just to test whether the driver can be opened
            try
            {
                TrueCrypt.OpenDriver();
            }
            catch (TrueCrypt.TrueCryptException ex)
            {
                // the TestCrypt driver could not be opened - most probably this is caused by a 64-bit operating system
                // which requires digitally signed drivers
                string msg;
                switch (ex.Cause)
                {
                    case TrueCrypt.TrueCryptException.ExceptionCause.DriverLoadFailed:
                        // check whether loading the driver failed due to the missing digital driver signature
                        if (((uint)ex.ErrorCode == 0x80004005) && 
                            Wow.Is64BitOperatingSystem && Wow.IsOSAtLeast(Wow.OSVersion.WIN_VISTA))
                        {
                            // missing digital driver signature
                            notifyIcon.BalloonTipClicked += notifyIcon_BalloonTipClicked;
                            notifyIcon.BalloonTipTitle = PageContext.GetInstance().GetResourceString("LoadTestCryptDriverFailedTitle");
                            notifyIcon.BalloonTipText = PageContext.GetInstance().GetResourceString("DriverSignatureEnforcement");
                            notifyIcon.ShowBalloonTip(30000);
                        }
                        else
                        {
                            // another error has occurred
                            msg = string.Format(PageContext.GetInstance().GetResourceString("LoadTestCryptDriverFailed"), ex.ErrorCode, ex.Message);
                            MessageBox.Show(this, msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case TrueCrypt.TrueCryptException.ExceptionCause.DriverOpenFailed:
                    default:
                        msg = string.Format(PageContext.GetInstance().GetResourceString("OpenTestCryptDriverFailed"), ex.ErrorCode, ex.Message);
                        MessageBox.Show(this, msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }   
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!silentExit)
            {
                // it is a little bit to easy to close the wizard unintentionally: let the user confirm that he wants to close the wizard
                e.Cancel = MessageBox.Show(this, PageContext.GetInstance().GetResourceString("Exit"), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes; 
            }
        }

        void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://testcrypt.sourceforge.net/");
        }

        void notifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            notifyIcon.BalloonTipClicked -= notifyIcon_BalloonTipClicked;
        }
        #endregion
    }
}
