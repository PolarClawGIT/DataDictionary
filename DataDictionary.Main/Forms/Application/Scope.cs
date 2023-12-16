using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Application
{
    partial class Scope : ApplicationBase
    {
        public Scope()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Scope;
        }

        private void Scope_Load(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            if (bindingSource.DataSource is null)
            { bindingSource.DataSource = Program.Data.Scopes; }

            scopeNavigation.AutoGenerateColumns = false;
            scopeNavigation.DataSource = bindingSource;

            IScopeItem nameOf;
            scopeNameData.DataBindings.Add(new Binding(nameof(scopeNameData.Text), bindingSource, nameof(nameOf.ScopeName), true));
            scopeDescriptionData.DataBindings.Add(new Binding(nameof(scopeDescriptionData.Text), bindingSource, nameof(nameOf.ScopeDescription), true));
        }

        void UnBindData() {
            scopeNameData.DataBindings.Clear();
            scopeDescriptionData.DataBindings.Clear();
            scopeNavigation.DataSource = null;
            bindingSource.DataSource = null;
        }

    }
}
