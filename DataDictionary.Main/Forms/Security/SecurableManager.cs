// Ignore Spelling: Securable

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
    partial class SecurableManager : ApplicationData
    {
        //TODO: all the coding

        ISecurity securityData = ISecurity.Create();
        SecurableIndex? securableKey;

        public SecurableManager() : base()
        {
            InitializeComponent();

            SetCommand(ScopeType.SecurityPrincipal,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);

            securableOwnerData.AutoGenerateColumns = false;
            securablePermissionData.AutoGenerateColumns = false;
        }

        public SecurableManager(IDataValue value) : this()
        {
            securableKey = value.Index;
        }

        private void ObjectManager_Load(object sender, EventArgs e)
        {

        }
    }
}
