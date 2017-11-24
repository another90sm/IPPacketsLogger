using System;
using System.IO;

namespace Utils.MessageLogging
{
    internal class MainLog
    {
        private StreamWriter w;
        private const string _defaultFileName = "Info.log";
        private string _lofFileName = string.Empty;

        internal MainLog()
        {
            this.CreateLogFileIfNotExist(_defaultFileName);
        }

        internal MainLog(string logFileName)
        {
            this.CreateLogFileIfNotExist(logFileName);
        }

        private void CreateLogFileIfNotExist(string logFileName)
        {
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\Logs";

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                string name = path + @"\" + logFileName;
                if (File.Exists(name))
                {
                    try
                    {
                        FileInfo fi = new FileInfo(name);
                        if (fi.Length > 500000)
                        {
                            File.Delete(name + ".9");
                            for (int i = 9; i > 0; i--)
                            {
                                int j = i - 1;
                                if (File.Exists(name + "." + j.ToString()))
                                {
                                    try { File.Move(name + "." + j.ToString(), name + "." + i.ToString()); }
                                    catch { }
                                }
                            }
                            File.Move(name, name + ".0");
                        }
                    }
                    catch { }

                }
                w = File.AppendText(name);
            }
            catch { w = null; }
        }

        internal void Log(string text)
        {
            if (w != null)
                try
                {
                    w.WriteLine("{0} {1} -> {2}",
                        DateTime.Now.ToString("dd.MM.yyyy"),
                        DateTime.Now.ToString("HH:mm:ss"),
                        text);
                    w.Flush();
                }
                catch { }
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
