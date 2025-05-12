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

        void OpenForm(AttributeValue attributeItem)
        {
            Activate(
                () => new Forms.Model.Attribute(attributeItem),
                (form) => form.IsOpenItem(attributeItem));
        }

        void OpenForm(EntityValue entityItem)
        {
            Activate(
                () => new Forms.Model.Entity(entityItem),
                (form) => form.IsOpenItem(entityItem));
        }

        void OpenForm(SubjectAreaValue subjectItem)
        {
            Activate(
                () => new Forms.Model.SubjectArea(subjectItem),
                (form) => form.IsOpenItem(subjectItem));
        }

        void OpenForm(ModelValue modelItem)
        {
            Activate(
                () => new Forms.Model.Model(modelItem),
                (form) => form.IsOpenItem(modelItem));
        }

        void OpenForm(TemplateValue templateValue)
        {
            Activate(
                () => new Forms.Scripting.ScriptingTemplate(templateValue),
                (form) => form.IsOpenItem(templateValue));
        }
    }
}
