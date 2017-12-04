using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualKeyloggerDetector
{
    class Test
    {
        public class ProgramInfo
        {
            public string Name;
            public string Path;
            public ulong WriteCount;
            public ulong Id;
        }

        public List<ProgramInfo> StartPrograms = new List<ProgramInfo>();
        public List<ProgramInfo> EndPrograms = new List<ProgramInfo>();
    }
}
