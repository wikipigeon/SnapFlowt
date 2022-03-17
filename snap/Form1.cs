using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
                new shot(this, 1);
            } else {
                string id = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                databse[id].Visible = false;
                new shot(this, 0);
            }
        }

        private void add_Click(object sender, EventArgs e) // new item req
        {
            silentScreenShot();
        }

        public void handle_newShot(Bitmap bm, int L, int T, int type){
            if(type != 3){
                this.Visible = true;
            }
            if(bm == null){
                if((dataGridView1.SelectedRows.Count > 0) && (type == 2)){
                    string tar = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    databse[tar].Visible = true;
                }
                return ;
            }
            if(type > 0){ // new item
                string[] s = {  "Visible", 
                                string.Empty, 
                                System.DateTime.Now.ToString()};
                dataGridView1.Rows.Add(s);
                dataGridView1.Rows[dataGridView1.Rows.Count-1].Selected = true;
            }
            string id = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            if(!databse.ContainsKey(id)){
                if((type & 0x1) == 1){
                    databse.Add(id, new view(this, id, bm, L, T));
                } else if(type == 2){
                    databse.Add(id, new view(this, id, bm, -1, -1));
                }
            } else {
                dataGridView1.SelectedRows[0].Cells[0].Value = "Visible";
                databse[id].reloadImage(bm, L, T);
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
            int index = dataGridView1.SelectedRows.Count;
            if(index > 0){
                string id = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                databse[id].Visible = (bool)(dataGridView1.SelectedRows[0].Cells[0].EditedFormattedValue);
                dataGridView1.SelectedRows[0].Cells[1].Selected = true;
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CellContentClick(sender, e);
        }

        private void open_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK){
                using(FileStream file = File.Open(openFileDialog1.FileName, FileMode.Open)){
                    Bitmap bm = new Bitmap(file);
                    handle_newShot(bm, -1, -1, 2);
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            silentScreenShot();
        }

        private void silentScreenShot(){
            if(this.Visible){
                this.Visible = false;
                new shot(this, 1);
            } else {
                new shot(this, 3);
            }
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
                //using(FileStream file = File.Open(openFileDialog1.FileName, FileMode.Open))
                //{
                //    Bitmap bm = new Bitmap(file);
                //    handle_newShot(bm, -1, -1, 2);
                //}
            string localFilePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            using(FileStream file = File.Open(localFilePath, FileMode.Open)){
                Bitmap bm = new Bitmap(file);
                handle_newShot(bm, -1, -1, 2);
            }

        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            string localFilePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            string extension = System.IO.Path.GetExtension(localFilePath);
            if(extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp"){
                e.Effect = DragDropEffects.Copy;
            } else {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
