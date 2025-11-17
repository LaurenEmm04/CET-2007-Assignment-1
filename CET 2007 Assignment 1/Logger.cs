using System;
using System.IO;

namespace CET2007A1
{
    public class Logger
    {
        private static Logger instance;
        private static readonly string logPath = "Log.txt";

        private Logger() { }

        public static Logger GetInstance()
        {
            if (instance == null)
                instance = new Logger();
            return instance;
        }

        public void Log(string message)
        {
            string entry = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
            File.AppendAllText(logPath, entry);  //saves to file
        }
    }
}
