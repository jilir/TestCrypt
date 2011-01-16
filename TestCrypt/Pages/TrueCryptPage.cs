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
                                       "the TrueCrypt source code it does not use the password to decrypt user data." +
                                       Environment.NewLine + Environment.NewLine + 
                                       "TestCrypt can also scan for hidden TrueCrypt volumes. In case of hidden " +
                                       "TrueCrypt volumes the header is stored twice: one header is encrypted with " +
                                       "the normal volume password and the second one is encrypted with the hidden " +
                                       "volume password. Therefore to scan for hidden TrueCrypt volumes you may " +
                                       "either use the password of the normal or the password of the hidden volume.";
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
            bool ready = (txtPassword.Text == txtConfirm.Text) && (txtPassword.Text.Length > 0);
            if (base.ready != ready)
            {
                base.ready = ready;
                OnReadyChanged(new EventArgs());
            }
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

                MessageBox.Show(this, messageBuilder.ToString(), "TestCrypt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            else
            {
                // store the TrueCrypt password using already the structure which can be passed to TrueCrypt
                context.Password = txtPassword.Text;
            }
        }
        #endregion
    }
}
