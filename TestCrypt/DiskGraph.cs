using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TestCrypt
{
    public partial class DiskGraph : UserControl
    {
        #region LocalTypes
        public enum PartitionType
        {
            Primary,
            Extended,
            Logical
        }
        #endregion

        #region Attributes
        private static Color[] partitionTypeColors = new Color[] { Color.DarkBlue, Color.DarkGreen, Color.Blue };
        private PhysicalDrive.DriveInfo driveInfo;

        private uint selectedPartitionNumber;
        #endregion

        #region Properties
        public PhysicalDrive.DriveInfo DriveInfo
        {
            get { return this.driveInfo; }
            set 
            { 
                this.driveInfo = value;
                Invalidate();
            }
        }

        public uint SelectedPartitionNumber
        {
            get { return this.selectedPartitionNumber; }
            set 
            {
                this.selectedPartitionNumber = value;
                Invalidate();
            }
        }
        #endregion

        #region Constructors
        public DiskGraph()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
        #endregion

        #region Methods
        public static Color GetColor(PartitionType partitionType)
        {
            return partitionTypeColors[(int)partitionType];
        }

        private void PaintPartition(PaintEventArgs e, Rectangle rectPartition, PhysicalDrive.PARTITION_INFORMATION_EX partition, PartitionType partitionType)
        {
            // ensure that a primary or logical partition should be drawn
            System.Diagnostics.Debug.Assert(partitionType != PartitionType.Extended);

            // store the current clipping region
            Region clip = e.Graphics.Clip;

            // draw a black rectangle around the current partition
            Pen penPartition = new Pen(Color.Black, 2.0f);
            Rectangle rectBorder = rectPartition;
            rectBorder.Inflate(-(int)Math.Ceiling(penPartition.Width / 2), -(int)Math.Ceiling(penPartition.Width / 2));
            e.Graphics.DrawRectangle(penPartition, rectBorder);

            // fill the rectangle with white color (partition not selected) or with a hatched brush (partition selected) 
            Rectangle rectInner = rectBorder;
            rectInner.Inflate(-(int)Math.Ceiling(penPartition.Width / 2), -(int)Math.Ceiling(penPartition.Width / 2));
            Brush brush;
            if (partition.PartitionNumber == selectedPartitionNumber)
            {
                brush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(230, 230, 230), Color.White);
            }
            else
            {
                brush = new SolidBrush(Color.White);
            }
            e.Graphics.FillRectangle(brush, rectInner);           

            // add the title - a box coloured using the partition type color with border which contains the partition number
            Rectangle rectTitle = rectInner;
            rectTitle.Height = 17;
            e.Graphics.FillRectangle(new SolidBrush(GetColor(partitionType)), rectTitle);
            e.Graphics.DrawLine(penPartition, new Point(rectTitle.Left, rectTitle.Bottom), new Point(rectTitle.Right, rectTitle.Bottom));

            // draw further information about the partiton
            e.Graphics.Clip = new Region(rectInner);

            //e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Font font = new Font(new FontFamily("Arial"), 9, FontStyle.Regular);
            e.Graphics.DrawString(string.Format("[{0}]", partition.PartitionNumber), font, new SolidBrush(Color.White), rectInner.Left, rectInner.Top);
            string type = PartitionTypes.GetPartitionType(partition.Mbr.PartitionType);
            if (type == null)
            {
                type = "Unknown";
            }
            e.Graphics.DrawString(type, font, new SolidBrush(Color.Black), rectInner.Left, rectInner.Top + 20);
            e.Graphics.DrawString(PhysicalDrive.GetAsBestFitSizeUnit(partition.PartitionLength), font, new SolidBrush(Color.Black), rectInner.Left, rectInner.Top + 40);
            
            // reset the clipping region
            e.Graphics.Clip = clip;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // just draw nothing in case there is nothing to draw
            if (driveInfo == null)
                return;

            // draw real partitions (primary or logical) only and ignore extended partitions
            long curOffset = long.MinValue;
            for (int i = 0; i < driveInfo.Partitions.Count; i++)
            {
                // prepare the rectangle for this partition
                long minOffset = long.MaxValue;
                int partition = -1;
                for (int j = 0; j < driveInfo.Partitions.Count; j++)
                {
                    if ((driveInfo.Partitions[j].StartingOffset >= curOffset) &&
                        (driveInfo.Partitions[j].StartingOffset < minOffset))
                    {
                        minOffset = driveInfo.Partitions[j].StartingOffset;
                        partition = j;
                    }
                }

                // break the loop if there is no further partition to draw
                if (partition == -1) break;

                Rectangle rectPrimary = new Rectangle((int)Math.Ceiling((driveInfo.Partitions[partition].StartingOffset / (double)driveInfo.Size) * ClientRectangle.Width),
                                                      0,
                                                      (int)((driveInfo.Partitions[partition].PartitionLength / (double)driveInfo.Size) * ClientRectangle.Width),
                                                      ClientRectangle.Height);
                curOffset = driveInfo.Partitions[partition].StartingOffset + driveInfo.Partitions[partition].PartitionLength;
                if ((driveInfo.Partitions[partition].Mbr.PartitionType != 0x05U))
                {
                    rectPrimary.Inflate(-1, -3);
                    PaintPartition(e, rectPrimary, driveInfo.Partitions[partition], PartitionType.Primary);
                }
                else
                {
                    rectPrimary.Inflate(-1, -1);

                    // draw a green rectangle around the current partition
                    Pen penPartition = new Pen(GetColor(PartitionType.Extended), 2.0f);
                    Rectangle rectBorder = rectPrimary;
                    rectBorder.Inflate(-(int)Math.Ceiling(penPartition.Width / 2), -(int)Math.Ceiling(penPartition.Width / 2));
                    e.Graphics.DrawRectangle(penPartition, rectBorder);

                    // get the rectangle for the inner region of the extended partition
                    Rectangle rectInner = rectBorder;
                    rectInner.Inflate(-(int)Math.Ceiling(penPartition.Width / 2), -(int)Math.Ceiling(penPartition.Width / 2));

                    // draw all logical partitions within the extended partition
                    PhysicalDrive.PARTITION_INFORMATION_EX extended = driveInfo.Partitions[partition];
                    long curLogicalOffset = driveInfo.Partitions[partition].StartingOffset + 1;
                    for (int j = 0; j < driveInfo.Partitions.Count; j++)
                    {
                        partition = -1;
                        minOffset = long.MaxValue;
                        for (int k = 0; k < driveInfo.Partitions.Count; k++)
                        {
                            if ((driveInfo.Partitions[k].Mbr.PartitionType != 0x05) &&
                                (driveInfo.Partitions[k].StartingOffset >= curLogicalOffset) &&
                                (driveInfo.Partitions[k].StartingOffset < minOffset))
                            {
                                minOffset = driveInfo.Partitions[k].StartingOffset;
                                partition = k;
                            }
                        }

                        // break the loop if there is no further partition to draw
                        if (partition == -1) break;
                        Rectangle rectLogical = new Rectangle(rectInner.Left + (int)Math.Ceiling(((driveInfo.Partitions[partition].StartingOffset - extended.StartingOffset) / (double)extended.PartitionLength) * rectInner.Width), 
                                                              0, 
                                                              (int)((driveInfo.Partitions[partition].PartitionLength / (double)extended.PartitionLength) * rectInner.Width), 
                                                              ClientRectangle.Height);
                        curLogicalOffset = driveInfo.Partitions[partition].StartingOffset + driveInfo.Partitions[partition].PartitionLength;
  
                        rectLogical.Inflate(-1, -3);
                        PaintPartition(e, rectLogical, driveInfo.Partitions[partition], PartitionType.Logical);
                    }
                }              
            }
        }
        #endregion
    }
}
