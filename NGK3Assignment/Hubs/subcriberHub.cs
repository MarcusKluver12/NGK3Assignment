using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NGK3Assignment.Hubs;
using NGK3Assignment.Models;

namespace NGK3Assignment.Hubs
{
    
    public class SubcriberHub : Hub
    {
        public async Task weatherUpdate(WeatherStation weather)
        {
            await Clients.All.SendAsync("newWeatherUpdate", weather);
        }

    }
}
