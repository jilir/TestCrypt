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

        private uint volume = uint.MaxValue;
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

        #region Methods
        /// <summary>
        /// Parses a string which contains either an LBA address or an C/H/S address.
        /// </summary>
        /// <param name="text">The string which contains either an LBA address or an C/H/S address.</param>
        /// <param name="lba">The LBA address which has been parsed.</param>
        /// <returns>True if the string has been successfully parsed, otherwise false.</returns>
        bool ParseChsOrLba(string text, out long lba)
        {
            if (text != null)
            {
                if (long.TryParse(text, out lba))
                {
                    return true;
                }
                else
                {
                    string[] chsToken = text.Split('/');
                    if (chsToken.Length == 3)
                    {
                        PhysicalDrive.CylinderHeadSector chs;
                        if (long.TryParse(chsToken[0], out chs.Cylinders))
                        {
                            if (uint.TryParse(chsToken[1], out chs.TracksPerCylinder))
                            {
                                if (uint.TryParse(chsToken[2], out chs.SectorsPerTrack))
                                {
                                    lba = PhysicalDrive.ChsToLba(chs, context.Drive.Geometry);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                lba = 0;
            }

            return false;
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
            if (context.Drive.Volume != volume)
            {
                context.VolumeBeginAnalyzer = new PageContext.VolumeAnalyzer(PageContext.AnalyzeType.None, 0);
                context.VolumeEndAnalyzer = new PageContext.VolumeAnalyzer(PageContext.AnalyzeType.None, 0);
                dgvCustomAnalyzer.Rows.Clear();
            }
            volume = context.Drive.Volume;

            txtEndSectors.Minimum = txtBeginSectors.Minimum = 1;
            txtBeginSectors.Maximum = txtEndSectors.Maximum = context.Drive.Size / context.Drive.Geometry.BytesPerSector;
            txtBeginSectors.Value = context.VolumeBeginAnalyzer.Sectors;
            txtEndSectors.Value = context.VolumeEndAnalyzer.Sectors;
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
        
        private void VolumeAnalyzerPage_PageNext(object sender, PageTransitionEventArgs e)
        {
            context.CustomAnalyzer.Clear();
            foreach (DataGridViewRow row in dgvCustomAnalyzer.Rows)
            {
                if ((row.Cells[0].Value != null) || ((string)row.Cells[1].Value != null))
                {
                    long startLba, endLba;
                    bool ret = ParseChsOrLba((string)row.Cells[0].Value, out startLba);
                    System.Diagnostics.Debug.Assert(ret);
                    ret = ParseChsOrLba((string)row.Cells[1].Value, out endLba);
                    System.Diagnostics.Debug.Assert(ret);
                    context.CustomAnalyzer.Add(new PageContext.ScanRange(startLba, endLba));
                }
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

        private void dgvCustomAnalyzer_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            string startStr = (string)dgvCustomAnalyzer.Rows[e.RowIndex].Cells[0].Value;
            string endStr = (string)dgvCustomAnalyzer.Rows[e.RowIndex].Cells[1].Value;
            if ((startStr == null) && (endStr == null))
            {
                // ignore empty row
            }
            else
            {
                long start, end;
                string errMessage = null;
                if (!ParseChsOrLba(startStr, out start))
                {
                    errMessage = "Start offset format is unknown";
                }
                else if (!ParseChsOrLba(endStr, out end))
                {
                    errMessage = "End offset format is unknown";
                }
                else
                {
                    long maxLba = context.Drive.Size / context.Drive.Geometry.BytesPerSector;
                    if ((start < 0) || (start >= maxLba))
                    {
                        errMessage = "Start offset out of range";
                    }
                    else if ((end < 0) || (end >= maxLba))
                    {
                        errMessage = "End offset out of range";
                    }
                    else if (start > end)
                    {
                        errMessage = "Start offset is greater than end offset";
                    }
                }

                if (errMessage != null)
                {
                    MessageBox.Show(this, string.Format("The custom analyzer range is not valid: {0}.", errMessage), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
           
        }
        #endregion
    }
}
