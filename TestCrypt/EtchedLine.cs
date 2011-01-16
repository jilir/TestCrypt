using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TestCrypt
{
    public partial class EtchedLine : UserControl
    {
        #region Attributes
        Color darkColor = SystemColors.ControlDark;
        Color lightColor = SystemColors.ControlLightLight;
        #endregion

        #region Properties
        [Category("Appearance")]
        public Color DarkColor
        {
            get { return darkColor; }

            set
            {
                darkColor = value;
                Refresh();
            }
        }

        [Category("Appearance")]
        public Color LightColor
        {
            get { return lightColor; }

            set
            {
                lightColor = value;
                Refresh();
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public EtchedLine()
        {
            InitializeComponent();

            // avoid receiving the focus
            SetStyle(ControlStyles.Selectable, false);
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            Brush lightBrush = new SolidBrush(lightColor);
            Brush darkBrush = new SolidBrush(darkColor);
            Pen lightPen = new Pen(lightBrush, 1);
            Pen darkPen = new Pen(darkBrush, 1);
            
            e.Graphics.DrawLine(darkPen, 0, 0, this.Width, 0);
            e.Graphics.DrawLine(lightPen, 0, 1, this.Width, 1);
        }
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize (e);
            Refresh();
        }
        #endregion
    }
}
