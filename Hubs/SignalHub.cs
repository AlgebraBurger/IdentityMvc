using CrmCoreLite.Infrastructure;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CrmCoreLite.Hubs
{
    public class SignalHub : Hub
    {
        public void BroadCast()
        {
            Clients.All.broadMessage("BroadCast...");
        }
    }
}
