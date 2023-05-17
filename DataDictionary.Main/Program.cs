using DataDictionary.Main.Properties;
using Toolbox.DbContext;

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
            BusinessLayer.UnitTest.AppContext = new Context() { ServerName = Settings.Default.AppServer, DatabaseName =  Settings.Default.AppDatabase };

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }
    }
}