// Ignore Spelling: Securable Admin

using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
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
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Security
{
    partial class SecurableManager : ApplicationData
    {
        ISecurity securityData = ISecurity.Create();
        SecurableIndex? securableKey;
        Func<SecurableIndex?, Boolean> isAuthorized = (key) => false;

        public SecurableManager() : base()
        {
            InitializeComponent();

            SetIcon(ScopeType.SecuritySecurable);
            SetCommand(ScopeType.SecurityPrincipal,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);

            securableOwnerData.AutoGenerateColumns = false;
            securablePermissionData.AutoGenerateColumns = false;
        }

        public SecurableManager(SecurableIndex value, Func<Boolean> isAdmin) : this()
        {
            securableKey = value;
            isAuthorized = (key) => isAdmin() // Do not know which admin type to check. Need to have it passed.
                || BusinessData.Authorization.IsOwner(key)
                || BusinessData.Authorization.IsSecurityAdmin;
        }

        private void ObjectManager_Load(object sender, EventArgs e)
        {
            IsLocked(true);
            IsWaitCursor(true);
            IDatabaseWork factory = BusinessData.GetDbFactory();
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.OpenConnection());
            if (securableKey is SecurableIndex key)
            { work.AddRange(securityData.Load(factory, key)); }
            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            {


                //TODO: Add binding

                if (isAuthorized(securableKey))
                {
                    CommandButtons[CommandImageType.OpenDatabase].IsEnabled = true;
                    CommandButtons[CommandImageType.SaveDatabase].IsEnabled = true;
                    CommandButtons[CommandImageType.DeleteDatabase].IsEnabled = true;
                }
                else
                {
                    CommandButtons[CommandImageType.OpenDatabase].IsEnabled = false;
                    CommandButtons[CommandImageType.SaveDatabase].IsEnabled = false;
                    CommandButtons[CommandImageType.DeleteDatabase].IsEnabled = false;
                }

                IsLocked(false);
                IsWaitCursor(false);
            }
        }

        //TODO: Handled events
    }
}
