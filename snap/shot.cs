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
    public partial class shot : Form
    {
        public shot(Form1 f1, int t)
        {
            InitializeComponent();
            callBackTarget = f1;
            type = t;
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.Activate();
        }
        private Form1 callBackTarget;
        private int type;

        // capture
        private bool capBegin;
        private int cX;
        private int cY;

        private void shot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(true){
                callBackTarget.handle_newShot(null, 0, 0, type);
                this.Close();
            }
        }

        private void shot_MouseDown(object sender, MouseEventArgs e)
        {
            capBegin = true;
            this.WindowState = FormWindowState.Normal;
            cX = MousePosition.X;
            cY = MousePosition.Y;
            this.Left = cX;
            this.Top = cY;
            this.Size = new Size(0, 0);
            this.Opacity = 0.5D;
        }

        private void shot_MouseMove(object sender, MouseEventArgs e)
        {
            if(capBegin){
                this.Left = (cX < MousePosition.X)? cX: MousePosition.X;
                this.Top = (cY < MousePosition.Y)? cY: MousePosition.Y;
                int W = Math.Abs(MousePosition.X - cX);
                int H = Math.Abs(MousePosition.Y - cY);
                this.Size = new Size(W, H);
            }
        }

        private void shot_MouseUp(object sender, MouseEventArgs e)
        {
            if(capBegin){
                capBegin = false;
                this.Visible = false;
                Bitmap bm = new Bitmap(this.Size.Width, this.Size.Height);
                Graphics grap = Graphics.FromImage(bm);
                grap.CopyFromScreen(new Point(this.Left, this.Top),
                                    new Point(0, 0),
                                    new Size(this.Size.Width, this.Size.Height));
                callBackTarget.handle_newShot(bm, this.Left, this.Top, type);
            }
            this.Close();
        }
    }
}
