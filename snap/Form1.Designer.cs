using System.Collections.Generic;
using System.Windows.Forms;

namespace snap{
    public partial class Form1 : Form{
        // public Dictionary<string, data> database = new Dictionary<string, data>();
        private Form3 scrnSnip;
    }
}

namespace snap
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.snap = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.name1 = new System.Windows.Forms.TextBox();
            this.trsp = new System.Windows.Forms.TrackBar();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.visb = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trsp)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.snap, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.add, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.delete, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.name1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.trsp, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.visb, 3, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(778, 244);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint_1);
            // 
            // snap
            // 
            this.snap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.snap.Enabled = false;
            this.snap.Location = new System.Drawing.Point(585, 99);
            this.snap.Name = "snap";
            this.snap.Size = new System.Drawing.Size(190, 42);
            this.snap.TabIndex = 5;
            this.snap.Text = "&snap";
            this.snap.UseVisualStyleBackColor = true;
            this.snap.Click += new System.EventHandler(this.snap_Click);
            // 
            // add
            // 
            this.add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.add.Location = new System.Drawing.Point(585, 3);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(190, 42);
            this.add.TabIndex = 0;
            this.add.Text = "&Add";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // delete
            // 
            this.delete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.delete.Enabled = false;
            this.delete.Location = new System.Drawing.Point(585, 51);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(190, 42);
            this.delete.TabIndex = 1;
            this.delete.Text = "&Delete";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // name1
            // 
            this.name1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.name1, 2);
            this.name1.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.name1.Location = new System.Drawing.Point(3, 195);
            this.name1.MaxLength = 30;
            this.name1.Name = "name1";
            this.name1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.name1.Size = new System.Drawing.Size(382, 32);
            this.name1.TabIndex = 4;
            this.name1.TextChanged += new System.EventHandler(this.name1_TextChanged);
            // 
            // trsp
            // 
            this.trsp.AllowDrop = true;
            this.tableLayoutPanel1.SetColumnSpan(this.trsp, 2);
            this.trsp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trsp.Enabled = false;
            this.trsp.LargeChange = 2;
            this.trsp.Location = new System.Drawing.Point(391, 195);
            this.trsp.Maximum = 100;
            this.trsp.Name = "trsp";
            this.trsp.Size = new System.Drawing.Size(384, 46);
            this.trsp.TabIndex = 7;
            this.trsp.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trsp.Value = 50;
            this.trsp.Scroll += new System.EventHandler(this.trsp_Scroll);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.listBox1, 3);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.tableLayoutPanel1.SetRowSpan(this.listBox1, 4);
            this.listBox1.Size = new System.Drawing.Size(576, 179);
            this.listBox1.TabIndex = 8;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // visb
            // 
            this.visb.AutoSize = true;
            this.visb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.visb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visb.Enabled = false;
            this.visb.Location = new System.Drawing.Point(585, 147);
            this.visb.Name = "visb";
            this.visb.Size = new System.Drawing.Size(190, 42);
            this.visb.TabIndex = 4;
            this.visb.Text = "Visible";
            this.visb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.visb.UseVisualStyleBackColor = true;
            this.visb.CheckedChanged += new System.EventHandler(this.visb_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 244);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SnapFlowt";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trsp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.TextBox name1;
        private System.Windows.Forms.Button snap;
        private System.Windows.Forms.TrackBar trsp;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox visb;
    }
}

