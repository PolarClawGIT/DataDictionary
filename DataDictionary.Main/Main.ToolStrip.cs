using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.Library;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Main.Forms.ApplicationWide;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main
{
    partial class Main
    {

        private void manageLibrariesCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Library.LibraryManager()); }

        private void manageDatabasesCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Catalog.CatalogManager()); }

        private void NewAttributeCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Model.Attribute(null)); }

        private void NewEntityCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Model.Entity(null)); }

        private void NewSubjectAreaCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Model.ModelSubjectArea(null)); }

        private void ManageModelCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Model.ModelManager()); }

        private void MenuCatalogItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<CatalogValue, Forms.Catalog.DbCatalog>
                    (ScopeType.Database, data)
                { SelectedForm = (data) => new Forms.Catalog.DbCatalog(data) },
                BusinessData.CatalogModel.DbCatalogs);
        }

        private void MenuAttributes_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<AttributeValue, Forms.Model.Attribute>
                    (ScopeType.ModelAttribute, data)
                { SelectedForm = (data) => new Forms.Model.Attribute(data) },
                BusinessData.Model.Attributes);
        }

        private void subjectAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<SubjectAreaValue, Forms.Model.ModelSubjectArea>
                    (ScopeType.ModelSubjectArea, data)
                { SelectedForm = (data) => new Forms.Model.ModelSubjectArea(data) },
                BusinessData.Model.SubjectAreas);
        }

        private void browseHelpCommand_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<HelpSubjectValue, Forms.General.HelpSubject>
                    (ScopeType.ApplicationHelp, data)
                { SelectedForm = (data) => new Forms.General.HelpSubject(data) },
                BusinessData.ApplicationData.HelpSubjects);
        }

        private void viewLibrarySourceCommand_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<LibrarySourceValue, Forms.Library.LibrarySource>
                    (ScopeType.Library, data)
                { SelectedForm = (data) => new Forms.Library.LibrarySource(data) },
                BusinessData.LibraryModel.LibrarySources);
        }

        private void viewLibraryMemberCommand_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<LibraryMemberValue, Forms.Library.LibraryMember>
                    (ScopeType.LibraryType, data)
                { SelectedForm = (data) => new Forms.Library.LibraryMember(data) },
                BusinessData.LibraryModel.LibraryMembers);
        }

        private void menuConstraintItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<ConstraintValue, Forms.Catalog.DbConstraint>
                    (ScopeType.DatabaseConstraint, data)
                { SelectedForm = (data) => new Forms.Catalog.DbConstraint(data) },
                BusinessData.CatalogModel.DbConstraints);
        }

        private void menuConstraintColumnItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.DatabaseConstraintColumn, data),
                BusinessData.CatalogModel.DbConstraintColumns);
        }

        private void menuDataTypeItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<DomainValue, Forms.Catalog.DbDomain>
                    (ScopeType.DatabaseDomain, data)
                { SelectedForm = (data) => new Forms.Catalog.DbDomain(data) },
                BusinessData.CatalogModel.DbDomains);
        }

        private void menuRoutineItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<RoutineValue, Forms.Catalog.DbRoutine>
                    (ScopeType.DatabaseProcedure, data)
                { SelectedForm = (data) => new Forms.Catalog.DbRoutine(data) },
                BusinessData.CatalogModel.DbRoutines);
        }

        private void menuRoutineParameterItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<RoutineParameterValue, Forms.Catalog.DbRoutineParameter>
                    (ScopeType.DatabaseProcedureParameter, data)
                { SelectedForm = (data) => new Forms.Catalog.DbRoutineParameter(data) },
                BusinessData.CatalogModel.DbRoutineParameters);
        }

        private void menuRoutineColumnItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.DatabaseFunctionColumn, data),
                BusinessData.CatalogModel.DbRoutineColumns);
        }

        private void menuReferenceItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.DatabaseReference, data),
                BusinessData.CatalogModel.DbReferences);
        }

        private void menuSchemaItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<SchemaValue, Forms.Catalog.DbSchema>
                    (ScopeType.DatabaseSchema, data)
                { SelectedForm = (data) => new Forms.Catalog.DbSchema(data) },
                BusinessData.CatalogModel.DbSchemta);
        }

        private void menuTableItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<TableValue, Forms.Catalog.DbTable>
                    (ScopeType.DatabaseTable, data)
                { SelectedForm = (data) => new Forms.Catalog.DbTable(data) },
                BusinessData.CatalogModel.DbTables);
        }

        private void menuColumnItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<TableColumnValue, Forms.Catalog.DbTableColumn>
                    (ScopeType.DatabaseTableColumn, data)
                { SelectedForm = (data) => new Forms.Catalog.DbTableColumn(data) },
                BusinessData.CatalogModel.DbTableColumns);
        }

        private void menuPropertyItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.DatabaseProperty, data),
                BusinessData.CatalogModel.DbProperties);
        }

        private void menuAttributeProperties_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ModelAttributeProperty, data),
                BusinessData.Model.Attributes.Properties);
        }

        private void menuAttributeAlaises_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ModelAttributeAlias, data),
                BusinessData.Model.Attributes.Aliases);
        }

        private void menuAttributeDefinitions_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ModelAttributeDefinition, data),
                BusinessData.Model.Attributes.Definitions);
        }

        private void menuEntities_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<EntityValue, Forms.Model.Entity>
                    (ScopeType.ModelEntity, data)
                { SelectedForm = (data) => new Forms.Model.Entity(data) },
                BusinessData.Model.Entities);
        }

        private void menuEntityProperties_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ModelEntityProperty, data),
                BusinessData.Model.Entities.Properties);
        }

        private void menuEntityDefinitions_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ModelEntityDefinition, data),
                BusinessData.Model.Entities.Definitions);
        }

        private void menuEntityAlias_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ModelEntityAlias, data),
                BusinessData.Model.Entities.Aliases);
        }

        private void menuEntityAttributes_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ModelEntityAttribute, data),
                BusinessData.Model.Entities.Attributes);
        }

        private void menuModelProperty_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ModelProperty, data),
                BusinessData.Model.Properties);
        }

        private void menuModelDefinition_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ModelDefinition, data),
                BusinessData.Model.Definitions);
        }

        private void manageScriptingCommand_ButtonClick(object sender, EventArgs e)
        { Activate(() => new Forms.Scripting.ScriptingTemplate(null)); }

        private void menuScriptingTemplates_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView<TemplateValue, Forms.Scripting.ScriptingTemplate>
                    (ScopeType.ScriptingTemplate, data)
                { SelectedForm = (data) => new Forms.Scripting.ScriptingTemplate(data) },
                BusinessData.ScriptingEngine.Templates);
        }

        private void menuScriptingPath_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ScriptingTemplatePath, data),
                BusinessData.ScriptingEngine.TemplatePaths);
        }

        private void menuScriptingDocument_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ScriptingTemplateDocument, data),
                BusinessData.ScriptingEngine.TemplateDocuments);
        }

        private void menuScriptingNode_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ScriptingTemplateNode, data),
                BusinessData.ScriptingEngine.TemplateNodes);
        }

        private void menuScriptingAttribute_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new DetailDataView(ScopeType.ScriptingTemplateAttribute, data),
                BusinessData.ScriptingEngine.TemplateAttributes);
        }

        private void SecurityPrincipal_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Security.PrincipalManager()); }

        private void SecurityRole_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Security.RoleManager()); }
    }
}
