using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace GSM.Common.Contracts
{
    [ServiceContract]
    public interface IRemoteProcSvc
    {
        [OperationContract]
        bool EndProcess(int processID);

        [OperationContract(Name = "EndProcessByInfo")]
        bool EndProcess(string processInfo);

        [OperationContract]
        bool StartProcess(string processInfo);
    }
}
