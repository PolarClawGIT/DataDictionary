using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Security
{
    partial class Authorization : ApplicationData
    {
        public override Boolean IsOpenItem(object? item)
        { return true; } // Only single copy allowed

        FormBinding formBinding;

        public Authorization()
        {
            InitializeComponent();

            formBinding = new FormBinding()
            {
                PrincipalBinding = bindingPrincipal,
                RoleBinding = bindingRole,
                SecurableBinding = bindingSecurable,
                MemberBinding = bindingMember,
                OwnerBinding = bindingOwner,
                PermissionBinding = bindingPermission,
                DoWork = base.DoWork
            };

            SetIcon(ScopeType.Security);

            SetCommand(
                ButtonType.OpenDatabase,
                ButtonType.SaveDatabase);

            authorizationTab.ImageList = new ImageList();
            authorizationTab.ImageList.AddImages(
                ScopeType.SecurityPrincipal,
                ScopeType.SecurityRole,
                ScopeType.SecuritySecurable);

            principalsTab.ImageKey = ScopeType.SecurityPrincipal.GetName();
            rolesTab.ImageKey = ScopeType.SecurityRole.GetName();
            securableTab.ImageKey = ScopeType.SecuritySecurable.GetName();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {
            formBinding.Load(doBinding);

            void doBinding(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is null)
                {
                    principalData.AutoGenerateColumns = false;
                    principalData.DataSource = bindingPrincipal;

                    principalLoginData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalLogin)));
                    principalNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalName)));
                    principalAnnotationData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalAnnotation)));

                    membershipData.AutoGenerateColumns = false;
                    membershipData.DataSource = bindingMember;

                    membershipColumn.DataPropertyName = nameof(IRoleMembershipValue.RoleId);
                    membershipColumn.ValueMember = nameof(IRoleValue.RoleId);
                    membershipColumn.DisplayMember = nameof(IRoleValue.RoleName);
                    membershipColumn.DataSource = bindingRole;

                    roleData.AutoGenerateColumns = false;
                    roleData.DataSource = bindingRole;

                    roleNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingRole, nameof(IRoleValue.RoleName)));
                    roleDescriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingRole, nameof(IRoleValue.RoleDescription)));

                    isSecurityAdminData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingRole, nameof(IRoleValue.IsSecurityAdmin)));
                    isHelpAdminData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingRole, nameof(IRoleValue.IsHelpAdmin)));
                    isHelpOwnerData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingRole, nameof(IRoleValue.IsHelpOwner)));
                    isCatalogAdminData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingRole, nameof(IRoleValue.IsCatalogAdmin)));
                    isCatalogOwnerData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingRole, nameof(IRoleValue.IsCatalogOwner)));
                    isLibraryAdminData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingRole, nameof(IRoleValue.IsLibraryAdmin)));
                    isLibraryOwnerData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingRole, nameof(IRoleValue.IsLibraryOwner)));
                    isModelAdminData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingRole, nameof(IRoleValue.IsModelAdmin)));
                    isModelOwnerData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingRole, nameof(IRoleValue.IsModelOwner)));
                    isScriptAdminData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingRole, nameof(IRoleValue.IsScriptAdmin)));
                    isScriptOwnerData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingRole, nameof(IRoleValue.IsScriptOwner)));

                    securableData.AutoGenerateColumns = false;
                    securableData.DataSource = bindingSecurable;

                    securableTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSecurable, nameof(ISecurableValue.SecurableTitle)));

                    principlaNameColumn.DataPropertyName = nameof(ISecurableOwnerValue.PrincipalId);
                    principlaNameColumn.ValueMember = nameof(IPrincipalValue.PrincipalId);
                    principlaNameColumn.DisplayMember = nameof(IPrincipalValue.PrincipalName);
                    principlaNameColumn.DataSource = bindingPrincipal;

                    objectPermissionData.AutoGenerateColumns = false;
                    objectPermissionData.DataSource = bindingPermission;

                    permissionRoleColumn.DataPropertyName = nameof(ISecurablePermissionValue.RoleId);
                    permissionRoleColumn.ValueMember = nameof(IRoleValue.RoleId);
                    permissionRoleColumn.DisplayMember = nameof(IRoleValue.RoleName);
                    permissionRoleColumn.DataSource = bindingRole;

                    // Security
                    IsLocked(formBinding.GetLocked());
                    SetAuthorization(formBinding.GetAuthorization);
                }
            }
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            formBinding.Load(onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            formBinding.Save(onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { }
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
        }

        private void BindingMember_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (formBinding.TryNewValue(out RoleMembershipValue? value))
            { e.NewObject = value; }
        }

        private void BindingOwner_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (formBinding.TryNewValue(out SecurableOwnerValue? value))
            { e.NewObject = value; }
        }

        private void BindingPermission_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (formBinding.TryNewValue(out SecurablePermissionValue? value))
            { e.NewObject = value; }
        }

        private void PrincipalData_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        { e.Cancel = formBinding.TryRemovePrincipal(); }

        private void SecurableData_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        { e.Cancel = formBinding.TryRemoveSecurable(); }

        private void RoleData_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        { e.Cancel = formBinding.TryRemoveRole(); }
    }
}
