namespace Task1.Classes
{
    public class LogItemTemplate {
        public string Msg { get; set; }
        public LogTypesEnum Title { get; set; }

        public LogItemTemplate(LogTypesEnum from, string msg) {
            this.Msg = msg;
            this.Title = from;
        }
    }
}
