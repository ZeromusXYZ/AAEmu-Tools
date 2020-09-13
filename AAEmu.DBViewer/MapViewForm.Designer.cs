namespace AAEmu.DBViewer
{
    partial class MapViewForm
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
            this.pView = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.pView.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pView
            // 
            this.pView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pView.Controls.Add(this.statusStrip1);
            this.pView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pView.Location = new System.Drawing.Point(0, 0);
            this.pView.Name = "pView";
            this.pView.Size = new System.Drawing.Size(628, 460);
            this.pView.TabIndex = 0;
            this.pView.Paint += new System.Windows.Forms.PaintEventHandler(this.pView_Paint);
            this.pView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pView_MouseDown);
            this.pView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pView_MouseMove);
            this.pView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pView_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslPos});
            this.statusStrip1.Location = new System.Drawing.Point(0, 434);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(624, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslPos
            // 
            this.tsslPos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslPos.Name = "tsslPos";
            this.tsslPos.Size = new System.Drawing.Size(50, 17);
            this.tsslPos.Text = "Position";
            this.tsslPos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MapViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 460);
            this.Controls.Add(this.pView);
            this.Name = "MapViewForm";
            this.Text = "Map View";
            this.pView.ResumeLayout(false);
            this.pView.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslPos;
    }
}