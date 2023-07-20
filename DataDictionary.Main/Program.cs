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
        /// Worker Queue (Background and Foreground). Singleton access point.
        /// </summary>
        public static WorkerQueue Worker { get; } = new WorkerQueue();

        /// <summary>
        /// Mediator to allow messages to be sent between related forms. Messenger
        /// </summary>
        public static Mediator Messenger { get; } = new Mediator();


        /// <summary>
        /// Data used by the Application
        /// </summary>
        public static ModelData Data { get; } = new ModelData(new Context()
        { //TODO: Can this be secured better?
            ServerName = Settings.Default.AppServer,
            DatabaseName = Settings.Default.AppDatabase,
            ApplicationRole = Settings.Default.AppDbRole,
            ApplicationRolePassword = Settings.Default.AppDbRolePassword
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

        private static Toolbox.DbContext.Context AppContext = new DbSchemaContext()
        {
            ApplicationRole = Settings.Default.AppDbRole,
            ApplicationRolePassword = Settings.Default.AppDbRolePassword
        };

        public static void ShowException(Exception ex)
        {
            using (Dialog.ExceptionDialog dialog = new Dialog.ExceptionDialog(ex))
            { dialog.ShowDialog(); }
        }
    }
}