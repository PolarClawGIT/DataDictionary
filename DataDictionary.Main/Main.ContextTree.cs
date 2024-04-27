using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.Library;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using Toolbox.Threading;

namespace DataDictionary.Main
{
    partial class Main
    {
        protected override void HandleMessage(RefreshNavigation message)
        {
            base.HandleMessage(message);
            this.DoWork(contextNameNavigation.Load(BusinessData.NamedScope));
        }

        private void RefreshCommand_Click(object? sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(BusinessData.LoadNamedScope());
            work.AddRange(contextNameNavigation.Load(BusinessData.NamedScope));
            this.DoWork(work);
        }

        private void DataSourceNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (contextNameNavigation.SelectedNode is TreeNode node)
            {
                INamedScopeValue? item = node.GetNamedScope();
                if (item is INamedScopeValue taget)
                {
                    dynamic dataNode = taget;
                    Activate(dataNode);
                }
            }
        }

        void Activate(ICatalogValue catalogItem)
        { Activate((data) => new Forms.Database.DbCatalog(catalogItem), catalogItem); }

        void Activate(BusinessLayer.Database.ISchemaValue schemaItem)
        { Activate((data) => new Forms.Database.DbSchema(schemaItem), schemaItem); }

        void Activate(ITableValue tableItem)
        { Activate((data) => new Forms.Database.DbTable(tableItem), tableItem); }

        void Activate(ITableColumnValue columnItem)
        { Activate((data) => new Forms.Database.DbTableColumn(columnItem), columnItem); }

        void Activate(IConstraintValue constraintItem)
        { Activate((data) => new Forms.Database.DbConstraint(constraintItem), constraintItem); }

        void Activate(IRoutineValue routineItem)
        { Activate((data) => new Forms.Database.DbRoutine(routineItem), routineItem); }

        void Activate(IRoutineParameterValue routineParameterItem)
        { Activate((data) => new Forms.Database.DbRoutineParameter(routineParameterItem), routineParameterItem); }

        void Activate(IDomainValue domainItem)
        { Activate((data) => new Forms.Database.DbDomain(domainItem), domainItem); }

        void Activate(ILibrarySourceValue sourceItem)
        { Activate((data) => new Forms.Library.LibrarySource(sourceItem), sourceItem); }

        void Activate(ILibraryMemberValue memberItem)
        { Activate((data) => new Forms.Library.LibraryMember(memberItem), memberItem); }

        void Activate(AttributeValue attributeItem)
        { Activate((data) => new Forms.Domain.DomainAttribute(attributeItem), attributeItem); }

        void Activate(DomainEntityItem entityItem)
        { Activate((data) => new Forms.Domain.DomainEntity(entityItem), entityItem); }

        void Activate(ModelSubjectAreaItem subjectItem)
        { Activate((data) => new Forms.Domain.ModelSubjectArea(subjectItem), subjectItem); }

        void Activate(ModelItem modelItem)
        { Activate((data) => new Forms.Model.Model(modelItem), modelItem); }

        void Activate(BusinessLayer.Scripting.SchemaValue schemaItem)
        { Activate((data) => new Forms.Scripting.SchemaManager(schemaItem), schemaItem); }
    }
}
