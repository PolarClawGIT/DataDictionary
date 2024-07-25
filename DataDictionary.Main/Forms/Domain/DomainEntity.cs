using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.Main.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using DataDictionary.Resource.Enumerations;
using DataDictionary.Main.Dialogs;
using System.Linq;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainEntity : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingEntity.Current is IEntityValue current && ReferenceEquals(current, item); }

        protected DomainEntity() : base()
        {
            InitializeComponent();

            attributeSelect.Image = ImageEnumeration.GetImage(ScopeType.ModelAttribute, CommandImageType.Default);
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

            if (bindingEntity.Current is IEntityValue current)
            {
                Setup(bindingEntity, CommandImageType.Delete);

                bindingProperty.DataSource = new BindingView<EntityPropertyValue>(BusinessData.DomainModel.Entities.Properties, w => key.Equals(w));
                bindingDefinition.DataSource = new BindingView<EntityDefinitionValue>(BusinessData.DomainModel.Entities.Definitions, w => key.Equals(w));
                bindingAlias.DataSource = new BindingView<EntityAliasValue>(BusinessData.DomainModel.Entities.Aliases, w => key.Equals(w));
                bindingSubjectArea.DataSource = new BindingView<EntitySubjectAreaValue>(BusinessData.DomainModel.Entities.SubjectArea, w => key.Equals(w));
                bindingAttributeDetail.DataSource = new BindingView<AttributeValue>(BusinessData.DomainModel.Attributes, w => true);
                bindingAttribute.DataSource = new BindingView<EntityAttributeValue>(BusinessData.DomainModel.Entities.Attributes, w => key.Equals(w));
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            PropertyNameList.Load(propertyIdColumn);
            DefinitionNameList.Load(definitionColumn);
            ScopeNameList.Load(aliaseScopeColumn);

            this.DataBindings.Add(new Binding(nameof(this.Text), bindingEntity, nameof(IEntityValue.EntityTitle), false, DataSourceUpdateMode.OnPropertyChanged));

            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingEntity, nameof(IEntityValue.EntityTitle)));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingEntity, nameof(IEntityValue.EntityDescription)));

            memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), bindingEntity, nameof(IAttributeValue.MemberName), false, DataSourceUpdateMode.OnPropertyChanged));

            propertiesData.AutoGenerateColumns = false;
            propertiesData.DataSource = bindingProperty;

            definitionData.AutoGenerateColumns = false;
            definitionData.DataSource = bindingDefinition;

            aliasesData.AutoGenerateColumns = false;
            aliasesData.DataSource = bindingAlias;

            // TODO: Add fill of attribute data.
            // Can this add the AttributeValue?
            // What to display of the attribute?
            // Navigation directly to the attribute?
            AttributeNameList.Load(attributeColumn);



            attributeData.AutoGenerateColumns = false;
            attributeData.DataSource = bindingAttribute;

            attributeTitleData.DataBindings.Add(new Binding(nameof(attributeTitleData.Text), bindingAttributeDetail, nameof(IAttributeValue.AttributeTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            attributeOrderData.DataBindings.Add(new Binding(nameof(attributeOrderData.Text), bindingAttribute, nameof(IEntityAttributeValue.OrdinalPosition), false, DataSourceUpdateMode.OnPropertyChanged));
            attributeMemberData.DataBindings.Add(new Binding(nameof(attributeMemberData.Text), bindingAttributeDetail, nameof(IAttributeValue.MemberName), false, DataSourceUpdateMode.OnPropertyChanged));
            subjectArea.BindTo(bindingSubjectArea);

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingEntity.Current is not IEntityValue);
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            if (bindingEntity.Current is IEntityValue current)
            { DoWork(BusinessData.DomainModel.Entities.Delete(current)); }
        }

        private void BindingProperty_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingEntity.Current is IEntityValue current)
            {
                EntityPropertyValue newItem = new EntityPropertyValue(current);
                newItem.PropertyId = domainProperty.PropertyId;
                newItem.PropertyValue = domainProperty.PropertyValue;
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingEntity.Current is EntityValue current)
            {
                EntityAliasValue newItem = new EntityAliasValue(current);
                newItem.AliasName = namedScopeData.ScopePath.MemberFullPath;
                newItem.AliasScope = namedScopeData.Scope;
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingAlias.Current is IAliasValue current)
            {
                NamedScopePath path = new NamedScopePath(NamedScopePath.Parse(current.AliasName).ToArray());

                namedScopeData.ScopePath = path;
                namedScopeData.Scope = current.AliasScope;
            }
        }

        private void NamedScopeData_OnApply(object sender, EventArgs e)
        {
            if (bindingAlias.DataSource is IList<IAliasValue> aliases
                && aliases.FirstOrDefault(
                    w => w.AliasScope == namedScopeData.Scope
                    && new NamedScopePath(NamedScopePath.Parse(w.AliasName).ToArray()) == namedScopeData.ScopePath)
                is IAliasValue value)
            { bindingAlias.Position = aliases.IndexOf(value); }
            else { bindingAlias.AddNew(); }
        }

        private void BindingProperty_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingProperty.Current is EntityPropertyValue current)
            {
                domainProperty.PropertyId = current.PropertyId ?? Guid.Empty;
                domainProperty.PropertyValue = current.PropertyValue ?? String.Empty;
            }
        }

        private void DomainProperty_OnApply(object sender, EventArgs e)
        {
            if (bindingProperty.DataSource is IList<EntityPropertyValue> properties
                && properties.FirstOrDefault(
                    w => w.PropertyId == domainProperty.PropertyId)
                is EntityPropertyValue value)
            {
                value.PropertyValue = domainProperty.PropertyValue;
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

        private void BindingDefinition_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingEntity.Current is EntityValue current)
            {
                EntityDefinitionValue newItem = new EntityDefinitionValue(current);
                newItem.DefinitionId = domainDefinition.DefinitionId;
                newItem.DefinitionSummary = domainDefinition.DefinitionSummary;
                newItem.DefinitionText = domainDefinition.DefinitionText;
                e.NewObject = newItem;
            }
        }

        private void BindingDefinition_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingDefinition.Current is EntityDefinitionValue current)
            {
                domainDefinition.DefinitionId = current.DefinitionId ?? Guid.Empty;
                domainDefinition.DefinitionText = current.DefinitionText;
                domainDefinition.DefinitionSummary = current.DefinitionSummary ?? String.Empty;
            }
        }

        private void DomainDefinition_OnApply(object sender, EventArgs e)
        {
            if (bindingDefinition.DataSource is IList<EntityDefinitionValue> definition
                && definition.FirstOrDefault(
                    w => domainDefinition.Definition is IDefinitionIndex
                    && domainDefinition.Definition.Equals(w))
                is EntityDefinitionValue value)
            {
                value.DefinitionSummary = domainDefinition.DefinitionSummary;
                value.DefinitionText = domainDefinition.DefinitionText;
                bindingDefinition.Position = definition.IndexOf(value);
            }
            else { bindingDefinition.AddNew(); }
        }

        private void MemberNameData_Validating(object sender, CancelEventArgs e)
        {
            NamedScopePath path = new NamedScopePath(NamedScopePath.Parse(memberNameData.Text).ToArray());
            memberNameData.Text = path.MemberFullPath;
        }

        private void BindingAttribute_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingEntity.Current is EntityValue current)
            {
                e.NewObject = new EntityAttributeValue(current)
                {
                    OrdinalPosition = bindingAttribute.Count + 1
                };
            }
        }

        private void BindingAttribute_CurrentChanged(object sender, EventArgs e)
        { SetAttributeDetail(); }

        private void SetAttributeDetail()
        {
            if (bindingAttribute.Current is EntityAttributeValue current)
            {
                AttributeIndex key = new AttributeIndex(current);

                if (bindingAttributeDetail.DataSource is IList<AttributeValue> attributes
                    && attributes.FirstOrDefault(w => key.Equals(w)) is AttributeValue attribute)
                { bindingAttributeDetail.Position = attributes.IndexOf(attribute); }
            }

        }

        private void BindingAttributeDetail_AddingNew(object sender, AddingNewEventArgs e)
        {
            AttributeValue newValue = new AttributeValue();

            if (bindingAttribute.Current is EntityAttributeValue attribute)
            { attribute.AttributeId = newValue.AttributeId; }


            e.NewObject = newValue;
        }


        private void AttributeTitleData_Validated(object sender, EventArgs e)
        { AttributeNameList.Load(attributeColumn); }

        private void AttributeSelect_Click(object sender, EventArgs e)
        {
            if (bindingAttribute.DataSource is IList<EntityAttributeValue> attributes)
            {
                List<AttributeIndex> keys = attributes.Select(s => new AttributeIndex(s)).ToList();

                using (var dialog = new SelectionDialog<AttributeValue, AttributeIndex>
                {
                    Icon = ImageEnumeration.Cast(ScopeType.ModelAttribute).WindowIcon,
                    Text = ImageEnumeration.Cast(ScopeType.ModelAttribute).Name,
                    DataSource = BusinessData.DomainModel.Attributes,
                    Selected = keys,
                    GetDescription = (value) => { return value.AttributeDescription ?? String.Empty; },
                    AsIndex = (value) => { return new AttributeIndex(value); }
                })
                {
                    if (dialog.ShowDialog() is DialogResult.OK)
                    {
                        foreach (AttributeIndex item in attributes.Select(s => new AttributeIndex(s)).Except(dialog.Selected).ToList())
                        { // Remove
                            if (attributes.FirstOrDefault(w => item.Equals(w)) is EntityAttributeValue removeItem)
                            { bindingAttribute.Remove(removeItem); }
                        }

                        foreach (AttributeIndex item in dialog.Selected.Except(attributes.Select(s => new AttributeIndex(s))).ToList())
                        { // Add
                            bindingAttribute.AddNew();
                            if (bindingAttribute.Current is EntityAttributeValue newItem)
                            { newItem.AttributeId = item.AttributeId; }

                            SetAttributeDetail();
                        }
                    }
                }
            }

        }
    }
}
