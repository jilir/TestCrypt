using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TestCrypt.Pages
{
    public partial class VolumeAnalyzerPage : WizardPage
    {
        #region Attributes
        private PageContext context;

        private string volume;
        #endregion

        #region Constructors
        public VolumeAnalyzerPage(PageContext context)
        {
            InitializeComponent();
            
            this.context = context;

            // this wizard page is always ready
            this.ready = true;
        }
        #endregion

        #region Events
        private void VolumeAnalyzerPage_PageActivated(object sender, EventArgs e)
        {
            // ensure that a drive has been selected
            if (context.Drive == null)
            {
                throw new NullReferenceException();
            }

            // discard stored partition analyzer parameters in case another drive has been selected
            if (volume != null)
            {
                if (context.Drive.Volume != volume)
                {
                    context.VolumeBeginAnalyzer = new PageContext.VolumeAnalyzer(PageContext.AnalyzeType.None, 0);
                    context.VolumeEndAnalyzer = new PageContext.VolumeAnalyzer(PageContext.AnalyzeType.None, 0);
                }
            }
            volume = context.Drive.Volume;

            txtEndSectors.Minimum = txtBeginSectors.Minimum = 0;
            txtBeginSectors.Maximum = txtEndSectors.Maximum = context.Drive.Size / context.Drive.Geometry.BytesPerSector;
            switch (context.VolumeBeginAnalyzer.AnalyzerType)
            {
                case PageContext.AnalyzeType.Automatic:
                    optBeginAutomatic.Checked = true;
                    break;
                case PageContext.AnalyzeType.Manual:
                    optBeginManual.Checked = true;
                    break;
                case PageContext.AnalyzeType.None:
                    optBeginNoAnalyze.Checked = true;
                    break;
            }
            switch (context.VolumeEndAnalyzer.AnalyzerType)
            {
                case PageContext.AnalyzeType.Automatic:
                    optEndAutomatic.Checked = true;
                    break;
                case PageContext.AnalyzeType.Manual:
                    optEndManual.Checked = true;
                    break;
                case PageContext.AnalyzeType.None:
                    optEndNoAnalyze.Checked = true;
                    break;
            }               
        }

        private void grpBegin_CheckedChanged(object sender, EventArgs e)
        {
            txtBeginSectors.Enabled = optBeginManual.Checked;
            lblBeginSectors.Enabled = optBeginManual.Checked;
            if (optBeginAutomatic.Checked)
            {
                context.VolumeBeginAnalyzer.AnalyzerType = PageContext.AnalyzeType.Automatic;
            }
            else if (optBeginManual.Checked)
            {
                context.VolumeBeginAnalyzer.AnalyzerType = PageContext.AnalyzeType.Manual;
            }
            else
            {
                context.VolumeBeginAnalyzer.AnalyzerType = PageContext.AnalyzeType.None;
            }
        }

        private void grpEnd_CheckedChanged(object sender, EventArgs e)
        {
            txtEndSectors.Enabled = optEndManual.Checked;
            lblEndSectors.Enabled = optEndManual.Checked;
            if (optEndAutomatic.Checked)
            {
                context.VolumeEndAnalyzer.AnalyzerType = PageContext.AnalyzeType.Automatic;
            }
            else if (optEndManual.Checked)
            {
                context.VolumeEndAnalyzer.AnalyzerType = PageContext.AnalyzeType.Manual;
            }
            else
            {
                context.VolumeEndAnalyzer.AnalyzerType = PageContext.AnalyzeType.None;
            }
        }

        private void txtBeginSectors_ValueChanged(object sender, EventArgs e)
        {
            context.VolumeBeginAnalyzer.Sectors = (uint)txtBeginSectors.Value;
        }

        private void txtEndSectors_ValueChanged(object sender, EventArgs e)
        {
            context.VolumeEndAnalyzer.Sectors = (uint)txtEndSectors.Value;
        }
        #endregion
    }
}
