using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Gladiator_SrvMgr.Hubs
{
    public class NotificationHub : Hub
    {
        public void SendDetails(GSM.Common.DTOs.ServerDTO serverDetails)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            hubContext.Clients.All.broadcastServerDetails(serverDetails);
            hubContext = null;
            serverDetails = null;
        }
    }
}