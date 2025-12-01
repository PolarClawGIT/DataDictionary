using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Security
{
    partial class Authorization
    {
        class FormBinding
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            ISecurity data = ISecurity.Create();

            public required BindingSource PrincipalBinding { private get; init; }
            BindingView<PrincipalValue> Principals =
                new BindingView<PrincipalValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource RoleBinding { private get; init; }
            BindingView<RoleValue> Roles =
                new BindingView<RoleValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource SecurableBinding { private get; init; }
            BindingView<SecurableValue> Securables =
                new BindingView<SecurableValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource MemberBinding { private get; init; }
            BindingView<RoleMembershipValue> RoleMembers =
                new BindingView<RoleMembershipValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource OwnerBinding { private get; init; }
            BindingView<SecurableOwnerValue> SecurableOwners =
                new BindingView<SecurableOwnerValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource PermissionBinding { private get; init; }
            BindingView<SecurablePermissionValue> SecurablePermissions =
                new BindingView<SecurablePermissionValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };


            public FormBinding() : base()
            { }


            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.AddRange(data.Delete());
                work.AddRange(data.Load(factory));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    PrincipalBinding.RaiseListChangedEvents = false;
                    RoleBinding.RaiseListChangedEvents = false;
                    SecurableBinding.RaiseListChangedEvents = false;

                    Principals = new BindingView<PrincipalValue>(data.Principals);
                    Roles = new BindingView<RoleValue>(data.Roles);
                    Securables = new BindingView<SecurableValue>(data.Securables);

                    PrincipalBinding.DataSource = Principals;
                    RoleBinding.DataSource = Roles;
                    SecurableBinding.DataSource = Securables;

                    PrincipalBinding.RaiseListChangedEvents = true;
                    RoleBinding.RaiseListChangedEvents = true;
                    SecurableBinding.RaiseListChangedEvents = true;

                    PrincipalBinding.ResetBindings(false);
                    RoleBinding.ResetBindings(false);
                    MemberBinding.ResetBindings(false);

                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Save(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.AddRange(data.Save(factory));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {

                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public Boolean GetAuthorization(Enumerations.CommandType command)
            {
                Boolean isGrant = false;
                //SecurableIndex securable = BusinessData.Model.ModelIndex;
                //isGrant = BusinessData.Authorization.IsGrant(securable);

                switch (command)
                {
                    case Enumerations.CommandType.Default: return true;
                    case Enumerations.CommandType.Delete: return BusinessData.Authorization.IsSecurityAdmin || isGrant;
                    case Enumerations.CommandType.OpenDatabase: return BusinessData.Authorization.IsSecurityAdmin || isGrant;
                    case Enumerations.CommandType.SaveDatabase: return BusinessData.Authorization.IsSecurityAdmin || isGrant;
                    case Enumerations.CommandType.DeleteDatabase: return BusinessData.Authorization.IsSecurityAdmin || isGrant;
                    case Enumerations.CommandType.HistoryDatabase: return false;
                    default: return false;
                }
            }
        }
    }
}
