using System;
using System.Drawing;
using System.Windows.Forms;

namespace interface_all
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_left = new System.Windows.Forms.Button();
            this.bt_right = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_left
            // 
            this.bt_left.BackColor = System.Drawing.Color.Linen;
            this.bt_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.bt_left.FlatAppearance.BorderSize = 0;
            this.bt_left.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_left.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold);
            this.bt_left.ForeColor = System.Drawing.Color.Gray;
            this.bt_left.Location = new System.Drawing.Point(0, 0);
            this.bt_left.Name = "bt_left";
            this.bt_left.Size = new System.Drawing.Size(94, 856);
            this.bt_left.TabIndex = 2;
            this.bt_left.Text = "<";
            this.bt_left.UseVisualStyleBackColor = false;
            this.bt_left.Click += new System.EventHandler(this.bt_left_Click);
            // 
            // bt_right
            // 
            this.bt_right.BackColor = System.Drawing.Color.Linen;
            this.bt_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.bt_right.FlatAppearance.BorderSize = 0;
            this.bt_right.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_right.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold);
            this.bt_right.ForeColor = System.Drawing.Color.Gray;
            this.bt_right.Location = new System.Drawing.Point(1359, 0);
            this.bt_right.Name = "bt_right";
            this.bt_right.Size = new System.Drawing.Size(93, 856);
            this.bt_right.TabIndex = 0;
            this.bt_right.Text = ">";
            this.bt_right.UseVisualStyleBackColor = false;
            this.bt_right.Click += new System.EventHandler(this.bt_right_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::interface_all.Properties.Resources._1;
            this.pictureBox1.Location = new System.Drawing.Point(94, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1265, 856);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1452, 856);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bt_right);
            this.Controls.Add(this.bt_left);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private Button bt_left;
        private Button bt_right;
        private PictureBox pictureBox1;
    }
}

