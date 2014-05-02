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
    /// Class for the encryption settings page of the TestCrypt wizard.
    /// </summary>
    public partial class PageEncryption : WizardPage
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
        public PageEncryption(WizardPage previousPage)
        {
            this.previousPage = previousPage;
            this.pageSearch = new PageSearch(this);
            InitializeComponent();
            CheckBox_CheckedChanged(null, new EventArgs());
        }
        #endregion

        #region Events
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool ready = ((grpPkcs5Prf.Controls.OfType<CheckBox>().Where(c => c.Checked).Count() > 0) &&
                          (grpMode.Controls.OfType<CheckBox>().Where(c => c.Checked).Count() > 0) &&
                          (grpEncryptionAlgorithm.Controls.OfType<CheckBox>().Where(c => c.Checked).Count() > 0));
            if (base.ready != ready)
            {
                base.ready = ready;
                OnReadyChanged(new EventArgs());
            }
        }

        private void PageEncryption_PageNext(object sender, PageTransitionEventArgs e)
        {

            /*Nullable<TrueCrypt.Password> password = Password;
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
                        e.Page = new PageAnalyze(this);
                        break;
                    case Mode.SearchAdvanced:
                        e.Page = pageSearch;
                        break;
                    default:
                        System.Diagnostics.Debug.Fail("Not Implemented");
                        break;
                }
            }*/
        }

        /// <summary>
        /// "PageBack" event handler of the wizard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:TestCrypt.PageTransitionEventArgs" /> instance containing the event data.</param>
        private void PageEncryption_PageBack(object sender, PageTransitionEventArgs e)
        {
            // return to the previous wizard page
            e.Page = previousPage;
        }
        #endregion
    }
}
