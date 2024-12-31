using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs
{
    public class Chat:Hub
    {
        public async Task Send(string name, string message, string time)
        {
            await Clients.All.SendAsync("addNewMessageToPage", name, message, time);
        }

    }
}
