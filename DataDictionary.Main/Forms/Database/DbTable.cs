﻿using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbTable : ApplicationBase, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingTable.Current is IDbTableItem current && ReferenceEquals(current, item); }

        public DbTable() : base()
        {
            InitializeComponent();

            importDataCommand.DropDown = importOptions;
            importDataCommand.Enabled = true;
            importDataCommand.ButtonClick += ImportDataCommand_Click;
            importDataCommand.ToolTipText = "Import the Table/View to the Domain Model";
        }

        public DbTable(IDbTableItem tableItem) :this()
        {
            DbTableKeyName key = new DbTableKeyName(tableItem);
            DbExtendedPropertyKeyName propertyKey = new DbExtendedPropertyKeyName(key);

            bindingTable.DataSource = new BindingView<DbTableItem>(BusinessData.DatabaseModel.DbTables, w => key.Equals(w));
            bindingTable.Position = 0;

            if (bindingTable.Current is IDbTableItem current)
            {
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();
                this.Icon = new ScopeKey(current).Scope.ToIcon();

                bindingColumns.DataSource = new BindingView<DbTableColumnItem>(BusinessData.DatabaseModel.DbTableColumns, w => key.Equals(w));
                bindingConstraints.DataSource = new BindingView<DbConstraintItem>(BusinessData.DatabaseModel.DbConstraints, w => key.Equals(w));
                bindingProperties.DataSource = new BindingView<DbExtendedPropertyItem>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
            }
        }

        private void DbTable_Load(object sender, EventArgs e)
        {
            IDbTableItem bindingNames;
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), bindingTable, nameof(bindingNames.DatabaseName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), bindingTable, nameof(bindingNames.SchemaName)));
            tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), bindingTable, nameof(bindingNames.TableName)));
            tableTypeData.DataBindings.Add(new Binding(nameof(tableTypeData.Text), bindingTable, nameof(bindingNames.TableType)));
            isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), bindingTable, nameof(bindingNames.IsSystem)));

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = bindingProperties;

            tableColumnsData.AutoGenerateColumns = false;
            tableColumnsData.DataSource = bindingColumns;

            tableConstraintData.AutoGenerateColumns = false;
            tableConstraintData.DataSource = bindingConstraints;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingTable.Current is not IDbTableItem);
        }

        private void ImportDataCommand_Click(object? sender, EventArgs e)
        {
            if (bindingTable.Current is IDbTableItem current)
            {
                BusinessData.DomainModel.Attributes.Import(BusinessData.DatabaseModel, current);
                BusinessData.DomainModel.Entities.Import(BusinessData.DatabaseModel, current);
            }
        }
    }
}
