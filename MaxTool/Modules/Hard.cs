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
using OpenHardwareMonitor.Hardware;
using MaxTool;

namespace MaxTools.Modules
{
    public partial class Hard : UserControl
    {
        Computer thisComputer;
        public Hard()
        {
            InitializeComponent();
            thisComputer = new Computer() { CPUEnabled = true };
            thisComputer.Open();
        }
        int a = 0;
        private void UserControl1_Load(object sender, EventArgs e)
        {
            
            if (a == 0)
            {
                #region MOBO e CPU
                MBlbl.Text = HardwareInfo.getmoboInfo() + HardwareInfo.getbeiosver();                

                CPUlbl.Text = HardwareInfo.getcpuinfo();

                ramBox.Text = "";
                dskBox.Text = "";
                moboBox.Text = "";
                gpuBox.Text = "";
                cpuBox.Text = "";

                #endregion

                #region RAM

                try
                { RAMlbl.Text = "Total: " + HardwareInfo.GetPhysicalMemory() + HardwareInfo.getmeminfo() + HardwareInfo.getmeminfo2(); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "WMI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Console.SetOut(new TextBoxWriter(ramBox));
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

                #endregion

                #region DSK

                try
                {
                    Console.SetOut(new TextBoxWriter(dskBox));

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
                        if (dsktp == 8) dsktype = "SCSI";
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

                #endregion

                #region GPU

                Console.SetOut(new TextBoxWriter(gpuBox));
                ManagementObjectSearcher gpuinfo = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                string gpu = string.Empty;
                int vram = 0;
                foreach (ManagementObject mo in gpuinfo.Get())
                {
                    PropertyData description = mo.Properties["Description"];
                    if (description != null)
                    {      
                        gpu = ((string)(description.Value));
                        gpu = gpu.Replace("(TM)", "").Replace("(tm)", "").Replace("™", "").Replace("®", "").Replace("(R)", "").Replace("(r)", "").Replace("©", "").Replace("(C)", "").Replace("(c)", "")
                            .Replace("    ", " ").Replace("  ", " ").Replace("Express", "").Replace("Chipset", "").Replace("Microsoft", "").Replace("Corporation", "").Replace("(", "")
                            .Replace(")", "").Replace("WDDM", "").Replace("WDM", "");
                        
                        
                    }
                }
                ManagementObjectSearcher vramS = new ManagementObjectSearcher("select AdapterRAM from Win32_VideoController");

                foreach (ManagementObject rg in vramS.Get())
                {
                    var gpram = rg.Properties["AdapterRAM"].Value as UInt32?;

                    if (gpram.HasValue)
                    {
                        vram = ((int)gpram / 1048576);
                    }
                }
                string bus = "";
                string clVR = "";
                string clBG = "";
                string clAG = "";
                string tGPU = "";
                string tVR = "";
                string RPM = "";

                if (gpu.Contains("AMD") == true)
                {
                    foreach (var hardwareItem in thisComputer.Hardware)
                    {
                        if (hardwareItem.HardwareType == HardwareType.GpuAti)
                        {
                            hardwareItem.Update();
                            foreach (IHardware subHardware in hardwareItem.SubHardware)
                                subHardware.Update();
                            foreach (var sensor in hardwareItem.Sensors)
                            {
                                if (sensor.SensorType == SensorType.Temperature)
                                {

                                    tGPU = sensor.Value.Value.ToString();
                                }

                                if (sensor.SensorType == SensorType.Clock)
                                {

                                    clAG = sensor.Value.Value.ToString();
                                }
                            }
                        }
                    }
                    Console.WriteLine("Nome: {0}" + "\t" + "BUS: {1}bits" + "\n" + "Capacidade VRAM: {2}GB" + "\t" + "Clock VRAM: {3}Mhz" + "\n" +
                    "Clock Base GPU: {4}Mhz" + "\t" + "Clock Atual GPU: {5}Mhz" + "\n" + "Temp. GPU: {6}°C" + "\t" + "Temp. VRAM: {7}°C" + "\t" +
                    "Velocidade FAN: {8}RPM", gpu, bus, vram.ToString(), clVR, clBG, clAG, tGPU, tVR, RPM);

                }

                if (gpu.Contains("Nvidia") == true)
                {
                    foreach (var hardwareItem in thisComputer.Hardware)
                    {
                        if (hardwareItem.HardwareType == HardwareType.GpuNvidia)
                        {
                            hardwareItem.Update();
                            foreach (IHardware subHardware in hardwareItem.SubHardware)
                                subHardware.Update();
                            foreach (var sensor in hardwareItem.Sensors)
                            {
                                if (sensor.SensorType == SensorType.Temperature)
                                {

                                    tGPU = sensor.Value.Value.ToString();
                                }

                                if (sensor.SensorType == SensorType.Clock)
                                {

                                    clAG = sensor.Value.Value.ToString();
                                }
                            }
                        }

                    }
                    Console.WriteLine("Nome: {0}" + "\t" + "BUS: {1}bits" + "\n" + "Capacidade VRAM: {2}GB" + "\t" + "Clock VRAM: {3}Mhz" + "\n" +
                    "Clock Base GPU: {4}Mhz" + "\t" + "Clock Atual GPU: {5}Mhz" + "\n" + "Temp. GPU: {6}°C" + "\t" + "Temp. VRAM: {7}°C" + "\t" +
                    "Velocidade FAN: {8}RPM", gpu, bus, vram.ToString(), clVR, clBG, clAG, tGPU, tVR, RPM);

                }

                if (gpu.Contains("Intel") == true)
                {
                    Console.WriteLine("Nome: {0}" + "\t" + "\n" + "Capacidade VRAM: {1}GB" + "\t" + "Clock Base GPU: {2}Mhz" + 
                        "\n" + "Clock Atual GPU: {3}Mhz" + "\n" + "Temp. GPU: {4}°C", gpu, vram.ToString(), clBG, tGPU);
                }
               

                #endregion

                a = 1;
            }
        }
    }
}
