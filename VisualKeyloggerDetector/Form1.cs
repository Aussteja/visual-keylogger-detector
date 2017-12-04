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
        private Test m_Test;
        private int m_KeyCount = 100;

        public Form1()
        {
            InitializeComponent();

            m_Timer = new Timer();
            m_Timer.Interval = 100;
            m_Timer.Start();
            m_Timer.Tick += new EventHandler(timer_Tick);

            m_Test = new Test();
            var processes = Win32_Process.GetAllProcesses();
            foreach (var item in processes)
            {
                var info = new Test.ProgramInfo();
                info.Name = item.Name;
                info.Path = item.ExecutablePath;
                info.WriteCount = item.WriteTransferCount.Value;
                info.Id = item.ProcessId.Value;
                m_Test.StartPrograms.Add(info);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            return;
            VirtualInput.SendInput('v');
            var processes = Win32_Process.GetAllProcesses();
            Console.WriteLine(processes.Count);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            VirtualInput.SendInput('a');
            m_KeyCount--;
            if (m_KeyCount == 0)
            {
                m_Timer.Stop();

                finish_Test();
            }
        }

        private void finish_Test()
        {
            var processes = Win32_Process.GetAllProcesses();
            foreach (var item in processes)
            {
                var info = new Test.ProgramInfo();
                info.Name = item.Name;
                info.Path = item.ExecutablePath;
                info.WriteCount = item.WriteTransferCount.Value;
                info.Id = item.ProcessId.Value;
                m_Test.EndPrograms.Add(info);
            }

            foreach (var startInfo in m_Test.StartPrograms)
            {
                var endInfo = m_Test.EndPrograms.FirstOrDefault(info => info.Id == startInfo.Id);
                if (endInfo == null)
                    continue;

                long writeDif = (long)endInfo.WriteCount - (long)startInfo.WriteCount;
                if (writeDif < 50)
                    continue;

                Console.WriteLine(startInfo.Name);
            }
        }
    }
}
