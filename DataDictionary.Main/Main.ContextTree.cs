using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.Library;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
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
            namedScopeData.ReloadCommand();
        }

        private void namedScopeData_OnNamedScopeSelected(object sender, NamedScopeValueEventArgs e)
        {
            if (e.Value is INamedScopeSourceValue target)
            {
                dynamic dataNode = target;
                Activate(dataNode);
            }
        }

        void Activate(ICatalogValue catalogItem)
        { Activate((data) => new Forms.Catalog.DbCatalog(catalogItem), catalogItem); }

        void Activate(ISchemaValue schemaItem)
        { Activate((data) => new Forms.Catalog.DbSchema(schemaItem), schemaItem); }

        void Activate(ITableValue tableItem)
        { Activate((data) => new Forms.Catalog.DbTable(tableItem), tableItem); }

        void Activate(ITableColumnValue columnItem)
        { Activate((data) => new Forms.Catalog.DbTableColumn(columnItem), columnItem); }

        void Activate(IConstraintValue constraintItem)
        { Activate((data) => new Forms.Catalog.DbConstraint(constraintItem), constraintItem); }

        void Activate(IRoutineValue routineItem)
        { Activate((data) => new Forms.Catalog.DbRoutine(routineItem), routineItem); }

        void Activate(IRoutineParameterValue routineParameterItem)
        { Activate((data) => new Forms.Catalog.DbRoutineParameter(routineParameterItem), routineParameterItem); }

        void Activate(IDomainValue domainItem)
        { Activate((data) => new Forms.Catalog.DbDomain(domainItem), domainItem); }

        void Activate(ILibrarySourceValue sourceItem)
        { Activate((data) => new Forms.Library.LibrarySource(sourceItem), sourceItem); }

        void Activate(ILibraryMemberValue memberItem)
        { Activate((data) => new Forms.Library.LibraryMember(memberItem), memberItem); }

        void Activate(AttributeValue attributeItem)
        { Activate((data) => new Forms.Domain.DomainAttribute(attributeItem), attributeItem); }

        void Activate(EntityValue entityItem)
        { Activate((data) => new Forms.Domain.DomainEntity(entityItem), entityItem); }

        void Activate(SubjectAreaValue subjectItem)
        { Activate((data) => new Forms.Model.ModelSubjectArea(subjectItem), subjectItem); }

        void Activate(ModelValue modelItem)
        { Activate((data) => new Forms.Model.Model(modelItem), modelItem); }

        void Activate(TemplateValue templateValue)
        { Activate((data) => new Forms.Scripting.ScriptingTemplate(templateValue), templateValue); }

    }
}
