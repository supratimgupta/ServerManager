using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM.Common.DTOs
{
    public class UserRoleDTO
    {
        public string UserRoleID { get; set; }

        public UserDTO User { get; set; }

        public RoleDTO Role { get; set; }
    }
}
