using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using System.ComponentModel;
using System.Data;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using DataDictionary.Main.Dialogs;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Forms.Model.ComboBoxList;
using DataDictionary.BusinessLayer.DbWorkItem;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Model
{
    partial class Attribute : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingAttribute.Current is IAttributeValue current && ReferenceEquals(current, item); }

        FormBinding formBinding;
        FixedBinding fixedBinding;
        Boolean needsData = false;

        protected Attribute() : base()
        {
            InitializeComponent();
            formBinding = new FormBinding()
            {
                BindingAlias = bindingAlias,
                BindingAttribute = bindingAttribute,
                BindingSubjectArea = bindingSubjectArea,
                BindingProperty = bindingProperty,
                BindingDefinition = bindingDefinition,
                DoWork = base.DoWork
            };
            formBinding.Init();

            fixedBinding = new FixedBinding();

            SetRowState(
                bindingAttribute,
                bindingProperty,
                bindingDefinition,
                bindingAlias,
                bindingSubjectArea);
            SetTitle(bindingAttribute);
            SetCommand(ScopeType.ModelAttribute,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase,
                CommandImageType.HistoryDatabase);

            aliasAddCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelEntityAlias, CommandImageType.Add);
            aliasSelectCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelEntityAlias, CommandImageType.Select);
        }

        public Attribute(IAttributeIndex? attribute) : this()
        {
            if (attribute is null)
            { attribute = formBinding.NewValue(); }
            else { formBinding.SetPosition(attribute); }
        }

        public Attribute(IAttributeIndex attribute, ITemporalIndex temporal) : this(attribute)
        { formBinding.SetPosition(attribute, temporal); needsData = true; }

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
                titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingAttribute, nameof(IAttributeValue.AttributeTitle)));
                descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingAttribute, nameof(IAttributeValue.AttributeDescription), false, DataSourceUpdateMode.OnPropertyChanged));

                memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), bindingAttribute, nameof(IAttributeValue.AttributeName), false, DataSourceUpdateMode.OnPropertyChanged));

                DataTypeList.Load(dataTypeData, BusinessData.Model.Attributes.Values.Select(s => s.DataType).OfType<String>().Distinct());
                dataTypeData.DataBindings.Add(new Binding(nameof(dataTypeData.Text), bindingAttribute, nameof(IAttributeValue.DataType), false, DataSourceUpdateMode.OnPropertyChanged));
                dataLengthData.DataBindings.Add(new Binding(nameof(dataLengthData.Text), bindingAttribute, nameof(IAttributeValue.DataLength), false, DataSourceUpdateMode.OnPropertyChanged));
                dataPrecisionData.DataBindings.Add(new Binding(nameof(dataPrecisionData.Text), bindingAttribute, nameof(IAttributeValue.DataPrecision), false, DataSourceUpdateMode.OnPropertyChanged));

                isSingleValueData.DataBindings.Add(new Binding(nameof(isSingleValueData.Checked), bindingAttribute, nameof(IAttributeValue.IsSingleValue), false, DataSourceUpdateMode.OnPropertyChanged));
                isMultiValuedData.DataBindings.Add(new Binding(nameof(isMultiValuedData.Checked), bindingAttribute, nameof(IAttributeValue.IsMultiValue), false, DataSourceUpdateMode.OnPropertyChanged));
                isSimpleTypeData.DataBindings.Add(new Binding(nameof(isSimpleTypeData.Checked), bindingAttribute, nameof(IAttributeValue.IsSimpleType), false, DataSourceUpdateMode.OnPropertyChanged));
                isCompositeTypeData.DataBindings.Add(new Binding(nameof(isCompositeTypeData.Checked), bindingAttribute, nameof(IAttributeValue.IsCompositeType), false, DataSourceUpdateMode.OnPropertyChanged));
                isIntegralData.DataBindings.Add(new Binding(nameof(isIntegralData.Checked), bindingAttribute, nameof(IAttributeValue.IsIntegral), false, DataSourceUpdateMode.OnPropertyChanged));
                isDerivedData.DataBindings.Add(new Binding(nameof(isDerivedData.Checked), bindingAttribute, nameof(IAttributeValue.IsDerived), false, DataSourceUpdateMode.OnPropertyChanged));
                isValuedData.DataBindings.Add(new Binding(nameof(isValuedData.Checked), bindingAttribute, nameof(IAttributeValue.IsValued), false, DataSourceUpdateMode.OnPropertyChanged));
                isNullableData.DataBindings.Add(new Binding(nameof(isNullableData.Checked), bindingAttribute, nameof(IAttributeValue.IsNullable), false, DataSourceUpdateMode.OnPropertyChanged));
                isNonKeyData.DataBindings.Add(new Binding(nameof(isNonKeyData.Checked), bindingAttribute, nameof(IAttributeValue.IsNonKey), false, DataSourceUpdateMode.OnPropertyChanged));
                isKeyData.DataBindings.Add(new Binding(nameof(isKeyData.Checked), bindingAttribute, nameof(IAttributeValue.IsKey), false, DataSourceUpdateMode.OnPropertyChanged));

                PropertyNameList.Load(propertyIdColumn, fixedBinding.Properties);
                propertiesData.AutoGenerateColumns = false;
                propertiesData.DataSource = bindingProperty;
                propertyControl.BindTo(bindingProperty, fixedBinding.Properties);

                DefinitionNameList.Load(definitionColumn, fixedBinding.Definitions);
                definitionData.AutoGenerateColumns = false;
                definitionData.DataSource = bindingDefinition;
                definitionControl.BindTo(bindingDefinition, fixedBinding.Definitions);

                subjectArea.BindTo(bindingSubjectArea, fixedBinding.SubjectAreas);

                // Alias Handling
                ScopeNameList.Load(aliaseScopeColumn);
                ScopeNameList.Load(aliasScopeData);

                aliasesData.AutoGenerateColumns = false;
                aliasesData.DataSource = bindingAlias;

                aliasScopeData.DataBindings.Add(new Binding(nameof(aliasScopeData.SelectedValue), bindingAlias, nameof(IEntityAliasValue.AliasScope), false, DataSourceUpdateMode.OnPropertyChanged) { DataSourceNullValue = ScopeNameList.NullValue });
                aliasNameData.DataBindings.Add(new Binding(nameof(aliasNameData.Text), bindingAlias, nameof(EntityAliasValue.AliasPath), false, DataSourceUpdateMode.OnPropertyChanged));

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
                    if (temporal.TryGetValue(out AttributeValue? attribute))
                    { return new Attribute(attribute, new TemporalIndex(temporal)); }
                    else { throw new InvalidOperationException("Could not convert TemporalValue back to HelpSubjectValue"); }
                }
            });
        }

        private void BindingProperty_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is AttributeValue current)
            {
                AttributePropertyValue newItem = new AttributePropertyValue(current);
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is AttributeValue current)
            {
                AttributeAliasValue newItem = new AttributeAliasValue(current);
                e.NewObject = newItem;
            }
        }

        private void BindingAlias_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingAlias.Current is AttributeAliasValue current)
            {
                Boolean inModel = BusinessData.NamedScope.PathKeys(current.AliasPath).Count > 0;
                isAliasInModelData.Checked = inModel;
                aliasNameData.ReadOnly = inModel;
                aliasScopeData.ReadOnly = inModel;
            }
        }

        private void BindingProperty_CurrentChanged(object sender, EventArgs e)
        { }

        private void BindingDefinition_CurrentChanged(object sender, EventArgs e)
        { }

        private void BindingDefinition_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingAttribute.Current is AttributeValue current)
            {
                AttributeDefinitionValue newItem = new AttributeDefinitionValue(current);
                e.NewObject = newItem;
            }
        }

        private void BindingSubjectArea_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (addingSubject is ISubjectAreaValue subject && bindingAttribute.Current is AttributeValue attribute)
            {
                AttributeSubjectAreaValue newItem = new AttributeSubjectAreaValue(attribute, subject);
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
                && data.FirstOrDefault(w => key.Equals(w)) is AttributeSubjectAreaValue target)
            { bindingSubjectArea.Remove(target); }
        }

        private void MemberNameData_Validating(object sender, CancelEventArgs e)
        {
            PathIndex path = new PathIndex(PathIndex.Parse(memberNameData.Text).ToArray());
            memberNameData.Text = path.MemberFullPath;
        }

        private void AliasSelectCommand_Click(object sender, EventArgs e)
        {
            if (bindingAlias.DataSource is IList<AttributeAliasValue> alias)
            {
                using (var dialog = new SelectionDialog(this))
                {
                    dialog.FilterScopes.Add(ScopeType.ModelAttribute);
                    dialog.FilterScopes.Add(ScopeType.DatabaseTableColumn);
                    dialog.FilterScopes.Add(ScopeType.DatabaseViewColumn);
                    dialog.FilterScopes.Add(ScopeType.LibraryTypeField);
                    dialog.FilterScopes.Add(ScopeType.LibraryTypeProperty);

                    dialog.BuildData(alias.SelectMany(s => BusinessData.NamedScope.PathKeys(s.AliasPath)));

                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        IEnumerable<INamedScopeValue> selected = dialog.SelectedByNamedScope();
                        IEnumerable<AttributeAliasValue> inModel = alias.Where(w => BusinessData.NamedScope.PathKeys(w.AliasPath).Count() > 0);

                        foreach (INamedScopeValue addItem in selected.Where(w => !alias.Select(s => s.AliasPath).Contains(w.Path)).ToList())
                        { // Add
                            if (bindingAlias.AddNew() is AttributeAliasValue newValue)
                            {
                                newValue.AliasPath = addItem.Path;
                                newValue.AliasScope = addItem.Scope;
                            }
                        }
                    }
                }
            }
        }

        private void AliasAddCommand_Click(object sender, EventArgs e)
        {
            if (bindingAlias.AddNew() is AttributeAliasValue newValue)
            { }
        }

        private void AliasNameData_Validating(object sender, CancelEventArgs e)
        {
            PathIndex path = new PathIndex(PathIndex.Parse(aliasNameData.Text).ToArray());
            aliasNameData.Text = path.MemberFullPath;
        }

    }
}
