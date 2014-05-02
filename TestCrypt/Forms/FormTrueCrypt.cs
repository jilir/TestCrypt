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
                    fs.Read(data, 0, data.Length);
                    fs.Close();

                    IntPtr cryptoInfo = IntPtr.Zero;
                    /*if (0 == TrueCrypt.ReadVolumeHeader(false, data, ref password.Value, ref cryptoInfo, IntPtr.Zero))
                    {
                        TrueCrypt.CRYPTO_INFO cryptInfoStruct = (TrueCrypt.CRYPTO_INFO)Marshal.PtrToStructure(cryptoInfo, typeof(TrueCrypt.CRYPTO_INFO));
                        MessageBox.Show(string.Format("TrueCrypt header successfully decrypted (Version {0}, Size {1}, Offset {2}, {3}, Sectorsize {4}).", cryptInfoStruct.HeaderVersion, cryptInfoStruct.VolumeSize, cryptInfoStruct.EncryptedAreaStart, cryptInfoStruct.hiddenVolume ? "Hidden" : "Normal", cryptInfoStruct.SectorSize), "Header Decrypted", MessageBoxButtons.OK);
                        refUnit = (ulong)nudOffset.Value;
                    }
                    else
                    {
                        MessageBox.Show("Invalid password or not a TrueCrypt header.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }*/
                }

                // select the default keyboard layout
                //InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;

            }
        }

        private void page_ReadyChanged(object sender, EventArgs e)
        {
            btnAccept.Enabled = page.Ready;
        }
        #endregion
    }
}
