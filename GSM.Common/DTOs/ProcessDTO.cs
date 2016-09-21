using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM.Common.DTOs
{
    [Serializable]
    public class ProcessDTO
    {

        public int BasePriority { get; set; }
        
        public bool EnableRaisingEvents { get; set; }

        public int ExitCode { get; set; }

        public DateTime ExitTime { get; set; }
        
        //public IntPtr Handle { get; }

        public int HandleCount { get; set; }

        public bool HasExited { get; set; }

        public int Id { get; set; }

        public string MachineName { get; set; }

        public string MainModule { get; set; }
        
        //public IntPtr MainWindowHandle { get; }

        public string MainWindowTitle { get; set; }
        
        //public IntPtr MaxWorkingSet { get; set; }
        
        //public IntPtr MinWorkingSet { get; set; }
        
        //public ProcessModuleCollection Modules { get; }

        public int NonpagedSystemMemorySize { get; set; }

        public long NonpagedSystemMemorySize64 { get; set; }

        public int PagedMemorySize { get; set; }

        public long PagedMemorySize64 { get; set; }

        public int PagedSystemMemorySize { get; set; }

        public long PagedSystemMemorySize64 { get; set; }

        public int PeakPagedMemorySize { get; set; }

        public long PeakPagedMemorySize64 { get; set; }

        public int PeakVirtualMemorySize { get; set; }

        public long PeakVirtualMemorySize64 { get; set; }

        public int PeakWorkingSet { get; set; }

        public long PeakWorkingSet64 { get; set; }
        
        public bool PriorityBoostEnabled { get; set; }
        
        //public ProcessPriorityClass PriorityClass { get; set; }

        public int PrivateMemorySize { get; set; }

        public long PrivateMemorySize64 { get; set; }

        public TimeSpan PrivilegedProcessorTime { get; set; }

        public string ProcessName { get; set; }
        
        //public IntPtr ProcessorAffinity { get; set; }

        public bool Responding { get; set; }

        public int SessionId { get; set; }
        
        //public StreamReader StandardError { get; }
        
        //public StreamWriter StandardInput { get; }
        
        //public StreamReader StandardOutput { get; }
        
        //public ProcessStartInfo StartInfo { get; set; }

        public DateTime StartTime { get; set; }
        
        //public ISynchronizeInvoke SynchronizingObject { get; set; }
        
        //public ProcessThreadCollection Threads { get; }

        public TimeSpan TotalProcessorTime { get; set; }

        public TimeSpan UserProcessorTime { get; set; }

        public int VirtualMemorySize { get; set; }

        public long VirtualMemorySize64 { get; set; }

        public int WorkingSet { get; set; }

        public long WorkingSet64 { get; set; }
    }
}
