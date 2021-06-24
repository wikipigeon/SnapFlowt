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
    public partial class volume : Form
    {
        public volume(view v, int op)
        {
            InitializeComponent();
            callBackTarget = v;
            this.trackBar1.Value = op;
        }
        private view callBackTarget;

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            callBackTarget.handle_volumeChange(trackBar1.Value); 
        }

        private void volume_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
