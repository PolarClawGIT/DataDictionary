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

            bindingPrincipal.DataSource = securityData.Principals;
            //bindingRole.DataSource = securityData.Roles;
            bindingMembers.DataSource = securityData.Memberships;

        }

        private void PrincipalManager_Load(object sender, EventArgs e)
        {

            IDatabaseWork factory = BusinessData.GetDbFactory();
            List<WorkItem> work = new List<WorkItem>();
            work.Add(factory.OpenConnection());
            work.AddRange(securityData.Load(factory));
            DoWork(work, onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            {
                principalLoginData.DataBindings.Add(new Binding(nameof(principalLoginData.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalLogin), false, DataSourceUpdateMode.OnPropertyChanged));
                principalNameData.DataBindings.Add(new Binding(nameof(principalNameData.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalName), false, DataSourceUpdateMode.OnPropertyChanged));
                principalAnnotationData.DataBindings.Add(new Binding(nameof(principalAnnotationData.Text), bindingPrincipal, nameof(IPrincipalValue.PrincipalAnnotation), false, DataSourceUpdateMode.OnPropertyChanged));

                RoleNameList.Load(roleIdColumn, securityData.Roles);
                roleMembershipData.AutoGenerateColumns = false;
                //roleMembershipData.DataSource = bindingMembers;

                principalOwnershipData.AutoGenerateColumns = false;
                //principalOwnershipData.DataSource = bindingOwnership;
            }
        }


        private void BindingPrincipal_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingPrincipal.Current is PrincipalValue current)
            {
                PrincipalIndex key = new PrincipalIndex(current);
                bindingMembers.DataSource = null;
                bindingMembers.DataSource = new BindingView<RoleMembershipValue>(securityData.Memberships, w => key.Equals(w));

                principalOwnershipData.DataSource = null;
                principalOwnershipData.DataSource = new BindingView<ObjectOwnerValue>(securityData.Owners, w => key.Equals(w));
            }
        }

        private void BindingMembers_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingPrincipal.Current is PrincipalValue current)
            { e.NewObject = new RoleMembershipValue(current); }
        }
    }
}
