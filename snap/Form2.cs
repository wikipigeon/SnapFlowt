﻿using System;
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
        private int sourceW = 10;
        private int sourceH = 10;

        public Form2()
        {
            InitializeComponent();
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Visible = true;
        }

        public bool available = false;

        private int LTRB = 0;

        public void Assign(int L, int T, int W, int H, Bitmap b){
            sourceW = W;
            sourceH = H;
            this.Size = new Size(W, H);
            this.Left = L;
            this.Top = T;
            this.pictureBox1.Image = (Image)b;
            this.Visible = true;
            available = true;
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left){
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

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Menu){
                key_menu = true;
            }
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Menu){
                key_menu = false;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Form2_MouseDown(sender, e);
        }
    }

}
