using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Task1.Classes;

namespace Task1
{
    public class MainWindowViewModel : INotifyPropertyChanged {
        public MainWindowViewModel() {
            this.Port = new Core.SerialPortWrapper();
            this.LogItems = new ObservableCollection<LogItemTemplate>();
            this.TextInput = "Hello World!";
            this.HexInput = "24;0A;";
            this.Port.DataReceived += Port_DataReceived;
            this.Port.ErrorReceived += Port_ErrorReceived;
            this.Port.PinChanged += Port_PinChanged;
            this.Title = "IWSK laboratorium nr 1 - zadanie 1";
            this.wasPing = false;
        }

        private DateTime lastPing;
        private bool wasPing;
        private string _title;

        public string Title {
            get { return _title; }
            set { _title = value; this.Notify("Title"); }
        }

        private ObservableCollection<LogItemTemplate> _logItems;
        public ObservableCollection<LogItemTemplate> LogItems {
            get { return _logItems; }
            set { _logItems = value; this.Notify("LogItems"); }
        }

        private string _textInput;
        public string TextInput {
            get { return _textInput; }
            set { _textInput = value; this.Notify("TextInput"); }
        }

        private string _hexInput;
        public string HexInput {
            get { return _hexInput; }
            set { _hexInput = value; this.Notify("HexInput"); }
        }

        private bool IsConfigured;
        private BackgroundWorker readWorker;
        private BackgroundWorker writeWorker;

        private Core.SerialPortWrapper _port;
        public Core.SerialPortWrapper Port {
            get { return _port; }
            set { _port = value; this.Notify("Port"); }
        }

        ICommand _dtr;
        public ICommand DTR {
            get { return _dtr ?? (_dtr = new RelayCommand(SwitchDTR)); }
        }
        private void SwitchDTR(object obj) {
            try { this.Port.DTR = !this.Port.DTR; this.Notify("Port"); }
            catch { }
        }

        ICommand _rts;
        public ICommand RTS {
            get {
                return _rts ?? (_rts = new RelayCommand(SwitchRTS)); }
        }
        private void SwitchRTS(object obj) {
            try {
                this.Port.RTS = !this.Port.RTS;
                this.Notify("Port");
            }
            catch { }
        }

        ICommand _openPort;
        public ICommand OpenPort {
            get { return _openPort ?? (_openPort = new RelayCommand(OpenPortClick)); }
        }
        private void OpenPortClick(object obj) {
            if(this.Port.IsOpen || this.IsConfigured)
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Error, "Port is already used!"));
            else {
                this.IsConfigured = true;
                this.Port.Open();
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Information, "Just Opened port " + Port.Name));
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Information, "Started listening"));
                this.Title = "RS-232" + ":" + this.Port.Name;
            }
            this.Notify("Port");
        }

        ICommand _refreshLines;
        public ICommand RefreshLines {
            get { return _refreshLines ?? (_refreshLines = new RelayCommand(RefreshLinesClick)); }
        }
        private void RefreshLinesClick(object obj) {
            this.Notify("Port");
        }

        ICommand _writeMsg;
        public ICommand WriteMsg {
            get { return _writeMsg ?? (_writeMsg = new RelayCommand(WriteMsgClick)); }
        }
        private void WriteMsgClick(object obj) {
            if(this.Port.IsOpen && this.IsConfigured) {
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.SendingText, string.Format("\"{0}\"", this.TextInput)));
                this.Port.WriteWithStopCharacters(this.TextInput);
            }
            else
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Error, "Cannot send input, because of port error!"));
        }

        ICommand _writeTransactMsg;
        public ICommand WriteTransact {
            get { return _writeTransactMsg ?? (_writeTransactMsg = new RelayCommand(WriteTransactClick)); }
        }
        private void WriteTransactClick(object obj) {
            this.LogItems.Add(new LogItemTemplate(LogTypesEnum.SendingText, string.Format("\"{0}\"", this.TextInput)));
            this.Port.SimplyTransaction(this.TextInput);
        }

        ICommand _writeHex;
        public ICommand WriteHex {
            get { return _writeHex ?? (_writeHex = new RelayCommand(WriteHexClick)); }
        }
        private void WriteHexClick(object obj) {
            if(this.Port.IsOpen && this.IsConfigured) {
                try {
                    var bytes = this.HexInput.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => (byte)Convert.ToInt32(x, 16)).ToArray();
                    this.Port.WriteData(bytes);
                    this.LogItems.Add(new LogItemTemplate(LogTypesEnum.SendingHex, this.HexInput));
                }
                catch(Exception e) {
                    this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Error, "Hex input not correct!"));
                }
            }
            else {
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Error, "Cannot send input, because of port error!"));
            }
        }

        ICommand _pingPong;
        public ICommand PingPong {
            get { return _pingPong ?? (_pingPong = new RelayCommand(PingPongClick)); }
        }
        private void PingPongClick(object obj) {
            if(this.Port.IsOpen && this.IsConfigured) {
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.SendingPing, "$Ping$"));
                this.wasPing = true;
                this.lastPing = DateTime.Now;
                this.Port.SimplyTransaction("$Ping$");
            }
            else
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Error, "Port is closed!"));
        }

        ICommand _closePort;
        public ICommand ClosePort {
            get { return _closePort ?? (_closePort = new RelayCommand(ClosePortClick)); }
        }
        private void ClosePortClick(object obj) {
            if(this.Port.IsOpen && this.IsConfigured || !this.Port.IsOpen && this.IsConfigured) {
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Information, "Closed port!"));
                this.Port.Close();
                this.IsConfigured = false;
                this.Title = "RS-232";
            }
            else if(!this.IsConfigured && !this.Port.IsOpen) { }
            else
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Error, "Cannot access to port: " + Port.Name));

            this.Notify("Port");
        }

        ICommand _reconfigurePort;
        public ICommand Reconfigure {
            get { return _reconfigurePort ?? (_reconfigurePort = new RelayCommand(ReconfigureClick)); }
        }
        private void ReconfigureClick(object obj) {
            if(this.Port.IsOpen && this.IsConfigured || !this.Port.IsOpen && this.IsConfigured) {
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Information, "Reconfigured port!"));
                this.Port.Close();
                this.Port.Open();
                this.IsConfigured = true;
                this.Title = "RS-232" + ":" + this.Port.Name;
            }
            else if(!this.IsConfigured && !this.Port.IsOpen) {
                this.OpenPortClick(null);
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Information, "Reconfigured port!"));
            }
            else
                this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Information, "Cannot access to port: " + Port.Name));

            this.Notify("Port");
        }

        private void Port_DataReceived(string msg, bool isBuffer) {
            if(isBuffer)
                App.Current.Dispatcher.Invoke(new Action(() => this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Buffer, string.Format("\"{0}\"", msg)))));
            else {
                App.Current.Dispatcher.Invoke(new Action(() => this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Receiving, string.Format("\"{0}\"", msg)))));

                if(msg == "$Ping$") {
                    this.Port.WriteWithStopCharacters("$Pong$");
                    App.Current.Dispatcher.Invoke(new Action(() => this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Information, string.Format("Responding: {0}", "$pong$")))));
                }
                else if(msg == "$Pong$" && this.wasPing) {                  
                    App.Current.Dispatcher.Invoke(new Action(() => this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Other, string.Format("RTD {0}  seconds", ( DateTime.Now - lastPing ).TotalSeconds)))));
                    this.wasPing = false;
                }
            }
        }

        private void Port_PinChanged(string msg) {
            App.Current.Dispatcher.Invoke(new Action(() => this.LogItems.Add(new LogItemTemplate(LogTypesEnum.PinChanged, string.Format("\"{0}\"", msg)))));
            this.Notify("Port");
        }

        private void Port_ErrorReceived(string msg) {
            App.Current.Dispatcher.Invoke(new Action(() => this.LogItems.Add(new LogItemTemplate(LogTypesEnum.Error, string.Format("\"{0}\"", msg)))));
            this.Notify("Port");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName) {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
