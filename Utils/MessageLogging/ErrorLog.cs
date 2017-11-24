using System;
using System.IO;

namespace Utils.MessageLogging
{
    internal class ErrorLog
    {
        private StreamWriter w;
        private const string _extension = ".txt";
        private const string _defaultFileName = "Errors.log";
        private string _name = string.Empty;

        internal ErrorLog()
        {
            this.CreateLogFileIfNotExist(_defaultFileName);
        }

        internal ErrorLog(string logFileName)
        {
            this.CreateLogFileIfNotExist(logFileName);
        }

        private void CreateLogFileIfNotExist(string logFileName)
        {
            try
            {
                this._name = logFileName;

                string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\Logs\Errors";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string name = path + @"\" + _name;
                string fullName = path + @"\" + _name + _extension;
                if (File.Exists(fullName))
                {
                    try
                    {
                        FileInfo fi = new FileInfo(fullName);
                        if (fi.Length > 1000000)
                        {
                            //string[] files = Directory.GetFiles(path, _name + "*.").Where(x => !x.EndsWith("zip")).ToArray();
                            string[] files = Directory.GetFiles(path, this._name + "*" + _extension);

                            for (int i = files.Length; i > 0; i--)
                            {
                                int j = i - 1;
                                if (File.Exists(name + "_" + j.ToString() + _extension))
                                {
                                    try { File.Move(name + "_" + j.ToString() + _extension, name + "_" + i.ToString() + _extension); }
                                    catch { }
                                }
                            }
                            File.Move(name + _extension, name + "_0" + _extension);
                        }
                    }
                    catch { }

                }
                w = File.AppendText(fullName);
            }
            catch { w = null; }
        }

        internal void Log(string text, string trace)
        {
            if (w != null)
                try
                {
                    w.WriteLine("\r\n{0} {1} : {2}",
                        DateTime.Now.ToString("dd.MM.yyyy"),
                        DateTime.Now.ToString("HH:mm:ss"),
                        text);
                    w.WriteLine("Trace: {0}", trace);
                    w.Flush();
                }
                catch { }
        }

        internal void Close()
        {
            try
            {
                if (w != null) w.Close();
                w = null;
            }
            catch { }
        }
    }
}
