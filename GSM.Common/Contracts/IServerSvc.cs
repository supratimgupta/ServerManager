using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM.Common.DTOs;

namespace GSM.Common.Contracts
{
    public interface IServerSvc : IDisposable
    {
        ServerDTO GetServerInformation();
        ServerDTO GetServerInformation(string ip);

        ServerDTO GetServerInformation(string name, string ip);

        //void Dispose();
    }
}
