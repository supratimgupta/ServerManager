using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM.Common.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }

        public string LoginID { get; set; }

        public string Password { get; set; }

        public string EmailID { get; set; }

        public bool IsActive { get; set; }
    }
}
