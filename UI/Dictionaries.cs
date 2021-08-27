using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace Task1
{
    public class SerialPortDictionaries {
        private static Dictionary<string, string> PrepareNameDictionary() {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            var names = SerialPort.GetPortNames();
            names.ToList().ForEach(x => dictionary.Add(x, x));

            return dictionary;
        }
        public static Dictionary<string, string> NameDictionary { get { return PrepareNameDictionary(); } }

        private static Dictionary<int, string> _baudRateDictionary = new Dictionary<int, string>() {
            //standard baud rates
            {110,    "110"},
            {300,    "300"},
            {600,    "600"},
            {1200,   "1200"},
            {2400,   "2400"},
            {4800,   "4800"},
            {9600,   "9600"},
            {14400,  "14400"},
            {19200,  "19200"},
            {28800,  "28800"},
            {38400,  "38400"},
            {56000,  "56000"},
            {57600,  "57600"},
            {115200, "115200"}     
        };
        public static Dictionary<int, string> BaudRateDictionary { get { return _baudRateDictionary; } }

        private static Dictionary<Parity, string> _parityDictionary = new Dictionary<Parity, string>() {
            {Parity.Even, "E"},
            {Parity.Odd,  "O"},
            {Parity.None, "N"}
        };
        public static Dictionary<Parity, string> ParityDictionary { get { return _parityDictionary; } }

        private static Dictionary<int, string> _dataBitsDictionary = new Dictionary<int, string>() {
            {7, "7"}, {8, "8"}
        };
        public static Dictionary<int, string> DataBitsDictionary { get { return _dataBitsDictionary; } }

        private static Dictionary<StopBits, string> _stopBitsDictionary = new Dictionary<StopBits, string>() {
            {StopBits.One, "1"}, {StopBits.Two, "2"}
        };
        public static Dictionary<StopBits, string> StopBitsDictionary { get { return _stopBitsDictionary; } }

        private static Dictionary<Handshake, string> _handshakeDictionary = new Dictionary<Handshake, string>() {
            {Handshake.None, "Brak"},
            {Handshake.RequestToSend, "RTS/CTS"},
            {Handshake.RequestToSendXOnXOff, "DTR/DSR"}, // nie jest to DTR, ale -> Data Terminal Ready (DTR) 
                                                        //is typically enabled during XON/XOFF software handshaking 
                                                        //and Request to Send/Clear to Send (RTS/CTS) hardware handshaking, 
                                                        //and modem communications.
            //DTR można włączyć za pomocą public bool DtrEnable { get; set; } z System.IO.Ports, albo SerialPortWrapper.DTR
            {Handshake.XOnXOff, "XON/XOFF"}
        };

        public static Dictionary<string, string> TerminatorDictionary { get { return _terminatorDictionary; } }

        private static Dictionary<string, string> _terminatorDictionary = new Dictionary<string, string>() {
            {"\r", "CR"},
            {"\n", "LF"},
            {"\r\n", "CR-LF"},
            {"", "własny"}
        };
        public static Dictionary<Handshake, string> HandshakeDictionary { get { return _handshakeDictionary; } }
    }
}
