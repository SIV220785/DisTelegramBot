using CommonServiceLocator;
using DisBotTelegram.BLL.DTO;
using DisBotTelegram.BLL.Interfaces;
using DisBotTelegram.BLL.Services;
using DisBotTelegram.PL.Desktop.Model;
using DisBotTelegram.PL.Desktop.ReleyCommand;
using DisBotTelegram.PL.Desktop.Services.WindowFactory;
using DisBotTelegram.PL.Desktop.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace DisBotTelegram.PL.Desktop.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        public IWindowFactory WindowFactory => ServiceLocator.Current.GetInstance<IWindowFactory>();


        private readonly UnityContainer _container;
        private readonly ModelUserService _modelUserService;
        private readonly ModelWorkTimeDispatcherService _modelWorkTimeDispatcherService;
        private string _login;
        private bool _isConnectedAdmin;
        private bool _isConnectedDispatcher;
        private string _password;
        private WorkTimeDispatcherInfo _workTimeDis;



        #endregion

        #region Properties
        internal SynchronizationContext Context { get; private set; }
        public ObservableCollection<UserInfo> Users { get; set; }

        public ObservableCollection<WorkTimeDispatcherInfo> WorksTimeDispatcher { get; set; }

        public UserModel User { get; set; }

        public bool IsConnectedAdmin
        {
            get { return _isConnectedAdmin; }
            set { _isConnectedAdmin = value; OnPropertyChanged(); }
        }
        public bool IsConnectedDispatcher
        {
            get { return _isConnectedDispatcher; }
            set { _isConnectedDispatcher = value; OnPropertyChanged(); }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;

                foreach (var item in Users)
                {
                    if (!item.UserLogin.Equals(_login))
                    {
                        IsConnectedDispatcher = false;
                        IsConnectedAdmin = false;
                        break;
                    }
                }
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        public ICommand EnterLogin { get; set; }
        public OpenWindowCommand OpenWindowCommand { get; private set; }
        public ShowDialogCommand ShowDialogCommand { get; private set; }
        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            _container = new UnityContainer();
            _container.RegisterType<IUserService, UserService>();
            _container.RegisterType<IWorkTimeDispatcherService, WorkTimeDispatcherService>();
            _modelUserService = new ModelUserService(_container);
            _modelWorkTimeDispatcherService = new ModelWorkTimeDispatcherService(_container);

            Users = _modelUserService.GetUsers();
            _workTimeDis = new WorkTimeDispatcherInfo();

            Context = SynchronizationContext.Current;
            ShowDialogCommand = new ShowDialogCommand(PostOpenDialog, PreOpenDialog);
            OpenWindowCommand = new OpenWindowCommand();
            EnterLogin = new RelayCommand(OnEnterLogin);
        }


        #endregion

        #region Commands
        private void OnEnterLogin(object parametr)
        {
            int count = 0;
            foreach (var item in Users)
            {
                if (item.UserLogin.Equals(_login) && item.Password.Equals(_password))
                {
                    if (item.UserLogin.Equals("Admin"))
                    {
                        var windowAdmin = WindowFactory.CreateWindow(new WindowCreationOptions()
                        {
                            WindowSize = new WindowSize(new Size(800, 450)),
                            Title = "Admin menu",
                            Tag = "AdminWindow",
                            SizeToContent = SizeToContent.Height
                        });

                        var adminControl = new AdminControl();
                        windowAdmin.Content = adminControl;
                        Application.Current.MainWindow.Hide();
                        var resultAdmin = windowAdmin.ShowDialog();
                        Application.Current.MainWindow.Show();

                        if (!resultAdmin != true)
                            return;
                        break;
                    }
                    else
                    {
                        var windowDispatcher = WindowFactory.CreateWindow(new WindowCreationOptions()
                        {
                            WindowSize = new WindowSize(new Size(800, 450)),
                            Title = "Dispatcher menu",
                            Tag = "DispatcherWindow",
                            SizeToContent = SizeToContent.Height,

                        });

                        var dispatcharControl = new DispatcherControl();
                        windowDispatcher.Content = dispatcharControl;

                        Application.Current.MainWindow.Hide();
                        var resultDispatcher = windowDispatcher.ShowDialog();
                        Application.Current.MainWindow.Show();

                        if (!resultDispatcher != true)
                            return;
                        break;
                    }
                }
                else
                    count++;
                if (Users.Count == count)
                {
                    IsConnectedAdmin = false;
                    IsConnectedDispatcher = false;
                    MessageBox.Show("Не верно введены данные!!!");
                }
            }



            //var login = Users.Select(x => new { Login = x.UserLogin, Password = x.Password }).Where(x => x.Login.Equals(Login) && x.Password.Equals(Password));//.Where(x=> x.Equals(Password));
            //if (login.Count() != 0)
            //{

            //}
        }

        public void PreOpenDialog()
        {
            foreach (var item in Users)
            {
                if (item.UserLogin.Equals(_login) && item.Password.Equals(_password))
                {
                    IsConnectedDispatcher = true;
                    WorkTimeDispatcherInfo AddworkTimeDB = new WorkTimeDispatcherInfo()
                    {
                        UserId = item.Id,
                        Login = item.UserLogin,
                        EnterDispatchar = DateTime.Now,
                        OutDispatcher = DateTime.Now,
                    };
                    _modelWorkTimeDispatcherService.Add(AddworkTimeDB);
                    break;
                }
            }
            WorksTimeDispatcher = _modelWorkTimeDispatcherService.GetWokrsTime();
            _workTimeDis = WorksTimeDispatcher.Last();
            Application.Current.MainWindow.Hide();
        }

        public void PostOpenDialog(bool? dialogResult)
        {
            Login = String.Empty;
            Password = String.Empty;
            IsConnectedAdmin = false;
            IsConnectedDispatcher = false;
            Users.Clear();
            Users = _modelUserService.GetUsers();
            _workTimeDis.OutDispatcher = DateTime.Now;
            _modelWorkTimeDispatcherService.Updete(_workTimeDis);
            Application.Current.MainWindow.Show();
        }
        #endregion
    }
}

