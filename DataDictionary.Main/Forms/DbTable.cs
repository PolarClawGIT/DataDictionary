using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.Main.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.Mediator;

namespace DataDictionary.Main.Forms
{
    partial class DbTable : ApplicationFormBase
    {
        class FormData
        {
            public IDbTableItem? DbTable { get; set; }
            public IEnumerable<IDbExtendedPropertyItem>? DbExtendedProperty { get; set; }
        }
        FormData thisData = new FormData();

        public DbTable() : base()
        {
            InitializeComponent();
            Program.Messenger.AddColleague(this);
            SendMessage(new Messages.FormAddMdiChild() { ChildForm = this });
        }

        public DbTable(IDbTableItem tableItem) :this()
        { thisData.DbTable = tableItem; }

        private void DbTable_Load(object sender, EventArgs e)
        { }

        #region IColleague
        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public void RecieveMessage(object? sender, MessageEventArgs message)
        { HandleMessage((dynamic)message); }

        void SendMessage(MessageEventArgs message)
        {
            if (OnSendMessage is EventHandler<MessageEventArgs> handler)
            { handler(this, message); }
        }

        void HandleMessage(MessageEventArgs message) { }
        #endregion
    }
}
