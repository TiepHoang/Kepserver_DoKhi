using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public class DataGridViewTextAndImageColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewTextAndImageColumn()
        {
            this.CellTemplate = new TextAndImageCell();
        }

        [Browsable(false)]
        public Func<object, TextAndImageCell.TextAndImageCellData> FuncGetValueDisplay { get; set; }

        public override object Clone()
        {
            var tmp = base.Clone() as DataGridViewTextAndImageColumn;
            tmp.FuncGetValueDisplay = this.FuncGetValueDisplay;
            return tmp;
        }
    }


    public class TextAndImageCell : DataGridViewTextBoxCell
    {
        public class TextAndImageCellData
        {
            public TextAndImageCellData()
            {
                Images = new List<Image>();
            }

            public List<Image> Images { get; set; }
            public Color? BackColor { get; set; }
            public string Text { get; set; }
            public bool EnableNhapNhay { get; set; }
        }

        TextAndImageCellData ValueDisplay => DataGridViewTextAndImageColumn?.FuncGetValueDisplay?.Invoke(DataBoundItem);

        DataGridViewTextAndImageColumn DataGridViewTextAndImageColumn => this.OwningColumn as DataGridViewTextAndImageColumn;
        object DataBoundItem => this.DataGridView.Rows[this.RowIndex].DataBoundItem;

        public override object Clone()
        {
            TextAndImageCell tmp = base.Clone() as TextAndImageCell;
            return tmp;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds,
        Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
        object value, object formattedValue, string errorText,
        DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
        {
            TextAndImageCellData cellData = ValueDisplay;

            //set BackColor
            var backcolor = cellData?.BackColor;
            if (backcolor.HasValue)
            {
                Style.BackColor = backcolor.Value;
            }

            //draw text and images
            int image_x = 0;
            int width_image = 0;
            var cellBounds_text = cellBounds;

            if (cellData?.Text != null)
            {
                formattedValue = cellData.Text;
            }
            if (cellData?.Images?.Count > 0)
            {
                width_image = cellData.Images.Sum(q => q.Size.Width) + 1;
                if (string.IsNullOrEmpty(formattedValue + ""))
                {
                    //không có text => image_x ở giữa
                    image_x = cellBounds.X + (cellBounds.Width - width_image) / 2;
                }
                else
                {
                    //có text => image_x ở cuối
                    image_x = cellBounds.X + cellBounds.Width - width_image;
                    cellBounds_text.Width -= width_image;
                }
            }

            base.Paint(graphics, clipBounds, cellBounds_text, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            if (cellData?.Images?.Count > 0)
            {
                System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();

                //fill default khu vực image sắp vẽ
                Rectangle tmp = new Rectangle(image_x, cellBounds.Y, cellBounds.X + cellBounds.Width - image_x, cellBounds.Height);
                base.Paint(graphics, clipBounds, tmp, rowIndex, cellState, null, null, errorText, cellStyle, advancedBorderStyle, paintParts);

                //vẽ image
                tmp.Height -= this.DataGridView.RowTemplate.DividerHeight + 1;
                graphics.SetClip(tmp);
                foreach (var item in cellData.Images)
                {
                    int image_y = cellBounds.Location.Y + (cellBounds.Height - item.Size.Height) / 2;

                    graphics.DrawImage(item, image_x, image_y );
                    image_x += item.Size.Width;
                }
                graphics.EndContainer(container);
            }

            //EnableNhapNhay
            if (this.DataGridView is iDataGridView grv)
            {
                if (cellData?.EnableNhapNhay == true)
                {
                    grv.AddCellNhapNhay(this);
                }
                else
                {
                    grv.RemoveCellNhapNhay(this);
                }
            }
        }
    }
}
