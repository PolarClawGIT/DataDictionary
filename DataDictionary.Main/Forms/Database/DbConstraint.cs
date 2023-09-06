using DataDictionary.DataLayer.DatabaseData;
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

namespace DataDictionary.Main.Forms.Database
{
    partial class DbConstraint : ApplicationBase, IApplicationDataForm
    {

        public DbConstraintKey DataKey { get; private set; }

        public Object? OpenItem { get; }

        public DbConstraint() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbKey;
            DataKey = new DbConstraintKey(new DbConstraintItem());
        }

        public DbConstraint(IDbConstraintKey constraintItem) : this()
        {
            DataKey = new DbConstraintKey(constraintItem);
            this.Text = DataKey.ToString();
        }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }

        private void DbConstraint_Load(object sender, EventArgs e)
        { BindData(); }

        private void BindData()
        {
            DbConstraintItem? data = Program.Data.DbConstraints.FirstOrDefault(w => DataKey.Equals(w));

            if (data is not null)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data, nameof(data.SchemaName)));
                constraintNameData.DataBindings.Add(new Binding(nameof(constraintNameData.Text), data, nameof(data.ConstraintName)));
                constraintTypeData.DataBindings.Add(new Binding(nameof(constraintTypeData.Text), data, nameof(data.ConstraintType)));
                tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), data, nameof(data.TableName)));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.DbExtendedProperties.GetProperties(DataKey).ToList();

                constraintColumnsData.AutoGenerateColumns = false;
                constraintColumnsData.DataSource = new BindingView<DbConstraintColumnItem>(Program.Data.DbConstraintColumns, w => DataKey.Equals(w));
            }
        }

        private void UnBindData()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            constraintNameData.DataBindings.Clear();
            constraintTypeData.DataBindings.Clear();
            tableNameData.DataBindings.Clear();

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
