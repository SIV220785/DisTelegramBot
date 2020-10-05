using DisBotTelegram.BLL.DTO;
using DisBotTelegram.BLL.Interfaces;
using DisBotTelegram.BLL.Services;
using DisBotTelegram.PL.Desktop.Helper;
using DisBotTelegram.PL.Desktop.Model;
using DisBotTelegram.PL.Desktop.ReleyCommand;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace DisBotTelegram.PL.Desktop.ViewModels
{
    public class AdminControlModel : BaseViewModel
    {
        #region Fields        
        private readonly UnityContainer _container;
        private readonly ModelUserService _modelUserService;
        private readonly ModelClientService _modelClientService;
        private readonly ModelClientMessageService _modelClientMessageService;
        private readonly ModelDespatcherMessageService _modelDespatcherMessageService;

        private ObservableCollection<MessageAll> _messages;
        private ObservableCollection<UserInfo> _users;
        private ObservableCollection<ClientInfo> _clients;
        private ObservableCollection<ClientMessageInfo> _clentMessages;
        private ObservableCollection<DispatcherMessageInfo> _dispatcherMessages;

        private UserInfo _userLinq;
        private ClientInfo _clientLinq;
        private DateTime _fromDate;
        private DateTime _toDate;
        private string _login;
        private string _password;
        private string _repeatPassword;
        private string _firstName;
        private string _lastName;
        private bool _isChackLogin;
        private string _checkLoginText;
        private string _checkPaswwordText;
        #endregion

        #region Properties
        internal SynchronizationContext Context { get; private set; }

        public ObservableCollection<MessageAll> Messages
        {
            get { return _messages?? (_messages = new ObservableCollection<MessageAll>()); }
            set { _messages = value; OnPropertyChanged(); }
        }
        public ObservableCollection<UserInfo> Users
        {
            get { return _users ?? (_users = new ObservableCollection<UserInfo>()); }
            set { _users = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ClientInfo> Clients
        {
            get { return _clients ?? (_clients = new ObservableCollection<ClientInfo>()); }
            set { _clients = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ClientMessageInfo> ClentMessages
        {
            get { return _clentMessages ?? (_clentMessages = new ObservableCollection<ClientMessageInfo>()); }
            set { _clentMessages = value; OnPropertyChanged(); }
        }
        public ObservableCollection<DispatcherMessageInfo> DispatcherMessages
        {
            get { return _dispatcherMessages ?? (_dispatcherMessages = new ObservableCollection<DispatcherMessageInfo>()); }
            set { _dispatcherMessages = value; OnPropertyChanged(); }
        }
        public UserInfo UserLinq
        {
            get { return _userLinq; }
            set { _userLinq = value; OnPropertyChanged(); }
        }
        public ClientInfo ClientLinq
        {
            get { return _clientLinq; }
            set { _clientLinq = value; OnPropertyChanged(); }
        }
        public DateTime FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; OnPropertyChanged(); }
        }
        public DateTime ToDate
        {
            get { return _toDate; }
            set { _toDate = value; OnPropertyChanged(); }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                foreach (var item in Users)
                {
                    if (item.UserLogin.Equals(Login))
                    {

                        IsCheckLogin = false;
                        CheckLoginText = "This login exists!";
                        break;
                    }
                    else if (String.IsNullOrEmpty(Login))
                    {
                        IsCheckLogin = false;
                        CheckLoginText = "Enter Login!";
                    }
                    else
                    {
                        IsCheckLogin = true;
                        CheckLoginText = "This login don`t exists!";
                    }

                }
                OnPropertyChanged();
                OnPropertyChanged("IsCorrectPassword");
            }
        }
        public string Paswword
        {
            get { return _password; }
            set
            {
                _password = value; OnPropertyChanged();
                OnPropertyChanged("IsCorrectPassword");
            }
        }
        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set
            {
                _repeatPassword = value;
                if (Paswword.Equals(RepeatPassword))
                {
                    // IsCorrectPassword = true;
                    CheckPaswwordText = "Password is correct";
                }
                else if (String.IsNullOrEmpty(RepeatPassword))
                {
                    // IsCorrectPassword = false;
                    CheckPaswwordText = "Password entry is`t correct";
                }
                else
                {
                    // IsCorrectPassword = false;
                    CheckPaswwordText = "Enter сonfirm зassword";
                }
                OnPropertyChanged();
                OnPropertyChanged("IsCorrectPassword");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(); }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }
        public string CheckLoginText
        {
            get { return _checkLoginText; }
            set { _checkLoginText = value; OnPropertyChanged(); }
        }
        public string CheckPaswwordText
        {
            get { return _checkPaswwordText; }
            set { _checkPaswwordText = value; OnPropertyChanged(); }
        }
        public bool IsCheckLogin
        {
            get { return _isChackLogin; }
            set { _isChackLogin = value; OnPropertyChanged(); }
        }
        public bool IsCorrectPassword
        {
            get { return !String.IsNullOrWhiteSpace(Paswword) && !String.IsNullOrWhiteSpace(RepeatPassword) && Paswword.Equals(RepeatPassword); }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand OkCommandLinq { get; set; }

        #endregion

        #region Ctor
        public AdminControlModel()
        {
            _container = new UnityContainer();
            _container.RegisterType<IUserService, UserService>();
            _container.RegisterType<IClientService, ClientService>();
            _container.RegisterType<IClientMessageService, ClientMessageService>();
            _container.RegisterType<IDispatcherMessageService, DispatcherMessageService>();
            _modelUserService = new ModelUserService(_container);
            _modelClientService = new ModelClientService(_container);
            _modelClientMessageService = new ModelClientMessageService(_container);
            _modelDespatcherMessageService = new ModelDespatcherMessageService(_container);
            
            _messages = new ObservableCollection<MessageAll>();

            _users = _modelUserService.GetUsers();
            _clients = _modelClientService.GetClients();
            _clentMessages = _modelClientMessageService.GetClientMessage();
            _dispatcherMessages = _modelDespatcherMessageService.GetDispatcharMessage();

            _fromDate = new DateTime(2019, 05, 01);
            _toDate = DateTime.Now;
            _checkLoginText = "Enter Login!";

            OkCommandLinq = new RelayCommand(OnCommandLinq);
            SaveCommand = new RelayCommand(OnSaveCommand, CanOnSaveExecute);
        }

        
        #endregion

        #region Commands
        private bool CanOnSaveExecute(object obj)
        {
            return IsCorrectPassword && IsCheckLogin;
        }
        private void OnSaveCommand(object obj)
        {
            if (IsCheckLogin)
            {
                UserInfo tmpUserinfo = new UserInfo()
                {
                    UserLogin = Login,
                    Password = this.Paswword,
                    FirstName = this.FirstName,
                    LastName = this.LastName

                };
                _modelUserService.Add(tmpUserinfo);
                _users.Add(tmpUserinfo);
                Login = String.Empty;
                Paswword = String.Empty;
                RepeatPassword = String.Empty;
                FirstName = String.Empty;
                LastName = String.Empty;
                return;
            }
            else
            {
                MessageBox.Show("Erorr");
            }
        }
        private void OnCommandLinq(object obj)
        {

            Messages.Clear();
            _toDate += new TimeSpan(23, 59, 59);

            if (UserLinq != null && ClientLinq != null)
            {
                var allMessages = Users.Where(user => user.Id == UserLinq.Id)
                    .SelectMany(a => a.Messages)
                    .Select(p => new { name = UserLinq.UserLogin, dt = p.TimeMassage, message = p.MessageDispather, userId = p.UserId, clientId = p.ClientId })
                    .Union(Clients.Where(client => client.Id == ClientLinq.Id)
                                                                              .SelectMany(a => a.Messages)
                                                                              .Select(p => new { name = ClientLinq.TelegramId, dt = p.TimeMassage, message = p.MessageClient, userId = p.UserId, clientId = p.ClientId }))
                    .Where(p => p.name == UserLinq.UserLogin || p.name == ClientLinq.TelegramId)
                    .Where(p => p.dt >= FromDate && p.dt <= ToDate)
                    .Where(p => p.userId == UserLinq.Id && p.clientId == ClientLinq.Id)
                    .OrderBy(x => x.dt).ToList();

                foreach (var item in allMessages)
                {
                    MessageAll tempMessage = new MessageAll()
                    {
                        DateTime = item.dt,
                        Message = item.message,
                        UserName = item.name
                    };
                    Messages.Add(tempMessage);
                }
            }
            else if (ClientLinq != null && UserLinq == null)
            {
                MessageBox.Show("Не верно введены данные!!!");
                return;
            }
            else if (ClientLinq == null && UserLinq != null)
            {
                MessageBox.Show("Не верно введены данные!!!");
                return;
            }
            else
            {
                MessageBox.Show("Не верно введены данные!!!");
                return;
            }
        }
        #endregion

       
    }
}
