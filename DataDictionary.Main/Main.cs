using DataDictionary.BusinessLayer;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using Toolbox.Mediator;
using Toolbox.Threading;
using Toolbox.Threading.WorkItem;

namespace DataDictionary.Main
{
    partial class Main : Form, IColleague
    {
        #region Static Data
        enum navigationTabImageIndex
        {
            Database,
            Domain
        }
        Dictionary<navigationTabImageIndex, Image> navigationTabImages = new Dictionary<navigationTabImageIndex, Image>()
        {
            {navigationTabImageIndex.Database, Resources.Database },
            {navigationTabImageIndex.Domain, Resources.Dictionary }
        };
        #endregion

        public Main()
        {
            InitializeComponent();
            ChildFormOpening += Main_ChildFormOpening;

            navigationTabs.ImageList = new ImageList();
            foreach (navigationTabImageIndex item in Enum.GetValues(typeof(navigationTabImageIndex)))
            { navigationTabs.ImageList.Images.Add(item.ToString(), navigationTabImages[item]); }
            navigationDbSchemaTab.ImageKey = navigationTabImageIndex.Database.ToString();
            navigationDomainTab.ImageKey = navigationTabImageIndex.Domain.ToString();

        }



        private void Main_Load(object sender, EventArgs e)
        {
            Program.WorkerQueue.ProgressChanged += WorkerQueue_ProgressChanged;
            Program.Messenger.AddColleague(this);
            Program.DbData.ListChanged += DbData_ListChanged;

        }

        private void DbData_ListChanged(object? sender, ListChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Main_FormClosing(object? sender, FormClosingEventArgs e)
        { }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.WorkerQueue.ProgressChanged -= WorkerQueue_ProgressChanged;
            Program.DbData.ListChanged -= DbData_ListChanged;
        }

        private void WorkerQueue_ProgressChanged(object? sender, WorkerProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercent;
            toolStripWorkerTask.Text = e.ProgressText;
        }


        private void importFromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.FirstOrDefault(w => w.GetType() == typeof(Forms.DbConnection)) is Forms.DbConnection existingForm)
            { existingForm.Activate(); }
            else
            {
                Form newForm = new Forms.DbConnection();
                newForm.MdiParent = this;
                newForm.Show();
            }
        }


        #region IColleague
        event EventHandler<FormOpenMessage> ChildFormOpening;
        private void Main_ChildFormOpening(object? sender, FormOpenMessage e)
        { e.FormOpened.MdiParent = this; }

        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public void RecieveMessage(object? sender, MessageEventArgs message)
        {
            if (message is FormOpenMessage openMessage && ChildFormOpening is EventHandler<FormOpenMessage> handler)
            { handler(sender, openMessage); }

        }
        void SendMessage(MessageEventArgs message)
        {
            if (OnSendMessage is EventHandler<MessageEventArgs> handler)
            { handler(this, message); }
        }
        #endregion

    }
}