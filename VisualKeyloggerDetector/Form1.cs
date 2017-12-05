using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace VisualKeyloggerDetector
{
    public partial class Form1 : Form
    {
        private Timer m_Timer;
        private Experiment m_Experiment;

        public Form1()
        {
            InitializeComponent();

            m_Experiment = new Experiment();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_Experiment.Start();

            //VirtualInput.SendInput('v');
            //var processes = Win32_Process.GetAllProcesses();
            //Console.WriteLine(processes.Count);
        }

        //private void finish_Test()
        //{
        //    var processes = Win32_Process.GetAllProcesses();
        //    foreach (var item in processes)
        //    {
        //        var info = new Test.ProgramInfo();
        //        info.Name = item.Name;
        //        info.Path = item.ExecutablePath;
        //        info.WriteCount = item.WriteTransferCount.Value;
        //        info.Id = item.ProcessId.Value;
        //        m_Test.EndPrograms.Add(info);
        //    }

        //    foreach (var startInfo in m_Test.StartPrograms)
        //    {
        //        var endInfo = m_Test.EndPrograms.FirstOrDefault(info => info.Id == startInfo.Id);
        //        if (endInfo == null)
        //            continue;

        //        long writeDif = (long)endInfo.WriteCount - (long)startInfo.WriteCount;
        //        if (writeDif < 50)
        //            continue;

        //        Console.WriteLine(startInfo.Name);
        //        Console.WriteLine(writeDif);
        //    }
        //}
    }
}
