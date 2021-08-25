using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace UI
{
    public class SerialPortDictionaries
    {
        #region Name
        public static Dictionary<string, string> NameDictionary
        {
            get { return PrepareNameDictionary(); }
        }
        private static Dictionary<string, string> PrepareNameDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            var names = SerialPort.GetPortNames();
            names.ToList().ForEach(x => dictionary.Add(x, x));

            return dictionary;
        }
        #endregion

        #region BaudRate
        private static Dictionary<int, string> _baudRateDictionary = new Dictionary<int, string>()
        {
            {110, "110"},
            {300, "300"},
            {600, "600"},
            {1200, "1200"},
            {2400, "2400"},
            {4800, "4800"},
            {9600, "9600"},
            {14400, "14400"},
            {19200, "19200"},
            {28800, "28800"},
            {38400,  "38400"},
            {56000,  "56000"},
            {57600, "57600"},
            {115200, "115200"}     
        };
        public static Dictionary<int, string> BaudRateDictionary
        {
            get { return _baudRateDictionary; }
        }
        #endregion

        #region Parity
        private static Dictionary<Parity, string> _parityDictionary = new Dictionary<Parity, string>()
        {
            {Parity.Even, "Even (E)"},
            {Parity.None, "None (N)"},
            {Parity.Odd, "Odd (O)"},
        };
        public static Dictionary<Parity, string> ParityDictionary
        {
            get { return _parityDictionary; }
        }
        #endregion

        #region DataBits
        private static Dictionary<int, string> _dataBitsDictionary = new Dictionary<int, string>()
        {
            {7, "7"},
            {8, "8"},
        };
        public static Dictionary<int, string> DataBitsDictionary
        {
            get { return _dataBitsDictionary; }
        }
        #endregion

        #region StopBits
        private static Dictionary<StopBits, string> _stopBitsDictionary = new Dictionary<StopBits, string>()
        {
            {StopBits.One, "Jeden"},
            {StopBits.Two, "Dwa"}
        };
        public static Dictionary<StopBits, string> StopBitsDictionary
        {
            get { return _stopBitsDictionary; }
        }
        #endregion

        #region Handshake
        private static Dictionary<Handshake, string> _handshakeDictionary = new Dictionary<Handshake, string>()
        {
            {Handshake.None, "Brak"},
            {Handshake.RequestToSend,Handshake.RequestToSend.ToString()},
            {Handshake.RequestToSendXOnXOff, Handshake.RequestToSendXOnXOff.ToString()},
            {Handshake.XOnXOff, "XON/XOFF"}
        };
        public static Dictionary<Handshake, string> HandshakeDictionary
        {
            get { return _handshakeDictionary; }
        }
        #endregion
    }
}
