using DisBotTelegram.BLL.DTO;
using DisBotTelegram.BLL.Helpers;
using System.Collections.Generic;

namespace DisBotTelegram.BLL.Interfaces
{
    internal interface IWorkTimeDispatcherService
    {
        ResultOperationInfo<IEnumerable<WorkTimeDispatcherInfo>> GetAll();
        ResultOperationInfo<WorkTimeDispatcherInfo> GetId(int itemId);
        ResultOperationInfo Add(WorkTimeDispatcherInfo item);
        ResultOperationInfo Update(WorkTimeDispatcherInfo item);
        ResultOperationInfo Delete(int itemId);
        ResultOperationInfo<WorkTimeDispatcherInfo> Create(WorkTimeDispatcherInfo itemInfo);
    }
}
