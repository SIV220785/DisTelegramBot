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
    internal class DispatcherMessageService : BaseService, IDispatcherMessageService
    {
        protected override Action<IMapperConfigurationExpression> MapperCustomConfiguration =>
         cfg =>
         {
             cfg.CreateMap<DispatherMessage, DispatcherMessageInfo>().ReverseMap();
         };

        public ResultOperationInfo<IEnumerable<DispatcherMessageInfo>> GetAll()
        {
            var collection = UnitOfWork.GetRepository<DispatherMessage>().GetAllIncluding(p => p.User);
            var collectionInfo = MapperInstance.Map<IEnumerable<DispatherMessage>, IEnumerable<DispatcherMessageInfo>>(collection);
            return new ResultOperationInfo<IEnumerable<DispatcherMessageInfo>>(collectionInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo<DispatcherMessageInfo> GetId(int itemId)
        {
            var item = UnitOfWork.GetRepository<DispatherMessage>().GetIncluding(itemId, p => p.User);
            var itemInfo = MapperInstance.Map<DispatherMessage, DispatcherMessageInfo>(item);
            return new ResultOperationInfo<DispatcherMessageInfo>(itemInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Add(DispatcherMessageInfo itemInfo)
        {
            var item = MapperInstance.Map<DispatcherMessageInfo, DispatherMessage>(itemInfo);
            item.User = null;
            var addedItem = UnitOfWork.GetRepository<DispatherMessage>().Add(item);
            return addedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo<DispatcherMessageInfo> Create(DispatcherMessageInfo itemInfo)
        {
            var item = MapperInstance.Map<DispatcherMessageInfo, DispatherMessage>(itemInfo);
            item.User = null;
            var addedItem = UnitOfWork.GetRepository<DispatherMessage>().Add(item);
            var addedItemInfo = MapperInstance.Map<DispatherMessage, DispatcherMessageInfo>(addedItem);
            return addedItem == null
                ? new ResultOperationInfo<DispatcherMessageInfo>(null, false, Localization.Error_OperationComplited)
                : new ResultOperationInfo<DispatcherMessageInfo>(addedItemInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Delete(int itemId)
        {
            var deletedRows = UnitOfWork.GetRepository<DispatherMessage>().DeleteBy(itemId);
            return deletedRows == 0
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Update(DispatcherMessageInfo itemInfo)
        {
            var item = MapperInstance.Map<DispatcherMessageInfo, DispatherMessage>(itemInfo);
            var updatedItem = UnitOfWork.GetRepository<DispatherMessage>().Update(item, item.Id);
            return updatedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
    }
}
