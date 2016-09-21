using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM.Common.DTOs
{
    [Serializable]
    public class DriveDTO
    {
        public string DriveID { get; set; }

        public string DriveName { get; set; }

        public float DriveCapacity { get; set; }

        public float DriveFreeSpace { get; set; }
        public float ConsumedSpace { get; set; }

        public bool IsReady { get; set; }

        public DriveDTO RootDirectory { get; set; }

        public string DriveMessage { get; set; }
    }
}
