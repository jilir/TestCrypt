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
using TestCrypt.Forms;
using Be.Windows.Forms;

namespace TestCrypt.Pages
{
    /// <summary>
    /// Class for the data fragment search of the TestCrypt wizard.
    /// </summary>
    public partial class PageFragment : WizardPage
    {
        #region Attributes
        /// <summary>
        /// The previous page of the wizard to return to in case of a "PageBack" event.
        /// </summary>
        private WizardPage previousPage;

        private PageSearch pageSearch;

        private DynamicByteProvider matchByteProvider = new DynamicByteProvider(new ByteCollection());
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="previousPage">The previous page of the wizard to return to in case of a "PageBack" event.</param>
        public PageFragment(WizardPage previousPage)
        {
            this.previousPage = previousPage;
            this.pageSearch = new PageSearch(this);
            InitializeComponent();
            hexMatch.ByteProvider = matchByteProvider;
            matchByteProvider.Changed += matchByteProvider_Changed;
        }
        #endregion

        #region Methods
        private void CheckReady()
        {
            bool ready = (!string.IsNullOrEmpty(txtHeader.Text) && (hexMatch.ByteProvider.Length > 0));
            if (base.ready != ready)
            {
                base.ready = ready;
                OnReadyChanged(new EventArgs());
            }
        }

        private string ConvertRequiredProgramVersion(ushort requiredProgramVersion)
        {
            StringBuilder sb = new StringBuilder();
            if (0 != (requiredProgramVersion & 0xF000))
            {
                sb.Append((byte)((requiredProgramVersion & 0xF000) >> 12));
            }
            sb.Append((byte)((requiredProgramVersion & 0x0F00) >> 8));
            sb.Append('.');
            sb.Append((byte)((requiredProgramVersion & 0x00F0) >> 4));
            if (0 != (requiredProgramVersion & 0x000F))
            {
                sb.Append((byte)((requiredProgramVersion & 0x000F) >> 0));
            }
            return sb.ToString();
        }
        #endregion

        #region Events
        /// <summary>
        /// "Click" event handler of the "Select" button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                FormTrueCrypt dlgTrueCrypt = new FormTrueCrypt(dlgOpenFile.FileName);
                if (dlgTrueCrypt.ShowDialog() == DialogResult.OK)
                {
                    txtHeader.Text = dlgOpenFile.FileName;

                    lsvProperties.BeginUpdate();
                    
                    // clear the properties already added to the list-view
                    lsvProperties.Items.Clear();

                    // add the properties of the TrueCrypt volume to the list-view
                    ListViewItem item = new ListViewItem(PageContext.GetInstance().GetResourceString("HeaderPropertySize"));
                    item.SubItems.Add(dlgTrueCrypt.CryptoInfo.VolumeSize.ToString());
                    lsvProperties.Items.Add(item);

                    item = new ListViewItem(PageContext.GetInstance().GetResourceString("HeaderPropertyHeaderVersion"));
                    item.SubItems.Add(dlgTrueCrypt.CryptoInfo.HeaderVersion.ToString());
                    lsvProperties.Items.Add(item);

                    item = new ListViewItem(PageContext.GetInstance().GetResourceString("HeaderPropertyRequiredTrueCryptVersion"));
                    item.SubItems.Add(ConvertRequiredProgramVersion(dlgTrueCrypt.CryptoInfo.RequiredProgramVersion));
                    lsvProperties.Items.Add(item);

                    item = new ListViewItem(PageContext.GetInstance().GetResourceString("HeaderPropertyType"));
                    item.SubItems.Add(PageContext.GetInstance().GetResourceString(dlgTrueCrypt.CryptoInfo.hiddenVolume? "HeaderPropertyTypeHidden" : "HeaderPropertyTypeNormal"));
                    lsvProperties.Items.Add(item);

                    item = new ListViewItem(PageContext.GetInstance().GetResourceString("HeaderPropertyEncryptedAreaOffset"));
                    item.SubItems.Add(dlgTrueCrypt.CryptoInfo.EncryptedAreaStart.ToString());
                    lsvProperties.Items.Add(item);       

                    item = new ListViewItem(PageContext.GetInstance().GetResourceString("HeaderPropertyEncryptionAlgorithm"));
                    item.SubItems.Add(dlgTrueCrypt.CryptoInfo.ea.ToString());
                    lsvProperties.Items.Add(item);

                    item = new ListViewItem(PageContext.GetInstance().GetResourceString("HeaderPropertyOperationMode"));
                    item.SubItems.Add(dlgTrueCrypt.CryptoInfo.mode.ToString());
                    lsvProperties.Items.Add(item);

                    item = new ListViewItem(PageContext.GetInstance().GetResourceString("HeaderPropertyPkcs5Prf"));
                    item.SubItems.Add(dlgTrueCrypt.CryptoInfo.pkcs5.ToString());
                    lsvProperties.Items.Add(item);

                    lsvProperties.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    lsvProperties.EndUpdate();
                    CheckReady();
                }
            }
        }

        void matchByteProvider_Changed(object sender, EventArgs e)
        {
            CheckReady();
        }

        private void TrueCryptPage_PageActivated(object sender, EventArgs e)
        {
           
        }

        private void TrueCryptPage_PageNext(object sender, PageTransitionEventArgs e)
        {
           
        }

        /// <summary>
        /// "PageBack" event handler of the wizard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:TestCrypt.PageTransitionEventArgs" /> instance containing the event data.</param>
        private void PageFragment_PageBack(object sender, PageTransitionEventArgs e)
        {
            if (MessageBox.Show(this, PageContext.GetInstance().GetResourceString("PageBackWarning"), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // return to the previous wizard page
                e.Page = previousPage;
            }
        }
        #endregion

    }
}
