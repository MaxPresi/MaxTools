using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MaxTools.bw
{
    public partial class outros : Form
    {
        public outros()
        {
            
            InitializeComponent();            
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

        

        
    }
}
