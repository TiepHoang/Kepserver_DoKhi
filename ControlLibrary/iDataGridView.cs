using LibraryExtentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    [DefaultEvent("MyCellClick")]
    public class iDataGridView : DataGridView, IControlNhapNhay
    {
        #region implement NhapNhay


        bool _isNhapNhay;
        public bool EnableNhapNhay
        {
            get => _isNhapNhay;
            set
            {
                _isNhapNhay = value;
                if (_isNhapNhay == false)
                {
                    this.BackColor = DefaultBackColor;
                    this.ForeColor = DefaultForeColor;
                }
            }
        }

        public System.Drawing.Color ColorNhapNhay { get; set; } = System.Drawing.Color.Red;

        TimerNhapNhay _timerNhapNhay;
        public TimerNhapNhay TimerNhapNhay
        {
            get => _timerNhapNhay;
            set
            {
                _timerNhapNhay?.Remove(this);
                _timerNhapNhay = value;
                _timerNhapNhay?.Add(this);
            }
        }

        volatile List<DataGridViewCell> lstCellNhapNhay = new List<DataGridViewCell>();
        public virtual void NhapNhay(bool flag_colored)
        {
            if (lstCellNhapNhay.Count > 0)
            {
                var backColor = flag_colored ? DefaultBackColor : ColorNhapNhay;
                var foreColor = flag_colored ? ColorNhapNhay : DefaultBackColor;
                foreach (var item in lstCellNhapNhay)
                {
                    item.Style.BackColor = backColor;
                    item.Style.ForeColor = foreColor;
                }
            }
        }

        public void RemoveCellNhapNhay(DataGridViewCell cell)
        {
            if (HasCellNhapNhay(cell))
            {
                lstCellNhapNhay.Remove(cell);
                cell.Style.BackColor = DefaultCellStyle.BackColor;
                cell.Style.ForeColor = DefaultCellStyle.ForeColor;
                this.Refresh();
            }
        }

        public void AddCellNhapNhay(DataGridViewCell cell)
        {
            if (HasCellNhapNhay(cell) == false)
            {
                lstCellNhapNhay.Add(cell);
                this.Refresh();
            }
        }

        public bool HasCellNhapNhay(DataGridViewCell cell)
        {
            return lstCellNhapNhay.Contains(cell);
        }

        #endregion
        #region BASIC

        public iDataGridView() : base()
        {
            InitializeComponent();

            var temp = TopLeftHeaderCell;
            AutoGenerateColumns = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CellFormat = true;
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // iDataGridView
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;
            this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DefaultCellStyle = dataGridViewCellStyle2;
            this.EnableHeadersVisualStyles = false;
            this.MultiSelect = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.RowTemplate.DividerHeight = 2;
            this.RowTemplate.Height = 24;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ShowEditingIcon = false;
            this.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mDataGridView_CellClick);
            this.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.mDataGridView_CellFormatting);
            this.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.iDataGridView_DataError);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        public delegate void HandleMyCellClick(object sender, DataGridViewCellEventArgs e, object dataBoundItem);
        public event HandleMyCellClick MyCellClick;

        private void mDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MyCellClick != null && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var data = this.Rows[e.RowIndex].DataBoundItem;
                MyCellClick.Invoke(sender, e, data);
            }
        }

        private void mDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (CellFormat && _dicFuncColumns.ContainsKey(e.ColumnIndex))
            {
                e.Value = _dicFuncColumns[e.ColumnIndex].Invoke(Rows[e.RowIndex].DataBoundItem);
            }
        }


        Dictionary<int, Func<object, object>> _dicFuncColumns = new Dictionary<int, Func<object, object>>();
        public bool CellFormat { get; private set; }
        public void BinDataPropertyName<T>(bool cellFormat, params ColumnFormat<T>[] columnFormat) where T : class
        {
            _dicFuncColumns.Clear();
            CellFormat = cellFormat;

            int min = Math.Min(columnFormat.Length, this.ColumnCount);
            for (int i = 0; i < min; i++)
            {
                if (columnFormat[i] != null)
                {
                    var col = this.Columns[i];
                    col.DefaultCellStyle.Format = columnFormat[i].Format;
                    col.DefaultCellStyle.Alignment = columnFormat[i].Alignment;
                    if (columnFormat[i].NullValue != null)
                    {
                        col.DefaultCellStyle.NullValue = columnFormat[i].NullValue;
                    }

                    if (columnFormat[i].DataPropertyName != null)
                    {
                        if (CellFormat && columnFormat[i].CellFormat)
                        {
                            var func = columnFormat[i].DataPropertyName.Compile();
                            _dicFuncColumns.Add(i, x =>
                            {
                                if (x is T obj && obj != null)
                                {
                                    return func.Invoke(obj);
                                }
                                return null;
                            });
                        }
                        col.DataPropertyName = columnFormat[i].DataPropertyName.GetPropertyName();
                    }
                }
            }
        }

        public class ColumnFormat<T>
        {
            public ColumnFormat(Expression<Func<T, object>> DataPropertyName = null, string Format = null, object NullValue = null, bool CellFormat = true)
            {
                this.CellFormat = CellFormat;
                this.Format = Format;
                this.NullValue = NullValue;
                Alignment = DataGridViewContentAlignment.MiddleCenter;

                this.DataPropertyName = DataPropertyName;
            }

            public Expression<Func<T, object>> DataPropertyName { get; set; }
            public bool CellFormat { get; }
            public string Format { get; set; }
            public DataGridViewContentAlignment Alignment { get; set; }
            public object NullValue { get; set; }
        }

        public virtual void iDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        #endregion
    }

}
