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
    public partial class DbColumn : Form, IColleague
    {
        IDbColumnItem? thisData;

        public DbColumn() : base()
        { 
            InitializeComponent(); 
            Program.Messenger.AddColleague(this);
            SendMessage(new FormAddMdiChild() { ChildForm = this });
        }

        public DbColumn(IDbColumnItem data) :this()
        { thisData = data; }

        private void DbColumn_Load(object sender, EventArgs e)
        {
            
        }

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
