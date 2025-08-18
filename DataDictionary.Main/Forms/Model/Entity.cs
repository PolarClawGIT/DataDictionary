using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using System.ComponentModel;
using System.Data;
using DataDictionary.Resource.Enumerations;
using DataDictionary.Main.Dialogs;
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
        Boolean needsData = false;

        protected Entity() : base()
        {
            InitializeComponent();
            attributeLayout.Enabled = false;

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

            attributeSelectCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelEntityAttribute, CommandImageType.Select);
            attributeNewCommand.Image = NavigationEnumeration.GetImage(ScopeType.ModelEntityAttribute, CommandImageType.Add);
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
                titleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingEntity, nameof(IEntityValue.EntityTitle)));
                descriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingEntity, nameof(IEntityValue.EntityDescription)));

                memberNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingEntity, nameof(IEntityValue.EntityName), false, DataSourceUpdateMode.OnPropertyChanged));

                // Attribute Handling
                attributeData.AutoGenerateColumns = false;
                attributeData.DataSource = bindingAttribute;

                attributeNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingAttribute, nameof(IEntityAttributeValue.AttributeName)));
                attributeOrderData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingAttribute, nameof(IEntityAttributeValue.OrdinalPosition), true, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
                attributeKnownAsData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingAttribute, nameof(IEntityAttributeValue.AttributeKnownAs)));
                attributeNullable.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IEntityAttributeValue.IsNullable), true, DataSourceUpdateMode.OnValidation, false));
                attributePrimaryKey.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IEntityAttributeValue.IsPrimaryKey), true, DataSourceUpdateMode.OnValidation, false));

                attributeTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingAttribute, nameof(IEntityAttributeValue.AttributeTitle)));
                attributeDescriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingAttribute, nameof(IEntityAttributeValue.AttributeDescription)));
                attributeInModelData.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingAttribute, nameof(IEntityAttributeValue.InModel), false, DataSourceUpdateMode.OnPropertyChanged));

                // Specialized Control Binding
                propertyData.BindTo(bindingProperty, formBinding.NewProperty);
                definitionData.BindTo(bindingDefinition, formBinding.NewDefinition);
                subjectArea.BindTo(formBinding.SubjectAreas.ToList, formBinding.AddSubjectArea, formBinding.RemoveSubjectArea );
                aliasData.BindTo(bindingAlias, formBinding.NewAlias,
                    ScopeType.ModelEntity,
                    ScopeType.DatabaseTable, ScopeType.DatabaseView,
                    ScopeType.LibraryType);

                // Security
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
                    else { throw new InvalidOperationException("Could not convert TemporalValue back to EntityValue"); }
                }
            });
        }

        private void MemberNameData_Validating(object sender, CancelEventArgs e)
        {
            PathIndex path = new PathIndex(PathIndex.Parse(memberNameData.Text).ToArray());
            memberNameData.Text = path.MemberFullPath;
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

        private void AttributeNameData_Validating(object sender, CancelEventArgs e)
        {
            if (formBinding.TryGetAttribute(out EntityAttributeValue? value))
            { value.AttributePath = new PathIndex(PathIndex.Parse(attributeNameData.Text).ToArray()); }
        }


    }
}
