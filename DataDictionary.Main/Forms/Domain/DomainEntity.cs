using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
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

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainEntity : ApplicationBase, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingEntity.Current is IDomainEntityItem current && ReferenceEquals(current, item); }

        public DomainEntity() : base()
        {
            InitializeComponent();
            newItemCommand.Click += NewItemCommand_Click;
        }

        private void NewItemCommand_Click(object? sender, EventArgs e)
        {
            if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == propertyTab)
            {
                bindingProperty.AddNew();
                domainProperty.RefreshControls();
            }
            else if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == aliasTab)
            {
                bindingAlias.AddNew();
                domainAlias.RefreshControls();
            }
            else { }
        }

        public DomainEntity(IDomainEntityItem entityItem) :this()
        {
            DomainEntityKey key = new DomainEntityKey(entityItem);
            bindingEntity.DataSource = new BindingView<DomainEntityItem>(BusinessData.DomainModel.Entities, w => key.Equals(w));
            bindingEntity.Position = 0;

            if (bindingEntity.Current is IDomainEntityItem current)
            {
                this.Icon = new ScopeKey(ScopeType.ModelEntity).Scope.ToIcon();
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();

                bindingProperty.DataSource = new BindingView<DomainEntityPropertyItem>(BusinessData.DomainModel.Entities.Properties, w => key.Equals(w));
                bindingAlias.DataSource = new BindingView<DomainEntityAliasItem>(BusinessData.DomainModel.Entities.Aliases, w => key.Equals(w));
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            IDomainEntityItem nameOfValues;
            PropertyNameItem.Load(propertyIdColumn);
            ScopeNameItem.Load(aliaseScopeColumn); 

            this.DataBindings.Add(new Binding(nameof(this.Text), bindingEntity, nameof(nameOfValues.EntityTitle)));
            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingEntity, nameof(nameOfValues.EntityTitle)));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingEntity, nameof(nameOfValues.EntityDescription)));


            propertiesData.AutoGenerateColumns = false;
            propertiesData.DataSource = bindingProperty;
            domainProperty.BindData(bindingProperty);

            aliasesData.AutoGenerateColumns = false;
            aliasesData.DataSource = bindingAlias;
            domainAlias.BindData(bindingAlias);

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingEntity.Current is not IDomainEntityItem);
        }

        private void DetailTabLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == propertyTab)
            {
                newItemCommand.Enabled = true;
                newItemCommand.Image = Resources.NewProperty;
                newItemCommand.ToolTipText = "add Property";
            }
            else if (detailTabLayout.TabPages[detailTabLayout.SelectedIndex] == aliasTab)
            {
                newItemCommand.Enabled = true;
                newItemCommand.Image = Resources.NewSynonym;
                newItemCommand.ToolTipText = "add Alias";
            }
            else
            {
                newItemCommand.Enabled = false;
                newItemCommand.Image = Resources.NewDocument;
            }
        }

        private void BindingProperty_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingEntity.Current is DomainEntityItem current)
            {
                DomainEntityPropertyItem newItem = new DomainEntityPropertyItem(current);
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingEntity.Current is DomainEntityItem current)
            {
                DomainEntityAliasItem newItem = new DomainEntityAliasItem(current);
                e.NewObject = newItem;
            }
        }
    }
}
