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
    internal class ClientService : BaseService, IClientService
    {
        protected override Action<IMapperConfigurationExpression> MapperCustomConfiguration =>
         cfg =>
         {
             cfg.CreateMap<Client, ClientInfo>()
             .ForPath(x => x.Messages, m => m.MapFrom(a => a.ClientMessages))
             .ReverseMap();
         };

        public ResultOperationInfo<IEnumerable<ClientInfo>> GetAll()
        {
            var collection = UnitOfWork.GetRepository<Client>().GetAllIncluding(p => p.ClientMessages);
            var collectionInfo = MapperInstance.Map<IEnumerable<Client>, IEnumerable<ClientInfo>>(collection);
            return new ResultOperationInfo<IEnumerable<ClientInfo>>(collectionInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo<ClientInfo> GetId(int itemId)
        {
            var item = UnitOfWork.GetRepository<Client>().GetIncluding(itemId, p => p.ClientMessages);
            var itemInfo = MapperInstance.Map<Client, ClientInfo>(item);
            return new ResultOperationInfo<ClientInfo>(itemInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Add(ClientInfo itemInfo)
        {
            var item = MapperInstance.Map<ClientInfo, Client>(itemInfo);
            var addedItem = UnitOfWork.GetRepository<Client>().Add(item);
            return addedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo<ClientInfo> Create(ClientInfo itemInfo)
        {
            var item = MapperInstance.Map<ClientInfo, Client>(itemInfo);
            var addedItem = UnitOfWork.GetRepository<Client>().Add(item);
            var addedItemInfo = MapperInstance.Map<Client, ClientInfo>(addedItem);
            return addedItem == null
                ? new ResultOperationInfo<ClientInfo>(null, false, Localization.Error_OperationComplited)
                : new ResultOperationInfo<ClientInfo>(addedItemInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Delete(int itemId)
        {
            var deletedRows = UnitOfWork.GetRepository<Client>().DeleteBy(itemId);
            return deletedRows == 0
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Update(ClientInfo itemInfo)
        {
            var item = MapperInstance.Map<ClientInfo, Client>(itemInfo);
            var updatedItem = UnitOfWork.GetRepository<Client>().Update(item, item.Id);
            return updatedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
    }
}
