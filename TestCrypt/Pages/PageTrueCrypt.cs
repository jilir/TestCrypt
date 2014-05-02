using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Resources;
using System.Linq;

namespace TestCrypt.Pages
{
    /// <summary>
    /// Class for the TrueCrypt settings page of the TestCrypt wizard.
    /// </summary>
    public partial class PageTrueCrypt : WizardPage
    {
        #region Attributes
        /// <summary>
        /// The previous page of the wizard to return to in case of a "PageBack" event.
        /// </summary>
        private WizardPage previousPage;

        /// <summary>
        /// The next page of the wizard for specific modes of TestCrypt.
        /// </summary>
        private PageEncryption pageEncryption;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the TrueCrypt password.
        /// </summary>
        public Nullable<TrueCrypt.Password> Password
        { 
            get
            {
                bool settingsValid = true;

                // check whether the password contains non-ASCII characters which cannot be used by TrueCrypt
                Regex regex = new Regex("[^\x20-\x7E]");
                MatchCollection matches = regex.Matches(txtPassword.Text);
                if (matches.Count > 0)
                {
                    string message = string.Format(PageContext.GetInstance().GetResourceString("PasswordWithInvalidCharacter"), string.Join(", ", string.Join(", ", matches.Cast<Match>().Select(m => m.Value))));
                    settingsValid = MessageBox.Show(this, message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
                }

                if (settingsValid)
                {
                    // store the TrueCrypt password using already the structure which can be passed to TrueCrypt
                    TrueCrypt.Password password = new TrueCrypt.Password(txtPassword.Text);
                    if (lsvKeyfiles.Items.Count > 0)
                    {
                        // keyfiles are simply "applied" to the password
                        if (!TrueCrypt.KeyFilesApply(ref password, lsvKeyfiles.Items.Cast<ListViewItem>().Select(item => item.Text).ToList()))
                        {
                            MessageBox.Show(this, PageContext.GetInstance().GetResourceString("ApplyKeyfilesFailed"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            settingsValid = false;
                        }
                    }
                    return password;
                }

                return null;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="previousPage">The previous page of the wizard to return to in case of a "PageBack" event.</param>
        public PageTrueCrypt(WizardPage previousPage)
        {
            this.previousPage = previousPage;
            this.pageEncryption = new PageEncryption(this);
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void CheckReady()
        {
            bool ready = ((txtPassword.Text == txtConfirm.Text) || (chkDisplayPassword.Checked)) && ((txtPassword.Text.Length > 0) || (lsvKeyfiles.Items.Count > 0));
            if (base.ready != ready)
            {
                base.ready = ready;
                OnReadyChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Enables or disables menu items for the keyfile context menu.
        /// </summary>
        private void UpdateUI()
        {
            mnuRemove.Enabled = (lsvKeyfiles.SelectedItems.Count > 0);
            mnuRemoveAll.Enabled = lsvKeyfiles.Items.Count > 0;
        }
        #endregion

        #region Events
        private void chkDisplayPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkDisplayPassword.Checked;
            lblConfirm.Enabled = !chkDisplayPassword.Checked;
            txtConfirm.Enabled = !chkDisplayPassword.Checked;
            CheckReady();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            chkUsLayout.Enabled = (txtPassword.Text.Length == 0) && (txtConfirm.Text.Length == 0);
            CheckReady();
        }

        private void chkUsLayout_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsLayout.Checked)
            {
                // select the US keyboard layout
                foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
                {
                    if (lang.LayoutName == "US")
                    {
                        InputLanguage.CurrentInputLanguage = lang;
                        break;
                    }
                }
                if (InputLanguage.CurrentInputLanguage.LayoutName != "US")
                {
                    MessageBox.Show(this, PageContext.GetInstance().GetResourceString("KeyboardLayoutNotAvailable"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    chkUsLayout.Checked = false;
                }
            }
            else
            {
                // select the default keyboard layout
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            }
        }

        private void lsvKeyfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void mnuAddFiles_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in dlgOpenFile.FileNames)
                {
                    if (!lsvKeyfiles.Items.ContainsKey(filename))
                    {
                        ListViewItem item = new ListViewItem(filename);
                        item.Name = filename;
                        lsvKeyfiles.Items.Add(item);
                    }
                }
                CheckReady();
                UpdateUI();
            }
        }

        private void mnuAddPath_Click(object sender, EventArgs e)
        {
            if (dlgFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                if (!lsvKeyfiles.Items.ContainsKey(dlgFolderBrowser.SelectedPath))
                {
                    ListViewItem item = new ListViewItem(dlgFolderBrowser.SelectedPath);
                    item.Name = dlgFolderBrowser.SelectedPath;
                    lsvKeyfiles.Items.Add(item);
                    CheckReady();
                    UpdateUI();
                }
            }
        }

        private void mnuRemove_Click(object sender, EventArgs e)
        {
            lsvKeyfiles.Items.RemoveAt(lsvKeyfiles.SelectedIndices[0]);
            CheckReady();
            UpdateUI();
        }

        private void mnuRemoveAll_Click(object sender, EventArgs e)
        {
            lsvKeyfiles.Items.Clear();
            CheckReady();
            UpdateUI();
        }

        private void PageTrueCrypt_PageActivated(object sender, EventArgs e)
        {
            if (InputLanguage.DefaultInputLanguage.LayoutName == "US")
            {
                chkUsLayout.Enabled = false;
                chkUsLayout.Checked = true;
            }
            else
            {
                chkUsLayout_CheckedChanged(sender, e);
            }
        }

        private void PageTrueCrypt_PageNext(object sender, PageTransitionEventArgs e)
        {
            Nullable<TrueCrypt.Password> password = Password;
            if (password.HasValue)
            {
                // select the default keyboard layout
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;

                // store the TrueCrypt password
                PageContext.GetInstance().Password = password.Value;

                // select the next page of the TestCrypt wizard
                switch (PageContext.GetInstance().Mode)
                {
                    case Mode.SearchQuick:
                    case Mode.SearchDeep:
                    case Mode.SearchAdvanced:
                        e.Page = pageEncryption;
                        break;
                    default:
                        System.Diagnostics.Debug.Fail("Not Implemented");
                        break;
                }
            }
        }

        /// <summary>
        /// "PageBack" event handler of the wizard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:TestCrypt.PageTransitionEventArgs" /> instance containing the event data.</param>
        private void PageTrueCrypt_PageBack(object sender, PageTransitionEventArgs e)
        {
            if (MessageBox.Show(this, PageContext.GetInstance().GetResourceString("PageBackWarning"), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // select the default keyboard layout
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;

                // return to the previous wizard page
                e.Page = previousPage;
            }
        }
        #endregion
    }
}
