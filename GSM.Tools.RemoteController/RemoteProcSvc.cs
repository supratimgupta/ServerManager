using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GSM.Tools.RemoteController
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RemoteProcSvc" in both code and config file together.
    public class RemoteProcSvc : GSM.Common.Contracts.IRemoteProcSvc
    {
        public bool EndProcess(int processID)
        {
            bool status = false;
            try
            {
                Process proc = Process.GetProcessById(processID);
                proc.Kill();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool EndProcess(string processInfo)
        {
            throw new NotImplementedException();
        }

        public bool StartProcess(string processInfo)
        {
            throw new NotImplementedException();
        }
    }
}
