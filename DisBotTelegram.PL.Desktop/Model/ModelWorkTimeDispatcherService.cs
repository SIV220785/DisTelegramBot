using DisBotTelegram.BLL.DTO;
using DisBotTelegram.BLL.Interfaces;
using System;
using System.Collections.ObjectModel;
using Unity;

namespace DisBotTelegram.PL.Desktop.Model
{
    internal class ModelWorkTimeDispatcherService
    {
        private readonly IWorkTimeDispatcherService _workTimeDispatcherService;
        private ObservableCollection<WorkTimeDispatcherInfo> _workTimeDispatchers;

        public ModelWorkTimeDispatcherService(IUnityContainer container)
        {
            _workTimeDispatcherService = container.Resolve<IWorkTimeDispatcherService>();
            _workTimeDispatchers = new ObservableCollection<WorkTimeDispatcherInfo>();
        }

        public ObservableCollection<WorkTimeDispatcherInfo> GetWokrsTime()
        {
            if (_workTimeDispatchers != null)
            {
                var result = _workTimeDispatcherService.GetAll();
                if (result.IsSuccess)
                {
                    return new ObservableCollection<WorkTimeDispatcherInfo>(result.Value);
                }

                throw new Exception(result.Message);
            }
            return _workTimeDispatchers;
        }
        public void Add(WorkTimeDispatcherInfo itemInfo)
        {
            var result = _workTimeDispatcherService.Add(itemInfo);
            if (result.IsSuccess)
            {
                return;
            }
            throw new Exception(result.Message);
        }
        public void Updete(WorkTimeDispatcherInfo itemInfo)
        {
            var result = _workTimeDispatcherService.Update(itemInfo);
            if (result.IsSuccess)
            {
                return;
            }
            throw new Exception(result.Message);
        }
    }
}

