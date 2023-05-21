using DataDictionary.BusinessLayer;
using DataDictionary.Main.Properties;

namespace DataDictionary.Main
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            Application.Run(new Main());

            Application.ThreadException -= Application_ThreadException;
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            using (Dialog.ExceptionDialog dialog = new Dialog.ExceptionDialog(e.Exception))
            { dialog.Show(); }
        }

        /// <summary>
        /// Business Context information. Singleton access point.
        /// </summary>
        public static BusinessContext BusinessContext { get; } = new BusinessContext() { DbContext = new Toolbox.DbContext.Context() { ServerName = Settings.Default.AppServer, DatabaseName = Settings.Default.AppDatabase } };
    }
}