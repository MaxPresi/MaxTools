using IWshRuntimeLibrary;
using Microsoft.Win32.TaskScheduler;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using File = System.IO.File;

namespace MaxTools.bw
{
    public partial class install : Form
    {


        public install()
        {
            InitializeComponent();
            cancelar.Enabled = false;
            ok.Enabled = true;
        }

        public void zerar(Control.ControlCollection controls)
        {
            foreach (CheckBox c in Controls)
            {
                c.Checked = false;
            }
        }


        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }

        public class TextBoxWriter : TextWriter
        {
            TextBox _output = null;

            public TextBoxWriter(TextBox output)
            {
                _output = output;
            }

            public override void Write(char value)
            {
                base.Write(value);
                _output.AppendText(value.ToString());
            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            #region pb2

            int pb2 = (iebox.Checked ? 1 : 0) + (zipbox.Checked ? 1 : 0) + (driverbox.Checked ? 1 : 0) +
                (chromebox.Checked ? 1 : 0) + (firebox.Checked ? 1 : 0) + (java7box.Checked ? 1 : 0) +
                (wsusbox.Checked ? 1 : 0) + (sumabox.Checked ? 1 : 0) + (adobox.Checked ? 1 : 0) +
                (java8box.Checked ? 1 : 0) + (pandabox.Checked ? 1 : 0) + (o16box.Checked ? 1 : 0) +
                (tasktempbox.Checked ? 1 : 0) + (taskfullbox.Checked ? 1 : 0) + (bitbox.Checked ? 1 : 0) +
                (tvbox.Checked ? 1 : 0) + (cleartempbox.Checked ? 1 : 0) + (avirabox.Checked ? 1 : 0) +
                (windowsbox.Checked ? 1 : 0) + (officebox.Checked ? 1 : 0) + (ativbox.Checked ? 1 : 0) +
                (win10box.Checked ? 1 : 0) + (photobox.Checked ? 1 : 0) + (prembox.Checked ? 1 : 0) +
                (animebox.Checked ? 1 : 0) + (illubox.Checked ? 1 : 0) + (afterbox.Checked ? 1 : 0) +
                (musebox.Checked ? 1 : 0) + (indebox.Checked ? 1 : 0) + (lightbox.Checked ? 1 : 0) +
                (audbox.Checked ? 1 : 0) + (dreambox.Checked ? 1 : 0) + (mediabox.Checked ? 1 : 0) +
                (bridgebox.Checked ? 1 : 0) + (corelbox.Checked ? 1 : 0) + (videobox.Checked ? 1 : 0) +
                (lumibox.Checked ? 1 : 0) + (sonybox.Checked ? 1 : 0) + (worksbox.Checked ? 1 : 0) +
                (visiobox.Checked ? 1 : 0) + (revibox.Checked ? 1 : 0) + (projectbox.Checked ? 1 : 0) +
                (autobox.Checked ? 1 : 0) + (vbox.Checked ? 1 : 0) + (draftbox.Checked ? 1 : 0) +
                (sketbox.Checked ? 1 : 0);
            progressBar1.Maximum = pb2;
            progressBar1.Value = 0;

            #endregion

            cancelar.Enabled = true;
            ok.Enabled = false;

            #region installer

            if (driverbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Executando DriverPack";
                try
                {
                    Process.Start(@"\Util\Driverpack\DriverPack.exe");
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (sumabox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Sumatra PDF";
                try
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Process.Start(@"\Apps\Sumatra64.exe", " /S");
                    }
                    else
                    {
                        Process.Start(@"\Apps\Sumatra32.exe", " /S");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (zipbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando 7zip";
                try
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Process.Start(@"\Apps\7z64.exe", @" /S /D=C:\rogram Files\7-Zip").WaitForExit();
                    }
                    else
                    {
                        Process.Start(@"\Apps\7z32.exe", @" /S /D=C:\rogram Files\7-Zip").WaitForExit();
                    }
                    Process.Start(@"C:\Program Files\7-Zip\7zFM.exe");
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe ou se foi instalado corretamente.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (chromebox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Google Chrome";
                try
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Process.Start(@"\Apps\Chrome64.exe", " /silent /install");
                    }
                    else
                    {
                        Process.Start(@"\Apps\Chrome32.exe", " /silent /install");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se está conectado à internet ou se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (firebox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando FireFox";
                try
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Process.Start(@"\Apps\Firefox64.exe", " -ms");
                    }
                    else
                    {
                        Process.Start(@"\Apps\Firefox32.exe", " -ms");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se está conectado à internet ou se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (adobox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe DC";
                try { Process.Start(@"\Apps\adobedc.exe", " /qn").WaitForExit(); }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se não existe outra instalação em andamento ou se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (java7box.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Java Compatível...";
                try
                {                   
                    Process.Start(@"\Apps\java7.exe", " /s WEB_JAVA_SECURITY_LEVEL=M SPONSORS=0").WaitForExit();  
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se está conectado à internet ou se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (iebox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Atualizando para Internet Explorer 11...";
                try
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Process.Start(@"\Apps\ie1164.exe");
                    }
                    else
                    {
                        Process.Start(@"\Apps\ie1132.exe");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se não tem outra instalação em andamento ou se o arquivo ainda existe.", "Erro ao atualizar o Internet Explorer.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (pandabox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Panda Free Antivírus...";
                try { Process.Start(@"\Apps\panda.exe"); }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (bitbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Bit Defender Antivírus...";
                try { Process.Start(@"\Apps\bit.exe"); }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (avirabox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Avira Home Antivirus...";
                try { Process.Start(@"\Apps\avira.exe", " /S /silent"); }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se está conectado à internet ou se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (tvbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Intalando o Teamviwer...";
                try { Process.Start(@"\Apps\TeamViewer.exe", "/S /norestart"); }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se está conectado à internet ou se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (o16box.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Office 2016";
                try
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Process.Start(@"\Apps\Office\2016\x64\setup.exe").WaitForExit();
                    }
                    else
                    {
                        Process.Start(@"\Apps\Office\2016\x86\setup.exe").WaitForExit();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se existe outra instalação em andamento ou se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (java8box.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Java 8";
                try { Process.Start(@"\Apps\java8.exe", " /S").WaitForExit(); }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se está conectado à internet ou se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            #endregion

            #region design
            if (corelbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando CorelDraw Suite X8";
                try
                {
                    Process.Start(@"\Apps\Design\Corel\Keygen.exe");
                    Process.Start(@"\Apps\Design\Corel\CorelLauncher.exe").WaitForExit();
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe e/ou desinstale o C++ 2015.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (photobox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe Photoshop CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\Photoshop\Setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (illubox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe Illustrator CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\Illustrator\Setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (indebox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe InDesign CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\InDesign\Setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (dreambox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe Dreamweaver CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\Dreamweaver\Setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (prembox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe Premiere Pro CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\Premiere\Setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (audbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe Audition CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\Audition\Setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (lightbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe Lightroom CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\Lightroom\Set-up.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (animebox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe Animate CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\Animate\Setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (afterbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe After Effects CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\After\Setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (musebox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe Muse CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\Muse\SetUp.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (autobox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando AutoCAD 2018";
                try
                {
                    Process.Start(@"\Apps\Design\CAD\Autocad\install.txt");
                    Process.Start(@"\Apps\Design\CAD\Autocad\Setup.exe").WaitForExit();
                    Process.Start(@"\Apps\Design\CAD\Autocad\update.exe").WaitForExit();
                    Process.Start(@"\Apps\Design\CAD\Autocad\install.exe");
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (sketbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Sketchup 2017";
                Process.Start(@"\Apps\Design\CAD\Sketchup\SketchUp2017-x64.msi").WaitForExit();
                label5.Text = "Crackeando Sketchup 2017";
                try
                {
                    string driveLetter = Path.GetPathRoot(Environment.CurrentDirectory);
                    string sourceFile3 = driveLetter + @"\Apps\Design\CAD\Sketchup\SketchUp.exe";
                    File.Copy(sourceFile3, @"C:\Program Files\SketchUp\SketchUp 2017\SketchUp.exe", true);
                    string sourceFile4 = driveLetter + @"\Apps\Design\CAD\Sketchup\LayOut.exe";
                    File.Copy(sourceFile4, @"C:\Program Files\SketchUp\SketchUp 2017\LayOut\LayOut.exe", true);
                    string sourceFile5 = driveLetter + @"\Apps\Design\CAD\Sketchup\Style Builder.exe";
                    File.Copy(sourceFile5, @"C:\Program Files\SketchUp 2017\Style Builder\Style Builder.exe", true);

                    MessageBox.Show("Sketchup crackeado com êxito.", "Não use drogas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Sketchup não foi crackeado e eu não sei porque T_T.", "Desculpa ;-;", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (projectbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Microsoft Project 2016";
                try { Process.Start(@"\Apps\Design\CAD\Project\Setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }
            if (sonybox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Sony Vegas Pro 14";
                Process.Start(@"\Apps\Design\SonyVegas\setup.exe").WaitForExit();
                label5.Text = "Crackeando Sony Vegas Pro 14";
                try
                {
                    string driveLetter = Path.GetPathRoot(Environment.CurrentDirectory);
                    string sourceFile3 = driveLetter + @"\Apps\Design\SonyVegas\patch.exe";
                    File.Copy(sourceFile3, @"C:\Program Files\VEGAS\VEGAS Pro 14.0\patch.exe", true);
                    Process.Start(@"C:\Program Files\VEGAS\VEGAS Pro 14.0\patch").WaitForExit();
                    MessageBox.Show("Sony Vegas patcheado com êxito.", "Patch of Exile.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Sony Vegas não foi pactheado por motivos desconhecidos D:", "Tem cupa eu?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (videobox.Checked)
            {
                progressBar1.PerformStep();
                MessageBox.Show("Quem usa esse programa hoje em dia???" + Environment.NewLine + "Espere a instalação terminar e marque outro programa" + Environment.NewLine +
                "como Premiere, After Effects, Sony Vegas ou Lighworks!", "Use um programa de verdade!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (draftbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando DraftSight 2016";
                Process.Start(@"\Apps\Design\CAD\DraftSight64.exe").WaitForExit();
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (vbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando V-Ray";
                Process.Start(@"\Apps\Design\CAD\Vray\vray.exe").WaitForExit();
                label5.Text = "Crackeando o V-Ray";
                try
                {
                    string driveLetter = Path.GetPathRoot(Environment.CurrentDirectory);
                    string sourceFile2 = driveLetter + @"\Apps\Design\CAD\Vray\vray.dll";
                    File.Copy(sourceFile2, @"C:\Program Files\Chaos Group\V-Ray\V-Ray 3.4 for SketchUp\extension\vrayappsdk\bin\vray.dll", true);
                    MessageBox.Show("V-Ray crackeado com sucesso.", "Prefiro Raio X", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("V-Ray não foi crackeado mas você já sabia disso!", "O PC tá usando protetor solar D:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (visiobox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Microsoft Visio 2016";
                try { Process.Start(@"\Apps\Design\CAD\Visio\Setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (worksbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando LightWorks";
                try { Process.Start(@"\Apps\Design\Lightworks_setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (lumibox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Lumion";
                Process.Start(@"\Apps\Design\CAD\Lumion\Lumion_6_0.exe").WaitForExit();
                label5.Text = "Crackeando Lumion";
                try
                {
                    string driveLetter = Path.GetPathRoot(Environment.CurrentDirectory);
                    string sourceFile3 = driveLetter + @"\Apps\Design\CAD\Lumion\crack\6E6FB247-4627-4FBE-8973-48344F23881E.dll";
                    File.Copy(sourceFile3, @"C:\Program Files\Lumion 6.0\Channels\6E6FB247-4627-4FBE-8973-48344F23881E.dll", true);
                    string sourceFile4 = driveLetter + @"\Apps\Design\CAD\Lumion\crack\A550BB21-BE5C-4675-B53E-3FA246F76538.dll";
                    File.Copy(sourceFile4, @"C:\Program Files\Lumion 6.0\Channels\A550BB21-BE5C-4675-B53E-3FA246F76538.dll", true);
                    string sourceFile5 = driveLetter + @"\Apps\Design\CAD\Lumion\crack\B028B538-D554-434B-88CE-AA79A717C396.dll";
                    File.Copy(sourceFile5, @"C:\Program Files\Lumion 6.0\Channels\B028B538-D554-434B-88CE-AA79A717C396.dll", true);
                    MessageBox.Show("Lumion crackeado com sucesso.", "Que aja a luz!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Lumion não foi crackeado mas ninguém liga...", "O iluminismo já acabou mesmo...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (revibox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Revit";
                try
                {
                    Process.Start(@"\Apps\Design\CAD\Revit\install.txt");
                    Process.Start(@"\Apps\Design\CAD\Revit\setup.exe").WaitForExit();
                    Process.Start(@"\Apps\Design\CAD\Revit\install.exe");
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (mediabox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe Media Encoder CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\MediaEncoder\Setup.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (bridgebox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Instalando Adobe Bridge CC 2017";
                try { Process.Start(@"\Apps\Design\Adobe\Bridge\Set-up.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            if (ativbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Ativando Programa(s) Adobe";
                try { Process.Start(@"\Apps\Design\Adobe\adobecc.exe").WaitForExit(); }

                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
                return;
            }

            #endregion

            #region repair

            if (cleartempbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Limpando arquivos temporários...";
                try
                {
                    Process regeditProcess = Process.Start("regedit.exe", "/s \\Util\\sageset11.reg");
                    regeditProcess.WaitForExit();
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C cleanmgr /sagerun:11";
                    process.StartInfo = startInfo;
                    process.Start();
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao executar comando.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (tasktempbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Criando tarefa de limpeza de disco...";
                try
                {
                    Process regeditProcess = Process.Start("regedit.exe", @"/s \Util\sageset11.reg");
                    regeditProcess.WaitForExit();

                    using (TaskService ts = new TaskService())
                    {
                        // Create a new task definition and assign properties
                        TaskDefinition td = ts.NewTask();
                        td.RegistrationInfo.Description = "Limpeza de Disco";

                        // Create a trigger that will fire the task at this time every other day
                        td.Triggers.Add(new MonthlyTrigger());

                        // Create an action that will launch Notepad whenever the trigger fires
                        td.Actions.Add(new ExecAction("cmd.exe", "/C cleanmgr /sagerun:11", null));

                        // Register the task in the root folder
                        ts.RootFolder.RegisterTaskDefinition(@"clrdsk", td);                        
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao criar tarefa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (taskfullbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Criando tarefa de notificação de espaço...";
                try
                {
                    Process regeditProcess = Process.Start("regedit.exe", @"/s \Util\taskfull.reg");
                    regeditProcess.WaitForExit();                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao executar comando.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (lowendbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Desativando serviços desnecessários...";
                try
                {
                    Process regeditProcess = Process.Start("regedit.exe", @"/s \Util\lowend.reg");
                    regeditProcess.WaitForExit();

                    ServiceController service = new ServiceController("SysMain");
                    service.Stop();
                    var timeout = new TimeSpan(0, 0, 5); // 5seconds
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    ServiceController service2 = new ServiceController("wuauserv");
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    ServiceController service3 = new ServiceController("WSearch");
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    ServiceController service4 = new ServiceController("CscService");
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    ServiceController service5 = new ServiceController("WinDefend");
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o arquivo ainda existe.", "Erro ao executar comando.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            #endregion

            #region ativar

            if (windowsbox.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Ativando Windows...";
                try
                {                    
                    Process.Start(@"\Apps\Ativadores\Windows7.exe").WaitForExit();
                }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o antivírus está desativado e se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            if (officebox.Checked || win10box.Checked)
            {
                progressBar1.PerformStep();
                label5.Text = "Ativando KMS...";
                try { Process.Start(@"Apps\Ativadores\KMSpico_setup.exe").WaitForExit(); }
                catch (Exception)
                {
                    MessageBox.Show("Verifique se o antivirus está desativado e se o arquivo ainda existe.", "Erro ao instalar programa.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                ok.Enabled = true;
                return;
            }

            #endregion

            progressBar1.PerformStep();
            label5.Text = "Instalação concluída com sucesso!";
            cancelar.Enabled = false;
            ok.Enabled = true;
            zerar(Controls);
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label5.Text = "Instalação cancelada...";
                progressBar1.Value = 0;
                cancelar.Enabled = false;
                ok.Enabled = true;
            }
            else
            {
                progressBar1.PerformStep();
                label5.Text = "Instalação concluída com sucesso!";
                cancelar.Enabled = false;
                ok.Enabled = true;
                zerar(Controls);
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (zipbox.Checked || driverbox.Checked || chromebox.Checked || firebox.Checked || pandabox.Checked || wsusbox.Checked || sumabox.Checked ||
                adobox.Checked || java8box.Checked || bitbox.Checked || o16box.Checked || java7box.Checked || win10box.Checked ||
                tvbox.Checked || avirabox.Checked || windowsbox.Checked || officebox.Checked || taskfullbox.Checked || avirabox.Checked || pandabox.Checked ||
                bitbox.Checked || tasktempbox.Checked || cleartempbox.Checked || lowendbox.Checked || ativbox.Checked || iebox.Checked || photobox.Checked || 
                prembox.Checked || animebox.Checked || illubox.Checked || afterbox.Checked || musebox.Checked || indebox.Checked || lightbox.Checked || 
                audbox.Checked || dreambox.Checked || mediabox.Checked || bridgebox.Checked || corelbox.Checked || videobox.Checked || lumibox.Checked || 
                visiobox.Checked || revibox.Checked || projectbox.Checked || autobox.Checked || vbox.Checked || draftbox.Checked || sketbox.Checked || 
                win10box.Checked)
            {
                backgroundWorker2.RunWorkerAsync();
                cancelar.Enabled = true;
                ok.Enabled = false;
            }
            else MessageBox.Show("Por favor, selecione pelo menos uma das opções acima para iniciar a instalação", "Nenhuma Opção Foi Escolhida",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cancelar_Click(object sender, EventArgs e)
        {
            backgroundWorker2.CancelAsync();
            cancelar.Enabled = false;
            ok.Enabled = true;
            label5.Text = "Instalação cancelada...";
            progressBar1.Value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zerar(Controls);
        }
        
    }
}
