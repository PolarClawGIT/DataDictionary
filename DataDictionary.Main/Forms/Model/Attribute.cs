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

        AttributeIndex formIndex;
        AttributeView formData;

        protected Attribute() : base()
        {
            InitializeComponent();

            SetRowState(
                bindingAttribute,
                bindingProperty,
                bindingDefinition,
                bindingAlias,
                bindingSubjectArea);
            SetTitle(bindingAttribute);
            SetCommand(ScopeType.ModelAttribute,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase);

            aliasAddCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelEntityAlias, CommandImageType.Add);
            aliasSelectCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelEntityAlias, CommandImageType.Select);

            formIndex = new AttributeIndex();
            formData = new AttributeView(BusinessData.Model); //Empty Attribute
        }

        public Attribute(IAttributeIndex? attribute) : this()
        {
            if (attribute is null)
            {
                AttributeValue attributeItem = new AttributeValue();
                formIndex = new AttributeIndex(attributeItem);
                formData.Attributes.Add(attributeItem);
            }
            else
            { formIndex = new AttributeIndex(attribute); }
        }


        private void Form_Load(object sender, EventArgs e)
        {
            PropertyNameList.Load(propertyIdColumn);
            DefinitionNameList.Load(definitionColumn);
            ScopeNameList.Load(aliaseScopeColumn);

            IDatabaseWork factory = BusinessData.GetDbFactory();
            List<WorkItem> work = new List<WorkItem>();
            IsLocked(true);

            work.Add(factory.OpenConnection());
            work.AddRange(formData.Load(formIndex));
            DoWork(work, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                SendMessage(new RefreshNavigation());

                bindingAttribute.DataSource = formData.Attributes;
                bindingAttribute.Position = 0;

                if (bindingAttribute.Current is IAttributeValue current)
                {
                    bindingProperty.DataSource = formData.Properties;
                    bindingDefinition.DataSource = formData.Definitions;
                    bindingAlias.DataSource = formData.Aliases;
                    bindingSubjectArea.DataSource = formData.SubjectArea;
                }

                titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingAttribute, nameof(IAttributeValue.AttributeTitle)));
                descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingAttribute, nameof(IAttributeValue.AttributeDescription), false, DataSourceUpdateMode.OnPropertyChanged));

                memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), bindingAttribute, nameof(IAttributeValue.AttributeName), false, DataSourceUpdateMode.OnPropertyChanged));

                DataTypeList.Load(dataTypeData, BusinessData.Model.ModelAttribute.Attributes.Select(s => s.DataType).OfType<String>().Distinct());
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

                PropertyNameList.Load(propertyIdColumn, formData.ModelProperty);
                propertiesData.AutoGenerateColumns = false;
                propertiesData.DataSource = bindingProperty;
                propertyControl.LoadControl(bindingProperty, formData.ModelProperty);

                DefinitionNameList.Load(definitionColumn, formData.ModelDefinitions);
                definitionData.AutoGenerateColumns = false;
                definitionData.DataSource = bindingDefinition;
                definitionControl.LoadControl(bindingDefinition, formData.ModelDefinitions);

                subjectArea.BindTo(bindingSubjectArea);

                // Alias Handling
                ScopeNameList.Load(aliaseScopeColumn);
                ScopeNameList.Load(aliasScopeData);

                aliasesData.AutoGenerateColumns = false;
                aliasesData.DataSource = bindingAlias;

                aliasScopeData.DataBindings.Add(new Binding(nameof(aliasScopeData.SelectedValue), bindingAlias, nameof(IEntityAliasValue.AliasScope), false, DataSourceUpdateMode.OnPropertyChanged) { DataSourceNullValue = ScopeNameList.NullValue });
                aliasNameData.DataBindings.Add(new Binding(nameof(aliasNameData.Text), bindingAlias, nameof(EntityAliasValue.AliasPath), false, DataSourceUpdateMode.OnPropertyChanged));

                IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingAttribute.Current is not IAttributeValue);
            }
        }


        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            bindingAttribute.RaiseListChangedEvents = false;
            bindingProperty.RaiseListChangedEvents = false;
            bindingDefinition.RaiseListChangedEvents = false;
            bindingAlias.RaiseListChangedEvents = false;
            bindingSubjectArea.RaiseListChangedEvents = false;

            formData.Remove();
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            if (bindingAttribute.Current is AttributeValue current)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();
                IsLocked(true);

                work.Add(factory.OpenConnection());
                work.AddRange(formData.Load(factory, current));
                DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                bindingAttribute.ResetBindings(false);
                bindingProperty.ResetBindings(false);
                bindingDefinition.ResetBindings(false);
                bindingAlias.ResetBindings(false);
                bindingSubjectArea.ResetBindings(false);

                IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingAttribute.Current is not IAttributeValue);
            }
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
            if (addingSubject is SubjectAreaValue subject && bindingAttribute.Current is AttributeValue attribute)
            {
                AttributeSubjectAreaValue newItem = new AttributeSubjectAreaValue(attribute, subject);
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
