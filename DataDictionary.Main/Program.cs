using DataDictionary.BusinessLayer;
using DataDictionary.Main.Properties;
using System.Runtime.CompilerServices;
using Toolbox.DbContext;
using Toolbox.Mediator;
using Toolbox.Threading;

namespace DataDictionary.Main
{
    internal static class Program
    {
        static Program()
        { }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            Application.ApplicationExit += Application_ApplicationExit;

            Worker.WorkException += WorkerQueue_WorkException;

            // Static Data
            BusinessData.ScriptingEngine.Columns.Load();

            Application.Run(new Main());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        { ShowException(e.Exception); }

        private static void WorkerQueue_WorkException(object? sender, WorkerExceptionEventArgs e)
        { ShowException(e.Exception); }

        private static void Application_ApplicationExit(object? sender, EventArgs e)
        {
            Application.ThreadException -= Application_ThreadException;
            Application.ApplicationExit -= Application_ApplicationExit;
            Worker.Dispose();
            Messenger.Dispose();
        }

        public static void ShowException(Exception ex)
        {
            using (Dialogs.ExceptionDialog dialog = new Dialogs.ExceptionDialog(ex))
            { dialog.ShowDialog(); }
        }
    }
}