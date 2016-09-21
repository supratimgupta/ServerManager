using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM.Common.DTOs
{
    public class ServerDTO
    {
        public string ServerID { get; set; }

        public string ServerName { get; set; }

        public string Description { get; set; }

        public List<DriveDTO> Drives { get; set; }

        public List<ProcessDTO> Processes { get; set; }
    }
}
