using DisBotTelegram.BLL.DTO;
using DisBotTelegram.BLL.Interfaces;
using System;
using System.Collections.ObjectModel;
using Unity;

namespace DisBotTelegram.PL.Desktop.Model
{
    public class ModelClientService
    {
        private readonly IClientService _clientService;
        private ObservableCollection<ClientInfo> _clients;

        public ModelClientService(IUnityContainer container)
        {
            _clientService = container.Resolve<IClientService>();
            _clients = new ObservableCollection<ClientInfo>();
        }

        public ObservableCollection<ClientInfo> GetClients()
        {
            if (_clients != null)
            {
                var result = _clientService.GetAll();
                if (result.IsSuccess)
                {
                    return new ObservableCollection<ClientInfo>(result.Value);
                }
            }
            return _clients;
        }
        public void Add(ClientInfo itemInfo)
        {
            var result = _clientService.Add(itemInfo);
            if (result.IsSuccess)
            {
                return;
            }
            throw new Exception(result.Message);
        }
    }
}
