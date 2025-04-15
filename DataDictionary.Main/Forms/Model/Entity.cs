using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Controls;
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
using DataDictionary.Main.Forms.Model.ComboBoxList;

namespace DataDictionary.Main.Forms.Model
{
    partial class Entity : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingEntity.Current is IEntityValue current && ReferenceEquals(current, item); }

        FormBinding formBinding;
        FixedBinding fixedBinding;
        Boolean needsData = false;

        protected Entity() : base()
        {
            InitializeComponent();
            formBinding = new FormBinding()
            {
                BindingAlias = bindingAlias,
                BindingEntity = bindingEntity,
                BindingSubjectArea = bindingSubjectArea,
                BindingProperty = bindingProperty,
                BindingDefinition = bindingDefinition,
                BindingAttribute = bindingAttribute,
                BindingAttributeDetail = bindingAttributeDetail,
                DoWork = base.DoWork
            };
            formBinding.Init();

            fixedBinding = new FixedBinding();

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

        public Entity(IEntityIndex? entity) : this()
        {
            if (entity is null)
            { entity = formBinding.NewValue(); }
            else { formBinding.SetPosition(entity); }
        }

        public Entity(IEntityIndex entity, ITemporalIndex temporal) : this(entity)
        { formBinding.SetPosition(entity, temporal); needsData = true; }

        private void Form_Load(object sender, EventArgs e)
        {
            PropertyNameList.Load(propertyIdColumn);
            DefinitionNameList.Load(definitionColumn);
            ScopeNameList.Load(aliaseScopeColumn);

            if (needsData)
            { formBinding.Load(onCompleting); }
            else { DoBinding(); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is null)
                {
                    DoBinding();
                    SendMessage(new RefreshNavigation());
                }
            }

            void DoBinding()
            {
                titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingEntity, nameof(IEntityValue.EntityTitle)));
                descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingEntity, nameof(IEntityValue.EntityDescription)));

                memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), bindingEntity, nameof(IEntityValue.EntityName), false, DataSourceUpdateMode.OnPropertyChanged));

                PropertyNameList.Load(propertyIdColumn, BusinessData.Model.Properties);
                propertiesData.AutoGenerateColumns = false;
                propertiesData.DataSource = bindingProperty;
                propertyControl.BindTo(bindingProperty, BusinessData.Model.Properties);

                DefinitionNameList.Load(definitionColumn, BusinessData.Model.Definitions);
                definitionData.AutoGenerateColumns = false;
                definitionData.DataSource = bindingDefinition;
                definitionControl.BindTo(bindingDefinition, BusinessData.Model.Definitions);

                // Attribute Handling
                attributeData.AutoGenerateColumns = false;
                attributeData.DataSource = bindingAttribute;

                attributePathData.DataBindings.Add(new Binding(nameof(attributePathData.Text), bindingAttribute, nameof(IEntityAttributeValue.AttributePath)));
                attributeOrderData.DataBindings.Add(new Binding(nameof(attributeOrderData.Text), bindingAttribute, nameof(IEntityAttributeValue.OrdinalPosition), false, DataSourceUpdateMode.OnPropertyChanged));
                attributeAliasData.DataBindings.Add(new Binding(nameof(attributeAliasData.Text), bindingAttribute, nameof(IEntityAttributeValue.AttributeTitle)));
                attributeNullable.DataBindings.Add(new Binding(nameof(attributeNullable.Checked), bindingAttribute, nameof(IEntityAttributeValue.IsNullable), true, DataSourceUpdateMode.OnValidation, false));
                attributePrimaryKey.DataBindings.Add(new Binding(nameof(attributePrimaryKey.Checked), bindingAttribute, nameof(IEntityAttributeValue.IsPrimaryKey), true, DataSourceUpdateMode.OnValidation, false));
                subjectArea.BindTo(bindingSubjectArea, BusinessData.Model.SubjectAreas);

                // Alias Handling
                ScopeNameList.Load(aliaseScopeColumn);
                ScopeNameList.Load(aliasScopeData);

                aliasesData.AutoGenerateColumns = false;
                aliasesData.DataSource = bindingAlias;

                aliasScopeData.DataBindings.Add(new Binding(nameof(aliasScopeData.SelectedValue), bindingAlias, nameof(IEntityAliasValue.AliasScope), false, DataSourceUpdateMode.OnPropertyChanged) { DataSourceNullValue = ScopeNameList.NullValue });
                aliasNameData.DataBindings.Add(new Binding(nameof(aliasNameData.Text), bindingAlias, nameof(EntityAliasValue.AliasPath), false, DataSourceUpdateMode.OnPropertyChanged));

                attributeTitleData.DataBindings.Add(new Binding(nameof(attributeTitleData.Text), bindingAttribute, nameof(IEntityAttributeValue.AttributeTitle)));
                attributeDescriptionData.DataBindings.Add(new Binding(nameof(attributeDescriptionData.Text), bindingAttribute, nameof(IEntityAttributeValue.AttributeDescription)));
                attributeInModelData.DataBindings.Add(new Binding(nameof(attributeInModelData.Checked), bindingAttribute, nameof(IEntityAttributeValue.InModel), false, DataSourceUpdateMode.OnPropertyChanged));

                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.GetAuthorization);
            }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            formBinding.RemoveValue();
            IsLocked(formBinding.GetLocked());
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            formBinding.Load(onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            formBinding.RemoveValue();
            formBinding.Save(onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            formBinding.Save(onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);

            Activate(() => new ApplicationWide.HistoryView(formBinding.GetTemporal())
            {
                OpenForm = (temporal) =>
                {
                    if (temporal.TryGetValue(out EntityValue? entity))
                    { return new Entity(entity, new TemporalIndex(temporal)); }
                    else { throw new InvalidOperationException("Could not convert TemporalValue back to HelpSubjectValue"); }
                }
            });
        }

        private void BindingProperty_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingEntity.Current is EntityValue current)
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
        { }

        private void BindingSubjectArea_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (addingSubject is ISubjectAreaValue subject && bindingEntity.Current is EntityValue entity)
            {
                EntitySubjectAreaValue newItem = new EntitySubjectAreaValue(entity, subject);
                e.NewObject = newItem;
            }
            addingSubject = null;
        }

        ISubjectAreaValue? addingSubject = null;
        private void SubjectArea_OnSubjectAdd(object sender, ISubjectAreaValue e)
        {
            addingSubject = e;
            bindingSubjectArea.AddNew();
        }

        private void SubjectArea_OnSubjectRemove(object sender, ISubjectAreaValue e)
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
                e.NewObject = newItem;
            }
        }

        private void BindingDefinition_CurrentChanged(object sender, EventArgs e)
        { }


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
        {
            //attributeNavigation.BindingSource = null;
            //bindingAttributeDetail.DataSource = null;
            attributeInModelData.Checked = false;

            if (bindingAttribute.Current is IEntityAttributeValue alias)
            {


                //AliasIndexName aliasIndex = new AliasIndexName(alias);

                ////TODO: Include Attribute Path, not just the alias of the Attribute.

                //var attributes = BusinessData.Model.Attributes.
                //    FindAttribute(aliasIndex).
                //    Select(s => new AttributeIndex(s)).
                //    Join(BusinessData.Model.Attributes.Values,
                //        key => key,
                //        attribute => new AttributeIndex(attribute),
                //        (key, attribute) => attribute).
                //    ToList();

                //bindingAttributeDetail.DataSource = attributes;
                //if (attributes.Count > 0)
                //{ attributeInModelData.Checked = true; }

                ////attributeNavigation.BindingSource = bindingAttributeDetail;
            }
        }

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
                                newItem.AttributeKnownAs = addItem.Title;
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

        private void bindingAttributeDetail_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void attributeNavigatorData_Load(object sender, EventArgs e)
        {

        }
    }
}
