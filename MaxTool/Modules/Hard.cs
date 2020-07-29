using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static MaxTool.Form1;
using System.Management;
using System.IO;
using MaxTool;

namespace MaxTools.Modules
{
    public partial class Hard : UserControl
    {
        public Hard()
        {
            InitializeComponent();
        }
        int a = 0;
        private void UserControl1_Load(object sender, EventArgs e)
        {
            
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
                        if (dsktp == 7) dsktype = "USB";
                        if (dsktp == 8) dsktype = "RAID";
                        if (dsktp == 12) dsktype = "SD";
                        if (dsktp == 13) dsktype = "MMC";
                        if (dsktp == 11)
                        {
                            if (drvtp == 4) dsktype = "SSD SATA";
                            else dsktype = "SATA HD";
                        }
                        if (dsktp == 17) dsktype = "SSD NVMe";
                        if (dskhealth == 0) dskhlt = "Boa";
                        if (dskhealth == 1) dskhlt = "ALERTA!";
                        if (dskhealth == 2) dskhlt = "RUIM!!!";
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
    }
}
