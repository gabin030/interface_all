using System.Drawing;
using System.Windows.Forms;

namespace interface_all
{
    partial class Form2
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label_instruct = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.bt_easy = new System.Windows.Forms.Button();
            this.bt_hard = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label_instruct);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(1129, 773);
            this.splitContainer1.SplitterDistance = 554;
            this.splitContainer1.TabIndex = 0;
            // 
            // label_instruct
            // 
            this.label_instruct.BackColor = System.Drawing.Color.Snow;
            this.label_instruct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_instruct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_instruct.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label_instruct.Font = new System.Drawing.Font("微軟正黑體", 25.8F, System.Drawing.FontStyle.Bold);
            this.label_instruct.Location = new System.Drawing.Point(0, 0);
            this.label_instruct.Name = "label_instruct";
            this.label_instruct.Size = new System.Drawing.Size(1129, 554);
            this.label_instruct.TabIndex = 0;
            this.label_instruct.Text = "請選擇本次測驗的難易度\n (建議14歲以下按easy)";
            this.label_instruct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitContainer2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1129, 215);
            this.panel3.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.bt_easy);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.bt_hard);
            this.splitContainer2.Size = new System.Drawing.Size(1129, 215);
            this.splitContainer2.SplitterDistance = 96;
            this.splitContainer2.TabIndex = 0;
            // 
            // bt_easy
            // 
            this.bt_easy.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bt_easy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt_easy.FlatAppearance.BorderSize = 0;
            this.bt_easy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_easy.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.bt_easy.Location = new System.Drawing.Point(0, 0);
            this.bt_easy.Name = "bt_easy";
            this.bt_easy.Size = new System.Drawing.Size(1129, 96);
            this.bt_easy.TabIndex = 0;
            this.bt_easy.Text = "EASY";
            this.bt_easy.UseVisualStyleBackColor = false;
            this.bt_easy.Click += new System.EventHandler(this.bt_easy_Click);
            // 
            // bt_hard
            // 
            this.bt_hard.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bt_hard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt_hard.FlatAppearance.BorderSize = 0;
            this.bt_hard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_hard.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.bt_hard.Location = new System.Drawing.Point(0, 0);
            this.bt_hard.Name = "bt_hard";
            this.bt_hard.Size = new System.Drawing.Size(1129, 115);
            this.bt_hard.TabIndex = 0;
            this.bt_hard.Text = "HARD";
            this.bt_hard.UseVisualStyleBackColor = false;
            this.bt_hard.Click += new System.EventHandler(this.bt_hard_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(556, 476);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1129, 773);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private Label label_instruct;
        private Panel panel3;
        private SplitContainer splitContainer2;
        private Button bt_easy;
        private Button bt_hard;
        private Label label1;
    }
}