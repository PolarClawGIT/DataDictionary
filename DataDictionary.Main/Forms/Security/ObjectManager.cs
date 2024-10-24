using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Security
{
    partial class ObjectManager : ApplicationData
    {

        ISecurity securityData = ISecurity.Create();

        public ObjectManager() : base()
        {
            InitializeComponent();

            SetCommand(ScopeType.SecurityPrincipal,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);

            objectOwnerData.AutoGenerateColumns = false;
            objectPermissionData.AutoGenerateColumns = false;
        }

        public ObjectManager(IDataValue value) : this()
        {
            bindingObject.DataSource = new BindingList<IDataValue>() { value };
        }

        private void ObjectManager_Load(object sender, EventArgs e)
        {

        }
    }
}
