namespace ControlLibrary
{
    partial class frmAlert
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer_close = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.iButton_ok = new ControlLibrary.iButton();
            this.iButton_cancle = new ControlLibrary.iButton();
            this.label_messgase = new ControlLibrary.iLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_close
            // 
            this.timer_close.Interval = 1000;
            this.timer_close.Tick += new System.EventHandler(this.timer_close_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.iButton_ok);
            this.panel1.Controls.Add(this.iButton_cancle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 48);
            this.panel1.TabIndex = 1;
            // 
            // iButton_ok
            // 
            this.iButton_ok.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iButton_ok.BackColor = System.Drawing.SystemColors.Control;
            this.iButton_ok.ColorNhapNhay = System.Drawing.Color.Red;
            this.iButton_ok.EnableNhapNhay = false;
            this.iButton_ok.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iButton_ok.Location = new System.Drawing.Point(109, 3);
            this.iButton_ok.Name = "iButton_ok";
            this.iButton_ok.Size = new System.Drawing.Size(130, 42);
            this.iButton_ok.TabIndex = 1;
            this.iButton_ok.Tag = "{0}";
            this.iButton_ok.Text = "OK";
            this.iButton_ok.TimerNhapNhay = null;
            this.iButton_ok.UseVisualStyleBackColor = true;
            this.iButton_ok.Click += new System.EventHandler(this.iButton_ok_Click);
            // 
            // iButton_cancle
            // 
            this.iButton_cancle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iButton_cancle.BackColor = System.Drawing.SystemColors.Control;
            this.iButton_cancle.ColorNhapNhay = System.Drawing.Color.Red;
            this.iButton_cancle.EnableNhapNhay = false;
            this.iButton_cancle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iButton_cancle.Location = new System.Drawing.Point(245, 3);
            this.iButton_cancle.Name = "iButton_cancle";
            this.iButton_cancle.Size = new System.Drawing.Size(130, 42);
            this.iButton_cancle.TabIndex = 0;
            this.iButton_cancle.Tag = "Cancle ({0})";
            this.iButton_cancle.Text = "Cancle ({0})";
            this.iButton_cancle.TimerNhapNhay = null;
            this.iButton_cancle.UseVisualStyleBackColor = true;
            this.iButton_cancle.Click += new System.EventHandler(this.iButton_cancle_Click);
            // 
            // label_messgase
            // 
            this.label_messgase.ColorNhapNhay = System.Drawing.Color.Red;
            this.label_messgase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_messgase.EnableNhapNhay = false;
            this.label_messgase.Location = new System.Drawing.Point(0, 0);
            this.label_messgase.Name = "label_messgase";
            this.label_messgase.Padding = new System.Windows.Forms.Padding(10);
            this.label_messgase.Size = new System.Drawing.Size(454, 89);
            this.label_messgase.TabIndex = 2;
            this.label_messgase.Tag = "{0}";
            this.label_messgase.Text = "iLabel1";
            this.label_messgase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_messgase.TimerNhapNhay = null;
            // 
            // frmAlert
            // 
            this.AcceptButton = this.iButton_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 137);
            this.Controls.Add(this.label_messgase);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAlert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THÔNG BÁO";
            this.Load += new System.EventHandler(this.frmAlert_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_close;
        private System.Windows.Forms.Panel panel1;
        private ControlLibrary.iButton iButton_cancle;
        private ControlLibrary.iButton iButton_ok;
        private iLabel label_messgase;
    }
}