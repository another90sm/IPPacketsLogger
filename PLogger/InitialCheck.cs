using InitialApplicationStart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLogger
{
    public class InitialCheck
    {
        public InitialCheck()
        {

        }

        public bool Check()
        {
            try
            {
                if (DataBaseInformation.WorkWithDataBase)
                {
                    DataBaseHelp dbHelp = new DataBaseHelp();
                    DialogResult dialogResult;

                    if (!dbHelp.CheckIfDatabaseExists())
                    {
                        dialogResult = MessageBox.Show(@"Data base is not found in directory set in 'PLogger.exe.config' file!/r/nWould you like to create data base?", "Qestion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (!dbHelp.CreateDatabase())
                            {
                                MessageBox.Show(@"Error occurred while creating database!/r/nView Log file for more information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show(@"If you want to use this application without database, please set attribute 'WorkWithDataBase' value to false in 'PLogger.exe.config' file.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                    else if (!dbHelp.CheckDatabaseStructure())
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
