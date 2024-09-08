using System.Drawing;
using EveOPreview.View.Custom;

namespace EveOPreview.View.Implementation
{
	partial class ThumbnailOverlay
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
            System.Windows.Forms.PictureBox OverlayAreaPictureBox;
            OverlayLabel = new BorderedLabel();
            OverlayAreaPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)OverlayAreaPictureBox).BeginInit();
            SuspendLayout();
            // 
            // OverlayAreaPictureBox
            // 
            OverlayAreaPictureBox.BackColor = System.Drawing.Color.Transparent;
            OverlayAreaPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            OverlayAreaPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            OverlayAreaPictureBox.Location = new System.Drawing.Point(0, 0);
            OverlayAreaPictureBox.Name = "OverlayAreaPictureBox";
            OverlayAreaPictureBox.Size = new System.Drawing.Size(284, 262);
            OverlayAreaPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            OverlayAreaPictureBox.TabIndex = 0;
            OverlayAreaPictureBox.TabStop = false;
            OverlayAreaPictureBox.MouseUp += OverlayArea_Click;
            // 
            // OverlayLabel
            // 
            OverlayLabel.AutoSize = true;
            OverlayLabel.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            OverlayLabel.ForeColor = System.Drawing.Color.Bisque;
            OverlayLabel.Location = new System.Drawing.Point(4, 4);
            OverlayLabel.Name = "OverlayLabel";
            OverlayLabel.Size = new System.Drawing.Size(25, 13);
            OverlayLabel.TabIndex = 1;
            OverlayLabel.Text = "...";
            // 
            // ThumbnailOverlay
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.Black;
            ClientSize = new System.Drawing.Size(284, 262);
            ControlBox = false;
            Controls.Add(OverlayLabel);
            Controls.Add(OverlayAreaPictureBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ThumbnailOverlay";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            Text = "PreviewOverlay";
            TransparencyKey = System.Drawing.Color.Black;
            ((System.ComponentModel.ISupportInitialize)OverlayAreaPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private BorderedLabel OverlayLabel;
	}
}