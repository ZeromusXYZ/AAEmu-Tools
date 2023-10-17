namespace AAEmu.DBEditor.forms
{
    partial class MapForm
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
            groupBox1 = new System.Windows.Forms.GroupBox();
            ViewPort = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)ViewPort).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(711, 63);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Map";
            // 
            // ViewPort
            // 
            ViewPort.Dock = System.Windows.Forms.DockStyle.Fill;
            ViewPort.Location = new System.Drawing.Point(0, 63);
            ViewPort.Name = "ViewPort";
            ViewPort.Size = new System.Drawing.Size(711, 381);
            ViewPort.TabIndex = 1;
            ViewPort.TabStop = false;
            ViewPort.Paint += ViewPort_Paint;
            // 
            // MapForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(711, 444);
            Controls.Add(ViewPort);
            Controls.Add(groupBox1);
            Name = "MapForm";
            Text = "Map";
            Load += MapForm_Load;
            ((System.ComponentModel.ISupportInitialize)ViewPort).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox ViewPort;
    }
}