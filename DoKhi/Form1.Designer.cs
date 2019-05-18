namespace DoKhi
{
    partial class Form1
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
            _dispose();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_main = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chart = new LiveCharts.WinForms.CartesianChart();
            this.iLabel1 = new ControlLibrary.iLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel_dsdiemdo = new System.Windows.Forms.FlowLayoutPanel();
            this.ucDauDo1 = new DoKhi.UC.ucDauDo();
            this.panel3 = new System.Windows.Forms.Panel();
            this.iButton1 = new System.Windows.Forms.Button();
            this.iLabel_statusKepserver = new System.Windows.Forms.Label();
            this.pictureBox_statusKepserver = new System.Windows.Forms.PictureBox();
            this.panelbase_log.SuspendLayout();
            this.panel_main.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel_dsdiemdo.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_statusKepserver)).BeginInit();
            this.SuspendLayout();
            // 
            // iRichTextBox_log
            // 
            this.iRichTextBox_log.Size = new System.Drawing.Size(1094, 164);
            // 
            // panelbase_log
            // 
            this.panelbase_log.Location = new System.Drawing.Point(0, 664);
            this.panelbase_log.Size = new System.Drawing.Size(1094, 164);
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.panel2);
            this.panel_main.Controls.Add(this.panel1);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1094, 664);
            this.panel_main.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chart);
            this.panel2.Controls.Add(this.iLabel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 438);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1094, 226);
            this.panel2.TabIndex = 1;
            // 
            // chart
            // 
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(0, 23);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(1092, 201);
            this.chart.TabIndex = 1;
            this.chart.Text = "cartesianChart1";
            this.chart.Visible = false;
            // 
            // iLabel1
            // 
            this.iLabel1.ColorNhapNhay = System.Drawing.Color.Red;
            this.iLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.iLabel1.EnableNhapNhay = false;
            this.iLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iLabel1.Location = new System.Drawing.Point(0, 0);
            this.iLabel1.Name = "iLabel1";
            this.iLabel1.Size = new System.Drawing.Size(1092, 23);
            this.iLabel1.TabIndex = 0;
            this.iLabel1.Tag = "Đồ thị - {0}";
            this.iLabel1.Text = "Đồ thị (cập nhật sau)";
            this.iLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.iLabel1.TimerNhapNhay = null;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel_dsdiemdo);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1094, 438);
            this.panel1.TabIndex = 0;
            // 
            // flowLayoutPanel_dsdiemdo
            // 
            this.flowLayoutPanel_dsdiemdo.AutoScroll = true;
            this.flowLayoutPanel_dsdiemdo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel_dsdiemdo.Controls.Add(this.ucDauDo1);
            this.flowLayoutPanel_dsdiemdo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_dsdiemdo.Location = new System.Drawing.Point(0, 38);
            this.flowLayoutPanel_dsdiemdo.Name = "flowLayoutPanel_dsdiemdo";
            this.flowLayoutPanel_dsdiemdo.Size = new System.Drawing.Size(1094, 400);
            this.flowLayoutPanel_dsdiemdo.TabIndex = 1;
            // 
            // ucDauDo1
            // 
            this.ucDauDo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucDauDo1.Location = new System.Drawing.Point(3, 3);
            this.ucDauDo1.Name = "ucDauDo1";
            this.ucDauDo1.Size = new System.Drawing.Size(186, 75);
            this.ucDauDo1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.iButton1);
            this.panel3.Controls.Add(this.iLabel_statusKepserver);
            this.panel3.Controls.Add(this.pictureBox_statusKepserver);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1094, 38);
            this.panel3.TabIndex = 0;
            // 
            // iButton1
            // 
            this.iButton1.Location = new System.Drawing.Point(213, 9);
            this.iButton1.Name = "iButton1";
            this.iButton1.Size = new System.Drawing.Size(109, 23);
            this.iButton1.TabIndex = 5;
            this.iButton1.Text = "Random data";
            this.iButton1.UseVisualStyleBackColor = true;
            this.iButton1.Click += new System.EventHandler(this.iButton1_Click);
            // 
            // iLabel_statusKepserver
            // 
            this.iLabel_statusKepserver.Location = new System.Drawing.Point(47, 7);
            this.iLabel_statusKepserver.Name = "iLabel_statusKepserver";
            this.iLabel_statusKepserver.Size = new System.Drawing.Size(160, 28);
            this.iLabel_statusKepserver.TabIndex = 4;
            this.iLabel_statusKepserver.Tag = "{0}";
            this.iLabel_statusKepserver.Text = "Running...";
            this.iLabel_statusKepserver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox_statusKepserver
            // 
            this.pictureBox_statusKepserver.Image = global::DoKhi.Properties.Resources.dot_gray;
            this.pictureBox_statusKepserver.Location = new System.Drawing.Point(12, 7);
            this.pictureBox_statusKepserver.Name = "pictureBox_statusKepserver";
            this.pictureBox_statusKepserver.Size = new System.Drawing.Size(29, 28);
            this.pictureBox_statusKepserver.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_statusKepserver.TabIndex = 1;
            this.pictureBox_statusKepserver.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 828);
            this.Controls.Add(this.panel_main);
            this.Name = "Form1";
            this.Text = "TRANG CHỦ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.panelbase_log, 0);
            this.Controls.SetChildIndex(this.panel_main, 0);
            this.panelbase_log.ResumeLayout(false);
            this.panel_main.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel_dsdiemdo.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_statusKepserver)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private ControlLibrary.iLabel iLabel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_dsdiemdo;
        private UC.ucDauDo ucDauDo1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox_statusKepserver;
        private System.Windows.Forms.Label iLabel_statusKepserver;
        private System.Windows.Forms.Button iButton1;
        private LiveCharts.WinForms.CartesianChart chart;
    }
}

