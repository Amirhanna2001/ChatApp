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
            await Groups.AddToGroupAsync(Context.ConnectionId, user.ChatRoom);
            _db._connections[Context.ConnectionId] = user;
            await Clients.Group(user.ChatRoom)
                .SendAsync("ReceiveMessage", user.Username, $"{user.Username} has joined {user.ChatRoom} chat", DateTime.Now);

            await SendConnectedUsers(user.ChatRoom);

            //await Clients.All.SendAsync("ReceiveMessage",user.Username, $"{user.Username} has joined the chat app.",DateTime.Now);
        }
        public async Task JoinSpecificChat(UserConnection user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,user.ChatRoom);
            _db._connections[Context.ConnectionId] = user;
            await Clients.Group(user.ChatRoom)
                .SendAsync("ReceiveMessage", user.Username, $"{user.Username} has joined {user.ChatRoom} chat", DateTime.Now);

            await SendConnectedUsers(user.ChatRoom);
        }

        public async Task SendMessage(string message)
        {
            if(!_db._connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
             await Clients.Groups(userConnection.ChatRoom)
                    .SendAsync("ReceiveMessage",userConnection,message,DateTime.Now);  
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if(_db._connections.TryGetValue(Context.ConnectionId,out UserConnection userConnection))
                return base.OnDisconnectedAsync(exception);

            Clients.Group(userConnection.ChatRoom)
                .SendAsync("ReceiveMessage", "AmirHanna", $"{userConnection.Username} has left the chat", DateTime.Now);
            SendConnectedUsers(userConnection.ChatRoom);
            return base.OnDisconnectedAsync(exception);
        }
        public  Task SendConnectedUsers(string room)
        {
            IEnumerable<string> users = _db._connections.Values.Where(u => u.ChatRoom == room).Select(us => us.Username);
            return Clients.Group(room).SendAsync("ConnectedUsers", users);
        }
    }
}