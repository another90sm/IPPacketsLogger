using InitialApplicationStart;
using PLogger.UserInterface;
using System;
using System.Windows.Forms;

namespace PLogger
{
    static class Startup
    {
        [STAThread]
        static void Main()
        {
            InitialCheck initialCheck = new InitialCheck();
            if (initialCheck.Check())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PLoggerForm());
            }
        }
    }
}