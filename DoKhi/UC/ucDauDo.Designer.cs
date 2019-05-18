namespace DoKhi.UC
{
    partial class ucDauDo
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox_status = new System.Windows.Forms.PictureBox();
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.iButton_viewdothi = new ControlLibrary.iButton();
            this.iLabel_name = new ControlLibrary.iLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_status)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_status
            // 
            this.pictureBox_status.Image = global::DoKhi.Properties.Resources.dot_green;
            this.pictureBox_status.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_status.Name = "pictureBox_status";
            this.pictureBox_status.Size = new System.Drawing.Size(29, 28);
            this.pictureBox_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_status.TabIndex = 0;
            this.pictureBox_status.TabStop = false;
            // 
            // textBox_value
            // 
            this.textBox_value.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_value.Location = new System.Drawing.Point(3, 37);
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(177, 32);
            this.textBox_value.TabIndex = 2;
            this.textBox_value.Tag = "{0} {1}";
            this.textBox_value.Text = "{0} {1}";
            this.textBox_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // iButton_viewdothi
            // 
            this.iButton_viewdothi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iButton_viewdothi.BackColor = System.Drawing.SystemColors.Control;
            this.iButton_viewdothi.ColorNhapNhay = System.Drawing.Color.Red;
            this.iButton_viewdothi.EnableNhapNhay = false;
            this.iButton_viewdothi.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iButton_viewdothi.Location = new System.Drawing.Point(149, 3);
            this.iButton_viewdothi.Name = "iButton_viewdothi";
            this.iButton_viewdothi.Size = new System.Drawing.Size(31, 28);
            this.iButton_viewdothi.TabIndex = 3;
            this.iButton_viewdothi.Tag = "{0}";
            this.iButton_viewdothi.TimerNhapNhay = null;
            this.iButton_viewdothi.UseVisualStyleBackColor = true;
            this.iButton_viewdothi.Click += new System.EventHandler(this.iButton_viewdothi_Click);
            // 
            // iLabel_name
            // 
            this.iLabel_name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iLabel_name.ColorNhapNhay = System.Drawing.Color.Red;
            this.iLabel_name.EnableNhapNhay = false;
            this.iLabel_name.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iLabel_name.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iLabel_name.Location = new System.Drawing.Point(38, 3);
            this.iLabel_name.Name = "iLabel_name";
            this.iLabel_name.Size = new System.Drawing.Size(105, 28);
            this.iLabel_name.TabIndex = 4;
            this.iLabel_name.Tag = "{0}";
            this.iLabel_name.Text = "NhietDo";
            this.iLabel_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.iLabel_name.TimerNhapNhay = null;
            // 
            // ucDauDo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.iLabel_name);
            this.Controls.Add(this.iButton_viewdothi);
            this.Controls.Add(this.textBox_value);
            this.Controls.Add(this.pictureBox_status);
            this.Name = "ucDauDo";
            this.Size = new System.Drawing.Size(184, 73);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_status)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_status;
        private System.Windows.Forms.TextBox textBox_value;
        private ControlLibrary.iButton iButton_viewdothi;
        private ControlLibrary.iLabel iLabel_name;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
