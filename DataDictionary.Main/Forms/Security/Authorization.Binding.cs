using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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
            BindingView<RoleMembershipValue> Memberships =
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
                work.Add(factory.OpenConnection());
                work.AddRange(data.Load(factory));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    PrincipalBinding.RaiseListChangedEvents = false;
                    RoleBinding.RaiseListChangedEvents = false;
                    SecurableBinding.RaiseListChangedEvents = false;

                    PrincipalBinding.CurrentChanged -= PrincipalBinding_CurrentChanged;
                    SecurableBinding.CurrentChanged -= SecurableBinding_CurrentChanged;

                    Principals = new BindingView<PrincipalValue>(data.Principals);
                    Roles = new BindingView<RoleValue>(data.Roles);
                    Securables = new BindingView<SecurableValue>(data.Securables)
                    { AllowEdit = false, AllowNew = false, AllowRemove = false };

                    PrincipalBinding.DataSource = Principals;
                    RoleBinding.DataSource = Roles;
                    SecurableBinding.DataSource = Securables;

                    PrincipalBinding.RaiseListChangedEvents = true;
                    RoleBinding.RaiseListChangedEvents = true;
                    SecurableBinding.RaiseListChangedEvents = true;

                    PrincipalBinding.ResetBindings(false);
                    RoleBinding.ResetBindings(false);
                    SecurableBinding.ResetBindings(false);

                    RefreshMembership();
                    RefreshSecurable();

                    PrincipalBinding.CurrentChanged += PrincipalBinding_CurrentChanged;
                    SecurableBinding.CurrentChanged += SecurableBinding_CurrentChanged;

                    if (onComplete is not null) { onComplete(args); }
                }

                void PrincipalBinding_CurrentChanged(Object? sender, EventArgs e)
                { RefreshMembership(); }

                void SecurableBinding_CurrentChanged(Object? sender, EventArgs e)
                { RefreshSecurable(); }
            }

            public void Save(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(data.Save(factory));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                { if (onComplete is not null) { onComplete(args); } }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out PrincipalValue? result)
            {
                if (PrincipalBinding.Position >= 0
                    && PrincipalBinding.Current is PrincipalValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetValue([NotNullWhen(true)] out SecurableValue? result)
            {
                if (SecurableBinding.Position >= 0
                    && SecurableBinding.Current is SecurableValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryNewValue([NotNullWhen(true)] out RoleMembershipValue? result)
            {
                if (PrincipalBinding.Position >= 0
                    && PrincipalBinding.Current is PrincipalValue value)
                {
                    PrincipalIndex key = new PrincipalIndex(value);
                    result = new RoleMembershipValue(key);
                    return true;
                }
                else { result = null; return false; }
            }

            public Boolean TryNewValue([NotNullWhen(true)] out SecurableOwnerValue? result)
            {
                if (SecurableBinding.Position >= 0
                    && SecurableBinding.Current is SecurableValue value)
                {
                    SecurableIndex key = new SecurableIndex(value);
                    result = new SecurableOwnerValue(key);
                    return true;
                }
                else { result = null; return false; }
            }

            public Boolean TryNewValue([NotNullWhen(true)] out SecurablePermissionValue? result)
            {
                if (SecurableBinding.Position >= 0
                    && SecurableBinding.Current is SecurableValue value)
                {
                    SecurableIndex key = new SecurableIndex(value);
                    result = new SecurablePermissionValue(key);
                    return true;
                }
                else { result = null; return false; }
            }

            public Boolean TryRemovePrincipal()
            {
                if (PrincipalBinding.Position >= 0
                    && PrincipalBinding.Current is PrincipalValue value)
                {
                    PrincipalIndex key = new PrincipalIndex(value);

                    foreach (RoleMembershipValue item in Memberships.Where(w => key.Equals(w)).ToList())
                    { Memberships.Remove(item); }

                    Principals.Remove(value);
                    return true;
                }
                else { return false; }
            }

            public Boolean TryRemoveRole()
            {
                if (RoleBinding.Position >= 0
                    && RoleBinding.Current is RoleValue value)
                {
                    RoleIndex key = new RoleIndex(value);

                    foreach (RoleMembershipValue item in Memberships.Where(w => key.Equals(w)).ToList())
                    { Memberships.Remove(item); }

                    foreach (SecurablePermissionValue item in SecurablePermissions.Where(w => key.Equals(w)).ToList())
                    { SecurablePermissions.Remove(item); }

                    Roles.Remove(value);
                    return true;
                }
                else { return false; }
            }

            public Boolean TryRemoveSecurable ()
            {
                if (SecurableBinding.Position >= 0
                    && SecurableBinding.Current is SecurableValue value)
                {
                    SecurableIndex key = new SecurableIndex(value);

                    foreach (var item in SecurableOwners.Where(w => key.Equals(w)).ToList())
                    { SecurableOwners.Remove(item); }

                    foreach (SecurablePermissionValue item in SecurablePermissions.Where(w => key.Equals(w)).ToList())
                    { SecurablePermissions.Remove(item); }

                    SecurableBinding.Remove(value);
                    return true;
                }
                else { return false; }
            }


            void RefreshMembership()
            {
                MemberBinding.RaiseListChangedEvents = false;

                if (TryGetValue(out PrincipalValue? principal))
                {
                    PrincipalIndex key = new PrincipalIndex(principal);
                    Memberships = new BindingView<RoleMembershipValue>(data.Memberships, w => key.Equals(w));
                }
                else
                {
                    Memberships = new BindingView<RoleMembershipValue>([])
                    { AllowEdit = false, AllowNew = false, AllowRemove = false };
                }

                MemberBinding.DataSource = Memberships;
                MemberBinding.RaiseListChangedEvents = true;
                MemberBinding.ResetBindings(false);
            }

            void RefreshSecurable()
            {
                OwnerBinding.RaiseListChangedEvents = false;
                PermissionBinding.RaiseListChangedEvents = false;

                if (TryGetValue(out SecurableValue? securable))
                {
                    SecurableIndex key = new SecurableIndex(securable);

                    SecurableOwners = new BindingView<SecurableOwnerValue>(data.Owners, w => key.Equals(w));
                    SecurablePermissions = new BindingView<SecurablePermissionValue>(data.Permissions, w => key.Equals(w));
                }
                else
                {
                    SecurableOwners = new BindingView<SecurableOwnerValue>([])
                    { AllowEdit = false, AllowNew = false, AllowRemove = false };

                    SecurablePermissions = SecurablePermissions = new BindingView<SecurablePermissionValue>([])
                    { AllowEdit = false, AllowNew = false, AllowRemove = false };
                }

                OwnerBinding.DataSource = SecurableOwners;
                PermissionBinding.DataSource = SecurablePermissions;
                OwnerBinding.RaiseListChangedEvents = true;
                PermissionBinding.RaiseListChangedEvents = true;
                OwnerBinding.ResetBindings(false);
                PermissionBinding.ResetBindings(false);
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
                    default: return false;
                }
            }

            public Boolean? GetLocked()
            {
                return !BusinessData.Authorization.IsSecurityAdmin;
            }
        }
    }
}
