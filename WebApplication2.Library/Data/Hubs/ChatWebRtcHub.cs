using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace DEV.Hubs
{
    // Client API:
    // 
    // updateUserList(List<User> userList)
    // callAccepted(User acceptingUser)
    // callDeclined(User decliningUser, string reason)
    // incomingCall(User callingUser)
    // receiveSignal(User signalingUser, string signal)

    public class WebRtcHub : Hub<IConnectionHub>
    {
 

        public class ConnectedUsers
        {
            public static   List<User> Users = new List<User>();
            public static   List<UserCall> UserCalls = new List<UserCall>();
            public static   List<CallOffer> CallOffers = new List<CallOffer>();
        }
 
        public class CallOffer
        {
            public User Caller { get; set; } = new User();
            public User Callee { get; set; } = new User();
        }
        public class User
        {
            public string Username { get; set; } = "";
            public string ConnectionId { get; set; } = "";
            public bool InCall { get; set; } = false;
        }
        public class UserCall
        {
            public List<User> Users { get; set; }=new List<User>();
        }


        public void Join(string username)
        {
            ConnectedUsers.Users.Add(new User
            {
                Username = username,
                ConnectionId = Context.ConnectionId,
                InCall = false
            });

 

            // Send down the new list to all clients
            SendUserListUpdate();
        }

        public override System.Threading.Tasks.Task OnDisconnectedAsync(Exception? exception)
        {
            // Hang up any calls the user is in
            HangUp(); // Gets the user from "Context" which is available in the whole hub

            // Remove the user 
            ConnectedUsers.Users.RemoveAll(u => u.ConnectionId == Context.ConnectionId);

            // Send down the new user list to all clients
            SendUserListUpdate();

            return base.OnDisconnectedAsync( exception);
        }
        public async Task SendMessage(string message)
        {
            await Clients.All.newMessage(Context.User.Identity.Name, message);
        }
        public void CallUser(string targetConnectionId)
        {
            var callingUser = ConnectedUsers.Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = ConnectedUsers.Users.SingleOrDefault(u => u.ConnectionId == targetConnectionId);

            // Make sure the person we are trying to call is still here
            if (targetUser == null)
            {
                // If not, let the caller know
                Clients.Caller.CallDeclined(targetUser, "The user you called has left.");
                return;
            }

            // And that they aren't already in a call
            if (GetUserCall(targetUser.ConnectionId) != null)
            {
                Clients.Caller.CallDeclined(targetUser, string.Format("{0} is already in a call.", targetUser.Username));
                return;
            }

            // They are here, so tell them someone wants to talk
            Clients.Client(targetConnectionId).IncomingCall(callingUser);

            // Create an offer
            ConnectedUsers.CallOffers.Add(new CallOffer {
                Caller = ConnectedUsers.Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId),
                Callee = ConnectedUsers.Users.SingleOrDefault(u => u.ConnectionId == targetConnectionId)
            });
        }

        public void AnswerCall(bool acceptCall, string targetConnectionId)
        {
            var callingUser = ConnectedUsers.Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = ConnectedUsers.Users.SingleOrDefault(u => u.ConnectionId == targetConnectionId); 
            // This can only happen if the server-side came down and clients were cleared, while the user
            // still held their browser session.
            if (callingUser == null)
            {
                return;
            }

            // Make sure the original caller has not left the page yet
            if (targetUser == null)
            {
                Clients.Caller.CallEnded(targetUser, "The other user in your call has left.");
                return;
            }

            // Send a decline message if the callee said no
            if (acceptCall == false) 
            {
                Clients.Client(targetConnectionId).CallDeclined(callingUser, string.Format("{0} did not accept your call.", callingUser.Username));
                return;
            }

            // Make sure there is still an active offer.  If there isn't, then the other use hung up before the Callee answered.
            var offerCount = ConnectedUsers.CallOffers.RemoveAll(c => c.Callee.ConnectionId == callingUser.ConnectionId
                                                  && c.Caller.ConnectionId == targetUser.ConnectionId);
            if (offerCount < 1)
            {
                Clients.Caller.CallEnded(targetUser, string.Format("{0} has already hung up.", targetUser.Username));
                return;
            }

            // And finally... make sure the user hasn't accepted another call already
            if (GetUserCall(targetUser.ConnectionId) != null)
            {
                // And that they aren't already in a call
                Clients.Caller.CallDeclined(targetUser, string.Format("{0} chose to accept someone elses call instead of yours :(", targetUser.Username));
                return;
            }

            // Remove all the other offers for the call initiator, in case they have multiple calls out
            ConnectedUsers.CallOffers.RemoveAll(c => c.Caller.ConnectionId == targetUser.ConnectionId);

            // Create a new call to match these folks up
            ConnectedUsers.UserCalls.Add(new UserCall
            {
                Users = new List<User> { callingUser, targetUser }
            });

            // Tell the original caller that the call was accepted
            Clients.Client(targetConnectionId).CallAccepted(callingUser);

            // Update the user list, since thes two are now in a call
            SendUserListUpdate();
        }

        public void HangUp()
        {
            var callingUser = ConnectedUsers.Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);

            if (callingUser == null)
            {
                return;
            }

            var currentCall = GetUserCall(callingUser.ConnectionId);

            // Send a hang up message to each user in the call, if there is one
            if (currentCall != null)
            {
                foreach (var user in currentCall.Users.Where(u => u.ConnectionId != callingUser.ConnectionId))
                {
                    Clients.Client(user.ConnectionId).CallEnded(callingUser, string.Format("{0} has hung up.", callingUser.Username));
                }

                // Remove the call from the list if there is only one (or none) person left.  This should
                // always trigger now, but will be useful when we implement conferencing.
                currentCall.Users.RemoveAll(u => u.ConnectionId == callingUser.ConnectionId);
                if (currentCall.Users.Count < 2)
                {
                    ConnectedUsers.UserCalls.Remove(currentCall);
                }
            }

            // Remove all offers initiating from the caller
            ConnectedUsers.CallOffers.RemoveAll(c => c.Caller.ConnectionId == callingUser.ConnectionId);

            SendUserListUpdate();
        }

        // WebRTC Signal Handler
        public void SendSignal(string signal, string targetConnectionId)
        {
            var callingUser = ConnectedUsers.Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
            var targetUser = ConnectedUsers.Users.SingleOrDefault(u => u.ConnectionId == targetConnectionId);

            // Make sure both users are valid
            if (callingUser == null || targetUser == null)
            {
                return;
            }

            // Make sure that the person sending the signal is in a call
            var userCall = GetUserCall(callingUser.ConnectionId);

            // ...and that the target is the one they are in a call with
            if (userCall != null && userCall.Users.Exists(u => u.ConnectionId == targetUser.ConnectionId))
            {
                // These folks are in a call together, let's let em talk WebRTC
                Clients.Client(targetConnectionId).ReceiveSignal(callingUser, signal);
            }
        }

        #region Private Helpers

        private void SendUserListUpdate()
        { 
            ConnectedUsers.Users.ForEach(u => u.InCall = (GetUserCall(u.ConnectionId) != null));
             
            Clients.All.UpdateUserList(ConnectedUsers.Users);
         } 
        private UserCall GetUserCall(string connectionId)
        {
            var matchingCall =
                ConnectedUsers.UserCalls.SingleOrDefault(uc => uc.Users.SingleOrDefault(u => u.ConnectionId == connectionId) != null);
            return matchingCall;
        }

        #endregion

    }
    public interface IConnectionHub
    {
        Task UpdateUserList(List<WebRtcHub.User> userList);
        Task CallAccepted(WebRtcHub.User acceptingUser);
        Task newMessage(string username,string message); 
        Task CallDeclined(WebRtcHub.User decliningUser, string reason);
        Task IncomingCall(WebRtcHub.User callingUser);
        Task ReceiveSignal(WebRtcHub.User signalingUser, string signal);
        Task CallEnded(WebRtcHub.User signalingUser, string signal);
    }

}