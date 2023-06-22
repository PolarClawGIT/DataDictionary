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
        /// Worker Queue (Background and Forground). Singleton access point.
        /// </summary>
        public static WorkerQueue WorkerQueue { get; } = new WorkerQueue();

        /// <summary>
        /// Mediator to allow messages to be sent between related forms. Messenger
        /// </summary>
        public static Mediator Messenger { get; } = new Mediator();

        /// <summary>
        /// Application Data, Db MetaData
        /// </summary>
        public static DatabaseMetaData DbData { get; } = new DatabaseMetaData();

        static Program()
        { }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Settings.Default.Reset();

            WorkerQueue.WorkException += WorkerQueue_WorkException;

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            Application.ApplicationExit += Application_ApplicationExit;
            Application.Run(new Main());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            using (Dialog.ExceptionDialog dialog = new Dialog.ExceptionDialog(e.Exception))
            { dialog.ShowDialog(); }
        }

        private static void WorkerQueue_WorkException(object? sender, Exception e)
        {
            using (Dialog.ExceptionDialog dialog = new Dialog.ExceptionDialog(e))
            { dialog.ShowDialog(); }
        }

        private static void Application_ApplicationExit(object? sender, EventArgs e)
        {
            Application.ThreadException -= Application_ThreadException;
            Application.ApplicationExit -= Application_ApplicationExit;
            WorkerQueue.Dispose();
            Messenger.Dispose();
        }

    }
}