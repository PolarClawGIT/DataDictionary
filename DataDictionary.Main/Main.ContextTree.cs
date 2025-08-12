using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.AppLibrary;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;

namespace DataDictionary.Main
{
    partial class Main
    {
        protected override void HandleMessage(RefreshNavigation message)
        {
            base.HandleMessage(message);
            namedScopeData.ReloadCommand();
        }

        private void NamedScopeData_OnNamedScopeSelected(object sender, NamedScopeValueEventArgs e)
        {
            if (e.Value is INamedScopeSourceValue target)
            {
                dynamic dataNode = target;
                OpenForm(dataNode);
            }
        }

        void OpenForm(ICatalogValue catalogItem)
        {
            Activate(
                () => new Forms.Catalog.DbCatalog(catalogItem),
                (form) => form.IsOpenItem(catalogItem));
        }


        void OpenForm(ISchemaValue schemaItem)
        {
            Activate(
                () => new Forms.Catalog.DbSchema(schemaItem),
                (form) => form.IsOpenItem(schemaItem));
        }

        void OpenForm(ITableValue tableItem)
        {
            Activate(
                () => new Forms.Catalog.DbTable(tableItem),
                (form) => form.IsOpenItem(tableItem));
        }

        void OpenForm(ITableColumnValue columnItem)
        {
            Activate(
                () => new Forms.Catalog.DbTableColumn(columnItem),
                (form) => form.IsOpenItem(columnItem));
        }

        void OpenForm(IConstraintValue constraintItem)
        {
            Activate(
                () => new Forms.Catalog.DbConstraint(constraintItem),
                (form) => form.IsOpenItem(constraintItem));
        }

        void OpenForm(IRoutineValue routineItem)
        {
            Activate(
                () => new Forms.Catalog.DbRoutine(routineItem),
                (form) => form.IsOpenItem(routineItem));
        }

        void OpenForm(IRoutineParameterValue routineParameterItem)
        {
            Activate(
                () => new Forms.Catalog.DbRoutineParameter(routineParameterItem),
                (form) => form.IsOpenItem(routineParameterItem));
        }

        void OpenForm(IDomainValue domainItem)
        {
            Activate(
                () => new Forms.Catalog.DbDomain(domainItem),
                (form) => form.IsOpenItem(domainItem));
        }

        void OpenForm(ILibrarySourceValue sourceItem)
        {
            Activate(
                () => new Forms.Library.LibrarySource(sourceItem),
                (form) => form.IsOpenItem(sourceItem));
        }

        void OpenForm(ILibraryMemberValue memberItem)
        {
            Activate(
                () => new Forms.Library.LibraryMember(memberItem),
                (form) => form.IsOpenItem(memberItem));
        }

        void OpenForm(IAttributeValue attributeItem)
        {
            Activate(
                () => new Forms.Model.Attribute(attributeItem),
                (form) => form.IsOpenItem(attributeItem));
        }

        void OpenForm(IEntityValue entityItem)
        {
            Activate(
                () => new Forms.Model.Entity(entityItem),
                (form) => form.IsOpenItem(entityItem));
        }

        void OpenForm(IProcessValue processItem)
        {
            Activate(
                () => new Forms.Model.Process(processItem),
                (form) => form.IsOpenItem(processItem));
        }

        void OpenForm(ISubjectAreaValue subjectItem)
        {
            Activate(
                () => new Forms.Model.SubjectArea(subjectItem),
                (form) => form.IsOpenItem(subjectItem));
        }

        void OpenForm(IModelValue modelItem)
        {
            Activate(
                () => new Forms.Model.Model(modelItem),
                (form) => form.IsOpenItem(modelItem));
        }

        void OpenForm(ITemplateValue template)
        {
            Activate(
                () => new Forms.Scripting.Template(template),
                (form) => form.IsOpenItem(template));
        }

        void OpenForm(IDataSourceValue dataSource)
        {
            Activate(
                () => new Forms.Scripting.DataSource(dataSource),
                (form) => form.IsOpenItem(dataSource));
        }

        [Obsolete("replace", true)]
        void OpenForm(ScriptingTemplateValue templateValue)
        {
            Activate(
                () => new Forms.Scripting.ScriptingTemplate(templateValue),
                (form) => form.IsOpenItem(templateValue));
        }
    }
}
