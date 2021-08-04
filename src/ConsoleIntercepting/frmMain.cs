using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConsoleIntercepting
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        Runner runner = null;

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtPath.Text = Application.StartupPath + "\\testapp.exe";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            DialogResult Res = dlgOpen.ShowDialog();

            if (Res == DialogResult.OK)
            {
                txtPath.Text = dlgOpen.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            runner = new Runner(txtPath.Text);
            runner.LogSend += new LogMessage(runner_LogSend);            
            runner.Start();
        }

        void runner_LogSend(string Data)
        {
            Invoke((MethodInvoker)delegate
            {
                lvConsole.Items.Add(Data);
                lvConsole.EnsureVisible(lvConsole.Items.Count - 1);
            });
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (runner != null)
            {
                runner.Stop();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (runner != null)
            {
                runner.Stop();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lvConsole.Items.Clear();
        }
    }
}
