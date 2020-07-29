using MaxTools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Net;
using IWshRuntimeLibrary;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Text.RegularExpressions;
using OpenHardwareMonitor.Hardware;
using System.Timers;
using MaxTools.bw;
using Microsoft.Win32.TaskScheduler;
using System.ServiceProcess;

namespace MaxTool
{
    public partial class Form1 : Form
    {
        Computer thisComputer;

        public Form1()
        {
            InitializeComponent();
            
            timer1.Start();
            thisComputer = new Computer() { CPUEnabled = true };
            thisComputer.Open();
            backgroundWorker1.RunWorkerAsync();

            outros frm = new outros();
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            tabControl1.TabPages[1].Controls.Add(frm);

            install frm2 = new install();
            frm2.TopLevel = false;
            frm2.Visible = true;
            frm2.FormBorderStyle = FormBorderStyle.None;
            frm2.Dock = DockStyle.Fill;
            tabControl1.TabPages[0].Controls.Add(frm2);
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

        #region MaxTools Hardware Info       
       
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {           
                PerformanceCounter myCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
                int dskPercent = Convert.ToInt32(myCounter.NextValue());
                Thread.Sleep(500);
                dskPercent = Convert.ToInt32(myCounter.NextValue());
            
        }

        private void Timer1_Tick_1(object sender, EventArgs e)
            {
            

            foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();
                    // CPU TEMP
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {

                            string cputemp = string.Format("{0}°C", sensor.Value.Value.ToString());
                            cpulbl.Text = cputemp;
                        }
                    }
                    // CPU USAGE
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Load)
                        {

                            int cpuPercent = Convert.ToInt32(sensor.Value.Value);
                            cpusage.Text = cpuPercent.ToString() + "%";
                        }
                    }
                    // RAM USAGE
                    var wmiObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

                    var memoryValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                        FreePhysicalMemory = Double.Parse(mo["FreePhysicalMemory"].ToString()),
                        TotalVisibleMemorySize = Double.Parse(mo["TotalVisibleMemorySize"].ToString())
                    }).FirstOrDefault();

                    if (memoryValues != null)
                    {
                        var percent = ((memoryValues.TotalVisibleMemorySize - memoryValues.FreePhysicalMemory) / memoryValues.TotalVisibleMemorySize) * 100;
                        int ramus = Convert.ToInt32(percent);
                        ramusage.Text = ramus.ToString() + "%";
                    }
                    // DISK USAGE
                    PerformanceCounter disk = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
                    int diskValues = 0;
                    diskValues = Convert.ToInt32(disk.NextValue());
                    dskusage.Text = diskValues.ToString() + "%";
                    
                }
            }
        }

        public static class HardwareInfo
        {
            
            public static string getProcessorInfo()
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM win32_processor");
                string info = string.Empty;
                foreach (ManagementObject wmi in searcher.Get())
                {
                    info = (string)wmi["Name"];
                    info = info.Replace("(TM)", "").Replace("(tm)", "").Replace("(R)", "").Replace("(r)", "")
                    .Replace("(C)", "").Replace("(c)", "").Replace("    ", " ").Replace("  ", " ").Replace("APU with Radeon Graphics ", "")
                    .Replace("™", "").Replace("®", "").Replace("©", "").Replace("CPU ", "").Replace("CPU", "")
                    .Replace("APU with Radeon Graphics", "").Replace("Eight-Core ", "").Replace("Six-Core ", "").Replace("Quad-Core ", "")
                    .Replace("Processor ", "").Replace("Eight-Core", "").Replace("Six-Core", "").Replace("Quad-Core", "").Replace("Genuine ", "").Replace("Genuine", "")
                    .Replace("Processor", "");
                    int index = info.IndexOf("@");
                    if (index > 0)
                        info = info.Substring(0, index);
                }
                return info;
            }

            public static string GetPhysicalMemory()
            {
                ManagementScope oMs = new ManagementScope();
                ObjectQuery oQuery = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
                ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
                ManagementObjectCollection oCollection = oSearcher.Get();

                long MemSize = 0;
                long mCap = 0;

                // In case more than one Memory sticks are installed
                foreach (ManagementObject obj in oCollection)
                {
                    mCap = Convert.ToInt64(obj["Capacity"]);
                    MemSize += mCap;
                }
                MemSize = ((MemSize / 1024) / 1024) / 1024;
                return MemSize.ToString() + " GB";
            }

            public static string GetOSInformation()
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                foreach (ManagementObject wmi in searcher.Get())
                {
                    try
                    {
                        return ((string)wmi["Caption"]).Trim() + " " + (string)wmi["OSArchitecture"];
                    }
                    catch { }
                }
                return "Linux Kernel Version 4.0";
            }

            public static string GetGPUName()
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                string gpu = string.Empty;
                foreach (ManagementObject mo in searcher.Get())
                {
                    PropertyData currentBitsPerPixel = mo.Properties["CurrentBitsPerPixel"];
                    PropertyData description = mo.Properties["Description"];
                    if (currentBitsPerPixel != null && description != null)
                    {
                        if (currentBitsPerPixel.Value != null)
                        {
                            gpu = ((string)(description.Value));
                            gpu = gpu.Replace("(TM)", "™").Replace("(tm)", "™").Replace("(R)", "®").Replace("(r)", "®").Replace("(C)", "©").Replace("(c)", "©")
                                .Replace("    ", " ").Replace("  ", " ").Replace("Express ", "").Replace("Chipset ", "").Replace("Microsoft ", "").Replace("Corporation ", "").Replace("(", "")
                                .Replace(")", "").Replace("WDDM ", "").Replace("WDM ", "").Replace("Express", "").Replace("Chipset", "").Replace("Microsoft", "").Replace("Corporation", "").Replace("(", "")
                                .Replace(")", "").Replace("WDDM", "").Replace("WDM", "").Replace("1.1", "");
                        }
                    }
                }
                return gpu;
            }

            public static string getmoboInfo()
            {
                string mobo = "mobalalalala";
                ManagementObjectSearcher objMOS = new ManagementObjectSearcher("\\root\\cimv2", "SELECT * FROM Win32_ComputerSystem");
                foreach (ManagementObject Mobject in objMOS.Get())
                {
                    try
                    {
                        mobo = "Modelo: " + (string)Mobject["Manufacturer"] + " " + (string)Mobject["Model"];
                        mobo = mobo.Replace("Technology", "").Replace("Co", "").Replace("Ltd", "").Replace(".", "").Replace(",", "").Replace("To Be Filled By O.E.M", "Desconhecido")
                            .Replace("To Be Filled By OEM", "Desconhecido").Replace("ASUSTeK COMPUTER INC", "Asus").Replace("To be filled by OEM", "Desconhecido");
                        return mobo;
                    }
                    catch (Exception)
                    {
                        return "- WMI ERROR -" + Environment.NewLine + "Por favor, consulte HWiNFO.";
                    }
                }
                return "Favor consultar HWiNFO" + Environment.NewLine + " para mais informações.";
            }


            public static string getbeiosver()
            {
                ManagementObjectSearcher objbios = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
                foreach (ManagementObject biobject in objbios.Get())
                {
                    try
                    {
                        return Environment.NewLine + "Versão da Bios: " + (string)biobject["SMBIOSBIOSVersion"];
                    }
                    catch (Exception)
                    {
                        return "- WMI ERROR -" + Environment.NewLine + "Por favor, consulte HWiNFO.";
                    }
                }
                return "Favor consultar HWiNFO" + Environment.NewLine + " para mais informações.";
            }


            public static string getcpuinfo()
            {
                long l2size = 0;
                long l2cap = 0;
                long l3cap = 0;
                long l3size = 0;
                decimal cpuspd = 0;
                decimal cpucapspd = 0;
                long core = 0;
                long thread = 0;
                string cpu = string.Empty;
                string l3 = string.Empty;
                string l2 = string.Empty;

                ManagementObjectSearcher objcpu = new ManagementObjectSearcher("\\root\\cimv2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject cpuobject in objcpu.Get())
                {
                    l2cap = Convert.ToInt64(cpuobject["L2CacheSize"]);
                    l3cap = Convert.ToInt64(cpuobject["L3CacheSize"]);
                    cpucapspd = Convert.ToDecimal(cpuobject["MaxClockSpeed"]);
                    core = Convert.ToInt64(cpuobject["NumberOfCores"]);
                    thread = Convert.ToInt64(cpuobject["NumberOfLogicalProcessors"]);                 
                    l3size += l3cap;
                    cpuspd = cpucapspd / 1000;
                    l2size = l2cap * core;
                    l3size = l3size / 1024;
                    if (l3size == 0) l3 = "Sem cache L3";
                    else l3 = l3size.ToString() + "MB";
                    if (l2size >= 1024)
                    {
                        l2size = l2size / 1024;
                        l2 = l2size.ToString() + "MB";
                    }
                    else l2 = l2size.ToString() + "KB";
                    try
                    {
                        cpu = "Fabricante: " + (string)cpuobject["Manufacturer"] + "   Velocidade: " + cpuspd.ToString() + "GHz" + Environment.NewLine + 
                            "Modelo: " + getcpumodel() + Environment.NewLine + "Núcleos: " + core.ToString() + "   Threads: " + thread.ToString() + 
                            Environment.NewLine + "Cache L2: " + l2 + "   Cache L3: " + l3;
                        cpu = cpu.Replace("Genuine ","").Replace("Genuine", "").Replace("AuthenticAMD", "AMD");
                        return cpu;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "WMI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
 
               }
                return "Favor consultar HWiNFO" + Environment.NewLine + " para mais informações.";
            }

            public static string getmeminfo()
            {
                long memdev = 0;
                try
                {
                    ManagementObjectSearcher objmem = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemoryArray");
                    foreach (ManagementObject memobject in objmem.Get())
                    {
                        memdev = Convert.ToInt64(memobject["MemoryDevices"]);

                        return "   Slots: " + memdev.ToString();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "PhysicalMemoryArray Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return "    Slots: N/I";
            }

            public static string getmeminfo2()
            {
                long memspd = 0;
                long memtype = 0;
                string memfintp = "non ddr";
                try
                {
                    ManagementObjectSearcher objmem2 = new ManagementObjectSearcher("\\root\\cimv2", "SELECT * FROM Win32_PhysicalMemory");
                    foreach (ManagementObject memobject2 in objmem2.Get())
                    {
                        memspd = Convert.ToInt64(memobject2["ConfiguredClockSpeed"]);
                        memtype = Convert.ToInt64(memobject2["MemoryType"]);
                        if (memtype == 20) memfintp = "DDR";
                        if (memtype == 21) memfintp = "DDR2";
                        if (memtype == 24) memfintp = "DDR3";
                        if (memtype >= 25 && memtype == 0) memfintp = "DDR4";

                        return "   Tipo: " + memfintp + "   Velocidade: " + memspd.ToString() + "Mhz";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "PhysicalMemory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                return "Favor consultar HWiNFO" + Environment.NewLine + " para mais informações.";
            }

            public static string getcpumodel()
            {
                string cpum = "opa";
                cpum = getProcessorInfo();
                cpum = cpum.Replace("(TM)", "").Replace("(tm)", "").Replace("(R)", "").Replace("(r)", "")
                    .Replace("(C)", "").Replace("(c)", "").Replace("    ", " ").Replace("  ", " ").Replace("Intel","").Replace("AMD", "")
                    .Replace("™","").Replace("®","").Replace("©", "").Replace("CPU","");
                int index = cpum.IndexOf("@");
                if (index > 0)
                    cpum = cpum.Substring(0, index);
                return cpum;

            }
        }        

        #endregion

        #region Abas
        private void Form1_Load(object sender, EventArgs e)
        {
            
            tabControl1.SelectedTab = installtab;
            label2.Text = HardwareInfo.getProcessorInfo() + ", " + HardwareInfo.GetPhysicalMemory() + " RAM" + ", " + HardwareInfo.GetGPUName();
            label1.Text = HardwareInfo.GetOSInformation();            
            intall.BackColor = Color.FromArgb(64, 64, 64);
            hard.BackColor = SystemColors.Desktop;
            util.BackColor = SystemColors.Desktop;           
        }

        private void intall_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = installtab;
            intall.BackColor = Color.FromArgb(64, 64, 64);            
            tools.BackColor = SystemColors.Desktop;
            hard.BackColor = SystemColors.Desktop;
            util.BackColor = SystemColors.Desktop;
            
        }

        private void tools_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = toolstab;
            tools.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));            
            intall.BackColor = SystemColors.Desktop;
            hard.BackColor = SystemColors.Desktop;
            util.BackColor = SystemColors.Desktop;
        }


        private void util_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = utiltab;
            util.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));            
            intall.BackColor = SystemColors.Desktop;
            hard.BackColor = SystemColors.Desktop;
            tools.BackColor = SystemColors.Desktop;
        }



        #endregion       

        #region tools
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

        #endregion

        #region util

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

        #endregion

        #region hard
        int a = 0;
        private void hard_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = hardtab;
            hard.BackColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));            
            intall.BackColor = SystemColors.Desktop;
            util.BackColor = SystemColors.Desktop;
            tools.BackColor = SystemColors.Desktop;

            

            if (a == 0)
            {
                label7.Text = HardwareInfo.getmoboInfo() + HardwareInfo.getbeiosver();

                try
                { label8.Text = "Total: " + HardwareInfo.GetPhysicalMemory() + HardwareInfo.getmeminfo() + HardwareInfo.getmeminfo2(); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "WMI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                label9.Text = HardwareInfo.getcpuinfo();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                string gpuname = HardwareInfo.GetGPUName();

                Console.SetOut(new TextBoxWriter(textBox4));
                
                
                Console.SetOut(new TextBoxWriter(textBox1));
                ConnectionOptions connection = new ConnectionOptions();
                connection.Impersonation = ImpersonationLevel.Impersonate;
                ManagementScope scope = new ManagementScope("\\root\\CIMV2", connection);
                scope.Connect();
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection allmem = searcher.Get();
                long memsz = 0;
                long memfsz = 0;
                foreach (ManagementObject onemem in allmem)
                {

                    memsz = Convert.ToInt64(onemem["Capacity"]);
                    memfsz = ((memsz / 1024) / 1024) / 1024;
                    Console.WriteLine("Slot: {0}   Modelo: {1} {2}   Tamanho: {3}GB",
                        onemem["DeviceLocator"], onemem["Manufacturer"], onemem["Model"], memfsz.ToString());
                }

                try
                {
                    Console.SetOut(new TextBoxWriter(textBox2));
                    if (Environment.OSVersion.Version.Major == 6)
                    {
                        if (Environment.OSVersion.Version.Minor == 1)
                        {
                            long vlmax = 0;
                            long vlcrt = 0;
                            long szmax = 0;
                            long szcrt = 0;
                            DriveInfo[] allDrives = DriveInfo.GetDrives();
                            string driveLetter = Path.GetPathRoot(Environment.CurrentDirectory);
                            foreach (DriveInfo d in allDrives)
                            {
                                if (d.IsReady == true)
                                {
                                    vlmax = Convert.ToInt64(d.TotalSize);
                                    vlcrt = Convert.ToInt64(d.TotalFreeSpace);
                                    szmax = ((vlmax / 1024) / 1024) / 1024;
                                    szcrt = ((vlcrt / 1024) / 1024) / 1024;
                                    if (d.Name != driveLetter)
                                        if (szmax != 20)
                                            Console.WriteLine("Disco: {0}" + "\t" + "Total: {1}GB" + "\t" + "Livres: {2}GB" + "\t" + "Formato: {3}" + "\t" + "Tipo: {4}", d.Name, szmax, szcrt, d.DriveFormat, d.DriveType);

                                }
                            }
                        }
                    }
                    else
                    {
                        ManagementScope scope2 = new ManagementScope(@"\\localhost\ROOT\Microsoft\Windows\Storage");
                        ObjectQuery query2 = new ObjectQuery("SELECT * FROM MSFT_PhysicalDisk");
                        ManagementObjectSearcher searcher2 = new ManagementObjectSearcher(scope2, query2);
                        ManagementObjectCollection allPDisks = searcher2.Get();
                        long dsktp = 0;
                        long drvtp = 0;
                        long dskhealth = 0;
                        string dsktype = "Outro";
                        string dskhlt = "Desconhecido";

                        foreach (ManagementObject onePDisk in allPDisks)
                        {
                            dsktp = Convert.ToInt64(onePDisk["BusType"]);
                            drvtp = Convert.ToInt64(onePDisk["MediaType"]);
                            dskhealth = Convert.ToInt64(onePDisk["HealthStatus"]);
                            if (dsktp == 3) dsktype = "IDE";
                            if (dsktp == 7) dsktype = "USB";
                            if (dsktp == 8) dsktype = "RAID";
                            if (dsktp == 12) dsktype = "SD";
                            if (dsktp == 13) dsktype = "MMC";
                            if (dsktp == 11)
                            {
                                if (drvtp == 4) dsktype = "SATA SSD";
                                else dsktype = "SATA HD";
                            }
                            if (dskhealth == 0) dskhlt = "Boa";
                            if (dskhealth == 1) dskhlt = "ALERTA!";
                            if (dskhealth == 2) dskhlt = "RUIM!!!";
                            if (onePDisk["Model"].ToString() != "My Passport 0820")
                                Console.WriteLine("{0}" + "\t" + "{1}" + "\t" + "{2}" + "\t" + "{3}", "Disco: " + onePDisk["DeviceId"], "Modelo: " + onePDisk["Model"], "Tipo: " + dsktype, "Saúde: " + dskhlt);
                        }
                        Console.WriteLine("");
                        long vlmax = 0;
                        long vlcrt = 0;
                        long szmax = 0;
                        long szcrt = 0;
                        string driveLetter = Path.GetPathRoot(Environment.CurrentDirectory);
                        DriveInfo[] allDrives = DriveInfo.GetDrives();
                        foreach (DriveInfo d in allDrives)
                        {
                            if (d.IsReady == true)
                            {
                                vlmax = Convert.ToInt64(d.TotalSize);
                                vlcrt = Convert.ToInt64(d.TotalFreeSpace);
                                szmax = ((vlmax / 1024) / 1024) / 1024;
                                szcrt = ((vlcrt / 1024) / 1024) / 1024;
                                if (d.Name != driveLetter)
                                    if (szmax != 20)
                                        Console.WriteLine("Volume: {0}" + "\t" + "Total: {1}GB" + "\t" + "Livres: {2}GB" + "\t" + "Formato: {3}", d.Name, szmax, szcrt, d.DriveFormat);

                            }

                        }
                    }
                }
                catch (Exception dskex)
                { MessageBox.Show(dskex.Message, "Erro ao obter informações", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                Console.SetOut(new TextBoxWriter(textBox3));
                ManagementObjectSearcher gpuinfo = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                string gpu = string.Empty;
                foreach (ManagementObject mo in gpuinfo.Get())
                {
                    PropertyData currentBitsPerPixel = mo.Properties["CurrentBitsPerPixel"];
                    PropertyData description = mo.Properties["Description"];
                    if (currentBitsPerPixel != null && description != null)
                    {
                        if (currentBitsPerPixel.Value != null)
                        {
                            gpu = ((string)(description.Value));
                            gpu = gpu.Replace("(TM)", "").Replace("(tm)", "").Replace("™", "").Replace("®", "").Replace("(R)", "").Replace("(r)", "").Replace("©", "").Replace("(C)", "").Replace("(c)", "")
                                .Replace("    ", " ").Replace("  ", " ").Replace("Express", "").Replace("Chipset", "").Replace("Microsoft", "").Replace("Corporation", "").Replace("(", "")
                                .Replace(")", "").Replace("WDDM", "").Replace("WDM", "");
                            Console.WriteLine("Nome: {0}   ");
                        }
                    }
                }
                a = a + 1;
            }

            
        }


















        #endregion

        
    }
}

