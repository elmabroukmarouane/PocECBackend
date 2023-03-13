using Poc.Ecare.Server.RealTime.Hubs.Interface;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Poc.Ecare.Server.RealTime.Hubs.Classe
{
    public class RealTimeHub : Hub, IRealTimeHub
    {
        #region ATTRIBUTE
        IHubContext<RealTimeHub> _hubContext;
        private IList<string> ConnectedUsers { get; set; }
        #endregion

        #region CONTRUCTOR
        public RealTimeHub(
            IHubContext<RealTimeHub> hubContext)
        {
            _hubContext = hubContext;
        }
        #endregion

        #region METHODS
        public override async Task OnConnectedAsync()
        {
            ConnectedUsers.Add(Context.ConnectionId);
            await base.OnConnectedAsync();
            var message = "Connected successfully!";
            await Clients.Caller.SendAsync("Message", message);
        }

        public async Task GetConnectedUsersList(int count)
        {
            await _hubContext.Clients.All.SendAsync("CountConnectedUsers", count);
        }

        public async Task SendToAll(object[] entities)
        {
            await _hubContext.Clients.All.SendAsync("SendToAll", entities);
        }

        public async Task SendToSpecifiOnes(object entities, IList<string> specificUsersIds)
        {
            foreach (var userId in specificUsersIds)
            {
                await _hubContext.Clients.Client(userId).SendAsync("SendToSpecifiOnes", entities);
            }
        }

#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        public override Task OnDisconnectedAsync(Exception? exception)
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        {
            ConnectedUsers.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
        #endregion
    }
}
