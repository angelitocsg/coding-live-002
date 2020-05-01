using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MyApi.Entities;
using MyApi.Interfaces;

namespace MyApi.Hubs
{
    public class ClickCounterHub : Hub<IClickCounterHubResponse>, IClickCounterHubRequest
    {
        private static Counter _counter;
        public static Counter Counter
        {
            get
            {
                if (_counter == null) { _counter = new Counter(); }
                return _counter;
            }
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.All.SendClickCounters(Counter);
        }

        public async Task HahaClick(string who)
        {
            Counter.HahaCounter++;
            await Clients.All.SendClickCounters(Counter);
        }

        public async Task HeartClick(string who)
        {
            Counter.HeartCounter++;
            await Clients.All.SendClickCounters(Counter);
        }

        public async Task LikeClick(string who)
        {
            Counter.LikeCounter++;
            await Clients.All.SendClickCounters(Counter);
        }

        public async Task TongueClick(string who)
        {
            Counter.TongueCounter++;
            await Clients.All.SendClickCounters(Counter);
        }
    }
}