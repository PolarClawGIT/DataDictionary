using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.Library;
using DataDictionary.BusinessLayer.Model;
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

        /// <summary>
        /// Used in determine if the node should expand the node or not.
        /// </summary>
        /// <remarks>
        /// Normally, a double click anywhere on the node will expand/collapse the node.
        /// 
        /// The code Event NodeMouseClick (happens first), captures what was clicked (to be passed to Expand/Collapse).
        /// The code Events Before Expand/Collapse, depending on if +/- clicked cancel the action.
        /// The code Events After Expand/Collapse, reset the flag back to null (the event hand been handled).
        /// The code Event NodeMouseDoubleClick (happens last), determine if +/- was clicked an ignore the event if so.
        ///   Null = Click was not fired. Node.Expanded() or Node.Collapse() is used. Expand/Collapse reset to Null.
        ///   True = +/- of the node was clicked (possibly double clicked).
        ///   False = Something other then the +/- of the node was clicked (possibly double clicked).
        /// </remarks>
        Boolean? isTreeNodePlusMinus = null;

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
