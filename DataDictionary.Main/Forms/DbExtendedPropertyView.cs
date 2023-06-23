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
    public partial class DbExtendedPropertyView : Form, IColleague
    {
        public DbExtendedPropertyView()
        {
            InitializeComponent();
            Program.Messenger.AddColleague(this);
            SendMessage(new Messages.FormAddMdiChild() { ChildForm = this });
        }

        private void DbExtendedPropertyView_Load(object sender, EventArgs e)
        {
            extendedPropertyData.DataSource = Program.DbData.DbExtendedProperties;
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
