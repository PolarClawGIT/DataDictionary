using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Controls;
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
    partial class PrincipalManager : ApplicationData
    {
        //TODO: Build out screen.
        // List of Ownership  (view only? Scoped to Model?)

        ISecurity securityData = ISecurity.Create();

        public PrincipalManager()
        {
            InitializeComponent();

            SetIcon(ScopeType.SecurityPrincipal);
            SetCommand(ScopeType.SecurityPrincipal,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);

            principalData.AutoGenerateColumns = false;
            membershipData.AutoGenerateColumns = false;
            ownershipData.AutoGenerateColumns = false;

        }

        private void PrincipalManager_Load(object sender, EventArgs e)
        {
            IsLocked(true);
            IDatabaseWork factory = BusinessData.GetDbFactory();
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.OpenConnection());
            work.AddRange(securityData.Load(factory));
            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                bindingPrincipal.DataSource = securityData.Principals;

                principalData.DataSource = bindingPrincipal;
                membershipData.DataSource = bindingMembers;
                ownershipData.DataSource = bindingOwnership;

                principalLoginData.DataBindings.Add(new Binding(nameof(principalLoginData.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalLogin), false, DataSourceUpdateMode.OnPropertyChanged));
                principalNameData.DataBindings.Add(new Binding(nameof(principalNameData.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalName), false, DataSourceUpdateMode.OnPropertyChanged));
                principalAnnotationData.DataBindings.Add(new Binding(nameof(principalAnnotationData.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalAnnotation), false, DataSourceUpdateMode.OnPropertyChanged));

                RoleNameList.Load(roleIdColumn, securityData.Roles);

                if (BusinessData.Authorization.IsSecurityAdmin)
                {
                    CommandButtons[CommandImageType.Add].IsEnabled = true;
                    CommandButtons[CommandImageType.Delete].IsEnabled = true;
                    CommandButtons[CommandImageType.OpenDatabase].IsEnabled = true;
                    CommandButtons[CommandImageType.SaveDatabase].IsEnabled = true;
                    CommandButtons[CommandImageType.DeleteDatabase].IsEnabled = true;
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

        private void BindingPrincipal_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingPrincipal.Current is PrincipalValue current)
            {
                PrincipalIndex key = new PrincipalIndex(current);
                bindingMembers.DataSource = null;
                bindingMembers.DataSource = new BindingView<RoleMembershipValue>(securityData.Memberships, w => key.Equals(w));

                bindingOwnership.DataSource = null;
                bindingOwnership.DataSource = new BindingView<ObjectOwnerValue>(securityData.Owners, w => key.Equals(w));
            }
        }

        private void BindingPrincipal_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new PrincipalValue()
            { PrincipalLogin = "domain\\user", PrincipalName = "(new User)" };
        }

        private void BindingMembers_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingPrincipal.Current is PrincipalValue current)
            { e.NewObject = new RoleMembershipValue(current); }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            bindingPrincipal.AddNew();
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            if (bindingPrincipal.Current is PrincipalValue current)
            {
                PrincipalIndex key = new PrincipalIndex(current);

                SuspendBinding();
                DoWork(securityData.Delete(key), onCompleting);

                //foreach (RoleMembershipValue item in securityData.Memberships.Where(w => key.Equals(w)))
                //{ securityData.Memberships.Remove(item); }

                //foreach (ObjectOwnerValue item in securityData.Owners.Where(w => key.Equals(w)))
                //{ securityData.Owners.Remove(item); }

                //securityData.Principals.Remove(current);

                void onCompleting(RunWorkerCompletedEventArgs args)
                { ResumeBinding(key); }
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

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
        }

        void ResumeBinding(PrincipalIndex? current = null)
        {


            bindingPrincipal.DataSource = securityData.Principals;

            if (current is PrincipalIndex key
                && bindingPrincipal.DataSource is IList<PrincipalValue> values
                && values.FirstOrDefault(w => key.Equals(w)) is PrincipalValue value)
            { bindingPrincipal.Position = values.IndexOf(value); }
            else
            { bindingPrincipal.Position = 0; }

            principalData.DataSource = bindingPrincipal;
            membershipData.DataSource = bindingMembers;
            ownershipData.DataSource = bindingOwnership;

            principalLoginData.DataBindings.Add(new Binding(nameof(principalLoginData.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalLogin), false, DataSourceUpdateMode.OnPropertyChanged));
            principalNameData.DataBindings.Add(new Binding(nameof(principalNameData.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalName), false, DataSourceUpdateMode.OnPropertyChanged));
            principalAnnotationData.DataBindings.Add(new Binding(nameof(principalAnnotationData.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalAnnotation), false, DataSourceUpdateMode.OnPropertyChanged));

            //bindingPrincipal.ResumeBinding();
            //bindingMembers.ResumeBinding();
            //bindingOwnership.ResumeBinding();
        }

        void SuspendBinding()
        {
            // BindingSource.SuspendBinding() & ResumeBinding()
            // are not working as expected.
            // This is brute force.

            principalData.DataSource = null;
            membershipData.DataSource = null;
            ownershipData.DataSource = null;

            principalLoginData.DataBindings.Clear();
            principalNameData.DataBindings.Clear();
            principalAnnotationData.DataBindings.Clear();

            bindingPrincipal.DataSource = null ;
            bindingMembers.DataSource = null;
            bindingOwnership.DataSource = null;

            //bindingPrincipal.SuspendBinding();
            //bindingMembers.SuspendBinding();
            //bindingOwnership.SuspendBinding();
        }

        private void principalData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void bindingPrincipal_DataError(object sender, BindingManagerDataErrorEventArgs e)
        {

        }
    }
}
