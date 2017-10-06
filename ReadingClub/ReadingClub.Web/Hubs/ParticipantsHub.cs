using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ReadingClub.Web.Hubs
{
    public class ParticipantsHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}