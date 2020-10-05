using DisBotTelegram.BLL.DTO;
using DisBotTelegram.BLL.Interfaces;
using System;
using System.Collections.ObjectModel;
using Unity;

namespace DisBotTelegram.PL.Desktop.Model
{
    internal class ModelUserService
    {
        private readonly IUserService _userService;
        private ObservableCollection<UserInfo> _clients;

        public ModelUserService(IUnityContainer container)
        {
            _userService = container.Resolve<IUserService>();
            _clients = new ObservableCollection<UserInfo>();
        }

        public ObservableCollection<UserInfo> GetUsers()
        {
            if (_clients != null)
            {
                var result = _userService.GetAll();
                if (result.IsSuccess)
                {
                    return new ObservableCollection<UserInfo>(result.Value);
                }

                throw new Exception(result.Message);
            }
            return _clients;
        }
        public void Add(UserInfo itemInfo)
        {
            var result = _userService.Add(itemInfo);
            if (result.IsSuccess)
            {
                return;
            }
            throw new Exception(result.Message);
        }
    }
}
