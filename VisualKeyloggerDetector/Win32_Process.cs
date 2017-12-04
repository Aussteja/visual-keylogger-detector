using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace VisualKeyloggerDetector
{
    public class Win32_Process
    {
        public string Caption;
        public string CommandLine;
        public string CreationClassName;
        public DateTime? CreationDate;
        public string CSCreationClassName;
        public string CSName;
        public string Description;
        public string ExecutablePath;
        public ushort? ExecutionState;
        public string Handle;
        public uint? HandleCount;
        public DateTime? InstallDate;
        public ulong? KernelModeTime;
        public uint? MaximumWorkingSetSize;
        public uint? MinimumWorkingSetSize;
        public string Name;
        public string OSCreationClassName;
        public string OSName;
        public ulong? OtherOperationCount;
        public ulong? OtherTransferCount;
        public uint? PageFaults;
        public uint? PageFileUsage;
        public uint? ParentProcessId;
        public uint? PeakPageFileUsage;
        public ulong? PeakVirtualSize;
        public uint? PeakWorkingSetSize;
        public uint? Priority;
        public ulong? PrivatePageCount;
        public uint? ProcessId;
        public uint? QuotaNonPagedPoolUsage;
        public uint? QuotaPagedPoolUsage;
        public uint? QuotaPeakNonPagedPoolUsage;
        public uint? QuotaPeakPagedPoolUsage;
        public ulong? ReadOperationCount;
        public ulong? ReadTransferCount;
        public uint? SessionId;
        public string Status;
        public DateTime? TerminationDate;
        public uint? ThreadCount;
        public ulong? UserModeTime;
        public ulong? VirtualSize;
        public string WindowsVersion;
        public ulong? WorkingSetSize;
        public ulong? WriteOperationCount;
        public ulong? WriteTransferCount;

        public static List<Win32_Process> GetAllProcesses()
        {
            var processes = new List<Win32_Process>();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process");
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject obj in collection)
            {
                var item = new Win32_Process();
                item.Caption = (string)obj["Caption"];
                item.CommandLine = (string)obj["CommandLine"];
                item.CreationClassName = (string)obj["CreationClassName"];
                item.CreationDate = ManagementDateTimeConverter.ToDateTime((string)obj["CreationDate"]);
                item.CSCreationClassName = (string)obj["CSCreationClassName"];
                item.CSName = (string)obj["CSName"];
                item.Description = (string)obj["Description"];
                item.ExecutablePath = (string)obj["ExecutablePath"];
                item.ExecutionState = (ushort?)obj["ExecutionState"];
                item.Handle = (string)obj["Handle"];
                item.HandleCount = (uint?)obj["HandleCount"];
                item.InstallDate = (DateTime?)obj["InstallDate"];
                item.KernelModeTime = (ulong?)obj["KernelModeTime"];
                item.MaximumWorkingSetSize = (uint?)obj["MaximumWorkingSetSize"];
                item.MinimumWorkingSetSize = (uint?)obj["MinimumWorkingSetSize"];
                item.Name = (string)obj["Name"];
                item.OSCreationClassName = (string)obj["OSCreationClassName"];
                item.OSName = (string)obj["OSName"];
                item.OtherOperationCount = (ulong?)obj["OtherOperationCount"];
                item.OtherTransferCount = (ulong?)obj["OtherTransferCount"];
                item.PageFaults = (uint?)obj["PageFaults"];
                item.PageFileUsage = (uint?)obj["PageFileUsage"];
                item.ParentProcessId = (uint?)obj["ParentProcessId"];
                item.PeakPageFileUsage = (uint?)obj["PeakPageFileUsage"];
                item.PeakVirtualSize = (ulong?)obj["PeakVirtualSize"];
                item.PeakWorkingSetSize = (uint?)obj["PeakWorkingSetSize"];
                item.Priority = (uint?)obj["Priority"];
                item.PrivatePageCount = (ulong?)obj["PrivatePageCount"];
                item.ProcessId = (uint?)obj["ProcessId"];
                item.QuotaNonPagedPoolUsage = (uint?)obj["QuotaNonPagedPoolUsage"];
                item.QuotaPagedPoolUsage = (uint?)obj["QuotaPagedPoolUsage"];
                item.QuotaPeakNonPagedPoolUsage = (uint?)obj["QuotaPeakNonPagedPoolUsage"];
                item.QuotaPeakPagedPoolUsage = (uint?)obj["QuotaPeakPagedPoolUsage"];
                item.ReadOperationCount = (ulong?)obj["ReadOperationCount"];
                item.ReadTransferCount = (ulong?)obj["ReadTransferCount"];
                item.SessionId = (uint?)obj["SessionId"];
                item.Status = (string)obj["Status"];
                item.TerminationDate = (DateTime?)obj["TerminationDate"];
                item.ThreadCount = (uint?)obj["ThreadCount"];
                item.UserModeTime = (ulong?)obj["UserModeTime"];
                item.VirtualSize = (ulong?)obj["VirtualSize"];
                item.WindowsVersion = (string)obj["WindowsVersion"];
                item.WorkingSetSize = (ulong?)obj["WorkingSetSize"];
                item.WriteOperationCount = (ulong?)obj["WriteOperationCount"];
                item.WriteTransferCount = (ulong?)obj["WriteTransferCount"];

                processes.Add(item);
            }

            return processes;
        }
    }
}
