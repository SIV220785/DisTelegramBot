using DisBotTelegram.BLL.DTO;
using DisBotTelegram.BLL.Interfaces;
using System;
using System.Collections.ObjectModel;
using Unity;

namespace DisBotTelegram.PL.Desktop.Model
{
    public class ModelDespatcherMessageService
    {
        private readonly IDispatcherMessageService _iDispatcherMessageService;
        private ObservableCollection<DispatcherMessageInfo> _dispatcheMessages;

        public ModelDespatcherMessageService(IUnityContainer container)
        {
            _iDispatcherMessageService = container.Resolve<IDispatcherMessageService>();
            _dispatcheMessages = new ObservableCollection<DispatcherMessageInfo>();
        }

        public ObservableCollection<DispatcherMessageInfo> GetDispatcharMessage()
        {
            if (_dispatcheMessages!=null)
            {
                var result = _iDispatcherMessageService.GetAll();
                if (result.IsSuccess)
                {
                    return new ObservableCollection<DispatcherMessageInfo>(result.Value);
                }
            }
            return _dispatcheMessages;
        }
        public void Add(DispatcherMessageInfo itemInfo)
        {
            var result = _iDispatcherMessageService.Add(itemInfo);
            if (result.IsSuccess)
            {
                return;
            }
            throw new Exception(result.Message);
        }
    }
}
