using CommonServiceLocator;
using DisBotTelegram.PL.Desktop.Services.WindowFactory;
using System.Windows;
using Unity;
using Unity.ServiceLocation;

namespace DisBotTelegram.PL.Desktop
{

    public partial class App 
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Регистрация сервисов
            var container = new UnityContainer();
            container.RegisterSingleton<IWindowFactory, WindowFactory>();

            // настройка статического класса  - ServiceLocator
            UnityServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
