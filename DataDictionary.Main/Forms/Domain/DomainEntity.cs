using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainEntity : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingEntity.Current is IEntityValue current && ReferenceEquals(current, item); }

        public DomainEntity() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(entityToolStrip, 0);
        }

        public DomainEntity(IEntityValue? entityItem) : this()
        {
            if (entityItem is null)
            {
                entityItem = new EntityValue();
                BusinessData.DomainModel.Entities.Add(entityItem);
            }

            EntityIndex key = new EntityIndex(entityItem);
            bindingEntity.DataSource = new BindingView<EntityValue>(BusinessData.DomainModel.Entities, w => key.Equals(w));
            bindingEntity.Position = 0;

            Setup(bindingEntity);

            if (bindingEntity.Current is IEntityValue current)
            {
                bindingProperty.DataSource = new BindingView<EntityPropertyValue>(BusinessData.DomainModel.Entities.Properties, w => key.Equals(w));
                bindingAlias.DataSource = new BindingView<EntityAliasValue>(BusinessData.DomainModel.Entities.Aliases, w => key.Equals(w));
                bindingSubjectArea.DataSource = new BindingView<EntitySubjectAreaValue>(BusinessData.DomainModel.Entities.SubjectArea, w => key.Equals(w));
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            IEntityValue nameOfValues;
            PropertyNameMember.Load(propertyIdColumn);
            ScopeNameMember.Load(aliaseScopeColumn);

            this.DataBindings.Add(new Binding(nameof(this.Text), bindingEntity, nameof(nameOfValues.EntityTitle)));
            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingEntity, nameof(nameOfValues.EntityTitle)));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingEntity, nameof(nameOfValues.EntityDescription)));

            EntityNameMember.Load(typeOfEntityData, BusinessData.DomainModel.Entities);
            typeOfEntityData.DataBindings.Add(new Binding(nameof(typeOfEntityData.SelectedValue), bindingEntity, nameof(nameOfValues.TypeOfEntityId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty));

            propertiesData.AutoGenerateColumns = false;
            propertiesData.DataSource = bindingProperty;

            aliasesData.AutoGenerateColumns = false;
            aliasesData.DataSource = bindingAlias;

            subjectArea.BindTo(bindingSubjectArea);

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingEntity.Current is not IEntityValue);
        }

        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {
            if (bindingEntity.Current is IEntityValue current)
            { BusinessData.DomainModel.Entities.Delete(current); }
        }

        private void BindingProperty_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingEntity.Current is IEntityValue current)
            {
                EntityPropertyValue newItem = new EntityPropertyValue(current);
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingEntity.Current is EntityValue current)
            {
                EntityAliasValue newItem = new EntityAliasValue(current);
                newItem.AliasName = namedScopeData.ScopePath.MemberFullPath;
                newItem.Scope = namedScopeData.Scope;
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingAlias.Current is AttributeAliasValue current)
            {
                NamedScopePath path = new NamedScopePath(NamedScopePath.Parse(current.AliasName).ToArray());

                namedScopeData.ScopePath = path;
                namedScopeData.Scope = current.Scope;
            }
        }

        private void NamedScopeData_OnApply(object sender, EventArgs e)
        {
            if (bindingAlias.DataSource is IList<IAliasValue> aliases
                && aliases.FirstOrDefault(
                    w => w.Scope == namedScopeData.Scope
                    && new NamedScopePath(NamedScopePath.Parse(w.AliasName).ToArray()) == namedScopeData.ScopePath)
                is IAliasValue value)
            { bindingAlias.Position = aliases.IndexOf(value); }
            else { bindingAlias.AddNew(); }
        }

        private void BindingProperty_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingProperty.Current is IPropertyValue current)
            {
                domainProperty.PropertyId = current.PropertyId ?? Guid.Empty;
                domainProperty.PropertyValue = current.PropertyValue ?? String.Empty; ;
                domainProperty.DefinitionText = current.DefinitionText ?? String.Empty;
            }
        }

        private void DomainProperty_OnApply(object sender, EventArgs e)
        {
            if (bindingProperty.DataSource is IList<IPropertyValue> properties
                && properties.FirstOrDefault(
                    w => w.PropertyId == domainProperty.PropertyId)
                is IPropertyValue value)
            {
                value.PropertyValue = domainProperty.PropertyValue;
                value.DefinitionText = domainProperty.DefinitionText;
                bindingProperty.Position = properties.IndexOf(value);
            }
            else { bindingProperty.AddNew(); }
        }

        private void BindingSubjectArea_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (addingSubject is SubjectAreaValue subject && bindingEntity.Current is EntityValue entity)
            {
                EntitySubjectAreaValue newItem = new EntitySubjectAreaValue(entity, subject);
                e.NewObject = newItem;
            }
            addingSubject = null;
        }

        SubjectAreaValue? addingSubject = null;
        private void SubjectArea_OnSubjectAdd(object sender, SubjectAreaValue e)
        {
            addingSubject = e;
            bindingSubjectArea.AddNew();
        }

        private void SubjectArea_OnSubjectRemove(object sender, SubjectAreaValue e)
        {
            SubjectAreaIndex key = new SubjectAreaIndex(e);

            if (bindingSubjectArea.DataSource is IEnumerable<ISubjectAreaIndex> data
                && data.FirstOrDefault(w => key.Equals(w)) is EntitySubjectAreaValue target)
            { bindingSubjectArea.Remove(target); }
        }
    }
}
