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
    internal class UserService : BaseService, IUserService
    {
        protected override Action<IMapperConfigurationExpression> MapperCustomConfiguration =>
         cfg =>
         {
             cfg.CreateMap<User, UserInfo>()
             .ForPath(x => x.Messages, m => m.MapFrom(a => a.DispatherMessages))
             .ReverseMap();
         };

        public ResultOperationInfo<IEnumerable<UserInfo>> GetAll()
        {
            var collection = UnitOfWork.GetRepository<User>().GetAllIncluding(p => p.DispatherMessages);
            var collectionInfo = MapperInstance.Map<IEnumerable<User>, IEnumerable<UserInfo>>(collection);
            return new ResultOperationInfo<IEnumerable<UserInfo>>(collectionInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo<UserInfo> GetId(int itemId)
        {
            var item = UnitOfWork.GetRepository<User>().GetIncluding(itemId, p => p.DispatherMessages);
            var itemInfo = MapperInstance.Map<User, UserInfo>(item);
            return new ResultOperationInfo<UserInfo>(itemInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Add(UserInfo itemInfo)
        {
            var item = MapperInstance.Map<UserInfo, User>(itemInfo);
            var addedItem = UnitOfWork.GetRepository<User>().Add(item);
            return addedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo<UserInfo> Create(UserInfo itemInfo)
        {
            var item = MapperInstance.Map<UserInfo, User>(itemInfo);
            item.DispatherMessages = null;
            var addedItem = UnitOfWork.GetRepository<User>().Add(item);
            var addedItemInfo = MapperInstance.Map<User, UserInfo>(addedItem);
            return addedItem == null
                ? new ResultOperationInfo<UserInfo>(null, false, Localization.Error_OperationComplited)
                : new ResultOperationInfo<UserInfo>(addedItemInfo, true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Delete(int itemId)
        {
            var deletedRows = UnitOfWork.GetRepository<User>().DeleteBy(itemId);
            return deletedRows == 0
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
        public ResultOperationInfo Update(UserInfo itemInfo)
        {
            var item = MapperInstance.Map<UserInfo, User>(itemInfo);
            var updatedItem = UnitOfWork.GetRepository<User>().Update(item, item.Id);
            return updatedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
    }
}
