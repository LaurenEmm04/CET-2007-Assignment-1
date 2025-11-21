using System;
using System.IO;

namespace CET2007A1
{

    /// <summary>
    /// Logger class for noting each system interaction
    /// </summary>
    public class Logger
    {
        private static Logger instance;
        private static readonly string logPath = "Log.txt"; //name of the file, and its permissions (read only)

        private Logger() { } //for json

        /// <summary>
        /// generates a singleton logger
        /// </summary>
        /// <returns></returns>
        public static Logger GetInstance()
        {
            if (instance == null)  //if the logger hasnt been made before, 
                instance = new Logger();   //creates the one instance of logger
            return instance; //if it has, it returns that one instance of logger, ensuring only one exists
        }

        /// <summary>
        /// Creates basic writable template for the log, including date, time, space for an inputted message and instructions to put it on a new line
        /// </summary>

        public void Log(string message)
        {
            try
            {
                string entry = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
                File.AppendAllText(logPath, entry);  //saves to file
            }
            catch (Exception ex)  //exception if there was ever an issue with the logger, such as if the file didnt exist.
            {
                Console.WriteLine("Logger error: " + ex.Message);
            }
            
        }
    }
}
