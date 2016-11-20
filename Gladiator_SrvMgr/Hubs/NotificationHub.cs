using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Gladiator_SrvMgr.Hubs
{
    public class NotificationHub : Hub
    {
        public void SendDetails(GSM.Common.DTOs.ServerDTO serverDetails)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            hubContext.Clients.Group(serverDetails.ServerIP).broadcastServerDetails(serverDetails);
            hubContext = null;
            serverDetails = null;
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if(stopCalled)
            {
                var ip = Context.QueryString["ipAddress"];
                Groups.Remove(Context.ConnectionId, ip);
            }
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnConnected()
        {
            var ip = Context.QueryString["ipAddress"];
            Groups.Add(Context.ConnectionId, ip);
            return base.OnConnected();
            //return base.OnConnected();
        }
    }
}