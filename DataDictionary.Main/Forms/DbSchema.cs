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
    public partial class DbSchema : Form, IColleague
    {
        IDbSchemaItem? thisData;

        public DbSchema()
        {
            InitializeComponent();
        }

        public DbSchema(IDbSchemaItem data) : this()
        { thisData = data; }

        private void DbSchema_Load(object sender, EventArgs e)
        {
            Program.Messenger.AddColleague(this);
        }

        #region IColleague
        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public void RecieveMessage(object? sender, MessageEventArgs message)
        {
            if (message is FormOpenMessage openMessage) { }

        }

        void SendMessage(MessageEventArgs message)
        {
            if (OnSendMessage is EventHandler<MessageEventArgs> handler)
            { handler(this, message); }
        }
        #endregion


    }
}
