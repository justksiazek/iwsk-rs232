using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace UI.Classes
{
    public class LogItemTemplate
    {
        public string Msg { get; set; }

        public LogTypesEnum Title { get; set; }

        public LogItemTemplate(LogTypesEnum from, string msg)
        {
            this.Msg = msg;
            this.Title = from;
        }
    }
}
