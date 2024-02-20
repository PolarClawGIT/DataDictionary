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
        /// <summary>
        /// Data used by the Application
        /// </summary>
        [Obsolete("Being Replaced with new Business Layer object")]
        public static ModelData Data { get; } = new ModelData(new Context()
        { //TODO: Can this be secured better?
            ServerName = Settings.Default.AppServer,
            DatabaseName = Settings.Default.AppDatabase,
            ApplicationRole = Settings.Default.AppDbRole,
            ApplicationRolePassword = Settings.Default.AppDbRolePassword,
            ValidateCommand = true // TODO: Used for Debugging. Set to False when going live.
        });

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