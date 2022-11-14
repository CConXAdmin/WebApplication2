
using WebApplication2.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using WebPush;

namespace WebApplication2.Hubs
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ChatHub(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public class ConnectedUsers
        {
            public static List<ConnectedUser> Ids = new List<ConnectedUser>();
        }
        public class ConnectedUser
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
            public bool isAdmin { get; set; } = false;
            public List<ConnectedRoom> Rooms { get; set; } = new List<ConnectedRoom>();
        }
        public class ConnectedRoom
        { 
            public string Name { get; set; } = "";
        } 
        public class ConnectedMessages
        {
            public static List<ConnectedMessage> Messages = new List<ConnectedMessage>();
        }
        public class ConnectedMessage
        {
            public string id { get; set; } = "";
            public string name { get; set; } = "";
            public string to { get; set; } = "";
            public string avatar { get; set; } = "";
            public string text { get; set; } = "";
            public string time { get; set; } = "";
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            if (Context.User.Identity.IsAuthenticated)
            {
                var isAdmin = Context.User.IsInRole("Admin");
                var isAdmintext = isAdmin ? " admin" : "";
                await Clients.All.SendAsync("newLog", Context.User.Identity.Name, $"{Context.User.Identity.Name} joined {isAdmintext}");

                ConnectedUsers.Ids.Add(new ConnectedUser { Id = Context.ConnectionId, Name = Context.User.Identity.Name, isAdmin = isAdmin });
                var message = String.Join(",", ConnectedUsers.Ids.Select(x => x.Name));
                await Clients.All.SendAsync("getUsers", Context.User.Identity.Name, message);
                await Clients.Caller.SendAsync("connected", Context.User.Identity.Name);

            }
            else
            {
                await Clients.Caller.SendAsync("connected", "Anonymous");
            }
   

            //await GetMessages();

        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            var user = ConnectedUsers.Ids.Where(x => x.Id == Context.ConnectionId).FirstOrDefault();
            ConnectedUsers.Ids.Remove(user);
            await base.OnDisconnectedAsync(exception);
            if (ConnectedUsers.Ids != null)
            {
                var message = String.Join(",", ConnectedUsers.Ids.Select(x => x.Name));
                await Clients.All.SendAsync("getUsers", Context.User.Identity.Name, message);
            }

            try
            {
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task Join(string module, string username)
        {
            username = Context.User.Identity.IsAuthenticated ? ""+Context.User.Identity.Name : username;

            var usr = ConnectedUsers.Ids.Where(x => x.Id == Context.ConnectionId);
            if (usr != null)
            {
                foreach (var usr2 in usr)
                { 
                     usr2.Rooms.Add(new ConnectedRoom { Name = module });
                }
            }

           await Groups.AddToGroupAsync(Context.ConnectionId, module);
            await Clients.Caller.SendAsync("welcome", "welcome", module, username);
            await Clients.OthersInGroup(module).SendAsync("welcome", "visitor", module, username);
            await GetRooms();

        }
        public async Task Leave(string module, string username)
        {
            username = Context.User.Identity.IsAuthenticated ? Context.User.Identity.Name : username;
            var usr = ConnectedUsers.Ids.Where(x => x.Id == Context.ConnectionId);
            if (usr != null)
            {
                foreach (var usr2 in usr)
                { 
                    usr2.Rooms.Remove(new ConnectedRoom { Name = module });
                }
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, module);

            await Clients.Caller.SendAsync("welcome", "bye", module, username);
            await Clients.OthersInGroup(module).SendAsync("welcome", "left", module, username);


        }
        public async Task GetUsers()
        {
            var message = String.Join(",", ConnectedUsers.Ids.Select(x => x.Name));
            await Clients.Caller.SendAsync("getUsers", Context.User.Identity.Name, message);
        }
        public async Task GetRooms()
        {
 
            var usr = ConnectedUsers.Ids.Where(x => x.Id == Context.ConnectionId);
            if (usr != null)
            {
                var s = "";
                foreach (var usr2 in usr)
                {
                    s += String.Join(",", usr2.Rooms.Select(x => x.Name));
                    await Clients.Caller.SendAsync("welcome", "here is your rooms", s, usr2.Name);


                    var t = ConnectedUsers.Ids.Select(x => $"{x.Id},{x.Name},{String.Join(",", x.Rooms.Select(y => y.Name))}");

                   await Clients.Caller.SendAsync("welcome", "here is all users", s, t);
                }
            }
            else
            {
                await Clients.Caller.SendAsync("welcome", "here is your rooms", "uhm", "who are you?");
            }
        }

        public async Task GetMessages()
        {
            var message = ConnectedMessages.Messages.Where(x => x.name == Context.User.Identity.Name || x.to == Context.User.Identity.Name || x.to == "Public");

 
            await Clients.Caller.SendAsync("getMessages", Context.User.Identity.Name, message);
        }
        public async Task Refresh()
        {
            await Clients.All.SendAsync("refresh", "Admin", "Hallo");
        }
        public async Task SendPrvtMessage(string user, string message, string time)
        {
            ConnectedMessages.Messages.Add(new ConnectedMessage { name = Context.User.Identity.Name, to = user, time = time, text = message });

            var users = ConnectedUsers.Ids.Where(x => x.Name == user);
            await Clients.Clients((IReadOnlyList<string>)users.Select(x => x.Id)).SendAsync("newPrvtMessage", Context.User.Identity.Name, user, message, time);

        }
        public async Task SendMessage(string type, string message, string time)
        {
            ConnectedMessages.Messages.Add(new ConnectedMessage { name = Context.User.Identity.Name, to = "Public", time = time, text = message });
            
            if(type == "newInfoBox") await Clients.Others.SendAsync("newInfoBox", Context.User.Identity.Name, message, time);
            else if (type == "newToast") await Clients.Others.SendAsync("newToast", Context.User.Identity.Name, message, time);
            else if (type == "newDialog") await Clients.Others.SendAsync("newDialog", Context.User.Identity.Name, message, time);
            else await Clients.All.SendAsync("newMessage", Context.User.Identity.Name, message, time);

            //var m = await SendWebPush(Context.User.Identity.Name, message);


        }
        public async Task SendDialog(string to, string message, object options)
        { 
            if (to != "Public")
            {
                var fndusr = ConnectedUsers.Ids.Where(x => x.Name == to).Select(x => x.Id);
                if (fndusr != null)
                {
                    await Clients.Clients(String.Join(",", fndusr)).SendAsync("newDialog", Context.User.Identity.Name, message, "green", "mif-cog", options);
                }
                else
                {
                    await Clients.Caller.SendAsync("newDialog", Context.User.Identity.Name, "Couldnt find user ", options);
                }
            }
            else
            {
                await Clients.All.SendAsync("newDialog", Context.User.Identity.Name, message, "green", "mif-cog", options);
            }
        }
        public async Task SendInfoBox(string to, string message, object options)
        {
            if (to != "Public")
            {
                var fndusr = ConnectedUsers.Ids.Where(x => x.Name == to).Select(x => x.Id);
                if (fndusr != null)
                {
                    await Clients.Clients(String.Join(",", fndusr)).SendAsync("newInfoBox", Context.User.Identity.Name, message, "green", "mif-cog", options);
                }
                else
                {
                    await Clients.Caller.SendAsync("newInfoBox", Context.User.Identity.Name, "Couldnt find user ", "green", "mif-cog", options);
                }
            }
            else
            {
                await Clients.All.SendAsync("newInfoBox", Context.User.Identity.Name, message, "green", "mif-cog", options);
            }
        }
        public async Task SendNotify(string to, string message, object options)
        {
            if (to != "Public")
            {
                var fndusr = ConnectedUsers.Ids.Where(x => x.Name == to).Select(x => x.Id);
                if (fndusr != null)
                {
                    await Clients.Clients(String.Join(",", fndusr)).SendAsync("newNotify", Context.User.Identity.Name, message, "green", "mif-cog", options);
                }
                else
                {
                    await Clients.Caller.SendAsync("newNotify", Context.User.Identity.Name, "Couldnt find user ", "green", "mif-cog", options);
                }
            }
            else
            {
                await Clients.All.SendAsync("newNotify", Context.User.Identity.Name, message, "green", "mif-cog", options);
            }
        }
        public async Task SendToast(string to, string message, object options)
        {
            if (to != "Public")
            {
                var fndusr = ConnectedUsers.Ids.Where(x => x.Name == to).Select(x => x.Id);
                if (fndusr != null)
                {
                    await Clients.Clients(String.Join(",", fndusr)).SendAsync("newToast", Context.User.Identity.Name, message, "green", "mif-cog", options);
                }
                else
                {
                    await Clients.Caller.SendAsync("newToast", Context.User.Identity.Name, "Couldnt find user ", "green", "mif-cog", options);
                }
            }
            else
            {
                await Clients.All.SendAsync("newToast", Context.User.Identity.Name, message, "green", "mif-cog", options);
            }
        }
        public async Task SendWindownotification(string to, string title, string body, object options)
        {
            if (to != "Public")
            {
                var fndusr = ConnectedUsers.Ids.Where(x => x.Name == to).Select(x => x.Id);
                if (fndusr != null)
                {
                    await Clients.Clients(fndusr).SendAsync("newWindownotification", Context.User.Identity.Name, title, body, options);
                    await Clients.Caller.SendAsync("newToast", Context.User.Identity.Name, String.Join(",", fndusr), "green", "mif-cog" );
                }
                else
                { 
                    await Clients.Caller.SendAsync("newWindownotification",  "Couldnt find user ", title, body, options);
                }
            }
            else
            {
                await Clients.All.SendAsync("newWindownotification", Context.User.Identity.Name, title, body,  options);
            }
        }
        //public async Task<string> SendWebPush(string title, string message)
        //{

        //    try
        //    {
        //        var m = "";

        //        var pl = new
        //        {
        //            title = title,
        //            message = message,
        //            icon = "/img/gear2bright.png",
        //            data = new
        //            {
        //                url = "https://cscan.azurewebsites.net/report",
        //                actions = new[] {
        //                    new { action= "openurl",title = "Open",icon= "/img/gear2orange.png", url="https://cscan.azurewebsites.net/setup" },
        //                    new { action= "dismiss",title = "Dismiss",icon= "/img/gear2pink.png", url="https://cscan.azurewebsites.net/admin" }
        //                }
        //            },
        //        };
        //        var payload = JsonConvert.SerializeObject(pl);
        //        var onl = ConnectedUsers.Ids.Select(x => x.Name);
        //        foreach (var device in _context.Devices.Include(x => x.ApplicationUser).Where(x => !onl.Contains(x.ApplicationUser.UserName)))
        //        {
        //            try
        //            {
        //                string vapidPublicKey = _configuration.GetSection("VapidKeys")["PublicKey"];
        //                string vapidPrivateKey = _configuration.GetSection("VapidKeys")["PrivateKey"];

        //                var pushSubscription = new PushSubscription(device.PushEndpoint, device.PushP256DH, device.PushAuth);
        //                var vapidDetails = new VapidDetails("mailto:example@example.com", vapidPublicKey, vapidPrivateKey);

        //                var webPushClient = new WebPushClient();
        //                await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
        //                m = m + device.Name;
        //            }
        //            catch (Exception ex)
        //            {
        //                m = m + device.Name + ex.Message;

        //            }
        //        }
        //        return m;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }



        //}
    }
}