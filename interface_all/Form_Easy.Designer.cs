using System;
using System.Drawing;
using System.Windows.Forms;

namespace interface_all
{
    partial class Form_Easy
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_count_backward = new System.Windows.Forms.Label();
            this.pictureBox_blackcross = new System.Windows.Forms.PictureBox();
            this.label_plus = new System.Windows.Forms.Label();
            this.label_compare = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_blackcross)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_count_backward
            // 
            this.label_count_backward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_count_backward.Font = new System.Drawing.Font("Microsoft JhengHei UI", 150F, System.Drawing.FontStyle.Bold);
            this.label_count_backward.Location = new System.Drawing.Point(0, 0);
            this.label_count_backward.Name = "label_count_backward";
            this.label_count_backward.Size = new System.Drawing.Size(1527, 1115);
            this.label_count_backward.TabIndex = 0;
            this.label_count_backward.Text = "label1";
            this.label_count_backward.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox_blackcross
            // 
            this.pictureBox_blackcross.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_blackcross.Image = global::interface_all.Properties.Resources.black;
            this.pictureBox_blackcross.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_blackcross.Name = "pictureBox_blackcross";
            this.pictureBox_blackcross.Size = new System.Drawing.Size(1527, 1115);
            this.pictureBox_blackcross.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_blackcross.TabIndex = 1;
            this.pictureBox_blackcross.TabStop = false;
            // 
            // label_plus
            // 
            this.label_plus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_plus.Font = new System.Drawing.Font("Microsoft JhengHei UI", 150F, System.Drawing.FontStyle.Bold);
            this.label_plus.Location = new System.Drawing.Point(0, 0);
            this.label_plus.Name = "label_plus";
            this.label_plus.Size = new System.Drawing.Size(1527, 1115);
            this.label_plus.TabIndex = 2;
            this.label_plus.Text = "first";
            this.label_plus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_compare
            // 
            this.label_compare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_compare.Font = new System.Drawing.Font("Microsoft JhengHei UI", 150F, System.Drawing.FontStyle.Bold);
            this.label_compare.Location = new System.Drawing.Point(0, 0);
            this.label_compare.Name = "label_compare";
            this.label_compare.Size = new System.Drawing.Size(1527, 1115);
            this.label_compare.TabIndex = 3;
            this.label_compare.Text = "scecond";
            this.label_compare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_Easy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1527, 1115);
            this.Controls.Add(this.label_compare);
            this.Controls.Add(this.label_plus);
            this.Controls.Add(this.pictureBox_blackcross);
            this.Controls.Add(this.label_count_backward);
            this.Name = "Form_Easy";
            this.Text = "Form_Easy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Easy_Load_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keydown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_blackcross)).EndInit();
            this.ResumeLayout(false);

        }





        #endregion

        private Timer timer1;
        private Label label_count_backward;
        private PictureBox pictureBox_blackcross;
        private Label label_plus;
        private Label label_compare;
    }
}