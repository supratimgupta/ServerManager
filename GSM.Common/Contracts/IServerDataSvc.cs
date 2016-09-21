using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM.Common.Contracts
{
    public interface IServerDataSvc
    {
        DTOs.ServerDTO GetServerDetails(string ip, string name);

        DTOs.ServerDTO GetServerDetails(string serverID);

        DTOs.ServerDTO GetServers();
    }
}
