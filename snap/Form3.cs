using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace snap
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = 0;
            this.WindowState = FormWindowState.Maximized;
            this.Activate();
            this.Show();
        }
        bool beginCap = false;
        public int currentX = 0;
        public int currentY = 0;

        private void Form3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(true){
                transp.touch = true;
                this.Close();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            beginCap = true;
            this.WindowState = FormWindowState.Normal;
            currentX = MousePosition.X;
            currentY = MousePosition.Y;
            this.Left = currentX;
            this.Top += currentY;
            this.Size = new Size(0, 0);
            this.Opacity = 0.5D;
        }

        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {
            if(beginCap){
                this.Left = (currentX<MousePosition.X)? currentX: MousePosition.X;
                this.Top = (currentY<MousePosition.Y)? currentY: MousePosition.Y;
                int W = Math.Abs(MousePosition.X - currentX);
                int H = Math.Abs(MousePosition.Y - currentY);
                this.Size = new Size(W, H);
            }
        }

        private void Form3_MouseUp(object sender, MouseEventArgs e)
        {
            if(beginCap){
                beginCap = false;
                this.Visible = false;
                transp.bt = new Bitmap(Size.Width, Size.Height);
                Graphics grap = Graphics.FromImage(transp.bt);
                grap.CopyFromScreen(new Point(this.Left, this.Top), 
                                    new Point(0, 0),
                                    new Size(this.Size.Width, this.Size.Height));
                transp.used = true;

                transp.database[transp.target].pip.Assign(  this.Left, 
                                                            this.Top,
                                                            transp.bt.Width,
                                                            transp.bt.Height,
                                                            transp.database[transp.target].transp,
                                                            transp.bt);
                transp.database[transp.target].pip.available = true;
                transp.touch = true;
                this.Close();
            }
        }
    }
}
