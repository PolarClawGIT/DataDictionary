using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.Main.Messages;
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
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    partial class DbConstraint : ApplicationFormBase, IApplicationDataForm
    {

        class FormData
        {
            public DbConstraintKey? ConstraintKey { get; set; }
            public IDbConstraintItem? DbConstraint { get; set; }
        }
        FormData data = new FormData();

        public Object? OpenItem { get; }

        public DbConstraint() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbKey;
        }

        public DbConstraint(IDbConstraintItem constraintItem) : this()
        {
            data.ConstraintKey = new DbConstraintKey(constraintItem);
            OpenItem = constraintItem;
            this.Text = data.ConstraintKey.ToString();
        }


        private void DbConstraint_Load(object sender, EventArgs e)
        { BindData(); }

        private void BindData()
        {
            if (data.ConstraintKey is not null)
            { data.DbConstraint = Program.Data.DbConstraints.FirstOrDefault(w => data.ConstraintKey == new DbConstraintKey(w)); }

            if (data.ConstraintKey is not null && data.DbConstraint is not null)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data.DbConstraint, nameof(data.DbConstraint.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data.DbConstraint, nameof(data.DbConstraint.SchemaName)));
                constraintNameData.DataBindings.Add(new Binding(nameof(constraintNameData.Text), data.DbConstraint, nameof(data.DbConstraint.ConstraintName)));
                constraintTypeData.DataBindings.Add(new Binding(nameof(constraintTypeData.Text), data.DbConstraint, nameof(data.DbConstraint.ConstraintType)));
                referenceSchemaNameData.DataBindings.Add(new Binding(nameof(referenceSchemaNameData.Text), data.DbConstraint, nameof(data.DbConstraint.ReferenceSchemaName)));
                referenceTableNameData.DataBindings.Add(new Binding(nameof(referenceTableNameData.Text), data.DbConstraint, nameof(data.DbConstraint.ReferenceObjectName)));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.DbExtendedProperties.GetProperties(data.DbConstraint).ToList();

                constraintColumnsData.AutoGenerateColumns = false;
                constraintColumnsData.DataSource = new BindingView<DbConstraintColumnItem>(Program.Data.DbConstraintColumns, w => new DbConstraintKey(w).Equals(data.ConstraintKey));
            }
        }

        private void UnBindData()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            constraintNameData.DataBindings.Clear();
            constraintTypeData.DataBindings.Clear();
            referenceSchemaNameData.DataBindings.Clear();
            referenceTableNameData.DataBindings.Clear();

            extendedPropertiesData.DataSource = null;
            constraintColumnsData.DataSource = null;
        }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }



        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion
    }
}
