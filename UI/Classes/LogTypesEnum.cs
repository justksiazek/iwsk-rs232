using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace UI.Classes
{
    public enum LogTypesEnum
    {
        Error = 0,
        Information,
        SendingText,
        SendingHex,
        SendingPing,
        Receiving,
        PinChanged,
        Buffer,
        Other
    }
}
