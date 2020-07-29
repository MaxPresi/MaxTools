using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MaxTools.Modules
{
    public partial class Util : UserControl
    {
        public Util()
        {
            InitializeComponent();
        }

        private void msconfig_Click(object sender, EventArgs e)
        {
            Process.Start("msconfig");
        }

        private void gerenciar_Click(object sender, EventArgs e)
        {
            Process.Start("compmgmt.msc");
        }

        private void eventos_Click(object sender, EventArgs e)
        {
            Process.Start("eventvwr.msc");
        }

        private void perfmon_Click(object sender, EventArgs e)
        {
            Process.Start("perfmon.exe", "/res");
        }

        private void impressoras_Click(object sender, EventArgs e)
        {
            Process.Start("control.exe", "/name microsoft.devicesandprinters");
        }

        private void ipconf_Click(object sender, EventArgs e)
        {
            Process.Start("cmd.exe", " /C ipconfig /release&ipconfig /renew&ipconfig /flushdns");
        }

        private void gpup_Click(object sender, EventArgs e)
        {
            Process.Start("gpupdate");
        }

        private void prompt_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = @"C:\";
            p.StartInfo.UseShellExecute = false;
            p.Start();
        }

        private void contas_Click(object sender, EventArgs e)
        {
            Process.Start("netplwiz");
        }

        private void dispositivos_Click(object sender, EventArgs e)
        {
            Process.Start("devmgmt.msc");
        }

        private void gpo_Click(object sender, EventArgs e)
        {
            Process.Start("gpedit.msc");
        }

        private void services_Click(object sender, EventArgs e)
        {
            Process.Start("services.msc");
        }

        private void limpardisco_Click(object sender, EventArgs e)
        {
            Process.Start("cleanmgr.exe", "/d");
        }

        private void painel_Click(object sender, EventArgs e)
        {
            Process.Start("control.exe");
        }

        private void memtest_Click(object sender, EventArgs e)
        {
            Process.Start("control.exe", "/name Microsoft.NetworkandSharingCenter");
        }

        private void winr_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string run = null;
                run = openFileDialog1.FileName;
                Process.Start(run);
            }
        }
    }
}
