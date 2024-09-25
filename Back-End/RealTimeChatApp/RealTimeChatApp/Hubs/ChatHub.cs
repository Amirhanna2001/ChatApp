using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp.DataServices;
using RealTimeChatApp.Models;

namespace RealTimeChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly SharedDb _db;

        public ChatHub(SharedDb db) => _db = db;

        public async Task JoinChat(UserConnection user)
        {
            await Clients.All.SendAsync("ReceiveMessage","admin", $"{user.Username} has joined the chat app.");
        }
        public async Task JoinSpecificChat(UserConnection user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,user.ChatRoom);

            await Clients.Group(user.ChatRoom)
                .SendAsync("ReceiveMessage","admin",$"{user.Username} has joined {user.ChatRoom}");

        }
    }
}
