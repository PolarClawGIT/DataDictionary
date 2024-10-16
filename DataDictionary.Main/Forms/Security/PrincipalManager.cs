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

            SetIcon(ScopeType.SecurityPrinciple);
            SetCommand(ScopeType.SecurityPrinciple,
                CommandImageType.Add,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase);

            bindingPrinciple.DataSource = securityData.Principles;
            //bindingRole.DataSource = securityData.Roles;
            bindingMembers.DataSource = securityData.Memberships;
            principleList.ResizeColumns();
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
                foreach (PrincipleValue item in securityData.Principles)
                {
                    ListViewItem listItem = new ListViewItem(item.PrincipleLogin);
                    principleList.Items.Add(listItem);
                    listPrinciples.Add(listItem, item);
                }
                if (principleList.Items.Count > 0)
                { principleList.Items[0].Selected = true; }

                principleLoginData.DataBindings.Add(new Binding(nameof(principleLoginData.Text), bindingPrinciple, nameof(IPrincipleValue.PrincipleLogin), false, DataSourceUpdateMode.OnPropertyChanged));
                principleNameData.DataBindings.Add(new Binding(nameof(principleNameData.Text), bindingPrinciple, nameof(IPrincipleValue.PrincipleName), false, DataSourceUpdateMode.OnPropertyChanged));
                principleAnnotationData.DataBindings.Add(new Binding(nameof(principleAnnotationData.Text), bindingPrinciple, nameof(IPrincipleValue.PrincipleAnnotation), false, DataSourceUpdateMode.OnPropertyChanged));

                RoleNameList.Load(roleIdColumn, securityData.Roles);
                roleMembershipData.AutoGenerateColumns = false;
                roleMembershipData.DataSource = bindingMembers;
            }
        }


        private void BindingPrinciple_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingPrinciple.Current is PrincipleValue current)
            {
                PrincipleIndex key = new PrincipleIndex(current);
                bindingMembers.DataSource = null;
                bindingMembers.DataSource = new BindingView<RoleMembershipValue>(securityData.Memberships, w => key.Equals(w));
            }
        }

        Dictionary<ListViewItem, PrincipleValue> listPrinciples = new Dictionary<ListViewItem, PrincipleValue>();
        private void PrincipleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bindingPrinciple.DataSource is IList<PrincipleValue> values
                && principleList.SelectedItems.OfType<ListViewItem>().FirstOrDefault() is ListViewItem item
                && listPrinciples.ContainsKey(item))
            { bindingPrinciple.Position = values.IndexOf(listPrinciples[item]); }
        }

        private void PrincipleList_Resize(object sender, EventArgs e)
        { principleList.ResizeColumns(); }

        private void BindingMembers_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingPrinciple.Current is PrincipleValue current)
            { e.NewObject = new RoleMembershipValue(current); }
        }
    }
}
