namespace LibraryLogging
{
    public sealed class Logger : ILogger
    {
        private static int InstanceCounter = 0;
        private static Logger LoggerInstance = null;
        public Logger() {
            InstanceCounter++;
        }
      
        public static Logger SingleInstance
        {
            get
            {
                if (LoggerInstance == null)
                {
                   LoggerInstance = new Logger();
                }
                return LoggerInstance;
            }
        }
        public void Log(string message)
        {
            StreamWriter sw = File.AppendText("Logger.txt");
            sw.WriteLine(message);
            sw.Close();
        }
        public void Log(string FullName, string message)
        {
            Log($"{(FullName == "" ? "Manager" : FullName)} : {DateTime.Now} : {message}");
        }
    
    }
}