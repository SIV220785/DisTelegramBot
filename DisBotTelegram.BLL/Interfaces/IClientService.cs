using DisBotTelegram.BLL.DTO;
using DisBotTelegram.BLL.Helpers;
using System.Collections.Generic;

namespace DisBotTelegram.BLL.Interfaces
{
    internal interface IClientService
    {
        ResultOperationInfo<IEnumerable<ClientInfo>> GetAll();
        ResultOperationInfo<ClientInfo> GetId(int itemId);
        ResultOperationInfo Add(ClientInfo item);
        ResultOperationInfo Update(ClientInfo item);
        ResultOperationInfo Delete(int itemId);
        ResultOperationInfo<ClientInfo> Create(ClientInfo itemInfo);
    }
}
