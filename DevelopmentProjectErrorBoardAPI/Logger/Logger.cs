namespace DevelopmentProjectErrorBoardAPI.Logger
{
    public class Logger : ILogger
    {
        private string filepath = "ErrorLogs.txt";

        public void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter(filepath,
                       true))
            {
                writer.WriteLine(DateTime.Now + " | " + message);
            }
        }
    }
}