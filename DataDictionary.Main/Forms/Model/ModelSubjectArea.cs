using DataDictionary.BusinessLayer.NameScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Properties;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Domain
{
    partial class ModelSubjectArea : ApplicationBase, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingSubject.Current is IModelSubjectAreaItem current && ReferenceEquals(current, item); }

        public ModelSubjectArea() : base()
        {
            InitializeComponent();

            deleteItemCommand.Click += DeleteItemCommand_Click;
            deleteItemCommand.Enabled = true;
            deleteItemCommand.Image = Resources.DeleteDiagram;
            deleteItemCommand.ToolTipText = "Remove the Subject Area";
        }

        public ModelSubjectArea(IModelSubjectAreaItem subjectAreaItem) : this()
        {
            ModelSubjectAreaKey key = new ModelSubjectAreaKey(subjectAreaItem);

            bindingSubject.DataSource = new BindingView<ModelSubjectAreaItem>(BusinessData.ModelSubjectAreas, w => key.Equals(w));
            bindingSubject.Position = 0;

            if(bindingSubject.Current is IModelSubjectAreaItem current)
            {
                this.Icon = ScopeType.ModelSubjectArea.ToIcon();
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();

                List<DomainAttributeKey> attributeKeys = BusinessData.DomainModel.Attributes.SubjectAreas.Where(w => key.Equals(w)).Select(s => new DomainAttributeKey(s)).ToList();
                bindingAttribute.DataSource = new BindingView<DomainAttributeItem>(BusinessData.DomainModel.Attributes, w => attributeKeys.Contains(new DomainAttributeKey(w)));

                List<DomainEntityKey> entityKeys = BusinessData.DomainModel.Entities.SubjectAreas.Where(w => key.Equals(w)).Select(s => new DomainEntityKey(s)).ToList();
                bindingEntity.DataSource = new BindingView<DomainEntityItem>(BusinessData.DomainModel.Entities, w => entityKeys.Contains(new DomainEntityKey(w)));
            }
        }

        private void DomainSubjectArea_Load(object sender, EventArgs e)
        {
            IModelSubjectAreaItem bindingNames;
            subjectAreaTitleData.DataBindings.Add(new Binding(nameof(subjectAreaTitleData.Text), bindingSubject, nameof(bindingNames.SubjectAreaTitle)));
            subjectAreaDescriptionData.DataBindings.Add(new Binding(nameof(subjectAreaDescriptionData.Text), bindingSubject, nameof(bindingNames.SubjectAreaDescription)));
            subjectAreaNameSpaceData.DataBindings.Add(new Binding(nameof(subjectAreaNameSpaceData.Text), bindingSubject, nameof(bindingNames.SubjectAreaNameSpace)));

            attributeData.AutoGenerateColumns = false;
            attributeData.DataSource = bindingAttribute;

            entityData.AutoGenerateColumns = false;
            entityData.DataSource = bindingEntity;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSubject.Current is not IModelSubjectAreaItem);
        }

        private void DeleteItemCommand_Click(object? sender, EventArgs e)
        {
            if (bindingSubject.Current is ModelSubjectAreaItem current)
            {
                this.IsLocked(true);
                ModelSubjectAreaKey key = new ModelSubjectAreaKey(current);

                NameScopeKey nameKey = new NameScopeKey(current);
                BusinessData.NameScope.Remove(nameKey);

                current.Remove();
                bindingAttribute.Clear();
                bindingEntity.Clear();

                RowState = current.RowState();
            }
        }

    }
}
