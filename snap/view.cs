using System;
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
    public partial class view : Form
    {
        // move control
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        const int SYSCMD = 0x0112;
        const int SC_MOVE = 0xF010;
        const int SC_SIZE = 0xF000;
        const int HTCAPTION = 0x0002;
        const int FLEXBILITY = 15;
        private bool key_menu = false;
        private bool key_ctrl = false;
        private bool key_shft = false;
        private bool scale_resize = false;
        private int sourceW = 30;
        private int sourceH = 30;
        private int freeX;
        private int freeY;
        private double actualW;
        private double actualH;
        private double scale;
        private double incr;
        private int LTRB = 0;

        public view(Form1 f1, string s, Bitmap bm, int L, int T)
        {
            InitializeComponent();
            this.Size = new Size(bm.Width, bm.Height);
            if(L>0){
                this.Left = L;
                this.Top = T;
            } else {
                this.StartPosition = FormStartPosition.WindowsDefaultLocation;
            }
            sourceW = this.Width;
            sourceH = this.Height;
            scale = Math.Sqrt(sourceW * sourceW + sourceH * sourceH);
            pictureBox1.Image = (Image)bm;
            callBackTarget = f1;
            this.thisName = s;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }
        private Form1 callBackTarget;
        private string thisName;

        public void reloadImage(Bitmap bm, int L, int T){
            this.Size = new Size(bm.Width, bm.Height);
            sourceW = this.Width;
            sourceH = this.Height;
            scale = Math.Sqrt(sourceW * sourceW + sourceH * sourceH);
            this.Left = L;
            this.Top = T;
            pictureBox1.Image = (Image)bm;
            this.Visible = true;
        }

        public void handle_volumeChange(int val){
            this.Opacity = val / 100.0;
        }

        private void view_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Menu){
                key_menu = true;
            } else if(e.Modifiers == Keys.Control){
                key_ctrl = true;
            } else if(Control.ModifierKeys == Keys.Shift){
                key_shft = true;
            }
        }

        private void view_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Menu){
                key_menu = false;
            } else if(e.Modifiers == Keys.None){
                key_ctrl = false;
                key_shft = false;
            }
        }

        private void view_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left){
                if(key_ctrl){
                    key_ctrl = false;
                    volume v0 = new volume(this, (int)(this.Opacity*100));
                    v0.Show();
                    v0.Left = MousePosition.X;
                    v0.Top = MousePosition.Y;
                    v0.Activate();
                } else if(key_menu){
                    key_menu = false;
                    this.Size = new Size(sourceW, sourceH);
                } else if(key_shft){
                    // move to mouse move handle
                    scale_resize = true;
                    freeX = MousePosition.X;
                    freeY = MousePosition.Y;
                    actualW = this.Width;
                    actualH = this.Height;

                } else {
                    LTRB = 0;
                    // near left side
                    LTRB |= (Math.Abs(MousePosition.X-this.Left) <= FLEXBILITY)? 0b1000: 0b0000;
                    // near top side
                    LTRB |= (Math.Abs(MousePosition.Y-this.Top) <= FLEXBILITY)? 0b0100: 0b0000;
                    // near right side
                    LTRB |= (Math.Abs(this.Right-MousePosition.X) <= FLEXBILITY)? 0b0010: 0b0000;
                    // near bottom side
                    LTRB |= (Math.Abs(this.Bottom-MousePosition.Y) <= FLEXBILITY)? 0b0001: 0b0000;

                    ReleaseCapture();
                    if ((LTRB & 0b0011) == 0b0011){ // RD
                        SendMessage(this.Handle, SYSCMD, SC_SIZE + 0x8, 0);
                    } else if ((LTRB & 0b0110) == 0b0110){ // RU
                        SendMessage(this.Handle, SYSCMD, SC_SIZE + 0x5, 0);
                    } else if ((LTRB & 0b1100) == 0b1100){ // LU
                        SendMessage(this.Handle, SYSCMD, SC_SIZE + 0x4, 0);
                    } else if ((LTRB & 0b1001) == 0b1001){ // LD
                        SendMessage(this.Handle, SYSCMD, SC_SIZE + 0x7, 0);
                    } else if ((LTRB & 0b0010) == 0b0010){ // R
                        SendMessage(this.Handle, SYSCMD, SC_SIZE + 0x2, 0);
                    } else if ((LTRB & 0b0100) == 0b0100){ // U
                        SendMessage(this.Handle, SYSCMD, SC_SIZE + 0x3, 0);
                    } else if ((LTRB & 0b1000) == 0b1000){ // L
                        SendMessage(this.Handle, SYSCMD, SC_SIZE + 0x1, 0);
                    } else if ((LTRB & 0b0001) == 0b0001){ // D
                        SendMessage(this.Handle, SYSCMD, SC_SIZE + 0x6, 0);
                    } else { // middle move
                        SendMessage(this.Handle, SYSCMD, SC_MOVE + HTCAPTION, 0);
                    }
                }
            } else if(e.Button == MouseButtons.Right){

            }
        }

        private void view_MouseMove(object sender, MouseEventArgs e)
        {
            if(scale_resize){
                double diffX = MousePosition.X - freeX;
                double diffY = MousePosition.Y - freeY;
                incr = Math.Sqrt(diffX * diffX + diffY * diffY);
                incr = (scale + incr) / scale;
                if(diffX > 0){
                    diffX = actualW * incr;
                    diffY = actualH * incr;
                } else {
                    diffX = actualW / incr;
                    diffY = actualH / incr;
                }
                this.Size = new Size((int)diffX, (int)diffY);

            }
        }

        private void view_MouseUp(object sender, MouseEventArgs e)
        {
            if(scale_resize){
                scale_resize = false;
                actualW = this.Width;
                actualH = this.Height;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            view_MouseDown(sender, e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            view_MouseMove(sender, e);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            view_MouseUp(sender, e);
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callBackTarget.handle_varVisibility(thisName, false);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callBackTarget.handle_deleteItem(thisName);
        }
    }
}
