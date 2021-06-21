using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snap
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private bool beginMove = false;
        private bool beginScale = false;
        private bool menu = false; // reset
        private bool ctrl = false; // free scale
        private int sourceW = 0;
        private int sourceL = 0;
        private double actualW = 0;
        private double actualL = 0;
        private double freeX = 0;
        private double freeY = 0;
        private double scaleX = 0;
        private double scaleY = 0;
        private double sc = 0;
        private double sc_1 = 0;
        public bool available = false;


        public void Assign(int L, int T, int W, int H, ref Bitmap b){
            sourceW = W;
            sourceL = H;
            actualW = sourceW;
            actualL = sourceL;
            this.Size = new Size(W, H);
            this.Left = L;
            this.Top = T;
            pictureBox1.BackgroundImage = (Image)b;
            this.Visible = true;
            available = true;
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left){
                beginMove = true;
                freeX = MousePosition.X;
                freeY = MousePosition.Y;
            } else if(  e.Button == MouseButtons.Right && menu){
                this.Size = new Size(sourceW, sourceL);
                actualW = sourceW;
                actualL = sourceL;
                menu = false;
            } else if(e.Button == MouseButtons.Right){
                beginScale = true;
                freeX = MousePosition.X;
                freeY = MousePosition.Y;
                if(!ctrl){
                    sc = Math.Sqrt(actualW * actualW + actualL * actualL);
                }

            }
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if(beginMove){
                this.Left += (int)(MousePosition.X - freeX);
                this.Top += (int)(MousePosition.Y - freeY);
                freeX = MousePosition.X;
                freeY = MousePosition.Y;
            } else if(beginScale){
                scaleX = MousePosition.X - freeX;
                scaleY = MousePosition.Y - freeY;

                if(ctrl){
                    actualW += scaleX;
                    actualL += scaleY;
                } else {
                    sc_1 = Math.Sqrt(scaleX * scaleX + scaleY * scaleY);
                    sc_1 = sc + sc_1 / 2;
                    sc_1 /= sc;
                    if(scaleX > 0){
                        actualW *= sc_1;
                        actualL *= sc_1;
                    } else {
                        actualW /= sc_1;
                        actualL /= sc_1;
                    }
                }
                this.Size = new Size((int)actualW, (int)actualL);
                freeX = MousePosition.X;
                freeY = MousePosition.Y;
            }
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            if(beginMove){
                beginMove = false;
            } else if(beginScale){
                beginScale = false;
            }

        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Menu){
                menu = true;
            } else if(e.KeyCode == Keys.ControlKey){
                ctrl = true;
            }
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Menu){
                menu = false;
            } else if(e.KeyCode == Keys.ControlKey){
                ctrl = false;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Form2_MouseDown(sender, e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Form2_MouseMove(sender, e);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Form2_MouseUp(sender, e);
        }
    }

}
