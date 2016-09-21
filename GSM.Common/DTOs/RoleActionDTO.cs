using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM.Common.DTOs
{
    public class RoleActionDTO
    {
        public string RoleActionID { get; set; }

        public RoleDTO Role { get; set; }

        public ActionDTO Action { get; set; }
    }
}
