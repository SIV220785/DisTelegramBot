using AutoMapper;
using DisBotTelegram.BLL.DTO;
using DisBotTelegram.BLL.Helpers;
using DisBotTelegram.BLL.Interfaces;
using DisBotTelegram.BLL.Services.Base;
using DisBotTelegram.DAL.Entities;
using System;
using System.Collections.Generic;

namespace DisBotTelegram.BLL.Services
{
    internal class WorkTimeDispatcherService : BaseService, IWorkTimeDispatcherService
    {
        protected override Action<IMapperConfigurationExpression> MapperCustomConfiguration =>
         cfg =>
         {
             cfg.CreateMap<WorkTimeDispatcher, WorkTimeDispatcherInfo>().ReverseMap();

         };

        public ResultOperationInfo<IEnumerable<WorkTimeDispatcherInfo>> GetAll()
        {
            var collection = UnitOfWork.GetRepository<WorkTimeDispatcher>().GetAllIncluding();
            var collectionInfo = MapperInstance.Map<IEnumerable<WorkTimeDispatcher>, IEnumerable<WorkTimeDispatcherInfo>>(collection);
            return new ResultOperationInfo<IEnumerable<WorkTimeDispatcherInfo>>(collectionInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo<WorkTimeDispatcherInfo> GetId(int itemId)
        {
            var item = UnitOfWork.GetRepository<WorkTimeDispatcher>().GetIncluding(itemId, p => p.Id);
            var itemInfo = MapperInstance.Map<WorkTimeDispatcher, WorkTimeDispatcherInfo>(item);
            return new ResultOperationInfo<WorkTimeDispatcherInfo>(itemInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Add(WorkTimeDispatcherInfo itemInfo)
        {
            var item = MapperInstance.Map<WorkTimeDispatcherInfo, WorkTimeDispatcher>(itemInfo);
            var addedItem = UnitOfWork.GetRepository<WorkTimeDispatcher>().Add(item);
            return addedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo<WorkTimeDispatcherInfo> Create(WorkTimeDispatcherInfo itemInfo)
        {
            var item = MapperInstance.Map<WorkTimeDispatcherInfo, WorkTimeDispatcher>(itemInfo);
            var addedItem = UnitOfWork.GetRepository<WorkTimeDispatcher>().Add(item);
            var addedItemInfo = MapperInstance.Map<WorkTimeDispatcher, WorkTimeDispatcherInfo>(addedItem);
            return addedItem == null
                ? new ResultOperationInfo<WorkTimeDispatcherInfo>(null, false, Localization.Error_OperationComplited)
                : new ResultOperationInfo<WorkTimeDispatcherInfo>(addedItemInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Delete(int itemId)
        {
            var deletedRows = UnitOfWork.GetRepository<WorkTimeDispatcher>().DeleteBy(itemId);
            return deletedRows == 0
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Update(WorkTimeDispatcherInfo itemInfo)
        {
            var item = MapperInstance.Map<WorkTimeDispatcherInfo, WorkTimeDispatcher>(itemInfo);
            var updatedItem = UnitOfWork.GetRepository<WorkTimeDispatcher>().Update(item, item.Id);
            return updatedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
    }
}
