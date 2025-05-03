// Ignore Spelling: Securable Admin

using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Forms.Security.ComboBoxList;
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
using Toolbox.BindingTable;
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
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);
            SetTitle(bindingSecurable);

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
            IDatabaseWork factory = BusinessData.GetDbFactory();
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.OpenConnection());
            if (securableKey is SecurableIndex key)
            { work.AddRange(securityData.Load(factory, key)); }
            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                bindingSecurable.DataSource = new BindingView<SecurableValue>(securityData.Securables, w => securableKey is not null && securableKey.Equals(w));
                bindingPermissions.DataSource = new BindingView<SecurablePermissionValue>(securityData.Permissions, w => securableKey is not null && securableKey.Equals(w));
                bindingOwner.DataSource = new BindingView<SecurableOwnerValue>(securityData.Owners, w => securableKey is not null && securableKey.Equals(w));

                RoleNameList.Load(roleIdColumn, securityData.Roles);
                PrincipalLoginList.Load(principalIdColumn, securityData.Principals);

                securableTitleData.DataBindings.Add(new Binding(nameof(securableTitleData.Text), bindingSecurable, nameof(ISecurableValue.SecurableTitle)));
                securablePermissionData.DataSource = bindingPermissions;
                securableOwnerData.DataSource = bindingOwner;

                if (isAuthorized(securableKey))
                {
                    CommandButtons[CommandImageType.SaveDatabase].IsEnabled = true;
                    CommandButtons[CommandImageType.DeleteDatabase].IsEnabled = true;
                }
                else
                {
                    CommandButtons[CommandImageType.SaveDatabase].IsEnabled = false;
                    CommandButtons[CommandImageType.DeleteDatabase].IsEnabled = false;
                }
            }
        }

        private void BindingPermissions_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingSecurable.Current is SecurableValue current)
            {
                SecurablePermissionValue newValue = new SecurablePermissionValue(current);
                newValue.SecurableTitle = current.SecurableTitle;
                e.NewObject = newValue;
            }
        }

        private void BindingOwner_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingSecurable.Current is SecurableValue current)
            {
                SecurableOwnerValue newValue = new SecurableOwnerValue(current);
                newValue.SecurableTitle = current.SecurableTitle;
                e.NewObject = newValue;
            }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            if (bindingSecurable.Current is SecurableValue current)
            {
                IsLocked(true);

                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                SecurableIndex key = new SecurableIndex(current);
                work.Add(factory.OpenConnection());
                work.AddRange(securityData.Save(factory, key));
                work.AddRange(securityData.Load(factory, key));
                DoWork(work, onComplete);
            }
            void onComplete(RunWorkerCompletedEventArgs args)
            { IsLocked(false); }
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            if (bindingSecurable.Current is SecurableValue current)
            {
                IsLocked(true);

                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                SecurableIndex key = new SecurableIndex(current);
                work.Add(factory.OpenConnection());
                work.AddRange(securityData.Delete(key));
                work.AddRange(securityData.Save(factory, key));
                DoWork(work, onComplete);

            }
            void onComplete(RunWorkerCompletedEventArgs args)
            {
                bindingPermissions.DataSource = new BindingView<SecurablePermissionValue>(securityData.Permissions, w => securableKey is not null && securableKey.Equals(w));
                bindingOwner.DataSource = new BindingView<SecurableOwnerValue>(securityData.Owners, w => securableKey is not null && securableKey.Equals(w));

                IsLocked(false);
            }
        }
    }
}
