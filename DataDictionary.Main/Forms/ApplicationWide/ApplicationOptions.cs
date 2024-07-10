using System.ComponentModel;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Properties;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Forms;
using DataDictionary.Resource.Enumerations;
using Toolbox.Threading;
using DataDictionary.BusinessLayer.DbWorkItem;

namespace DataDictionary.Main.ApplicationWide
{
    partial class ApplicationOptions : ApplicationBase
    {
        public ApplicationOptions() : base()
        {
            InitializeComponent();

            this.Icon = ImageEnumeration.GetIcon(ScopeType.ApplicationOption);
        }

        private void ApplicationOptions_Load(object sender, EventArgs e)
        {
            defaultModeOnLine.Checked = Settings.Default.IsOnLineMode;
            defaultModeOffLine.Checked = !Settings.Default.IsOnLineMode;
            defaultModeOnLine.DataBindings.Add(new Binding(nameof(defaultModeOnLine.Checked), Settings.Default, nameof(Settings.Default.IsOnLineMode)));
            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), Settings.Default, nameof(Settings.Default.AppServer)));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), Settings.Default, nameof(Settings.Default.AppDatabase)));
            applicationRoleData.DataBindings.Add(new Binding(nameof(applicationRoleData.Text), Settings.Default, nameof(Settings.Default.AppDbRole)));
            applicationFileData.DataBindings.Add(new Binding(nameof(applicationFileData.Text), Settings.Default, nameof(Settings.Default.AppDataFile)));
        }

        private void commandSaveToDatabase_Click(object sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            IDatabaseWork factory = BusinessData.GetDbFactory();
            work.Add(factory.OpenConnection());
            work.AddRange(BusinessData.ApplicationData.Save(factory));
            this.DoWork(work, OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { } // Nothing to do at this point
        }

        private void commandLoadFromDatabase_Click(object sender, EventArgs e)
        {
            SendMessage(new DbApplicationBatchStarting());

            List<WorkItem> work = new List<WorkItem>();
            IDatabaseWork factory = BusinessData.GetDbFactory();
            work.Add(factory.OpenConnection());
            work.AddRange(BusinessData.ApplicationData.Load(factory));
            this.DoWork(work, OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new DbApplicationBatchCompleted()); }
        }

        private void commandSaveToFile_Click(object sender, EventArgs e)
        {
            FileInfo appDataFile = new FileInfo(Path.Combine(Application.UserAppDataPath, Settings.Default.AppDataFile));

            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(BusinessData.ExportApplication(appDataFile));

#if DEBUG
            if(!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("VisualStudioEdition")))
            { // Running in Visual Studio & the build is a DEBUG, save to the executable & project folder as well.
                DirectoryInfo path = new DirectoryInfo(Application.StartupPath);

                while (path.Parent is not null)
                {
                    foreach (var item in path.GetFiles(Settings.Default.AppDataFile))
                    {
                        if(item.DirectoryName is String directory)
                        {
                            FileInfo otherFile = new FileInfo(Path.Combine(directory, Settings.Default.AppDataFile));
                            work.AddRange(BusinessData.ExportApplication(otherFile));
                        }
                    }

                    path = path.Parent;
                }
            }
#endif

            this.DoWork(work, OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { } // Nothing to do at this point
        }

        private void commandLoadFromFile_Click(object sender, EventArgs e)
        {
            SendMessage(new DbApplicationBatchStarting());

            FileInfo appDataFile = new FileInfo(Path.Combine(Application.UserAppDataPath, Settings.Default.AppDataFile));
            this.DoWork(BusinessData.ImportApplication(appDataFile), OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new DbApplicationBatchCompleted()); }
        }

        private void defaultModeOnLine_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IsOnLineMode = defaultModeOnLine.Checked;
            Settings.Default.Save();
            SendMessage(new OnlineStatusChanged());
        }
    }
}
