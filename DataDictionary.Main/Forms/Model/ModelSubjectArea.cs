using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Model
{
    partial class ModelSubjectArea : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingSubject.Current is ISubjectAreaValue current && ReferenceEquals(current, item); }

        public ModelSubjectArea() : base()
        {
            InitializeComponent();

            SetRowState(bindingSubject);
            SetTitle(bindingSubject);
            SetCommand(ScopeType.ModelSubjectArea,
                CommandImageType.Delete);
        }

        public ModelSubjectArea(ISubjectAreaValue? subjectAreaItem) : this()
        {
            if (subjectAreaItem is null)
            {
                subjectAreaItem = new SubjectAreaValue();
                BusinessData.SubjectAreas.Add(subjectAreaItem);
            }

            SubjectAreaIndex key = new SubjectAreaIndex(subjectAreaItem);

            bindingSubject.DataSource = new BindingView<SubjectAreaValue>(BusinessData.SubjectAreas, w => key.Equals(w));
            bindingSubject.Position = 0;

            if (bindingSubject.Current is ISubjectAreaValue current)
            {
                
                //List<AttributeIndex> attributeKeys = BusinessData.DomainModel.Attributes.SubjectAreas.Where(w => key.Equals(w)).Select(s => new AttributeIndex(s)).ToList();
                //bindingAttribute.DataSource = new BindingView<AttributeValue>(BusinessData.DomainModel.Attributes, w => attributeKeys.Contains(new AttributeIndex(w)));

                //List<EntityIndex> entityKeys = BusinessData.DomainModel.Entities.SubjectAreas.Where(w => key.Equals(w)).Select(s => new DomainEntityKey(s)).ToList();
                //bindingEntity.DataSource = new BindingView<EntityValue>(BusinessData.DomainModel.Entities, w => entityKeys.Contains(new DomainEntityKey(w)));
            }
        }

        private void DomainSubjectArea_Load(object sender, EventArgs e)
        {
            ISubjectAreaValue bindingNames;
            subjectAreaTitleData.DataBindings.Add(new Binding(nameof(subjectAreaTitleData.Text), bindingSubject, nameof(bindingNames.SubjectAreaTitle)));
            subjectAreaDescriptionData.DataBindings.Add(new Binding(nameof(subjectAreaDescriptionData.Text), bindingSubject, nameof(bindingNames.SubjectAreaDescription)));
            memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), bindingSubject, nameof(bindingNames.SubjectName)));

            attributeData.AutoGenerateColumns = false;
            attributeData.DataSource = bindingAttribute;

            entityData.AutoGenerateColumns = false;
            entityData.DataSource = bindingEntity;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSubject.Current is not ISubjectAreaValue);
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            if (bindingSubject.Current is SubjectAreaValue current)
            {
                this.IsLocked(true);
                SubjectAreaIndex key = new SubjectAreaIndex(current);

                current.Remove();
                bindingAttribute.Clear();
                bindingEntity.Clear();

                RowState = current.RowState();
            }
        }

        private void MemberNameData_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PathIndex path = new PathIndex(PathIndex.Parse(memberNameData.Text).ToArray());
            memberNameData.Text = path.MemberFullPath;
        }
    }
}
