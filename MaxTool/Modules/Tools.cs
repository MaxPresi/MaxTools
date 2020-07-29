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
    public partial class Tools : UserControl
    {
        public Tools()
        {
            InitializeComponent();
        }
        private void Aida_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\AIDA64\AIDA64EngineerPortable.exe");
        }

        private void Hwinfo_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\HWiNFO\HWiNFO64.exe");
        }

        private void processexp_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\procexp64.exe");
        }

        private void driverpack_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\DriverPack.exe");
        }

        private void minitool_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\Minitool\PartitionWizard10Portable.exe");
        }

        private void glary_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\GlaryUtilities\GlaryUtilities5Portable.exe");
        }

        private void rdp_Click(object sender, EventArgs e)
        {
            Process.Start("mstsc");
        }

        private void tvptb_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\Teamviewer\TeamViewer.exe");
        }

        private void chromeptb_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\chrome\GoogleChromePortable.exe");
        }

        private void firefoxptb_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\Firefox\FirefoxPortable.exe");
        }

        private void norton_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\NPE.exe");
        }

        private void adw_Click(object sender, EventArgs e)
        {
            Process.Start("https://downloads.malwarebytes.com/file/adwcleaner");
        }

        private void malware_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\Malwarebytes\MalwarebytesPortable.exe");
        }

        private void gdb_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\GetDataBack\GetDataBackProPortable.exe");
        }

        private void rstudio_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\RStudio\RStudioPortable.exe");
        }

        private void fixit_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\MicrosoftProgram_Install_and_Uninstall.meta");
        }

        private void iobit_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\revo\revo.exe");
        }

        private void burnin_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\burnin\BurnInTestPortable.exe");
        }

        private void kombustor_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\FurMark\FurMark.exe");
        }

        private void produkey_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\ProduKey64.exe");
        }

        private void stella_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\stellar.exe");
        }

        private void notepad_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\Notepad++\Notepad++Portable.exe");
        }

        private void unlocker_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\Unlocker\UnlockerPortable.exe");
        }

        private void cdinfo_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\crystaldisk\DiskInfo64.exe");
        }

        private void cdmark_Click(object sender, EventArgs e)
        {
            Process.Start(@"\util\crystaldisk\DiskMark64.exe");
        }
    }
}
