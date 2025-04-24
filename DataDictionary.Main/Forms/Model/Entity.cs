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
            SetCommand(ScopeType.ModelEntity,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase,
                CommandImageType.HistoryDatabase);

            attributeSelectCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelAttribute, CommandImageType.Select);
            attributeNewCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelAttribute, CommandImageType.Add);
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
                attributeKnownAsData.DataBindings.Add(new Binding(nameof(attributeKnownAsData.Text), bindingAttribute, nameof(IEntityAttributeValue.AttributeKnownAs)));
                attributeNullable.DataBindings.Add(new Binding(nameof(attributeNullable.Checked), bindingAttribute, nameof(IEntityAttributeValue.IsNullable), true, DataSourceUpdateMode.OnValidation, false));
                attributePrimaryKey.DataBindings.Add(new Binding(nameof(attributePrimaryKey.Checked), bindingAttribute, nameof(IEntityAttributeValue.IsPrimaryKey), true, DataSourceUpdateMode.OnValidation, false));
                subjectArea.BindTo(bindingSubjectArea, BusinessData.Model.SubjectAreas);
                attributeLayout.Enabled = false;

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

        private void BindingAlias_CurrentChanged(object sender, EventArgs e)
        {
            if (formBinding.TryGetAlias(out EntityAliasValue? current))
            {
                Boolean inModel = BusinessData.NamedScope.PathKeys(current.AliasPath).Count > 0;
                isAliasInModelData.Checked = inModel;
                aliasNameData.ReadOnly = inModel;
                aliasScopeData.ReadOnly = inModel;
            }
        }

        private void BindingProperty_CurrentChanged(object sender, EventArgs e)
        { }

        ISubjectAreaValue? addingSubject = null;
        private void SubjectArea_OnSubjectAdd(object sender, ISubjectAreaValue e)
        { formBinding.AddSubjectArea(e); }

        private void SubjectArea_OnSubjectRemove(object sender, ISubjectAreaValue e)
        { formBinding.RemoveSubjectArea(e); }

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
            if (formBinding.TryGetAttribute(out EntityAttributeValue? value))
            { attributeLayout.Enabled = true; }
            else { attributeLayout.Enabled = false; }
        }

        private void AttributeSelect_Click(object sender, EventArgs e)
        {
            if (bindingAttribute.DataSource is IList<EntityAttributeValue> attributes)
            {
                using (SelectionDialog dialog = new SelectionDialog(this))
                {
                    dialog.FilterScopes.Add(ScopeType.ModelAttribute);
                    IEnumerable<PathIndex> selected = attributes.Select(s => s.AttributePath);

                    dialog.BuildData(selected, GetDescription);

                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        formBinding.AddAttributes(dialog.SelectedByValue<AttributeValue>());
                        bindingAttribute.ResetCurrentItem();
                    }
                }
            }

            String GetDescription(INamedScopeSourceValue value)
            {   // Needed a physical method rather then a Lambda expression.
                // Properties don't get passed as expected.
                // I needed the property passed by Reference and that did not work.
                if (value is AttributeValue attribute)
                { return attribute.AttributeDescription ?? String.Empty; }
                else { return String.Empty; }
            }
        }

        private void AttributeNewCommand_Click(object sender, EventArgs e)
        { formBinding.AddAttribute(); }


        private void AliasAddCommand_Click(object sender, EventArgs e)
        { formBinding.AddAlias(); }

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
                    { formBinding.AddAlias(dialog.SelectedByNamedScope()); }
                }
            }
        }



        private void AliasNameData_Validating(object sender, CancelEventArgs e)
        {
            if (formBinding.TryGetAlias(out EntityAliasValue? value))
            { value.AliasPath = new PathIndex(PathIndex.Parse(aliasNameData.Text).ToArray()); }
        }

        private void AttributePathData_Validating(object sender, CancelEventArgs e)
        {
            if (formBinding.TryGetAttribute(out EntityAttributeValue? value))
            { value.AttributePath = new PathIndex(PathIndex.Parse(attributePathData.Text).ToArray()); }
        }


    }
}
