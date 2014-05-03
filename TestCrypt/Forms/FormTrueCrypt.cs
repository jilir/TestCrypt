using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using TestCrypt.Pages;

namespace TestCrypt.Forms
{
    public partial class FormTrueCrypt : Form
    {
        #region Attributes
        private PageTrueCrypt page = new PageTrueCrypt(null);
        private string filename;
        #endregion

        #region Properties
        public TrueCrypt.CRYPTO_INFO CryptoInfo { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public FormTrueCrypt(string filename)
        {
            this.filename = filename;
            InitializeComponent();
            pagePanel.Controls.Add(page);
            page.Size = pagePanel.Size;
            page.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            page.ReadyChanged += page_ReadyChanged;
        }
        #endregion

        #region Events
        /// <summary>
        /// "Click" event handler of the "OK" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            Nullable<TrueCrypt.Password> password = page.Password;
            if (password.HasValue)
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] data = new byte[TrueCrypt.TC_VOLUME_HEADER_GROUP_SIZE];
                    int count = fs.Read(data, 0, data.Length);
                    fs.Close();

                    int offset = 0;
                    TrueCrypt.Password pwd = password.Value;
                    while (offset + TrueCrypt.TC_VOLUME_HEADER_SIZE_LEGACY <= count)
                    {
                        Array.Copy(data, offset, data, 0, TrueCrypt.TC_VOLUME_HEADER_SIZE_LEGACY);
                        TrueCrypt.CRYPTO_INFO cryptoInfo = new TrueCrypt.CRYPTO_INFO();
                        if (0 == TrueCrypt.ReadVolumeHeader(false, data, ref pwd, IntPtr.Zero, ref cryptoInfo))
                        {
                            CryptoInfo = cryptoInfo;
                            break;
                        }
                        offset += TrueCrypt.TC_VOLUME_HEADER_SIZE;
                    }

                    if (offset + TrueCrypt.TC_VOLUME_HEADER_SIZE_LEGACY <= count)
                    {
                        // select the default keyboard layout and close the dialog (after setting the result of the dialog)
                        InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(PageContext.GetInstance().GetResourceString("PasswordIncorrect"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// "Click" event handler of the "Cancel" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // select the default keyboard layout
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
        }

        private void page_ReadyChanged(object sender, EventArgs e)
        {
            btnAccept.Enabled = page.Ready;
        }
        #endregion
    }
}
