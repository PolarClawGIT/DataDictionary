using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.AppLibrary;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Forms.ApplicationWide;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main
{
    partial class Main
    {

        private void manageLibrariesCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(static () => new Forms.Library.LibraryManager()); }

        private void manageDatabasesCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(static () => new Forms.Catalog.CatalogManager()); }

        private void NewAttributeCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(static () => new Forms.Model.Attribute(null)); }

        private void NewEntityCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(static () => new Forms.Model.Entity(null)); }

        private void NewSubjectAreaCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(static () => new Forms.Model.SubjectArea(null)); }

        private void ManageModelCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(static () => new Forms.Model.ModelManager()); }

        private void MenuCatalogItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <CatalogValue, Forms.Catalog.DbCatalog>
                (ScopeType.Database, BusinessData.CatalogModel.DbCatalogs)
            { SelectedForm = (data) => new Forms.Catalog.DbCatalog(data) });
        }

        private void MenuAttributes_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <AttributeValue, Forms.Model.Attribute>
                (ScopeType.ModelAttribute, BusinessData.Model.Attributes.Values)
            { SelectedForm = (data) => new Forms.Model.Attribute(data) });
        }

        private void subjectAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <SubjectAreaValue, Forms.Model.SubjectArea>
                (ScopeType.ModelSubjectArea, BusinessData.Model.SubjectAreas)
            { SelectedForm = (data) => new Forms.Model.SubjectArea(data) });
        }

        private void browseHelpCommand_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <HelpSubjectValue, Forms.General.HelpSubject>
                (ScopeType.ApplicationHelp, BusinessData.ApplicationData.HelpSubjects)
            { SelectedForm = (data) => new Forms.General.HelpSubject(data) });
        }

        private void viewLibrarySourceCommand_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <LibrarySourceValue, Forms.Library.LibrarySource>
                (ScopeType.Library, BusinessData.LibraryModel.LibrarySources)
            { SelectedForm = (data) => new Forms.Library.LibrarySource(data) });
        }

        private void viewLibraryMemberCommand_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <LibraryMemberValue, Forms.Library.LibraryMember>
                (ScopeType.LibraryType, BusinessData.LibraryModel.LibraryMembers)
            { SelectedForm = (data) => new Forms.Library.LibraryMember(data) });
        }

        private void menuConstraintItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <ConstraintValue, Forms.Catalog.DbConstraint>
                (ScopeType.DatabaseConstraint, BusinessData.CatalogModel.DbConstraints)
            { SelectedForm = (data) => new Forms.Catalog.DbConstraint(data) });
        }

        private void menuConstraintColumnItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.DatabaseConstraintColumn, BusinessData.CatalogModel.DbConstraintColumns));
        }

        private void menuDataTypeItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <DomainValue, Forms.Catalog.DbDomain>
                (ScopeType.DatabaseDomain, BusinessData.CatalogModel.DbDomains)
            { SelectedForm = (data) => new Forms.Catalog.DbDomain(data) });
        }

        private void menuRoutineItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <RoutineValue, Forms.Catalog.DbRoutine>
                (ScopeType.DatabaseProcedure, BusinessData.CatalogModel.DbRoutines)
            { SelectedForm = (data) => new Forms.Catalog.DbRoutine(data) });
        }

        private void menuRoutineParameterItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <RoutineParameterValue, Forms.Catalog.DbRoutineParameter>
                (ScopeType.DatabaseProcedureParameter, BusinessData.CatalogModel.DbRoutineParameters)
            { SelectedForm = (data) => new Forms.Catalog.DbRoutineParameter(data) });
        }

        private void menuRoutineColumnItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.DatabaseFunctionColumn, BusinessData.CatalogModel.DbRoutineColumns));
        }

        private void menuReferenceItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.DatabaseReference, BusinessData.CatalogModel.DbReferences));
        }

        private void menuSchemaItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <SchemaValue, Forms.Catalog.DbSchema>
                (ScopeType.DatabaseSchema, BusinessData.CatalogModel.DbSchemta)
            { SelectedForm = (data) => new Forms.Catalog.DbSchema(data) });
        }

        private void menuTableItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <TableValue, Forms.Catalog.DbTable>
                (ScopeType.DatabaseTable, BusinessData.CatalogModel.DbTables)
            { SelectedForm = (data) => new Forms.Catalog.DbTable(data) });
        }

        private void menuColumnItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <TableColumnValue, Forms.Catalog.DbTableColumn>
                (ScopeType.DatabaseTableColumn, BusinessData.CatalogModel.DbTableColumns)
            { SelectedForm = (data) => new Forms.Catalog.DbTableColumn(data) });
        }

        private void menuPropertyItem_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.DatabaseProperty, BusinessData.CatalogModel.DbProperties));
        }

        private void menuAttributeProperties_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelAttributeProperty, BusinessData.Model.Attributes.Properties));
        }

        private void menuAttributeAlaises_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelAttributeAlias, BusinessData.Model.Attributes.Aliases));
        }

        private void menuAttributeDefinitions_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelAttributeDefinition, BusinessData.Model.Attributes.Definitions));
        }

        private void menuEntities_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <EntityValue, Forms.Model.Entity>
                (ScopeType.ModelEntity, BusinessData.Model.Entities.Values)
            { SelectedForm = (data) => new Forms.Model.Entity(data) });
        }

        private void menuEntityProperties_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelEntityProperty, BusinessData.Model.Entities.Properties));
        }

        private void menuEntityDefinitions_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelEntityDefinition, BusinessData.Model.Entities.Definitions));
        }

        private void menuEntityAlias_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelEntityAlias, BusinessData.Model.Entities.Aliases));
        }

        private void menuEntityAttributes_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelEntityAttribute, BusinessData.Model.Entities.Attributes));
        }

        private void menuModelProperty_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelProperty, BusinessData.Model.Properties));
        }

        private void menuModelDefinition_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelDefinition, BusinessData.Model.Definitions));
        }

        private void manageScriptingCommand_ButtonClick(object sender, EventArgs e)
        { Activate(static () => new Forms.Scripting.TemplateManager()); }

        private void menuScriptingTemplates_Click(object sender, EventArgs e)
        {
            //TODO: Fix
            //Activate(static () => new DetailDataView
            //    <ScriptingTemplateValue, Forms.Scripting.ScriptingTemplate>
            //    (ScopeType.ScriptingTemplate, BusinessData.ScriptingEngine.Templates)
            //{ SelectedForm = (data) => new Forms.Scripting.ScriptingTemplate(data) });
        }

        private void menuScriptingPath_Click(object sender, EventArgs e)
        {
            //TODO: Fix
            //Activate(static () => new DetailDataView
            //    (ScopeType.ScriptingTemplatePath, BusinessData.ScriptingEngine.TemplatePaths));
        }

        private void menuScriptingDocument_Click(object sender, EventArgs e)
        {
            //TODO: Fix
            //Activate(static () => new DetailDataView
            //    (ScopeType.ScriptingTemplateDocument, BusinessData.ScriptingEngine.TemplateDocuments));
        }

        private void menuScriptingNode_Click(object sender, EventArgs e)
        {
            //TODO: Fix
            //Activate(static () => new DetailDataView
            //    (ScopeType.ScriptingTemplateNode, BusinessData.ScriptingEngine.TemplateNodes));
        }

        private void menuScriptingAttribute_Click(object sender, EventArgs e)
        {
            //TODO: Fix
            //Activate(static () => new DetailDataView
            //    (ScopeType.ScriptingTemplateAttribute, BusinessData.ScriptingEngine.TemplateAttributes));
        }

        private void SecurityPrincipal_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Security.PrincipalManager()); }

        private void SecurityRole_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Security.RoleManager()); }

        private void MenuProcess_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <ProcessValue, Forms.Model.Process>
                (ScopeType.ModelProcess, BusinessData.Model.Processes.Values)
            { SelectedForm = (data) => new Forms.Model.Process(data) });
        }

        private void MenuProcessAlias_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelProcessAlias, BusinessData.Model.Processes.Aliases));
        }

        private void MenuProcessArgument_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelProcessArgument, BusinessData.Model.Processes.Arguments));
        }

        private void MenuProcessDefinition_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelProcessDefinition, BusinessData.Model.Processes.Definitions));
        }

        private void MenuProcessProperty_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelProcessProperty, BusinessData.Model.Processes.Properties));
        }

        private void NewProcessCommand_ButtonClick(object sender, EventArgs e)
        {
            { Activate(static () => new Forms.Model.Process(null)); }
        }

        private void SecurityAuthorization_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.Security, BusinessData.Authorization));
        }
        private void newRelationshipCommand_ButtonClick(object sender, EventArgs e)
        {
            // Currently not used
        }
    }
}
