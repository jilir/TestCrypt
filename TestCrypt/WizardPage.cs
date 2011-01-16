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
            /// Stores a value indicating whether the event should be cancelled.
            /// </summary>
            private bool cancel;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets a value indicating whether the event should be cancelled.
            /// </summary>
            public bool Cancel
            {
                get { return this.cancel; }
                set { this.cancel = value; }
            }                
            #endregion

            #region Constructors
            /// <summary>
            /// Constructor.
            /// </summary>
            public PageTransitionEventArgs()
            {
                this.cancel = false;
            }
            #endregion
        }

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

        #region Attributes
        /// <summary>
        /// Stores whether the wizard page is ready and the wizard may continue with the next page on user request.
        /// </summary>
        protected bool ready;

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
        /// Gets or sets the title of the application wizard page.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <summary>
        /// Gets or sets the subtitle of the application wizard page.
        /// </summary>
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
        public WizardPage()
        {
            InitializeComponent();
        }
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
    }
}
