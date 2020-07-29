namespace MaxTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel2 = new System.Windows.Forms.Panel();
            this.cpusage = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.ramusage = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.dskusage = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.gpulbl = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.cpulbl = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.installtab = new System.Windows.Forms.TabPage();
            this.utiltab = new System.Windows.Forms.TabPage();
            this.toolstab = new System.Windows.Forms.TabPage();
            this.hardtab = new System.Windows.Forms.TabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tools = new System.Windows.Forms.Button();
            this.intall = new System.Windows.Forms.Button();
            this.hard = new System.Windows.Forms.Button();
            this.util = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BackgroundImage = global::MaxTools.Properties.Resources.back3;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cpusage);
            this.panel2.Controls.Add(this.label61);
            this.panel2.Controls.Add(this.ramusage);
            this.panel2.Controls.Add(this.label56);
            this.panel2.Controls.Add(this.dskusage);
            this.panel2.Controls.Add(this.label59);
            this.panel2.Controls.Add(this.gpulbl);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.cpulbl);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(1, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(629, 62);
            this.panel2.TabIndex = 56;
            // 
            // cpusage
            // 
            this.cpusage.AutoSize = true;
            this.cpusage.BackColor = System.Drawing.Color.Transparent;
            this.cpusage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpusage.ForeColor = System.Drawing.Color.White;
            this.cpusage.Location = new System.Drawing.Point(443, 6);
            this.cpusage.Name = "cpusage";
            this.cpusage.Size = new System.Drawing.Size(34, 15);
            this.cpusage.TabIndex = 12;
            this.cpusage.Text = "40°C";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.BackColor = System.Drawing.Color.Transparent;
            this.label61.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.ForeColor = System.Drawing.Color.White;
            this.label61.Location = new System.Drawing.Point(406, 6);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(35, 15);
            this.label61.TabIndex = 11;
            this.label61.Text = "CPU";
            // 
            // ramusage
            // 
            this.ramusage.AutoSize = true;
            this.ramusage.BackColor = System.Drawing.Color.Transparent;
            this.ramusage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ramusage.ForeColor = System.Drawing.Color.White;
            this.ramusage.Location = new System.Drawing.Point(520, 7);
            this.ramusage.Name = "ramusage";
            this.ramusage.Size = new System.Drawing.Size(34, 15);
            this.ramusage.TabIndex = 10;
            this.ramusage.Text = "40°C";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.BackColor = System.Drawing.Color.Transparent;
            this.label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.White;
            this.label56.Location = new System.Drawing.Point(483, 7);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(37, 15);
            this.label56.TabIndex = 9;
            this.label56.Text = "RAM";
            // 
            // dskusage
            // 
            this.dskusage.AutoSize = true;
            this.dskusage.BackColor = System.Drawing.Color.Transparent;
            this.dskusage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dskusage.ForeColor = System.Drawing.Color.White;
            this.dskusage.Location = new System.Drawing.Point(596, 7);
            this.dskusage.Name = "dskusage";
            this.dskusage.Size = new System.Drawing.Size(26, 15);
            this.dskusage.TabIndex = 8;
            this.dskusage.Text = "N/A";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.ForeColor = System.Drawing.Color.White;
            this.label59.Location = new System.Drawing.Point(559, 7);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(35, 15);
            this.label59.TabIndex = 7;
            this.label59.Text = "DSK";
            // 
            // gpulbl
            // 
            this.gpulbl.AutoSize = true;
            this.gpulbl.BackColor = System.Drawing.Color.Transparent;
            this.gpulbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpulbl.ForeColor = System.Drawing.Color.White;
            this.gpulbl.Location = new System.Drawing.Point(520, 30);
            this.gpulbl.Name = "gpulbl";
            this.gpulbl.Size = new System.Drawing.Size(34, 15);
            this.gpulbl.TabIndex = 6;
            this.gpulbl.Text = "40°C";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(483, 30);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(36, 15);
            this.label26.TabIndex = 5;
            this.label26.Text = "GPU";
            // 
            // cpulbl
            // 
            this.cpulbl.AutoSize = true;
            this.cpulbl.BackColor = System.Drawing.Color.Transparent;
            this.cpulbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpulbl.ForeColor = System.Drawing.Color.White;
            this.cpulbl.Location = new System.Drawing.Point(596, 30);
            this.cpulbl.Name = "cpulbl";
            this.cpulbl.Size = new System.Drawing.Size(34, 15);
            this.cpulbl.TabIndex = 4;
            this.cpulbl.Text = "40°C";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(559, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 16);
            this.label11.TabIndex = 3;
            this.label11.Text = "CPU";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(78, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(77, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.installtab);
            this.tabControl1.Controls.Add(this.utiltab);
            this.tabControl1.Controls.Add(this.toolstab);
            this.tabControl1.Controls.Add(this.hardtab);
            this.tabControl1.Location = new System.Drawing.Point(65, 36);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(575, 444);
            this.tabControl1.TabIndex = 57;
            // 
            // installtab
            // 
            this.installtab.BackColor = System.Drawing.Color.Black;
            this.installtab.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.installtab.Location = new System.Drawing.Point(4, 22);
            this.installtab.Name = "installtab";
            this.installtab.Padding = new System.Windows.Forms.Padding(3);
            this.installtab.Size = new System.Drawing.Size(567, 418);
            this.installtab.TabIndex = 0;
            this.installtab.Text = "              ";
            // 
            // utiltab
            // 
            this.utiltab.BackColor = System.Drawing.Color.Black;
            this.utiltab.Location = new System.Drawing.Point(4, 22);
            this.utiltab.Name = "utiltab";
            this.utiltab.Size = new System.Drawing.Size(567, 418);
            this.utiltab.TabIndex = 3;
            this.utiltab.Text = "tabPage1";
            // 
            // toolstab
            // 
            this.toolstab.BackColor = System.Drawing.Color.Black;
            this.toolstab.Location = new System.Drawing.Point(4, 22);
            this.toolstab.Name = "toolstab";
            this.toolstab.Padding = new System.Windows.Forms.Padding(3);
            this.toolstab.Size = new System.Drawing.Size(567, 418);
            this.toolstab.TabIndex = 2;
            this.toolstab.Text = "tabPage3";
            // 
            // hardtab
            // 
            this.hardtab.BackColor = System.Drawing.Color.Black;
            this.hardtab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hardtab.ForeColor = System.Drawing.Color.DeepPink;
            this.hardtab.Location = new System.Drawing.Point(4, 22);
            this.hardtab.Name = "hardtab";
            this.hardtab.Size = new System.Drawing.Size(567, 418);
            this.hardtab.TabIndex = 4;
            this.hardtab.Text = "tabPage1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.AddExtension = false;
            this.openFileDialog1.DereferenceLinks = false;
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Todos os Arquivos|*.*";
            this.openFileDialog1.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            this.openFileDialog1.Title = "Abrir Arquivo";
            // 
            // tools
            // 
            this.tools.AccessibleDescription = "Ferramentas";
            this.tools.AccessibleName = "Ferramentas";
            this.tools.BackColor = System.Drawing.Color.Black;
            this.tools.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tools.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.tools.FlatAppearance.BorderSize = 2;
            this.tools.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tools.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.tools.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tools.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tools.ForeColor = System.Drawing.Color.White;
            this.tools.Image = global::MaxTools.Properties.Resources.tool;
            this.tools.Location = new System.Drawing.Point(1, 266);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(68, 102);
            this.tools.TabIndex = 54;
            this.tools.UseVisualStyleBackColor = false;
            this.tools.Click += new System.EventHandler(this.tools_Click);
            // 
            // intall
            // 
            this.intall.AccessibleDescription = "Instalador";
            this.intall.AccessibleName = "Instalador";
            this.intall.BackColor = System.Drawing.Color.Black;
            this.intall.BackgroundImage = global::MaxTools.Properties.Resources.install;
            this.intall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.intall.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.intall.FlatAppearance.BorderSize = 2;
            this.intall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.intall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.intall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.intall.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.intall.ForeColor = System.Drawing.Color.White;
            this.intall.Location = new System.Drawing.Point(1, 62);
            this.intall.Name = "intall";
            this.intall.Size = new System.Drawing.Size(68, 102);
            this.intall.TabIndex = 50;
            this.intall.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.intall.UseVisualStyleBackColor = false;
            this.intall.Click += new System.EventHandler(this.intall_Click);
            // 
            // hard
            // 
            this.hard.AccessibleDescription = "Informações de Sistema";
            this.hard.AccessibleName = "Informações de Sistema";
            this.hard.BackColor = System.Drawing.Color.Black;
            this.hard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.hard.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.hard.FlatAppearance.BorderSize = 2;
            this.hard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.hard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.hard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hard.ForeColor = System.Drawing.Color.White;
            this.hard.Image = global::MaxTools.Properties.Resources.hard;
            this.hard.Location = new System.Drawing.Point(1, 368);
            this.hard.Name = "hard";
            this.hard.Size = new System.Drawing.Size(68, 102);
            this.hard.TabIndex = 59;
            this.hard.UseVisualStyleBackColor = false;
            this.hard.Click += new System.EventHandler(this.hard_Click);
            // 
            // util
            // 
            this.util.AccessibleDescription = "Atalhos Úteis";
            this.util.AccessibleName = "Atalhos Úteis";
            this.util.BackColor = System.Drawing.Color.Black;
            this.util.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.util.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.util.FlatAppearance.BorderSize = 2;
            this.util.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.util.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.util.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.util.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.util.ForeColor = System.Drawing.Color.White;
            this.util.Image = global::MaxTools.Properties.Resources.lnk1;
            this.util.Location = new System.Drawing.Point(1, 164);
            this.util.Name = "util";
            this.util.Size = new System.Drawing.Size(68, 102);
            this.util.TabIndex = 58;
            this.util.UseVisualStyleBackColor = false;
            this.util.Click += new System.EventHandler(this.util_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick_1);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(632, 470);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.intall);
            this.Controls.Add(this.tools);
            this.Controls.Add(this.hard);
            this.Controls.Add(this.util);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Max\'s Tools BETA";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button intall;
        private System.Windows.Forms.Button tools;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage toolstab;
        private System.Windows.Forms.TabPage utiltab;
        private System.Windows.Forms.TabPage hardtab;
        private System.Windows.Forms.Button util;
        private System.Windows.Forms.Button hard;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage installtab;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label gpulbl;
        public System.Windows.Forms.Label cpulbl;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Label cpusage;
        private System.Windows.Forms.Label label61;
        public System.Windows.Forms.Label ramusage;
        private System.Windows.Forms.Label label56;
        public System.Windows.Forms.Label dskusage;
        private System.Windows.Forms.Label label59;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

