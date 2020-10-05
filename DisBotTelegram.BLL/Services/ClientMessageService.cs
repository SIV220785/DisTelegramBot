using DisBotTelegram.BLL.Interfaces;
using DisBotTelegram.BLL.Services.Base;
using System;
using System.Collections.Generic;
using DisBotTelegram.BLL.DTO;
using DisBotTelegram.BLL.Helpers;
using AutoMapper;
using DisBotTelegram.DAL.Entities;

namespace DisBotTelegram.BLL.Services
{
    internal class ClientMessageService : BaseService, IClientMessageService
    {

        protected override Action<IMapperConfigurationExpression> MapperCustomConfiguration =>
         cfg =>
         {
             cfg.CreateMap<ClientMessage, ClientMessageInfo>().ReverseMap();
         };

        public ResultOperationInfo<IEnumerable<ClientMessageInfo>> GetAll()
        {
            var collection = UnitOfWork.GetRepository<ClientMessage>().GetAllIncluding(p => p.Client);
            var collectionInfo = MapperInstance.Map<IEnumerable<ClientMessage>, IEnumerable<ClientMessageInfo>>(collection);
            return new ResultOperationInfo<IEnumerable<ClientMessageInfo>>(collectionInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo<ClientMessageInfo> GetId(int itemId)
        {
            var item = UnitOfWork.GetRepository<ClientMessage>().GetIncluding(itemId, p => p.Client);
            var itemInfo = MapperInstance.Map<ClientMessage, ClientMessageInfo>(item);
            return new ResultOperationInfo<ClientMessageInfo>(itemInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Add(ClientMessageInfo itemInfo)
        {
            var item = MapperInstance.Map<ClientMessageInfo, ClientMessage>(itemInfo);
            var addedItem = UnitOfWork.GetRepository<ClientMessage>().Add(item);
            item.Client = null;
            return addedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo<ClientMessageInfo> Create(ClientMessageInfo itemInfo)
        {
            var item = MapperInstance.Map<ClientMessageInfo, ClientMessage>(itemInfo);
            item.Client = null;
            var addedItem = UnitOfWork.GetRepository<ClientMessage>().Add(item);
            var addedItemInfo = MapperInstance.Map<ClientMessage, ClientMessageInfo>(addedItem);
            return addedItem == null
                ? new ResultOperationInfo<ClientMessageInfo>(null, false, Localization.Error_OperationComplited)
                : new ResultOperationInfo<ClientMessageInfo>(addedItemInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Delete(int itemId)
        {
            var deletedRows = UnitOfWork.GetRepository<ClientMessage>().DeleteBy(itemId);
            return deletedRows == 0
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Update(ClientMessageInfo itemInfo)
        {
            var item = MapperInstance.Map<ClientMessageInfo, ClientMessage>(itemInfo);
            var updatedItem = UnitOfWork.GetRepository<ClientMessage>().Update(item, item.Id);
            return updatedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
    }
}
