using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace snap
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ThreadPool.RegisterWaitForSingleObject( Program.procStarted, 
                                                    wakeupSystem,
                                                    null, -1, false);
        }

        private Dictionary<string, view> databse = new Dictionary<string, view>();

        private void wakeupSystem(object sender, bool timeout){
            this.WindowState = FormWindowState.Normal;
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void snap_Click(object sender, EventArgs e) // decide new or change
        {
            this.Visible = false;
            if(dataGridView1.SelectedRows.Count == 0){
                new shot(this, true);
            } else {
                string id = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                databse[id].Visible = false;
                new shot(this, false);
            }
        }

        private void add_Click(object sender, EventArgs e) // new item req
        {
            this.Visible = false;
            new shot(this, true);
        }

        public void handle_newShot(Bitmap bm, int L, int T, bool type){
            this.Visible = true;

            if(bm == null){
                if(dataGridView1.SelectedRows.Count > 0){
                    string tar = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    databse[tar].Visible = true;
                }
                return ;
            }
            if(type){ // new item
                string[] s = {  "Visible", 
                                string.Empty,
                                System.DateTime.Now.ToString()};
                dataGridView1.Rows.Add(s);
                dataGridView1.Rows[dataGridView1.Rows.Count-1].Selected = true;
            }
            string id = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            if(!databse.ContainsKey(id)){
                databse.Add(id, new view(this, id, bm, L, T));
            } else {
                dataGridView1.SelectedRows[0].Cells[0].Value = "Visible";
                databse[id].activateImage(bm, L, T);
            }
        }

        public void handle_varVisibility(string id, bool value){
            if(!databse.ContainsKey(id)){
                return ;
            }
            databse[id].Visible = value;

            int index;
            for(index=0; index<dataGridView1.Rows.Count; ++index){
                if(id == dataGridView1.Rows[index].Cells[2].Value.ToString()){
                    break;
                }
            }
            if(index < dataGridView1.Rows.Count){ // finally found
                dataGridView1.Rows[index].Cells[0].Value = (value)? "Visible": "inVisible";
            }
        }

        public void handle_deleteItem(string id){
            if(!databse.ContainsKey(id)){
                return ;
            }
            databse[id].Close();
            databse.Remove(id);
            int index;
            for(index=0; index<dataGridView1.Rows.Count; ++index){
                if(id == dataGridView1.Rows[index].Cells[2].Value.ToString()){
                    break;
                }
            }
            if(index < dataGridView1.Rows.Count){ // finally found
                dataGridView1.Rows.Remove(dataGridView1.Rows[index]);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0){ // item selected
                int index = dataGridView1.SelectedRows[0].Index;
                string id = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                databse[id].Close();
                databse.Remove(id);
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                if(index > 0){
                    if(index == dataGridView1.Rows.Count){
                        dataGridView1.Rows[dataGridView1.Rows.Count-1].Selected = true;
                        return ;
                    }
                }
                if(dataGridView1.Rows.Count > 0){
                    dataGridView1.Rows[index].Selected = true;
                }
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            flipflop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized){
                this.Visible = false;
            } else if(this.WindowState == FormWindowState.Normal){
                this.Visible = true;
                this.Activate();
            }
        }

        private void flipflop()
        {
            if(this.WindowState == FormWindowState.Normal){
                this.WindowState = FormWindowState.Minimized;
            } else {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left){
                if(this.WindowState == FormWindowState.Normal){
                    this.Activate();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if( dataGridView1.SelectedRows.Count > 0){
                string id = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                databse[id].Visible = (bool)(dataGridView1.SelectedRows[0].Cells[0].EditedFormattedValue);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CellContentClick(sender, e);
        }
    }
}
