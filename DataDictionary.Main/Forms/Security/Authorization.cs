using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Security
{
    partial class Authorization : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
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

            SetCommand(ScopeType.Security,
                CommandType.OpenDatabase,
                CommandType.SaveDatabase);

            authorizationTab.ImageList = new ImageList().
                AddImages(
                ScopeType.SecurityPrincipal,
                ScopeType.SecurityRole,
                ScopeType.SecuritySecurable);

            principalsTab.ImageKey = ScopeType.SecurityPrincipal.GetName();
            rolesTab.ImageKey = ScopeType.SecurityRole.GetName();
            securableTab.ImageKey = ScopeType.SecuritySecurable.GetName();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {
            formBinding.Load(onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is null)
                {
                   //TODO:  Do the Binding
                }
            }
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
        }


    }
}
