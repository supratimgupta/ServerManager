using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using GSM.Common.Contracts;
using GSM.Common.DTOs;
using System.Net.Http.Headers;
using System.ServiceModel;

namespace Gladiator_SrvMgr.Controllers
{

    [RoutePrefix("api/server")]
    public class ServerController : ApiController
    {
        private IServerSvc _serverSvc = new GSM.Service.Implementations.ServerSvc();
        private object _lockObj = new object();
        Utilities.ThreadTracker _thTracker;

        private async Task RunMonitor()
        {
            ServerDTO serverDTO = null;
            while (true)
            {
                if (_thTracker == null)
                {
                    _thTracker = Utilities.ThreadTracker.GetInstance();
                }
                if (_thTracker.IsAppStopped)
                {
                    break;
                }
                using (_serverSvc = new GSM.Service.Implementations.ServerSvc())
                {
                    serverDTO = _serverSvc.GetServerInformation();
                    Hubs.NotificationHub hub = new Hubs.NotificationHub();
                    hub.SendDetails(serverDTO);
                    serverDTO = null;
                    hub = null;

                    _serverSvc = null;

                    await Task.Delay(1000);
                }
            }
        }

        /// <summary>
        /// Will be called from external modules
        /// </summary>
        /// <param name="serverDTO">Server DTO</param>
        /// <returns>Status</returns>
        [Route("sendDetail")]
        [HttpPost]
        public IHttpActionResult SendDetail(ServerDTO serverDTO)
        {
            Hubs.NotificationHub hub = new Hubs.NotificationHub();
            hub.SendDetails(serverDTO);
            serverDTO = null;
            hub = null;
            GC.Collect();
            return Ok(new { Status = true, Message = "" });
        }

        [Route("stopProcess")]
        [HttpPost]
        public IHttpActionResult StopProcess([FromBody]string processId)
        {
            int intProcId = Int32.Parse(processId);

            BasicHttpBinding wcfBinding = new BasicHttpBinding();
            EndpointAddress wcfEndPoint = new EndpointAddress("http://localhost:46963/Controller");
            ChannelFactory<GSM.Common.Contracts.IRemoteProcSvc> cfToCallWcf = new ChannelFactory<GSM.Common.Contracts.IRemoteProcSvc>(wcfBinding, wcfEndPoint);
            GSM.Common.Contracts.IRemoteProcSvc instance = cfToCallWcf.CreateChannel();
            // Call Service.
            bool status = instance.EndProcess(intProcId);
            string message = string.Empty;
            if(!status)
            {
                message = "Failed to stop process.";
            }

            cfToCallWcf.Close();

            return Ok(new { Status = status, Message = message });
        }

        /// <summary>
        /// Cannot use this architecture now, have memory leak problem
        /// </summary>
        /// <returns></returns>
        [Route("callCurrentServer")]
        [HttpPost]
        public IHttpActionResult CallCurrentServer()
        {
            lock(_lockObj)
            {
                if(_thTracker==null)
                {
                    _thTracker = Utilities.ThreadTracker.GetInstance();
                }
                Dictionary<string, int> dicJobs = _thTracker.GetRunningJobs();
                if (dicJobs == null || !dicJobs.Keys.Contains("CURRENT_SERVER") || dicJobs["CURRENT_SERVER"] <= 0)
                {
                    Task.Factory.StartNew(async () => await RunMonitor());
                    if(!dicJobs.Keys.Contains(""))
                    {
                        dicJobs.Add("CURRENT_SERVER", 1);
                    }
                    else if (dicJobs["CURRENT_SERVER"] < 0)
                    {
                        dicJobs["CURRENT_SERVER"] = 1;
                    }
                    else
                    {
                        dicJobs["CURRENT_SERVER"] = dicJobs["CURRENT_SERVER"]++;
                    }
                }
            }
            return Ok("completed the run");
        }
    }
}
