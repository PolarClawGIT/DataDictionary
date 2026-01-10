using DataDictionary.Main.Enumerations;
using System.ComponentModel;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.Main.Controls.ComboBoxList;

namespace DataDictionary.Main.Forms.Model
{
    partial class Attribute : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return item is IAttributeIndex attribute && attributeIndex.Equals(attribute); }

        FormBinding formBinding;
        AttributeIndex attributeIndex = new AttributeIndex();
        TemporalIndex? temporalIndex = null;

        protected Attribute() : base()
        {
            InitializeComponent();

            xElementRenderCommand.Image = ScopeType.ScriptingDocument.GetImage(CommandType.Default);

            formBinding = new FormBinding()
            {
                BindingAlias = bindingAlias,
                BindingAttribute = bindingAttribute,
                BindingSubjectArea = bindingSubjectArea,
                BindingProperty = bindingProperty,
                BindingDefinition = bindingDefinition,
                DoWork = base.DoWork
            };

            SetTitle(bindingAttribute);

            SetRowState(
                bindingAttribute,
                bindingProperty,
                bindingDefinition,
                bindingAlias,
                bindingSubjectArea);
            
            SetCommand(ScopeType.ModelAttribute,
                Enumerations.CommandType.Delete,
                Enumerations.CommandType.OpenDatabase,
                Enumerations.CommandType.SaveDatabase,
                Enumerations.CommandType.DeleteDatabase,
                Enumerations.CommandType.HistoryDatabase);
        }

        public Attribute(IAttributeIndex? attribute) : this()
        {
            if (attribute is IAttributeIndex)
            { attributeIndex = new AttributeIndex(attribute); }
            else { attributeIndex = new AttributeIndex(formBinding.NewValue()); }
        }

        public Attribute(IAttributeIndex attribute, ITemporalIndex temporal) : this(attribute)
        { temporalIndex = new TemporalIndex(); }

        private void Form_Load(object sender, EventArgs e)
        {
            if (temporalIndex is null)
            {
                formBinding.Load(attributeIndex);
                DoBinding();
            }
            else
            { formBinding.Load(attributeIndex, temporalIndex, onCompleting); }


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
                titleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingAttribute, nameof(IAttributeValue.AttributeTitle)));
                descriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingAttribute, nameof(IAttributeValue.AttributeDescription), false, DataSourceUpdateMode.OnPropertyChanged));

                memberNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingAttribute, nameof(IAttributeValue.AttributeName), false, DataSourceUpdateMode.OnPropertyChanged));

                DataTypeList.Load(dataTypeData, BusinessData.Model.Attribute.Attributes.Select(s => s.DataType).OfType<String>().Distinct());
                dataTypeData.DataBindings.Add(new Binding(nameof(ComboBox.Text), bindingAttribute, nameof(IAttributeValue.DataType), false, DataSourceUpdateMode.OnPropertyChanged));
                dataLengthData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingAttribute, nameof(IAttributeValue.DataLength), false, DataSourceUpdateMode.OnPropertyChanged));
                dataPrecisionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingAttribute, nameof(IAttributeValue.DataPrecision), false, DataSourceUpdateMode.OnPropertyChanged));

                isSingleValueData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IAttributeValue.IsSingleValue), false, DataSourceUpdateMode.OnPropertyChanged));
                isMultiValuedData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IAttributeValue.IsMultiValue), false, DataSourceUpdateMode.OnPropertyChanged));
                isSimpleTypeData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IAttributeValue.IsSimpleType), false, DataSourceUpdateMode.OnPropertyChanged));
                isCompositeTypeData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IAttributeValue.IsCompositeType), false, DataSourceUpdateMode.OnPropertyChanged));
                isIntegralData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IAttributeValue.IsIntegral), false, DataSourceUpdateMode.OnPropertyChanged));
                isDerivedData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IAttributeValue.IsDerived), false, DataSourceUpdateMode.OnPropertyChanged));
                isValuedData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IAttributeValue.IsValued), false, DataSourceUpdateMode.OnPropertyChanged));
                isNullableData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IAttributeValue.IsNullable), false, DataSourceUpdateMode.OnPropertyChanged));
                isNonKeyData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IAttributeValue.IsNonKey), false, DataSourceUpdateMode.OnPropertyChanged));
                isKeyData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IAttributeValue.IsKey), false, DataSourceUpdateMode.OnPropertyChanged));

                // Specialized Control Binding
                propertyData.BindTo(bindingProperty, formBinding.NewProperty);
                definitionData.BindTo(bindingDefinition, formBinding.NewDefinition);
                subjectArea.BindTo(formBinding.SubjectAreas.ToList, formBinding.AddSubjectArea, formBinding.RemoveSubjectArea);
                aliasData.BindTo(bindingAlias, formBinding.NewAlias, 
                    ScopeType.ModelAttribute,
                    ScopeType.DatabaseTableColumn, ScopeType.DatabaseViewColumn, ScopeType.DatabaseFunction,
                    ScopeType.LibraryTypeField, ScopeType.LibraryTypeProperty);

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.GetAuthorization);
            }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            formBinding.RemoveValue(attributeIndex);
            IsLocked(formBinding.GetLocked());
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            formBinding.Load(attributeIndex, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            formBinding.RemoveValue(attributeIndex);
            formBinding.Save(attributeIndex, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            formBinding.Save(attributeIndex, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);

            Activate(() => new ApplicationWide.HistoryView(formBinding.GetTemporal(attributeIndex))
            {
                OpenForm = (temporal) =>
                {
                    if (temporal.TryGetValue(out AttributeValue? attribute))
                    { return new Attribute(attribute, new TemporalIndex(temporal)); }
                    else { throw new InvalidOperationException("Could not convert TemporalValue back to AttributeValue"); }
                }
            });
        }

        private void MemberNameData_Validating(object sender, CancelEventArgs e)
        {
            PathIndex path = new PathIndex(PathIndex.Parse(memberNameData.Text).ToArray());
            memberNameData.Text = path.MemberFullPath;
        }

        private void XElementRenderCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.TryGetValue(out AttributeValue? value))
            { xElementData.Text = formBinding.GetXElement().ToString(); }
        }
    }
}
