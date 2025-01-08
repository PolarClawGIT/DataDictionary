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
using DataDictionary.BusinessLayer;
using DataDictionary.Main.Messages;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.BusinessLayer.AppModel;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainEntity : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingEntity.Current is IEntityValue current && ReferenceEquals(current, item); }

        Boolean isNew = false; // Flags the item as new to handled deferred Refresh.

        protected DomainEntity() : base()
        {
            InitializeComponent();

            SetRowState(
                bindingEntity,
                bindingProperty,
                bindingDefinition,
                bindingAlias,
                bindingSubjectArea,
                bindingAttribute);
            SetTitle(bindingEntity);
            SetCommand(ScopeType.ModelEntity, CommandImageType.Delete);

            attributeSelectCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelAttribute, CommandImageType.Select);
            aliasAddCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelEntityAlias, CommandImageType.Add);
            aliasSelectCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelEntityAlias, CommandImageType.Select);
        }

        public DomainEntity(IEntityValue? entityItem) : this()
        {
            if (entityItem is null)
            {
                entityItem = new EntityValue();
                BusinessData.Model.Entities.Add(entityItem);
                SendMessage(new RefreshNavigation());
            }

            EntityIndex key = new EntityIndex(entityItem);

            bindingEntity.DataSource = new BindingView<EntityValue>(BusinessData.Model.Entities, w => key.Equals(w));
            bindingEntity.Position = 0;

            if (bindingEntity.Current is IEntityValue current)
            {
                bindingProperty.DataSource = new BindingView<EntityPropertyValue>(BusinessData.Model.Entities.Properties, w => key.Equals(w));
                bindingDefinition.DataSource = new BindingView<EntityDefinitionValue>(BusinessData.Model.Entities.Definitions, w => key.Equals(w));
                bindingAlias.DataSource = new BindingView<EntityAliasValue>(BusinessData.Model.Entities.Aliases, w => key.Equals(w));
                bindingSubjectArea.DataSource = new BindingView<EntitySubjectAreaValue>(BusinessData.Model.Entities.SubjectArea, w => key.Equals(w));
                bindingAttribute.DataSource = new BindingView<EntityAttributeValue>(BusinessData.Model.Entities.Attributes, w => key.Equals(w));
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            PropertyNameList.Load(propertyIdColumn);
            DefinitionNameList.Load(definitionColumn);
            ScopeNameList.Load(aliaseScopeColumn);

            if (isNew) { SendMessage(new RefreshNavigation()); }

            this.DataBindings.Add(new Binding(nameof(this.Text), bindingEntity, nameof(IEntityValue.EntityTitle)));

            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingEntity, nameof(IEntityValue.EntityTitle)));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingEntity, nameof(IEntityValue.EntityDescription)));

            memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), bindingEntity, nameof(IEntityValue.EntityName), false, DataSourceUpdateMode.OnPropertyChanged));

            propertiesData.AutoGenerateColumns = false;
            propertiesData.DataSource = bindingProperty;

            definitionData.AutoGenerateColumns = false;
            definitionData.DataSource = bindingDefinition;

            // Attribute Handling
            attributeData.AutoGenerateColumns = false;
            attributeData.DataSource = bindingAttribute;

            attributeNameData.DataBindings.Add(new Binding(nameof(attributeNameData.Text), bindingAttribute, nameof(IEntityAttributeValue.AttributeName)));
            attributeOrderData.DataBindings.Add(new Binding(nameof(attributeOrderData.Text), bindingAttribute, nameof(IEntityAttributeValue.OrdinalPosition), false, DataSourceUpdateMode.OnPropertyChanged));
            attributeAliasData.DataBindings.Add(new Binding(nameof(attributeAliasData.Text), bindingAttribute, nameof(IEntityAttributeValue.AttributeAlias)));
            attributeNullable.DataBindings.Add(new Binding(nameof(attributeNullable.Checked), bindingAttribute, nameof(IEntityAttributeValue.IsNullable), true, DataSourceUpdateMode.OnValidation, false));
            attributePrimaryKey.DataBindings.Add(new Binding(nameof(attributePrimaryKey.Checked), bindingAttribute, nameof(IEntityAttributeValue.IsPrimaryKey), true, DataSourceUpdateMode.OnValidation, false));
            subjectArea.BindTo(bindingSubjectArea);

            // Alias Handling
            ScopeNameList.Load(aliaseScopeColumn);
            ScopeNameList.Load(aliasScopeData);

            aliasesData.AutoGenerateColumns = false;
            aliasesData.DataSource = bindingAlias;

            aliasScopeData.DataBindings.Add(new Binding(nameof(aliasScopeData.SelectedValue), bindingAlias, nameof(IEntityAliasValue.AliasScope), false, DataSourceUpdateMode.OnPropertyChanged) { DataSourceNullValue = ScopeNameList.NullValue });
            aliasNameData.DataBindings.Add(new Binding(nameof(aliasNameData.Text), bindingAlias, nameof(EntityAliasValue.AliasNameSpace), false, DataSourceUpdateMode.OnPropertyChanged));

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingEntity.Current is not IEntityValue);
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            if (bindingEntity.Current is IEntityValue current)
            { DoWork(BusinessData.Model.Entities.Delete(current), Complete); }

            void Complete(RunWorkerCompletedEventArgs args)
            { SendMessage(new RefreshNavigation()); }
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
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingAlias.Current is EntityAliasValue current)
            {
                Boolean inModel = BusinessData.NamedScope.PathKeys(current.AliasPath).Count > 0;
                isAliasInModelData.Checked = inModel;
                aliasNameData.ReadOnly = inModel;
                aliasScopeData.ReadOnly = inModel;
            }
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
            PathIndex path = new PathIndex(PathIndex.Parse(memberNameData.Text).ToArray());
            memberNameData.Text = path.MemberFullPath;
        }

        private void BindingAttribute_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingEntity.Current is EntityValue current)
            {
                e.NewObject = new EntityAttributeValue(current)
                { OrdinalPosition = bindingAttribute.Count + 1 };
            }
        }

        private void BindingAttribute_CurrentChanged(object sender, EventArgs e)
        { }

        private void AttributeTitleData_Validated(object sender, EventArgs e)
        { }

        private void AttributeSelect_Click(object sender, EventArgs e)
        {
            if (bindingAttribute.DataSource is IList<EntityAttributeValue> attributes)
            {
                using (SelectionDialog dialog = new SelectionDialog(this))
                {
                    dialog.FilterScopes.Add(ScopeType.ModelAttribute);

                    dialog.BuildData();

                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        IEnumerable<IPathValue> selected = dialog.SelectedByValue<AttributeValue>().OfType<IPathValue>();

                        var toAdd = selected.Where(w => !attributes.Any(a => a.AttributePath.Equals(w.Path))).ToList();
                        var toRemove = attributes.Where(w => !selected.Any(a => w.AttributePath.Equals(a.Path))).ToList();

                        foreach (EntityAttributeValue removeItem in toRemove)
                        { bindingAttribute.Remove(removeItem); }

                        foreach (IPathValue addItem in toAdd)
                        {
                            if (bindingAttribute.AddNew() is EntityAttributeValue newItem)
                            {
                                newItem.AttributeAlias = addItem.Title;
                                newItem.AttributePath = addItem.Path;
                            }
                        }
                    }
                }
            }

            // TODO: Hook Description into dialog.BuildData(???);
            String GetDescription(INamedScopeSourceValue value)
            {   // Needed a physical method rather then a Lambda expression.
                // Properties don't get passed as expected.
                // I needed the property passed by Reference and that did not work.
                if (value is AttributeValue attribute)
                { return attribute.AttributeDescription ?? String.Empty; }
                else { return String.Empty; }
            }

        }

        private void AliasAddCommand_Click(object sender, EventArgs e)
        {
            if (bindingAlias.AddNew() is EntityAliasValue newValue)
            { }
        }

        private void AliasSelectCommand_Click(object sender, EventArgs e)
        {
            if (bindingAlias.DataSource is IList<EntityAliasValue> alias)
            {
                using (var dialog = new SelectionDialog(this))
                {
                    dialog.FilterScopes.Add(ScopeType.ModelEntity);
                    dialog.FilterScopes.Add(ScopeType.DatabaseTable);
                    dialog.FilterScopes.Add(ScopeType.DatabaseView);
                    dialog.FilterScopes.Add(ScopeType.LibraryType);

                    dialog.BuildData(alias.SelectMany(s => BusinessData.NamedScope.PathKeys(s.AliasPath)));


                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        IEnumerable<INamedScopeValue> selected = dialog.SelectedByNamedScope();
                        IEnumerable<EntityAliasValue> inModel = alias.Where(w => BusinessData.NamedScope.PathKeys(w.AliasPath).Count() > 0);

                        foreach (EntityAliasValue removeItem in alias.Where(w => !selected.Select(s => s.Path).Contains(w.AliasPath)).ToList())
                        {
                            if (inModel.Contains(removeItem)) // Only remove items that are in this model
                            { alias.Remove(removeItem); }
                        }

                        foreach (INamedScopeValue addItem in selected.Where(w => !alias.Select(s => s.AliasPath).Contains(w.Path)).ToList())
                        { // Add
                            if (bindingAlias.AddNew() is EntityAliasValue newValue)
                            {
                                newValue.AliasPath = addItem.Path;
                                newValue.AliasScope = addItem.Scope;
                            }
                        }
                    }
                }
            }
        }

        private void AliasNameData_Validating(object sender, CancelEventArgs e)
        {
            PathIndex path = new PathIndex(PathIndex.Parse(aliasNameData.Text).ToArray());
            aliasNameData.Text = path.MemberFullPath;
        }

        private void bindingAlias_DataError(object sender, BindingManagerDataErrorEventArgs e)
        {

        }


    }
}
