using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using Toolbox.BindingTable;
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

        public static void SetupApplicationData(Action<RunWorkerCompletedEventArgs> onComplete)
        {
            if (Settings.Default.IsOnLineMode)
            { Worker.Enqueue(BusinessData.GetDbFactory().OpenConnection(), TestConnection); }
            else { LoadByFile(); }

            void TestConnection(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is Exception ex)
                { LoadByFile(); }
                else { LoadByDatabase(); }
            }

            void LoadByFile()
            {
                FileInfo appDataFile = new FileInfo(Path.Combine(Application.UserAppDataPath, Settings.Default.AppDataFile));
                FileInfo appInstallFile = new FileInfo(Settings.Default.AppDataFile);
                List<WorkItem> work = new List<WorkItem>();

                if (appDataFile.Exists) // AppData already contains the Application Data File
                { work.AddRange(BusinessData.ApplicationData.Load(appDataFile)); }
                else if (appInstallFile.Exists)
                { // AppData does not contain file but the install folder does (Copy it)

                    work.AddRange(BusinessData.ApplicationData.Load(appInstallFile));
                    work.AddRange(BusinessData.ApplicationData.Save(appDataFile));
                }

                Worker.Enqueue(work, FileComplete);

                void FileComplete(RunWorkerCompletedEventArgs args)
                { onComplete(args); }
            }

            void LoadByDatabase()
            {
                FileInfo appDataFile = new FileInfo(Path.Combine(Application.UserAppDataPath, Settings.Default.AppDataFile));
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(BusinessData.ApplicationData.Load(factory));
                work.AddRange(BusinessData.ApplicationData.Save(appDataFile));
                work.AddRange(BusinessData.LoadAuthorization(factory));

                Worker.Enqueue(work, DatabaseComplete);

                void DatabaseComplete(RunWorkerCompletedEventArgs args)
                { onComplete(args); }
            }
        }

    }
}