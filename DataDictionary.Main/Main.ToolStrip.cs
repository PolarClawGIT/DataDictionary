using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.AppLibrary;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppScripting;

//using DataDictionary.BusinessLayer.Obsolete;
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
                (ScopeType.ModelAttribute, BusinessData.Model.Attribute.Attributes)
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
                (ScopeType.DatabaseConstraintCheck, BusinessData.CatalogModel.DbConstraintColumns));
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
                (ScopeType.DatabaseSchema, BusinessData.CatalogModel.DbSchemata)
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
                (ScopeType.ModelAttributeProperty, BusinessData.Model.Attribute.Properties));
        }

        private void menuAttributeAlaises_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelAttributeAlias, BusinessData.Model.Attribute.Aliases));
        }

        private void menuAttributeDefinitions_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelAttributeDefinition, BusinessData.Model.Attribute.Definitions));
        }

        private void menuEntities_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <EntityValue, Forms.Model.Entity>
                (ScopeType.ModelEntity, BusinessData.Model.Entity.Entities)
            { SelectedForm = (data) => new Forms.Model.Entity(data) });
        }

        private void menuEntityProperties_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelEntityProperty, BusinessData.Model.Entity.Properties));
        }

        private void menuEntityDefinitions_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelEntityDefinition, BusinessData.Model.Entity.Definitions));
        }

        private void menuEntityAlias_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelEntityAlias, BusinessData.Model.Entity.Aliases));
        }

        private void menuEntityAttributes_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelEntityAttribute, BusinessData.Model.Entity.Attributes));
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

        [Obsolete]
        private void manageScriptingCommand_ButtonClick(object sender, EventArgs e)
        { Activate(static () => new Forms.Obsolete.TemplateManager()); }

        private void menuScriptingTemplates_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <BusinessLayer.Obsolete.TemplateValue, Forms.Obsolete.Template>
                (ScopeType.ScriptingTemplate, BusinessData.Scripting.Templates)
            { SelectedForm = (data) => new Forms.Obsolete.Template(data) });
        }

        [Obsolete]
        private void menuScriptingPath_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <BusinessLayer.Obsolete.DataSourceValue, Forms.Obsolete.DataSource>
                (ScopeType.ScriptingData, BusinessData.Scripting.DataSources)
            { SelectedForm = (data) => new Forms.Obsolete.DataSource(data) });
        }

        [Obsolete]
        private void menuScriptingDocument_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <BusinessLayer.Obsolete.DocumentValue, Forms.Obsolete.Document>
                (ScopeType.ScriptingDocument, BusinessData.Scripting.Documents)
            { SelectedForm = (data) => new Forms.Obsolete.Document(data) });
        }

        [Obsolete]
        private void menuScriptingNode_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ScriptingTemplateNode, BusinessData.Scripting.TemplateNodes));
        }

        [Obsolete]
        private void menuScriptingNodeOwner_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ScriptingTemplateNodeOwner, BusinessData.Scripting.TemplateNodeOwners));
        }

        [Obsolete]
        private void MenuScriptingAddTemplate_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Obsolete.Template(null)); }

        [Obsolete]
        private void MenuScriptingAddDocument_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Obsolete.Document(null)); }

        [Obsolete]
        private void MenuScriptingAddData_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Obsolete.DataSource(null)); }

        private void MenuProcess_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <ProcessValue, Forms.Model.Process>
                (ScopeType.ModelProcess, BusinessData.Model.Process.Processes)
            { SelectedForm = (data) => new Forms.Model.Process(data) });
        }

        private void MenuProcessAlias_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelProcessAlias, BusinessData.Model.Process.Aliases));
        }

        private void MenuProcessArgument_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelProcessArgument, BusinessData.Model.Process.Arguments));
        }

        private void MenuProcessDefinition_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelProcessDefinition, BusinessData.Model.Process.Definitions));
        }

        private void MenuProcessProperty_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ModelProcessProperty, BusinessData.Model.Process.Properties));
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

        private void SecuritySetAuthorization_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Security.Authorization()); }

        private void DatabaseMessagesCommand_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ApplicationLog, BusinessData.Messages));
        }

        private void ManageTemplateCommand_ButtonClick(object sender, EventArgs e)
        { Activate(static () => new Forms.Scripting.TemplateManager()); }


        private void MenuNewTemplate_Click(object sender, EventArgs e)
        { Activate(static () => new Forms.Scripting.Template(null)); }


        private void MenuTemplate_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <TemplateValue, Forms.Scripting.Template>
                (ScopeType.ScriptingTemplate, BusinessData.Templates)
            { SelectedForm = (data) => new Forms.Scripting.Template(data) });
        }

        private void MenuTemplateTransform_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <TransformValue, Forms.Scripting.Transform>
                (ScopeType.ScriptingTransform, BusinessData.Templates.Transforms)
            { SelectedForm = (data) => new Forms.Scripting.Transform(data) });
        }

        private void MenuTemplateSchemata_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                <SchemaDefinitionValue, Forms.Scripting.SchemaDefinition>
                (ScopeType.ScriptingSchema, BusinessData.Templates.Schemata)
            { SelectedForm = (data) => new Forms.Scripting.SchemaDefinition(data) });
        }

        private void MenuTemplateNode_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ScriptingSchema, BusinessData.Templates.SchemataNodes));
        }

        private void MenuTemplateNodeOwner_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ScriptingNodeOwner, BusinessData.Templates.SchemataNodeOwners));
        }

        private void MenuTemplateObject_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ScriptingObject, BusinessData.Templates.Objects));
        }

        private void MenuTemplateSchemaDocument_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ScriptingDocument, BusinessData.Templates.SchemaDocuments));
        }

        private void MenuTemplateTransformDocument_Click(object sender, EventArgs e)
        {
            Activate(static () => new DetailDataView
                (ScopeType.ApplicationDocument, BusinessData.Templates.TransformDocuments));
        }
    }
}
