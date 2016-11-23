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
        //Utilities.ThreadTracker _thTracker;

        /// <summary>
        /// Will be called from external modules
        /// </summary>
        /// <param name="serverDTO">Server DTO</param>
        /// <returns>Status</returns>
        [Route("sendDetail")]
        [HttpPost]
        public IHttpActionResult SendDetail(ServerDTO serverDTO)
        {
            Utilities.ServerTracker srvTrck = Utilities.ServerTracker.GetInstance;
            ServerDTO srvr = srvTrck.CurrentServerLists.FirstOrDefault(srv => srv.ServerIP == serverDTO.ServerIP);
            if(srvr==null)
            {
                srvTrck.CurrentServerLists.Add(serverDTO);
            }
            else
            {
                srvr = serverDTO;
            }
            Hubs.NotificationHub hub = new Hubs.NotificationHub();
            hub.SendDetails(serverDTO);
            serverDTO = null;
            hub = null;
            GC.Collect();
            return Ok(new { Status = true, Message = "" });
        }

        [Route("stopProcess")]
        [HttpPost]
        public IHttpActionResult StopProcess([FromBody]ReqProcessDTO process)
        {
            Utilities.ServerTracker srvTrck = Utilities.ServerTracker.GetInstance;
            ServerDTO srvr = srvTrck.CurrentServerLists.FirstOrDefault(srv => srv.ServerIP == process.ServerIP);

            bool status = true;
            string message = string.Empty;

            if(srvr!=null)
            {
                int intProcId = Int32.Parse(process.ProcessId);

                BasicHttpBinding wcfBinding = new BasicHttpBinding();
                EndpointAddress wcfEndPoint = new EndpointAddress(srvr.WCFHostingURL);
                ChannelFactory<GSM.Common.Contracts.IRemoteProcSvc> cfToCallWcf = new ChannelFactory<GSM.Common.Contracts.IRemoteProcSvc>(wcfBinding, wcfEndPoint);
                GSM.Common.Contracts.IRemoteProcSvc instance = cfToCallWcf.CreateChannel();
                // Call Service.
                status = instance.EndProcess(intProcId);
                //string message = string.Empty;
                if (!status)
                {
                    message = "Failed to stop process.";
                }

                cfToCallWcf.Close();
            }

            return Ok(new { Status = status, Message = message });
        }

        [Route("getSummary")]
        [HttpPost]
        public IHttpActionResult GetSummary()
        {
            List<ConnectionSummaryDTO> lstConnSum = new List<ConnectionSummaryDTO>();
            Utilities.ServerTracker srvTrck = Utilities.ServerTracker.GetInstance;
            if(srvTrck.CurrentServerLists!=null)
            {
                ConnectionSummaryDTO connSum = null;
                for(int i=0;i<srvTrck.CurrentServerLists.Count;i++)
                {
                    connSum = new ConnectionSummaryDTO();
                    connSum.IpAddress = srvTrck.CurrentServerLists[i].ServerIP;
                    connSum.SystemName = srvTrck.CurrentServerLists[i].ServerName;
                    lstConnSum.Add(connSum);
                }
            }
            return Ok(lstConnSum);
        }
    }
}
