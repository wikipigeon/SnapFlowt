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
            name1.Text = new string("New_1");
        }
        private bool inputing = false;

        private void wakeupSystem(object sender, bool timeout){
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }


        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {
            if(transp.touch){
                transp.touch = false;
                this.Opacity = 1.0D;
            }
            if(transp.used){
                visb.Checked = true;
                transp.used = false;
            }

        }

        private void snap_Click(object sender, EventArgs e)
        {
            string target = listBox1.SelectedItem.ToString();
            if(transp.database.ContainsKey(target)){
                transp.target = target;
                this.Opacity = 0.0D;
                scrnSnip = new Form3();
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            addElem(sender);
        }

        private void addElem(object sender){
            if(name1.Text == string.Empty){
                name1.Text = new string("New_1");
            }
            inputing = false;
            bool newer = true;
            if(!transp.database.ContainsKey(name1.Text)){
                listBox1.Items.Add(name1.Text);
                transp.database.Add(name1.Text, new data());
                newer = false;
            }
            int index = listBox1.FindString(name1.Text);
            listBox1.SetSelected(index, true);
            visb.Checked = newer;
        }

        private void visb_CheckedChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex < 0){
                return ;
            }
            string target = listBox1.SelectedItem.ToString();
            if(transp.database.ContainsKey(target)){
                if(visb.Checked && transp.database[target].pip.available){
                    transp.database[target].pip.Visible = true;
                    visb.Checked = true;
                } else {
                    transp.database[target].pip.Visible = false;
                    visb.Checked = false;
                }
                transp.database[target].visibility = visb.Checked;
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex < 0){
                return ;
            }
            string target = listBox1.SelectedItem.ToString();
            if(transp.database.ContainsKey(target)){
                // enableCtrl(false);
                if(transp.database.Count == 1){ // nothing leftover
                    listBox1.ClearSelected();
                    name1.Text = string.Empty;
                } else { // not empty yet
                    if(listBox1.SelectedIndex == 0){ // first item del
                        listBox1.SetSelected(1, true);
                    } else {
                        listBox1.SetSelected(listBox1.SelectedIndex-1, true);
                    }
                }
                listBox1.Items.Remove(target);
                transp.database[target].pip.Close();
                transp.database.Remove(target);
            }
        }

        private void trsp_Scroll(object sender, EventArgs e)
        {
            string target = listBox1.SelectedItem.ToString();
            if(transp.database.ContainsKey(target)){
                transp.database[target].transp = trsp.Value;
                transp.database[target].pip.Opacity = trsp.Value/100.0;
            }
        }

        private void name1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex < 0){
                enableCtrl(false);
                return ;
            }
            inputing = false;
            enableCtrl(true);
            data val_;
            string target = listBox1.SelectedItem.ToString();
            name1.Text = target;
            if(transp.database.TryGetValue(target, out val_)){
                trsp.Value = val_.transp;
                visb.Checked = val_.visibility;
            }
        }

        private void enableCtrl(bool TF)
        {
            if(!TF){
                visb.Checked = false;
                trsp.Value = 50;
            }
            delete.Enabled = TF;
            snap.Enabled = TF;
            visb.Enabled = TF;
            trsp.Enabled = TF;
        }

        private void name1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r'){
                inputing = false;
                e.Handled = true;
                addElem(sender);
            }
        }

        private void name1_MouseDown(object sender, MouseEventArgs e)
        {
            if(!inputing){
                inputing = true;
                name1.Text = string.Empty;
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
                this.Hide();
            }
        }

        private void flipflop()
        {
            if(this.WindowState == FormWindowState.Normal){
                this.WindowState = FormWindowState.Minimized;
            } else {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            // System.Environment.Exit(0);
        }

    }
}
