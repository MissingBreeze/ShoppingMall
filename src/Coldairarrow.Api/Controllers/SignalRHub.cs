using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers
{
    public class SignalRHub : Hub
    {

        private readonly IHttpContextAccessor _httpAccessor;

        public override Task OnConnectedAsync()
        {
            string connectId = Context.ConnectionId;
            var userid = _httpAccessor.HttpContext.Request.Query["user"];

            var UserId = _httpAccessor?.HttpContext?.User.Claims
                 .Where(x => x.Type == "userId").FirstOrDefault()?.Value;


            return base.OnConnectedAsync();
        }

    }
}
