using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveOPreview
{
    internal sealed class ExceptionHandler
    {
        private const string EXCEPTION_DUMP_FILE_NAME = "EVE-O Preview.log";
        private const string EXCEPTION_MESSAGE = "EVE-O Preview has encountered a problem and needs to close. Additional information has been saved in the crash log file.";
        private const string BACKUP_DUMP_FILE_NAME = "EVE-O Preview_Backup.log";

        public void SetupExceptionHandlers()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                return;
            }

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) => HandleException(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => HandleException(e.ExceptionObject as Exception);

            // For handling unobserved task exceptions
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                HandleException(e.Exception);
                e.SetObserved(); // To prevent app crash due to unobserved task exceptions
            };
        }

        private void HandleException(Exception exception)
        {
            try
            {
                string logEntry = CreateLogEntry(exception);
                WriteLog(logEntry, EXCEPTION_DUMP_FILE_NAME);
                ShowErrorMessage();
            }
            catch
            {
                // Attempt backup log file if main one fails
                try
                {
                    string backupLogEntry = $"Backup log entry (due to failure in primary log file): {CreateLogEntry(exception)}";
                    WriteLog(backupLogEntry, BACKUP_DUMP_FILE_NAME);
                }
                catch
                {
                    // If even backup log fails, give up
                }
            }
            finally
            {
                Environment.Exit(1);
            }
        }

        private string CreateLogEntry(Exception exception)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("********** EVE-O Preview Exception Log **********");
            sb.AppendLine($"Timestamp: {DateTime.Now}");
            sb.AppendLine($"Message: {exception?.Message}");
            sb.AppendLine($"Source: {exception?.Source}");
            sb.AppendLine($"Stack Trace: {exception?.StackTrace}");
            sb.AppendLine("*************************************************");
            return sb.ToString();
        }

        private void WriteLog(string logEntry, string fileName)
        {
            using (var writer = new StreamWriter(fileName, true)) // Append to the file
            {
                writer.WriteLine(logEntry);
            }
        }

        private void ShowErrorMessage()
        {
            MessageBox.Show(EXCEPTION_MESSAGE, @"EVE-O Preview", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}