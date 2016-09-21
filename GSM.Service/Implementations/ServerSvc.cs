using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM.Common.Contracts;
using GSM.Common.DTOs;
using System.IO;
using System.Diagnostics;

namespace GSM.Service.Implementations
{
    public class ServerSvc : IServerSvc
    {
        ServerDTO _serverDTO = null;
        DriveInfo[] _arrDriveInfo = null;
        Process[] _arrProcesses = null;

        public ServerSvc()
        {
            _serverDTO = new ServerDTO();
        }

        public ServerDTO GetServerInformation()
        {
            _arrDriveInfo = DriveInfo.GetDrives();          //Has memory leakage problem, need to find out another way
            _arrProcesses = Process.GetProcesses();         //Has memory leakage problem, need to find out another way
            this.MergeDriveInfo(ref _serverDTO, _arrDriveInfo);
            this.MergeProcessInfo(ref _serverDTO, _arrProcesses);
            return _serverDTO;
        }

        public void Dispose()
        {
            _serverDTO = null;
            _arrDriveInfo = null;
            _arrProcesses = null;
            GC.Collect();
        }

        private void MergeProcessInfo(ref ServerDTO serverDTO, Process[] arrProcessInfo)
        {
            if (arrProcessInfo != null)
            {
                serverDTO.Processes = new List<ProcessDTO>();
                ProcessDTO prcss = null;
                foreach (Process proc in arrProcessInfo)
                {
                    prcss = new ProcessDTO();
                    prcss.Id = proc.Id;
                    prcss.ProcessName = proc.ProcessName;
                    prcss.MachineName = proc.MachineName;
                    prcss.WorkingSet64 = proc.WorkingSet64;
                    prcss.PeakWorkingSet64 = proc.PeakWorkingSet64;
                    serverDTO.Processes.Add(prcss);
                }
                prcss = null;
            }
            arrProcessInfo = null;
        }

        private void MergeDriveInfo(ref ServerDTO serverDTO, DriveInfo[] arrDriveInfo)
        {
            if (arrDriveInfo != null)
            {
                serverDTO.Drives = new List<DriveDTO>();
                DriveDTO drv = null;
                foreach (DriveInfo di in arrDriveInfo)
                {
                    drv = new DriveDTO();
                    drv.DriveName = di.Name;
                    drv.IsReady = di.IsReady;
                    if(drv.IsReady)
                    {
                        drv.DriveID = di.VolumeLabel;
                        drv.DriveCapacity = di.TotalSize;
                        drv.DriveFreeSpace = di.TotalFreeSpace;
                        drv.ConsumedSpace = di.TotalSize - di.TotalFreeSpace;
                    }
                    else
                    {
                        drv.DriveMessage = "Device is not ready";
                    }
                    
                    serverDTO.Drives.Add(drv);
                }
                drv = null;
            }
            arrDriveInfo = null;
        }

        public ServerDTO GetServerInformation(string ip)
        {
            throw new NotImplementedException();
        }

        public ServerDTO GetServerInformation(string name, string ip)
        {
            throw new NotImplementedException();
        }
    }
}
