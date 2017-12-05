using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualKeyloggerDetector
{
    class Experiment
    {
        private string chars = "abcdefghijklmnopqrstuvwxyz";
        private Random random = new Random();

        public List<Test> Tests = new List<Test>();
        public int TestCount = 21;
        public int CurrentTest = 0;
        public float TestDuration = 1000; // ms
        public int MaxKeyPresses = 60;
        public int MinKeyPresses = 0;
        public int RemainingKeys;
        public int TickCount;

        public Timer m_Timer;

        public Experiment()
        {
            for (int i = 0; i < TestCount; i++)
            {
                var test = new Test();
                test.KeyPresses = (MaxKeyPresses - MinKeyPresses) * i / (TestCount - 1) + MinKeyPresses;
                test.PressAmount = (double)test.KeyPresses / (MaxKeyPresses - MinKeyPresses);
                Tests.Add(test);
            }


            m_Timer = new Timer();
            m_Timer.Interval = 20;
            m_Timer.Tick += new EventHandler(timer_Tick);
        }

        public void Start()
        {
            m_Timer.Start();
            StartTest();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (TickCount > 0)
            {
                if ((float)RemainingKeys / TickCount > 0.5f)
                {
                    if (RemainingKeys == 1)
                        VirtualInput.SendInput('7');
                    else
                        VirtualInput.SendInput(chars[random.Next(chars.Length)]);
                    RemainingKeys--;
                }
                TickCount--;
            }

            if (TickCount > 0)
                return;

            CollectInfo(Tests[CurrentTest].EndPrograms);
            CurrentTest++;

            if (CurrentTest == TestCount)
            {
                GetResults();
                m_Timer.Stop();
            }
            else
            {
                StartTest();
            }
        }

        private void StartTest()
        {
            TickCount = MaxKeyPresses;
            RemainingKeys = Tests[CurrentTest].KeyPresses;
            CollectInfo(Tests[CurrentTest].StartPrograms);
        }

        private void CollectInfo(List<Test.ProgramInfo> programs)
        {
            var processes = Win32_Process.GetAllProcesses();
            foreach (var item in processes)
            {
                var info = new Test.ProgramInfo();
                info.Name = item.Name;
                info.Path = item.ExecutablePath;
                info.WriteCount = item.WriteTransferCount.Value;
                info.Id = item.ProcessId.Value;
                programs.Add(info);
            }
        }

        private void GetResults()
        {
            var commonIds = Tests[0].StartPrograms.Select(pr => pr.Id).ToList();
            foreach (var test in Tests)
            {
                commonIds = commonIds.Intersect(test.StartPrograms.Select(pr => pr.Id)).ToList();
                commonIds = commonIds.Intersect(test.EndPrograms.Select(pr => pr.Id)).ToList();
            }

            using (var file = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "detector_results.txt"))
            {
                file.WriteLine("Skipping processes where there were none bytes written to hard drive at all");
                file.WriteLine("");

                foreach (var id in commonIds)
                {
                    var programInfo = Tests[0].StartPrograms.First(pr => pr.Id == id);

                    var InputValues = new List<double>();
                    var OutputValues = new List<double>();
                    double InputValueSum = 0;
                    double OutputValueSum = 0;

                    foreach (var test in Tests)
                    {
                        InputValues.Add(test.PressAmount);
                        InputValueSum += test.PressAmount;

                        var startInfo = test.StartPrograms.First(pr => pr.Id == id);
                        var endInfo = test.EndPrograms.First(pr => pr.Id == id);
                        long writtenBytes = (long)endInfo.WriteCount - (long)startInfo.WriteCount;

                        OutputValues.Add((double)writtenBytes);
                        OutputValueSum += (double)writtenBytes;
                    }

                    double InputAveraget = InputValueSum / TestCount;
                    double OutputAverage = OutputValueSum / TestCount;
                    if (OutputAverage == 0)
                        continue;

                    double SumProduct = 0;
                    double DeviationInput = 0;
                    double DeviationOutput = 0;

                    for (int i = 0; i < TestCount; i++)
                    {
                        double IV = InputValues[i];
                        double OV = OutputValues[i];
                        SumProduct += (IV - InputAveraget) * (OV - OutputAverage);
                        DeviationInput += (IV - InputAveraget) * (IV - InputAveraget);
                        DeviationOutput += (OV - OutputAverage) * (OV - OutputAverage);
                    }

                    DeviationInput = Math.Sqrt(DeviationInput);
                    DeviationOutput = Math.Sqrt(DeviationOutput);

                    double R = SumProduct / DeviationInput / DeviationOutput;

                    if (Math.Abs(R) > 0.8)
                        file.WriteLine("vvvv Possible Keylogger");

                    file.WriteLine(programInfo.Name + "  " + programInfo.Id + "  R: " + R);
                    file.WriteLine(programInfo.Path);

                    if (Math.Abs(R) > 0.8)
                        file.WriteLine("^^^^-------------------");

                    file.WriteLine("");
                }
            }
        }
    }
}
