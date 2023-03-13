using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.Ecare.Server.RealTime.Hubs.Interface
{
    public interface IRealTimeHub
    {
        Task SendToAll(object[] entities);
        Task SendToSpecifiOnes(object entities, IList<string> specificUsersIds);
        Task GetConnectedUsersList(int count);

    }
}
