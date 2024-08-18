using Microsoft.AspNetCore.SignalR;

namespace TalksterAPI.Hubs
{
    public class ChatHub :Hub
    {
        public async Task JoinGroup(string groupName, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("NewUser", $"{userName} enter the chat");
        }

        public async Task LeaveGroup(string groupName, string userName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("LeftUser", $"{userName} leave the chat");
        }

        public async Task SendMessage(NewMessage message)
        {
            await Clients.Group(message.GroupName).SendAsync("NewMessage", message);

        }

        public record NewMessage(string userName,  string message, string GroupName); 
    }
}
