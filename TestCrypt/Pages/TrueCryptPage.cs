using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TestCrypt.Pages
{
    public partial class TrueCryptPage : WizardPage
    {
        #region Attributes
        private PageContext context;
        #endregion

        #region Constructors
        public TrueCryptPage(PageContext context)
        {
            InitializeComponent();

            this.context = context;

            this.lblDescription.Text = "There is no possibility to identify a TrueCrypt volume by just looking at " +
                                       "the stored data because TrueCrypt encrypts everything including its volume " +
                                       "header. The password is required in order to decrypt and to verify the " +
                                       "integrity of the volume header. Although this tool includes large parts of " +
                                       "the TrueCrypt source code it does not use the password to decrypt user data.";
        }
        #endregion

        #region Methods
        private void CheckReady()
        {
            bool ready = (txtPassword.Text == txtConfirm.Text) && ((txtPassword.Text.Length > 0) || (lstKeyfiles.Items.Count > 0));
            if (base.ready != ready)
            {
                base.ready = ready;
                OnReadyChanged(new EventArgs());
            }
        }

        private void UpdateKeyfileUi()
        {
            btnRemove.Enabled = (lstKeyfiles.SelectedItem != null);
            btnRemoveAll.Enabled = lstKeyfiles.Items.Count > 0;
        }
        #endregion

        #region Events
        private void chkDisplayPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkDisplayPassword.Checked;
            txtConfirm.UseSystemPasswordChar = !chkDisplayPassword.Checked;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
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
                    MessageBox.Show("The \"US\" keyboard layout cannot be selected. Be aware that only input languages configured in the Windows® Control Panel can be used.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    chkUsLayout.Checked = false;
                }
            }
            else
            {
                // select the default keyboard layout
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            }
        }

        private void lstKeyfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateKeyfileUi();
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in dlg.FileNames)
                {
                    lstKeyfiles.Items.Add(filename);
                    btnRemoveAll.Enabled = true;
                }
                CheckReady();
            }
        }

        private void btnAddPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select a keyfile search path.";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                lstKeyfiles.Items.Add(dlg.SelectedPath);
                btnRemoveAll.Enabled = true;
                CheckReady();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lstKeyfiles.Items.RemoveAt(lstKeyfiles.SelectedIndex);
            CheckReady();
            UpdateKeyfileUi();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            lstKeyfiles.Items.Clear();
            CheckReady();
            UpdateKeyfileUi();
        }

        private void TrueCryptPage_PageActivated(object sender, EventArgs e)
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

        private void TrueCryptPage_PageBack(object sender, PageTransitionEventArgs e)
        {
            // select the default keyboard layout
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
        }

        private void TrueCryptPage_PageNext(object sender, PageTransitionEventArgs e)
        {
            // check whether the password contains non-ASCII characters which cannot be used by TrueCrypt
            Regex regex = new Regex("[^\x20-\x7E]");
            if (regex.IsMatch(txtPassword.Text))
            {
                string message = "The password you have entered contains at least one character which cannot be " +
                                 "stored using ASCII encoding. All current versions of TrueCrypt do not allow " +
                                 "creating a volume whose password contains non-ASCII characters." +
                                 Environment.NewLine + Environment.NewLine +
                                 "The following characters are allowed:" + Environment.NewLine;
                StringBuilder messageBuilder = new StringBuilder(message);
                for (byte i = 0x20; i <= 0x7EU; i++)
                {
                    messageBuilder.Append(Convert.ToChar(i));
                    messageBuilder.Append(' ');
                }
                messageBuilder.Append(Environment.NewLine + Environment.NewLine);
                messageBuilder.Append("Do you really want to continue using this password?");

                e.Cancel = (MessageBox.Show(this, messageBuilder.ToString(), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No);
            }
            
            if (!e.Cancel)
            {
                // store the password
                context.Password = txtPassword.Text;

                // store the keyfiles
                context.Keyfiles.Clear();
                foreach (string keyfile in lstKeyfiles.Items)
                {
                    context.Keyfiles.Add(keyfile);
                }

                // store the TrueCrypt password using already the structure which can be passed to TrueCrypt
                TrueCrypt.Password password = new TrueCrypt.Password(context.Password);
                if (context.Keyfiles.Count > 0)
                {
                    if (false == TrueCrypt.KeyFilesApply(ref password, context.Keyfiles))
                    {
                        MessageBox.Show(this, "An error has occurred applying the keyfiles.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
                context.TcPassword = password;

                // select the default keyboard layout
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            }
        }
        #endregion
    }
}
