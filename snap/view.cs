﻿using System;
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
        private int sourceW = 10;
        private int sourceH = 10;
        private int LTRB = 0;

        public view(Form1 f1, string s, Bitmap bm, int L, int T)
        {
            InitializeComponent();
            callBackTarget = f1;
            this.thisName = s;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            activateImage(bm, L, T);
        }
        private Form1 callBackTarget;
        private string thisName;

        public void activateImage(Bitmap bm, int L, int T){
            this.Visible = true;
            this.Size = new Size(bm.Width, bm.Height);
            this.Left = L;
            this.Top = T;
            sourceW = this.Width;
            sourceH = this.Height;
            pictureBox1.Image = (Image)bm;
            this.Activate();
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
            }
        }

        private void view_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Menu){
                key_menu = false;
            } else if(e.Modifiers == Keys.None){
                key_ctrl = false;
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
                    return ;
                }

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
            } else if(e.Button == MouseButtons.Right){
                if(key_menu){
                    key_menu = false;
                    this.Size = new Size(sourceW, sourceH);
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            view_MouseDown(sender, e);
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