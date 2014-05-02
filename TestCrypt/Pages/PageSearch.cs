using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TestCrypt.Pages
{
    public partial class PageSearch : WizardPage
    {
        #region Attributes
        /// <summary>
        /// The previous page of the wizard to return to in case of a "PageBack" event.
        /// </summary>
        private WizardPage previousPage;
        #endregion

        #region Constructors
         /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="previousPage">The previous page of the wizard to return to in case of a "PageBack" event.</param>
        public PageSearch(WizardPage previousPage)
        {
            this.previousPage = previousPage;
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Enables or disables menu items for the search context menu.
        /// </summary>
        private void UpdateUI()
        {
            mnuRemove.Enabled = (lsvSearch.SelectedItems.Count > 0);
            mnuRemoveAll.Enabled = lsvSearch.Items.Count > 0;

            // this wizard page is ready when at least one range for analysis has been added
            bool ready = lsvSearch.Items.Count > 0;
            if (base.ready != ready)
            {
                base.ready = ready;
                OnReadyChanged(new EventArgs());
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// "SelectedIndexChanged" event handler of the list-view.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        private void lsvSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// "Click" event handler of the "Add" menu-item.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
        private void mnuAdd_Click(object sender, EventArgs e)
        {
            TestCrypt.Forms.FormSearch dlgSearch = new Forms.FormSearch();
            dlgSearch.ShowDialog();
            UpdateUI();
        }

        /// <summary>
        /// "PageBack" event handler of the wizard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:TestCrypt.PageTransitionEventArgs" /> instance containing the event data.</param>
        private void PageSearch_PageBack(object sender, PageTransitionEventArgs e)
        {
            // return to the previous wizard page
            e.Page = previousPage;
        }
        #endregion
    }
}
