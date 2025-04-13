using Microsoft.AspNetCore.SignalR;
using ChatApp.Data;
using ChatApp.Models;
using System.Threading.Tasks;
using System;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string user, string message)
        {
            // Save to database
            var chatMessage = new ChatMessage
            {
                User = user,
                Message = message,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Broadcast message to all clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
