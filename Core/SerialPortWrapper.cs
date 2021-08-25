using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Core
{
    public class SerialPortWrapper
    {
        #region Fields & Properties

        private SerialPort _port;

        private string temporaryString;
        #endregion

        #region Ctors & DCtors

        public SerialPortWrapper()
        {
            this._port = new SerialPort();
            this._port.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);
            this._port.ErrorReceived += new SerialErrorReceivedEventHandler(_serialPort_ErrorReceived);
            this._port.PinChanged += new SerialPinChangedEventHandler(_serialPort_PinChanged);
            this.StopCharacters = "\r\n";
            this.temporaryString = string.Empty;
            this._port.ReadTimeout = 1000;
            this._port.WriteTimeout = 1000;
        }

        ~SerialPortWrapper()
        {
            _port.DataReceived -= _serialPort_DataReceived;
            if(this._port.IsOpen)
                this._port.Close();

        }
        #endregion

        #region Configuration Properties
        public string Name
        {
            get
            {
                return this._port.PortName;
            }
            set
            {
                this._port.PortName = value;
            }

        }

        public int BaudRate
        {
            get
            {
                return this._port.BaudRate;
            }
            set
            {
                this._port.BaudRate = value;
            }
        }

        public Parity Parity
        {
            get
            {
                return this._port.Parity;
            }
            set
            {
                this._port.Parity = value;
            }
        }

        public int DataBits
        {
            get
            {
                return this._port.DataBits;
            }
            set
            {
                this._port.DataBits = value;
            }
        }

        public StopBits StopBits
        {
            get
            {
                return this._port.StopBits;
            }
            set
            {
                this._port.StopBits = value;
            }
        }

        public Handshake Handshake
        {
            get
            {
                return this._port.Handshake;
            }
            set
            {
                this._port.Handshake = value;
            }
        }

        public int ReadTimeout
        {
            get
            {
                return this._port.ReadTimeout;
            }
            set
            {
                this._port.ReadTimeout = value;
            }
        }

        public int WriteTimeout
        {
            get
            {
                return this._port.WriteTimeout;
            }
            set
            {
                this._port.WriteTimeout = value;
            }
        }

        private string _stopCharacters;
        public string StopCharacters
        {
            get
            {
                return this._stopCharacters;
            }
            set
            {
                this._stopCharacters = value;
            }
        }
        #endregion

        #region Lines Properties

        public bool DTR
        {
            get
            {
                return this._port.DtrEnable;
            }
            set
            {
                this._port.DtrEnable = value;
            }
        }

        public bool DSR
        {
            get
            {
                return this._port.DsrHolding;
            }
        }

        public bool RTS
        {
            get
            {
                return this._port.RtsEnable;
            }
            set
            {
                this._port.RtsEnable = value;
            }
        }


        public bool CTS
        {
            get
            {
                return this._port.CtsHolding;
            }

        }

        public bool CD
        {
            get
            {
                return this._port.CDHolding;
            }
        }

        #endregion

        #region Methods

        public void ModbusWrite(byte [] frame)
        {
            try
            {
                this._port.DataReceived -= _serialPort_DataReceived;

                for(int i = 0; i < frame.Length; i += 2)
                {
                    if(i < frame.Length && i + 1 < frame.Length)
                        this._port.Write(frame, i, 2);
                    else if(i < frame.Length)
                        this._port.Write(frame, i, 1);
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch(Exception e)
            {
                this.ErrorReceived(e.Message.ToString());
            }
            finally
            {
                this._port.DataReceived += _serialPort_DataReceived;
            }
        }
        public void ModbusTransaction(byte[] frame, int count = 0)
        {
            int error = 0;
            try
            {
                this._port.DataReceived -= _serialPort_DataReceived;
                for(int i = 0; i < frame.Length; i += 2)
                {
                    if(i < frame.Length && i + 1 < frame.Length)
                        this._port.Write(frame, i, 2);
                    else if(i < frame.Length)
                        this._port.Write(frame, i, 1);
                    System.Threading.Thread.Sleep(10);
                }
                    
                //this._port.Write(frame, 0, frame.Length);

                string message = _port.ReadTo(this.StopCharacters);
                this.DataReceived(message);
                this.temporaryString = string.Empty;
            }
            catch(Exception e)
            {
                this._port.DiscardInBuffer();
                this._port.DiscardOutBuffer();
                this.ErrorReceived(e.Message.ToString() + " => " + count );
                error++;
            }
            finally
            {
                this._port.DataReceived += _serialPort_DataReceived;
                if(count > 0 && error > 0)
                    this.ModbusTransaction(frame, count - 1);
            }
        }

        public void SimplyTransaction(string msg)
        {
            try
            {
                this._port.DataReceived -= _serialPort_DataReceived;
                this.WriteWithStopCharacters(msg);
                string message = _port.ReadTo(this.StopCharacters);
                this.DataReceived(message);
                this.temporaryString = string.Empty;
            }
            catch(Exception e)
            {
                this._port.DiscardInBuffer();
                this._port.DiscardOutBuffer();
                this.ErrorReceived(e.Message.ToString());
            }
            finally
            {
                this._port.DataReceived += _serialPort_DataReceived;
            }

        }

        public void WriteWithStopCharacters(string msg)
        {
            try
            {
                this._port.Write(msg + this.StopCharacters);
            }
            catch(Exception e)
            {
                this.ErrorReceived(e.Message.ToString());
            }
        }

        public void WriteData(byte[] data)
        {
            try
            {
                this._port.Write(data, 0, data.Count());
            }
            catch(Exception e)
            {
                this.ErrorReceived(e.Message.ToString());
            }
        }

        public void Open()
        {
            try
            {
                this._port.Open();
            }
            catch(Exception e)
            {
                this.ErrorReceived(e.Message.ToString());
            }
        }

        public void Close()
        {
            try
            {
                this._port.Close();
            }
            catch(Exception e)
            {
                this.ErrorReceived(e.Message.ToString());
            }
        }
        public bool IsOpen
        {
            get
            {
                return this._port.IsOpen;
            }

        }

        #endregion

        #region Events

        #region DataReceived
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[this._port.ReadBufferSize];

            int bytesRead = this._port.Read(buffer, 0, buffer.Length);

            var toDecode = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            this.temporaryString += toDecode;

            if(temporaryString.IndexOf(this.StopCharacters) > -1)
            {
                this.temporaryString = this.temporaryString.Remove(this.temporaryString.IndexOf(this.StopCharacters));

                this.DataReceived(this.temporaryString);

                this.temporaryString = string.Empty;
            }
            else
            {
                this.DataReceived(toDecode, true);
            }

        }

        public delegate void DataReceivedHandler(string msg, bool IsBuffer = false);
        public event DataReceivedHandler DataReceived;

        #endregion

        #region ErrorReceived
        private void _serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            this.ErrorReceived(e.EventType.ToString());
        }

        public delegate void ErrorReceivedHandler(string msg);
        public event ErrorReceivedHandler ErrorReceived;

        #endregion

        #region PinChanged
        private void _serialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            this.PinChanged(e.EventType.ToString());
        }

        public delegate void PinChangedHandler(string msg);
        public event PinChangedHandler PinChanged;
        #endregion

        #endregion

    }
}
