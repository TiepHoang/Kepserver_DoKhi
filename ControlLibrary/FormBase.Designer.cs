namespace ControlLibrary
{
    partial class FormBase
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
            DisposeForm?.Invoke();
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
            this.iRichTextBox_log = new ControlLibrary.iRichTextBox();
            this.panelbase_log = new System.Windows.Forms.Panel();
            this.panelbase_log.SuspendLayout();
            this.SuspendLayout();
            // 
            // iRichTextBox_log
            // 
            this.iRichTextBox_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iRichTextBox_log.Location = new System.Drawing.Point(0, 0);
            this.iRichTextBox_log.Name = "iRichTextBox_log";
            this.iRichTextBox_log.Size = new System.Drawing.Size(800, 164);
            this.iRichTextBox_log.TabIndex = 0;
            this.iRichTextBox_log.Text = "";
            // 
            // panelbase_log
            // 
            this.panelbase_log.Controls.Add(this.iRichTextBox_log);
            this.panelbase_log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelbase_log.Location = new System.Drawing.Point(0, 286);
            this.panelbase_log.Name = "panelbase_log";
            this.panelbase_log.Size = new System.Drawing.Size(800, 164);
            this.panelbase_log.TabIndex = 1;
            // 
            // FormBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelbase_log);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmBase";
            this.Load += new System.EventHandler(this.FormBase_Load);
            this.panelbase_log.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected iRichTextBox iRichTextBox_log;
        protected System.Windows.Forms.Panel panelbase_log;
    }
}