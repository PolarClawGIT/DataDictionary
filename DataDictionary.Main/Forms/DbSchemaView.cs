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
    public partial class DbSchemaView : Form, IColleague
    {
        public DbSchemaView()
        {
            InitializeComponent();
            Program.Messenger.AddColleague(this);
            SendMessage(new Messages.FormAddMdiChild() { ChildForm = this });
        }

        private void DbSchemaView_Load(object sender, EventArgs e)
        { schemaData.DataSource = Program.DbData.DbSchemas; }

        #region IColleague
        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public void RecieveMessage(object? sender, MessageEventArgs message)
        { }

        void SendMessage(MessageEventArgs message)
        {
            if (OnSendMessage is EventHandler<MessageEventArgs> handler)
            { handler(this, message); }
        }
        #endregion
    }
}
