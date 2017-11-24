namespace Utils.MessageLogging
{
    public class MessageLoggManager
    {
        private static MainLog _inst;
        private static ErrorLog _instError;
        private const string _infoLogFileName = "PLogger.log";
        private const string _errLogFileName = "Errors.log";
        static readonly object _lockObject = new object();

        public static void Log(string text)
        {
            lock (_lockObject)
            {
                if (_inst == null)
                    _inst = new MainLog(_infoLogFileName);

                _inst.Log(text);
                _inst.Close();
                _inst = null;
            }
        }

        public static void Log(string text, string trace)
        {
            lock (_lockObject)
            {
                if (_inst == null)
                    _inst = new MainLog(_infoLogFileName);
                _inst.Log(text, trace);
                _inst.Close();
                _inst = null;
            }
        }

        public static void LogErr(string text, string trace)
        {
            lock (_lockObject)
            {
                if (_instError == null)
                    _instError = new ErrorLog(_errLogFileName);
                _instError.Log(text, trace);
                _instError.Close();
                _instError = null;
            }
        }
    }
}
