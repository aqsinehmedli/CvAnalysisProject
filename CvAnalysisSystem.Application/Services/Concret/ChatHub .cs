using CvAnalysisSystem.DAL.SqlServer.Context;
using CvAnalysisSystem.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using System.Timers;

namespace CvAnalysisSystem.Application.Services
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;
        public static ConcurrentDictionary<string, string> UserConnections = new();
        private static ConcurrentDictionary<string, bool> OperatorAssigned = new();
        private static ConcurrentDictionary<string, System.Timers.Timer> SessionTimeouts = new();
        private static TimeSpan TimeoutDuration = TimeSpan.FromMinutes(1);

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
            if (!string.IsNullOrEmpty(userId))
            {
                UserConnections[userId] = Context.ConnectionId;
                Console.WriteLine($"✅ Connected: {userId} -> {Context.ConnectionId}");

                if (userId == "operator")
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "operators");

                    var recentUsers = await _context.ChatMessages
                        .Where(m => m.SenderId != "operator" && m.ReceiverId == "operator")
                        .Select(m => m.SenderId)
                        .Distinct()
                        .Take(10)
                        .ToListAsync();

                    foreach (var user in recentUsers)
                    {
                        var history = await _context.ChatMessages
                            .Where(m => (m.SenderId == user && m.ReceiverId == "operator") ||
                                        (m.SenderId == "operator" && m.ReceiverId == user))
                            .OrderBy(m => m.Timestamp)
                            .ToListAsync();

                        foreach (var msg in history)
                        {
                            var senderName = msg.SenderId == "operator" ? "Operator" : msg.SenderId;
                            await Clients.Caller.SendAsync("ReceiveMessage", senderName, msg.Message);
                        }
                    }
                }
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = UserConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (user != null)
            {
                UserConnections.TryRemove(user, out _);
                OperatorAssigned.TryRemove(user, out _);

                if (SessionTimeouts.TryRemove(user, out var timer))
                {
                    timer.Stop();
                    timer.Dispose();
                }

                Console.WriteLine($"🔌 Disconnected: {user}");
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string senderId, string message)
        {
            if (string.IsNullOrWhiteSpace(senderId) || string.IsNullOrWhiteSpace(message))
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "System", "⚠️ Boş mesaj göndərilə bilməz.");
                return;
            }

            var isOperatorConnected = OperatorAssigned.TryGetValue(senderId, out var assigned) && assigned;
            var botResponse = ChatBotHelper.GetBotResponse(message);
            var receiver = isOperatorConnected ? "operator"
                         : botResponse == "Operatora bağlanılır..." ? "operator"
                         : "bot";

            _context.ChatMessages.Add(new ChatMessage
            {
                SenderId = senderId,
                ReceiverId = receiver,
                Message = message,
                Timestamp = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            if (botResponse == "Operatora bağlanılır..." && !isOperatorConnected)
            {
                OperatorAssigned[senderId] = true;
                StartSessionTimeout(senderId);
                await Clients.Client(Context.ConnectionId).SendAsync("ConnectToOperator", senderId);
                await Clients.Group("operators").SendAsync("ReceiveMessage", senderId, message);
                await Clients.Group("operators").SendAsync("ReceiveMessage", "System", $"❗ Yeni istifadəçi operator istəyir: {senderId}");
                return;
            }

            if (!isOperatorConnected)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "Bot", botResponse);
            }
            else
            {
                StartSessionTimeout(senderId);
                await Clients.Group("operators").SendAsync("ReceiveMessage", senderId, message);
            }
        }

        public async Task SendOperatorMessage(string receiverUserId, string message)
        {
            if (!UserConnections.TryGetValue(receiverUserId, out string connectionId)) return;

            _context.ChatMessages.Add(new ChatMessage
            {
                SenderId = "operator",
                ReceiverId = receiverUserId,
                Message = message,
                Timestamp = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            StartSessionTimeout(receiverUserId);
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", "Operator", message);
        }

        public async Task EndSession(string senderId)
        {
            OperatorAssigned.TryRemove(senderId, out _);
            if (SessionTimeouts.TryRemove(senderId, out var timer))
            {
                timer.Stop();
                timer.Dispose();
            }
            if (UserConnections.TryGetValue(senderId, out var connId))
            {
                await Clients.Client(connId).SendAsync("ReceiveMessage", "System", "💤 Operatorla sessiya başa çatdı. Yeni sual seçə bilərsiniz.");
            }
        }

        private void StartSessionTimeout(string userId)
        {
            if (SessionTimeouts.TryGetValue(userId, out var existingTimer))
            {
                existingTimer.Stop();
                existingTimer.Dispose();
            }

            var timer = new System.Timers.Timer(TimeoutDuration.TotalMilliseconds)
            {
                AutoReset = false,
                Enabled = true
            };

            timer.Elapsed += async (s, e) => await EndSession(userId);
            SessionTimeouts[userId] = timer;
            timer.Start();
        }
    }
}