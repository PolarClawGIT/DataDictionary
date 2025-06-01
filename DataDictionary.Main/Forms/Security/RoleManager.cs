using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
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
    partial class RoleManager : ApplicationData
    {
        // TODO: Candidate to re-factor with Binding class
        ISecurity securityData = ISecurity.Create();

        public RoleManager()
        {
            InitializeComponent();

            SetIcon(ScopeType.SecurityRole);
            SetCommand(ScopeType.SecurityRole,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);

            roleData.AutoGenerateColumns = false;
            membershipData.AutoGenerateColumns = false;
            objectPermissionData.AutoGenerateColumns = false;
        }

        private void RoleManager_Load(object sender, EventArgs e)
        {
            IsLocked(true);
            IDatabaseWork factory = BusinessData.GetDbFactory();
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.OpenConnection());
            work.AddRange(securityData.Load(factory));
            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                bindingRole.DataSource = securityData.Roles;
                SetTitle(bindingRole);

                roleData.DataSource = bindingRole;
                membershipData.DataSource = bindingMembers;
                objectPermissionData.DataSource = bindingPermission;

                roleData.Sort(roleNameColumn, ListSortDirection.Descending);

                roleNameData.DataBindings.Add(new Binding(nameof(roleNameData.Text), bindingRole, nameof(IRoleValue.RoleName), false, DataSourceUpdateMode.OnPropertyChanged));
                roleDescriptionData.DataBindings.Add(new Binding(nameof(roleDescriptionData.Text), bindingRole, nameof(IRoleValue.RoleDescription), false, DataSourceUpdateMode.OnPropertyChanged));

                isSecurityAdminData.DataBindings.Add(new Binding(nameof(isSecurityAdminData.Checked), bindingRole, nameof(IRoleValue.IsSecurityAdmin), false, DataSourceUpdateMode.OnPropertyChanged));
                isHelpAdminData.DataBindings.Add(new Binding(nameof(isHelpAdminData.Checked), bindingRole, nameof(IRoleValue.IsHelpAdmin), false, DataSourceUpdateMode.OnPropertyChanged));
                isHelpOwnerData.DataBindings.Add(new Binding(nameof(isHelpOwnerData.Checked), bindingRole, nameof(IRoleValue.IsHelpOwner), false, DataSourceUpdateMode.OnPropertyChanged));
                isCatalogAdminData.DataBindings.Add(new Binding(nameof(isCatalogAdminData.Checked), bindingRole, nameof(IRoleValue.IsCatalogAdmin), false, DataSourceUpdateMode.OnPropertyChanged));
                isCatalogOwnerData.DataBindings.Add(new Binding(nameof(isCatalogOwnerData.Checked), bindingRole, nameof(IRoleValue.IsCatalogOwner), false, DataSourceUpdateMode.OnPropertyChanged));
                isLibraryAdminData.DataBindings.Add(new Binding(nameof(isLibraryAdminData.Checked), bindingRole, nameof(IRoleValue.IsLibraryAdmin), false, DataSourceUpdateMode.OnPropertyChanged));
                isLibraryOwnerData.DataBindings.Add(new Binding(nameof(isLibraryOwnerData.Checked), bindingRole, nameof(IRoleValue.IsLibraryOwner), false, DataSourceUpdateMode.OnPropertyChanged));
                isModelAdminData.DataBindings.Add(new Binding(nameof(isModelAdminData.Checked), bindingRole, nameof(IRoleValue.IsModelAdmin), false, DataSourceUpdateMode.OnPropertyChanged));
                isModelOwnerData.DataBindings.Add(new Binding(nameof(isModelOwnerData.Checked), bindingRole, nameof(IRoleValue.IsModelOwner), false, DataSourceUpdateMode.OnPropertyChanged));
                isScriptAdminData.DataBindings.Add(new Binding(nameof(isScriptAdminData.Checked), bindingRole, nameof(IRoleValue.IsScriptAdmin), false, DataSourceUpdateMode.OnPropertyChanged));
                isScriptOwnerData.DataBindings.Add(new Binding(nameof(isScriptOwnerData.Checked), bindingRole, nameof(IRoleValue.IsScriptOwner), false, DataSourceUpdateMode.OnPropertyChanged));

                PrincipalLoginList.Load(principalIdColumn, securityData.Principals);

                if (BusinessData.Authorization.IsSecurityAdmin)
                {
                    CommandButtons[CommandImageType.Add].IsEnabled = true;
                    CommandButtons[CommandImageType.Delete].IsEnabled = true;
                    CommandButtons[CommandImageType.OpenDatabase].IsEnabled = true;
                }
                else
                {
                    CommandButtons[CommandImageType.Add].IsEnabled = false;
                    CommandButtons[CommandImageType.Delete].IsEnabled = false;
                    CommandButtons[CommandImageType.OpenDatabase].IsEnabled = false;
                    CommandButtons[CommandImageType.SaveDatabase].IsEnabled = false;
                    CommandButtons[CommandImageType.DeleteDatabase].IsEnabled = false;
                }

                IsLocked(false);
            }
        }

        private void BindingRoles_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingRole.Current is RoleValue current)
            {
                RoleIndex key = new RoleIndex(current);
                bindingMembers.DataSource = null;
                bindingMembers.DataSource = new BindingView<RoleMembershipValue>(securityData.Memberships, w => key.Equals(w));

                bindingPermission.DataSource = null;
                bindingPermission.DataSource = new BindingView<SecurablePermissionValue>(securityData.Permissions, w => key.Equals(w));

                if (BusinessData.Authorization.IsSecurityAdmin)
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
            else
            {
                bindingMembers.DataSource = null;
                bindingPermission.DataSource = null;
                CommandButtons[CommandImageType.SaveDatabase].IsEnabled = false;
                CommandButtons[CommandImageType.DeleteDatabase].IsEnabled = false;
            }
        }

        private void BindingRoles_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new RoleValue()
            { RoleName = "(new role)" };
        }

        private void BindingMembers_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingRole.Current is RoleValue current)
            { e.NewObject = new RoleMembershipValue(current); }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            bindingRole.AddNew();
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            List<WorkItem> work = new List<WorkItem>();

            foreach (DataGridViewRow item in roleData.SelectedRows)
            {
                if (item.DataBoundItem is RoleValue value)
                {
                    RoleIndex key = new RoleIndex(value);
                    work.AddRange(securityData.Delete(key));
                }
            }

            IsLocked(true);
            SuspendBinding(bindingRole);
            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                IsLocked(false);
                ResumeBinding(bindingRole);
                roleData.DataSource = bindingRole;
            }
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            IDatabaseWork factory = BusinessData.GetDbFactory();
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.OpenConnection());

            if (roleData.SelectedRows.Count == 0)
            {
                work.AddRange(securityData.Delete());
                work.AddRange(securityData.Load(factory));
            }
            else
            {
                foreach (DataGridViewRow item in roleData.SelectedRows)
                {
                    if (item.DataBoundItem is PrincipalValue value)
                    {
                        PrincipalIndex key = new PrincipalIndex(value);
                        work.AddRange(securityData.Delete(key));
                        work.AddRange(securityData.Load(factory, key));
                    }
                }
            }

            IsLocked(true);
            SuspendBinding(bindingRole);
            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                IsLocked(false);
                ResumeBinding(bindingRole);
            }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            IDatabaseWork factory = BusinessData.GetDbFactory();
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.OpenConnection());

            if (roleData.SelectedRows.Count == 0)
            { work.AddRange(securityData.Save(factory)); }
            else
            {
                foreach (DataGridViewRow item in roleData.SelectedRows)
                {
                    if (item.DataBoundItem is RoleValue value)
                    {
                        RoleIndex key = new RoleIndex(value);
                        work.AddRange(securityData.Save(factory, key));
                    }
                }
            }

            IsLocked(true);
            SuspendBinding(bindingRole);
            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                IsLocked(false);
                ResumeBinding(bindingRole);
                roleData.ClearSelection();
            }
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            IDatabaseWork factory = BusinessData.GetDbFactory();
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.OpenConnection());

            foreach (DataGridViewRow item in roleData.SelectedRows)
            {
                if (item.DataBoundItem is RoleValue value)
                {
                    RoleIndex key = new RoleIndex(value);
                    work.AddRange(securityData.Delete(key));
                    work.AddRange(securityData.Save(factory, key));
                }
            }

            SuspendBinding(bindingRole);
            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                IsLocked(false);
                ResumeBinding(bindingRole);
                roleData.ClearSelection();
            }
        }

        public override void SuspendBinding(BindingSource binding)
        {
            base.SuspendBinding(binding);

            roleData.DataSource = null;
        }

        public override void ResumeBinding(BindingSource binding)
        {
            base.ResumeBinding(binding);

            roleData.DataSource = bindingRole;
            roleData.Sort(roleNameColumn, ListSortDirection.Descending);
            roleData.ClearSelection();
        }


        private void RoleData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
