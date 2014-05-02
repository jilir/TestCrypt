using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TestCrypt
{
    /// <summary>
    /// Class for a page of an application wizard.
    /// </summary>
    public partial class WizardPage : UserControl
    {
        #region Local Types
        /// <summary>
        /// Provides data for the events PageNext and PageBack.
        /// </summary>
        public class PageTransitionEventArgs : EventArgs
        {
            #region Attributes
            /// <summary>
            /// The wizard page that should be activated or null if the page transition shall be cancelled.
            /// </summary>
            private WizardPage page;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets the wizard page that should be activated or null if the page transition shall be cancelled.
            /// </summary>
            public WizardPage Page
            {
                get { return this.page; }
                set { this.page = value; }
            }
            #endregion

            #region Constructors
            /// <summary>
            /// Constructor.
            /// </summary>
            public PageTransitionEventArgs()
            {
            }
            #endregion
        }
        #endregion

        #region Attributes
        /// <summary>
        /// Stores whether the wizard page is ready and the wizard may continue with the next page on user request.
        /// </summary>
        protected bool ready;

        /// <summary>
        /// Stores whether this is the first wizard page and the "back" button of the wizard shall be disabled.
        /// </summary>
        private bool firstPage;

        /// <summary>
        /// Stores whether this is the last wizard page and the wizard shall be terminated instead of continuing with
        /// the next page on user request.
        /// </summary>
        private bool lastPage;

        /// <summary>
        /// The title of the application wizard page.
        /// </summary>
        private string title;

        /// <summary>
        /// The subtitle of the application wizard page.
        /// </summary>
        private string subtitle;
        #endregion

        #region Properties
        /// <summary>
        /// Gets whether the wizard page is ready and the wizard may continue with the next page on user request.
        /// </summary>
        public bool Ready
        {
            get { return this.ready; }
        }

        /// <summary>
        /// Gets whether this is the first wizard page and the "back" button of the wizard shall be disabled.
        /// </summary>
        public bool FirstPage
        {
            get { return this.firstPage; }
        }

        /// <summary>
        /// Gets whether this is the last wizard page and the wizard shall be terminated instead of continuing with the
        /// next page on user request.
        /// </summary>
        public bool LastPage
        {
            get { return this.lastPage; }
        }

        /// <summary>
        /// Gets or sets the title of the application wizard page.
        /// </summary>
        [Localizable(true)] 
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <summary>
        /// Gets or sets the subtitle of the application wizard page.
        /// </summary>
        [Localizable(true)] 
        public string Subtitle
        {
            get { return this.subtitle; }
            set { this.subtitle = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="lastPage">True if this is the last wizard page and the wizard shall be terminated instead of 
        /// continuing with the next page on user request.</param>
        public WizardPage(bool firstPage, bool lastPage)
        {
            this.firstPage = firstPage;
            this.lastPage = lastPage;
            InitializeComponent();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public WizardPage() : this(false, false) { }
        #endregion

        #region Methods
        /// <summary>
        /// The event-invoking method that derived classes can override and use to fire the event ReadyChanged.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnReadyChanged(EventArgs e)
        {
            if (ReadyChanged != null)
            {
                ReadyChanged(this, e);
            }
        }

        /// <summary>
        /// The event-invoking method that derived classes can override and use to fire the event PageActivated.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        public virtual void OnPageActivated(EventArgs e)
        {
            if (PageActivated != null)
            {
                PageActivated(this, e);
            }
        }

        /// <summary>
        /// The event-invoking method that derived classes can override and use to fire the event PageNext.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        public virtual void OnPageNext(PageTransitionEventArgs e)
        {
            if (this.PageNext != null)
            {
                this.PageNext(this, e);
            }
        }

        /// <summary>
        /// The event-invoking method that derived classes can override and use to fire the event PageBack.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        public virtual void OnPageBack(PageTransitionEventArgs e)
        {
            if (this.PageBack != null)
            {
                this.PageBack(this, e);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Event which will be fired by the wizard pages whenever the property Ready has changed.
        /// </summary>
        public event EventHandler<EventArgs> ReadyChanged;

        /// <summary>
        /// Event which will be fired whenever the page has been activated and is displayed.
        /// </summary>
        public event EventHandler<EventArgs> PageActivated;

        /// <summary>
        /// Event which will be fired whenever the next button of the wizard page has been pressed.
        /// </summary>
        public event EventHandler<PageTransitionEventArgs> PageNext;

        /// <summary>
        /// Event which will be fired whenever the back button of the wizard page has been pressed.
        /// </summary>
        public event EventHandler<PageTransitionEventArgs> PageBack;
        #endregion

    }
}
