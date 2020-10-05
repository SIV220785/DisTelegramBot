using DisBotTelegram.BLL.DTO;
using DisBotTelegram.BLL.Interfaces;
using DisBotTelegram.BLL.Logic;
using DisBotTelegram.BLL.Logic.EventsArgs;
using DisBotTelegram.BLL.Services;
using DisBotTelegram.PL.Desktop.Model;
using DisBotTelegram.PL.Desktop.ReleyCommand;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Unity;

namespace DisBotTelegram.PL.Desktop.ViewModels
{
    public class DispatcherControlModel : BaseViewModel
    {
        #region Fields
        

        private ListBox _mainListBox;
        private readonly LogicBot _botLogic;
        private readonly UnityContainer _container;
        private readonly ModelClientMessageService _modelClientMessageService;
        private readonly ModelClientService _modelClientService;
        private readonly ModelDespatcherMessageService _modelDespatcherMessageService;
        private readonly ModelUserService _modelUserService;


        List<string> fdsfs;

        private bool _isConnect;
        private bool _isDisconnect;
        private bool _isSendMessage;

        private ObservableCollection<ClientInfo> _clients;
        private readonly ObservableCollection<UserInfo> _users;

        private string _message;
        private ObservableCollection<ClientInfo> _clientsChat;

        private ClientInfo _сhoiceClient;
        private UserModel _user;
        private string _userName;

        #endregion

        #region Properties
        public ICommand SendMessageCommand { get; set; }
        public ICommand ConnectCommand { get; set; }
        public ICommand DisconnectCommand { get; set; }
        public ObservableCollection<DisBotMessage> Messages { get; set; }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); }
        }

        public UserModel User
        {
            get { return _user?? (_user = new UserModel()); }
            set{ _user = value; OnPropertyChanged();}
        }

        public ClientInfo ChoiceClient
        {
            get { return _сhoiceClient; }
            set{_сhoiceClient = value; OnPropertyChanged();}
        }

        public string MessageChat
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        public bool IsConnect
        {
            get { return _isConnect; }
            set { _isConnect = value; OnPropertyChanged(); }
        }
        public bool IsDisconnect
        {
            get { return _isDisconnect; }
            set { _isDisconnect = value; OnPropertyChanged(); }
        }
        public bool IsSendMessage
        {
            get { return _isSendMessage; }
            set { _isSendMessage = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ClientInfo> ClientsChat
        {
            get { return _clientsChat; }
            set { _clientsChat = value; OnPropertyChanged(); }
        }

        public ListBox MainListBox
        {
            get { return _mainListBox ?? (_mainListBox = new ListBox()); }
            set { _mainListBox = value; OnPropertyChanged(); }

        }

        internal SynchronizationContext Context { get; private set; }
        #endregion

        #region Ctor
        public DispatcherControlModel()
        {
            _botLogic = new LogicBot();
            _container = new UnityContainer();
            _container.RegisterType<IClientMessageService, ClientMessageService>();
            _container.RegisterType<IClientService, ClientService>();
            _container.RegisterType<IDispatcherMessageService, DispatcherMessageService>();
            _container.RegisterType<IWorkTimeDispatcherService, WorkTimeDispatcherService>();
            _container.RegisterType<IUserService, UserService>();
            _modelClientMessageService = new ModelClientMessageService(_container);
            _modelClientService = new ModelClientService(_container);
            _modelDespatcherMessageService = new ModelDespatcherMessageService(_container);
            _modelUserService = new ModelUserService(_container);

            _clients = _modelClientService.GetClients();
            _users = _modelUserService.GetUsers();
            _clientsChat = new ObservableCollection<ClientInfo>();
            Messages = new ObservableCollection<DisBotMessage>();

            _user = new UserModel();
            _mainListBox = new ListBox();
            _сhoiceClient = new ClientInfo();
            _isConnect = true;


            Context = SynchronizationContext.Current;
            SendMessageCommand = new RelayCommand(OnSendMessageCommandExecute);
            ConnectCommand = new RelayCommand(OnConnectCommand);
            DisconnectCommand = new RelayCommand(OnDisconnectCommand);

            _botLogic.LogStart += BotLogicLogStart;
            UserName = ((MainWindow)Application.Current.MainWindow).tbLogin.Text;
            EventClosedWindow();
            InitUser();
        }

        #endregion

        #region Events
        private void DispatcherViewModel_Closed(object sender, EventArgs e)
        {
            if (IsDisconnect)
            {
                _botLogic.StopReceiving();
            }
        }
        private void BotLogicLogStart(object sender, BotEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AddClientInBdAndDTO(e);
                AddClientInCombaBox(e);

                var message = new DisBotMessage()
                {
                    Content = e.Message.Content,
                    Date = e.Message.Date,
                    LastName = e.Message.LastName,
                    FirstName = e.Message.FirstName,
                    UserName = e.Message.UserName,
                    FullName= e.Message.FirstName,  
                    Type = DisBotMessage.MessageType.OutMessage,
                };

                Messages.Add(message);
                _mainListBox.ScrollIntoView(message);

                _clients = _modelClientService.GetClients();
                var tmpId = 0;
                foreach (var item in _clients)
                {
                    if (item.TelegramId.Equals(e.Message.Id))
                    {
                        tmpId = item.Id;
                        break;
                    }
                }

                var messageDB = new ClientMessageInfo()
                {
                    MessageClient = e.Message.Content,
                    TimeMassage = e.Message.Date,
                    UserId = User.Id,
                    ClientId = tmpId
                };

                _modelClientMessageService.Add(messageDB);

                if (_clients.Count != 0)
                {
                    IsSendMessage = true;
                }
            });
        }
        #endregion

        #region Commands
        private void OnConnectCommand(object obj)
        {
            _mainListBox = obj as ListBox;
            _botLogic.ReciveMessage();
            IsConnect = false;
            IsDisconnect = true;
        }
        private void OnDisconnectCommand(object obj)
        {
            _botLogic.StopReceiving();
            IsConnect = true;
            IsDisconnect = false;
            IsSendMessage = false;
        }
        private void OnSendMessageCommandExecute(object obj)
        {
            if (String.IsNullOrEmpty(MessageChat))
            {
                return;
            }
            else if (IsConnect)
            {
                MessageChat = String.Empty;
                return;
            }
            else if (_clients.Count == 0)
            {
                return;
            }

            if (!String.IsNullOrEmpty(_сhoiceClient.TelegramId))
            {
                _botLogic.Bot_Send_Message(_сhoiceClient.TelegramId, MessageChat);
            }
            else
                _botLogic.Bot_Send_Message(_botLogic.Messages.Id, UserName + ": " + MessageChat);

            var tmpId = 0;
            foreach (var item in _clients)
            {
                if (item.TelegramId.Equals(_botLogic.Messages.Id))
                {
                    tmpId = item.Id;
                    break;
                }
            }
            var message = new DisBotMessage()
            {
                Content = MessageChat,
                Date = DateTime.Now,
                UserName = User.UserName,
                Type = DisBotMessage.MessageType.InMessage,
            };

            Messages.Add(message);
            _mainListBox.ScrollIntoView(message);

            var addDispatcherMessage = new DispatcherMessageInfo()
            {
                ClientId = tmpId,
                MessageDispather = MessageChat,
                TimeMassage = DateTime.Now,
                UserId = User.Id
            };
            _modelDespatcherMessageService.Add(addDispatcherMessage);
            MessageChat = String.Empty;
        }
        #endregion

        #region Metods
        private void AddClientInBdAndDTO(BotEventArgs e)
        {
            var countClientDB = 0;

            if (_clients.Count == 0)
            {
                var addClient = new ClientInfo()
                {
                    FirstName = e.Message.FirstName,
                    LastName = e.Message.LastName,
                    TelegramId = e.Message.Id,
                    Username = e.Message.UserName,                    
                };

                _modelClientService.Add(addClient);
                _clients.Add(addClient);
                _clientsChat.Add(addClient);
            }
            foreach (var client in _clients)
            {

                if (client.TelegramId.Equals(_botLogic.Messages.Id))
                {
                    break;
                }
                else if (!string.IsNullOrEmpty(client.TelegramId))
                {
                    countClientDB++;
                }

                if (_clients.Count == countClientDB)
                {
                    var addClient = new ClientInfo()
                    {
                        FirstName = e.Message.FirstName,
                        LastName = e.Message.LastName,
                        TelegramId = e.Message.Id,
                        Username = e.Message.UserName,
                    };
                    _modelClientService.Add(addClient);
                    _clients.Add(addClient);
                    _clientsChat.Add(addClient);
                    break;
                }
            }
        }
        private void AddClientInCombaBox(BotEventArgs e)
        {
            var coutClientList = 0;
            var addClientList = new ClientInfo()
            {
                FirstName = e.Message.FirstName,
                LastName = e.Message.LastName,
                TelegramId = e.Message.Id,
                Username = e.Message.UserName,
            };

            if (_clientsChat.Count == 0)
            {
                _clientsChat.Add(addClientList);
            }

            foreach (var item in _clientsChat)
            {
                if (item.TelegramId.Equals(e.Message.Id))
                {
                    break;
                }
                else if (!item.TelegramId.Equals(e.Message.Id))
                {
                    coutClientList++;
                }

                if (coutClientList == _clientsChat.Count())
                {
                    _clientsChat.Add(addClientList);
                    break;
                }
            }
        }
        
        private void EventClosedWindow()
        {
            for (int i = 0; i < Application.Current.Windows.Count; i++)
            {
                var window = Application.Current.Windows[i];
                if (window.Tag == null)
                {
                    continue;
                }
                else if (window.Tag.Equals("DispatcherWindow"))
                {
                    Application.Current.Windows[i].Closing += DispatcherViewModel_Closed;
                    break;
                }
            }
        }
        #endregion

        private void InitUser()
        {
            var userChoise = _users.Where(x => x.UserLogin.Equals(UserName));
            foreach (var item in userChoise)
            {
                User.Id = item.Id;
                User.LastName = item.LastName;
                User.FirstName = item.FirstName;
                User.UserName = item.UserLogin;
            }
        }
    }
}
