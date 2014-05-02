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
                dlgTrueCrypt.ShowDialog();
            }
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
