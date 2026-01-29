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
    partial class ApplicationOptions : ApplicationData
    {
        public ApplicationOptions() : base()
        {
            InitializeComponent();

            SetIcon(ScopeType.ApplicationOption); ;
        }

        private void ApplicationOptions_Load(object sender, EventArgs e)
        {
            defaultModeOnLine.Checked = Settings.Default.IsOnLineMode;
            defaultModeOffLine.Checked = !Settings.Default.IsOnLineMode;
            defaultModeOnLine.DataBindings.Add(new Binding(nameof(RadioButton.Checked), Settings.Default, nameof(Settings.Default.IsOnLineMode)));
            serverNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), Settings.Default, nameof(Settings.Default.AppServer)));
            databaseNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), Settings.Default, nameof(Settings.Default.AppDatabase)));
            applicationRoleData.DataBindings.Add(new Binding(nameof(TextBox.Text), Settings.Default, nameof(Settings.Default.AppDbRole)));
            applicationFileData.DataBindings.Add(new Binding(nameof(TextBox.Text), Settings.Default, nameof(Settings.Default.AppDataFile)));

            projectFolderData.DataBindings.Add(new Binding(nameof(TextBox.Text), Settings.Default, nameof(Settings.Default.UserProjects)));
            applicationDataFolder.DataBindings.Add(new Binding(nameof(TextBox.Text), Settings.Default, nameof(Settings.Default.UserData)));
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
            work.AddRange(BusinessData.ApplicationData.Save(appDataFile));

#if DEBUG
            if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("VisualStudioEdition")))
            { // Running in Visual Studio & the build is a DEBUG, save to the executable & project folder as well.
                DirectoryInfo path = new DirectoryInfo(Application.StartupPath);

                while (path.Parent is not null)
                {
                    foreach (var item in path.GetFiles(Settings.Default.AppDataFile))
                    {
                        if (item.DirectoryName is String directory)
                        {
                            FileInfo otherFile = new FileInfo(Path.Combine(directory, Settings.Default.AppDataFile));
                            work.AddRange(BusinessData.ApplicationData.Save(otherFile));
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

        private void CommandLoadFromFile_Click(object sender, EventArgs e)
        {
            SendMessage(new DbApplicationBatchStarting());

            FileInfo appDataFile = new FileInfo(Path.Combine(Application.UserAppDataPath, Settings.Default.AppDataFile));
            this.DoWork(BusinessData.ApplicationData.Load(appDataFile), OnComplete);

            void OnComplete(RunWorkerCompletedEventArgs args)
            { SendMessage(new DbApplicationBatchCompleted()); }
        }

        private void DefaultModeOnLine_CheckedChanged(object sender, EventArgs e)
        {
            if (defaultModeOnLine.Checked)
            { Program.SetupApplicationData(OnComplete); }
            else
            {
                Settings.Default.IsOnLineMode = defaultModeOnLine.Checked;
                Settings.Default.Save();
                SendMessage(new OnlineStatusChanged());
            }

            void OnComplete(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is not null && Settings.Default.IsOnLineMode)
                { Settings.Default.IsOnLineMode = false; }
                else { Settings.Default.IsOnLineMode = true; }
                Settings.Default.Save();

                if (args.Error is not null)
                { Program.ShowException(args.Error); }

                SendMessage(new OnlineStatusChanged());
            }
        }

        private void ProjectFolderData_SelectCommand(object sender, EventArgs e)
        {
            IDirectoryEnumeration value = DirectoryType.Projects.GetEnumeration();

            folderBrowserDialog.Reset();
            folderBrowserDialog.RootFolder = value.SpecialFolder;

            if (value.Directory is DirectoryInfo directory && directory.Exists)
            { folderBrowserDialog.SelectedPath = directory.FullName; }
            else
            { folderBrowserDialog.SelectedPath = Environment.GetFolderPath(value.SpecialFolder); }

            if (folderBrowserDialog.ShowDialog() is DialogResult.OK)
            {
                String newPath = Path.GetRelativePath(Environment.GetFolderPath(value.SpecialFolder), folderBrowserDialog.SelectedPath);
                value.RelativeFolder = newPath;
                //projectFolderData.Text = newPath;
                Settings.Default.UserProjects = newPath;
                Settings.Default.Save();
            }
        }

        private void ApplicationDataFolder_SelectCommand(object sender, EventArgs e)
        {
            IDirectoryEnumeration value = DirectoryType.Data.GetEnumeration();

            folderBrowserDialog.Reset();
            folderBrowserDialog.RootFolder = value.SpecialFolder;

            if (value.Directory is DirectoryInfo directory && directory.Exists)
            { folderBrowserDialog.SelectedPath = directory.FullName; }
            else
            { folderBrowserDialog.SelectedPath = Environment.GetFolderPath(value.SpecialFolder); }

            if (folderBrowserDialog.ShowDialog() is DialogResult.OK)
            {
                String newPath = Path.GetRelativePath(Environment.GetFolderPath(value.SpecialFolder), folderBrowserDialog.SelectedPath);
                value.RelativeFolder = newPath;
                //applicationDataFolder.Text = newPath;
                Settings.Default.UserData = newPath;
                Settings.Default.Save();
            }
        }

        private void ProjectFolderData_Validated(object sender, EventArgs e)
        {

        }

        private void ApplicationDataFolder_Validated(object sender, EventArgs e)
        {

        }
    }
}
