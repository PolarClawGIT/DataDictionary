using DataDictionary.BusinessLayer.AppGeneral;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.Library;
using DataDictionary.BusinessLayer.Model;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main
{
    partial class Main
    {

        private void manageLibrariesCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Library.LibraryManager()); }

        private void manageDatabasesCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Database.CatalogManager()); }

        private void NewAttributeCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Domain.DomainAttribute(null)); }

        private void NewEntityCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Domain.DomainEntity(null)); }

        private void NewSubjectAreaCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Model.ModelSubjectArea(null)); }

        private void ManageModelCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Model.ModelManager()); }

        private void MenuCatalogItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<CatalogValue, Forms.Database.DbCatalog>
                    (ScopeType.Database, data)
                { SelectedForm = (data) => new Forms.Database.DbCatalog(data) },
                BusinessData.DatabaseModel.DbCatalogs);
        }

        private void MenuAttributes_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<AttributeValue, Forms.Domain.DomainAttribute>
                    (ScopeType.ModelAttribute, data)
                { SelectedForm = (data) => new Forms.Domain.DomainAttribute(data) },
                BusinessData.DomainModel.Attributes);
        }

        private void subjectAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<SubjectAreaValue, Forms.Model.ModelSubjectArea>
                    (ScopeType.ModelSubjectArea, data)
                { SelectedForm = (data) => new Forms.Model.ModelSubjectArea(data) },
                BusinessData.SubjectAreas);
        }

        private void browseHelpCommand_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<HelpSubjectValue, Forms.General.HelpSubject>
                    (ScopeType.ApplicationHelp, data)
                { SelectedForm = (data) => new Forms.General.HelpSubject(data) },
                BusinessData.ApplicationData.HelpSubjects);
        }

        private void viewLibrarySourceCommand_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<LibrarySourceValue, Forms.Library.LibrarySource>
                    (ScopeType.Library, data)
                { SelectedForm = (data) => new Forms.Library.LibrarySource(data) },
                BusinessData.LibraryModel.LibrarySources);
        }

        private void viewLibraryMemberCommand_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<LibraryMemberValue, Forms.Library.LibraryMember>
                    (ScopeType.LibraryType, data)
                { SelectedForm = (data) => new Forms.Library.LibraryMember(data) },
                BusinessData.LibraryModel.LibraryMembers);
        }

        private void menuConstraintItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<ConstraintValue, Forms.Database.DbConstraint>
                    (ScopeType.DatabaseTableConstraint, data)
                { SelectedForm = (data) => new Forms.Database.DbConstraint(data) },
                BusinessData.DatabaseModel.DbConstraints);
        }

        private void menuConstraintColumnItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView(ScopeType.DatabaseTableColumn, data),
                BusinessData.DatabaseModel.DbConstraintColumns);
        }

        private void menuDataTypeItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<DomainValue, Forms.Database.DbDomain>
                    (ScopeType.DatabaseDomain, data)
                { SelectedForm = (data) => new Forms.Database.DbDomain(data) },
                BusinessData.DatabaseModel.DbDomains);
        }

        private void menuRoutineItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<RoutineValue, Forms.Database.DbRoutine>
                    (ScopeType.DatabaseProcedure, data)
                { SelectedForm = (data) => new Forms.Database.DbRoutine(data) },
                BusinessData.DatabaseModel.DbRoutines);
        }

        private void menuRoutineParameterItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<RoutineParameterValue, Forms.Database.DbRoutineParameter>
                    (ScopeType.DatabaseProcedureParameter, data)
                { SelectedForm = (data) => new Forms.Database.DbRoutineParameter(data) },
                BusinessData.DatabaseModel.DbRoutineParameters);
        }

        private void menuDependencyItem_Click(object sender, EventArgs e)
        {
            Activate((data) => 
                new Forms.DetailDataView(ScopeType.DatabaseDependency, data),
                BusinessData.DatabaseModel.DbReferences);
        }

        private void menuSchemaItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<SchemaValue, Forms.Database.DbSchema>
                    (ScopeType.DatabaseSchema, data)
                { SelectedForm = (data) => new Forms.Database.DbSchema(data) },
                BusinessData.DatabaseModel.DbSchemta);
        }

        private void menuTableItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<TableValue, Forms.Database.DbTable>
                    (ScopeType.DatabaseTable, data)
                { SelectedForm = (data) => new Forms.Database.DbTable(data) },
                BusinessData.DatabaseModel.DbTables);
        }

        private void menuColumnItem_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<TableColumnValue, Forms.Database.DbTableColumn>
                    (ScopeType.DatabaseTableColumn, data)
                { SelectedForm = (data) => new Forms.Database.DbTableColumn(data) },
                BusinessData.DatabaseModel.DbTableColumns);
        }

        private void menuPropertyItem_Click(object sender, EventArgs e)
        {
            Activate((data) => 
                new Forms.DetailDataView(ScopeType.DatabaseExtendedProperties, data), 
                BusinessData.DatabaseModel.DbExtendedProperties);
        }

        private void menuAttributeProperties_Click(object sender, EventArgs e)
        {
            Activate((data) => 
                new Forms.DetailDataView(ScopeType.ModelAttributeProperty, data), 
                BusinessData.DomainModel.Attributes.Properties);
        }

        private void menuAttributeAlaises_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView(ScopeType.ModelAttributeAlias, data),
                BusinessData.DomainModel.Attributes.Aliases);
        }

        private void menuAttributeDefinitions_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView(ScopeType.ModelAttributeDefinition, data),
                BusinessData.DomainModel.Attributes.Definitions);
        }

        private void menuEntities_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<EntityValue, Forms.Domain.DomainEntity>
                    (ScopeType.ModelEntity, data)
                { SelectedForm = (data) => new Forms.Domain.DomainEntity(data) },
                BusinessData.DomainModel.Entities);
        }

        private void menuEntityProperties_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView(ScopeType.ModelEntityProperty, data),
                BusinessData.DomainModel.Entities.Properties);
        }

        private void menuEntityDefinitions_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView(ScopeType.ModelEntityDefinition, data),
                BusinessData.DomainModel.Entities.Definitions);
        }

        private void menuEntityAlias_Click(object sender, EventArgs e)
        {
            Activate((data) => 
                new Forms.DetailDataView(ScopeType.ModelEntityAlias, data),
                BusinessData.DomainModel.Entities.Aliases);
        }

        private void menuEntityAttributes_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView(ScopeType.ModelEntityAttribute, data),
                BusinessData.DomainModel.Entities.Attributes);
        }

        private void menuModelProperty_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView(ScopeType.ModelProperty, data),
                BusinessData.DomainModel.Properties);
        }

        private void menuModelDefinition_Click(object sender, EventArgs e)
        {
            Activate((data) => 
                new Forms.DetailDataView(ScopeType.ModelDefinition, data), 
                BusinessData.DomainModel.Definitions);
        }

        private void manageScriptingCommand_ButtonClick(object sender, EventArgs e)
        { Activate(() => new Forms.Scripting.ScriptingTemplate(null)); }

        private void menuScriptingTemplates_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView<TemplateValue, Forms.Scripting.ScriptingTemplate>
                    (ScopeType.ScriptingTemplate, data)
                { SelectedForm = (data) => new Forms.Scripting.ScriptingTemplate(data) },
                BusinessData.ScriptingEngine.Templates);
        }

        private void menuScriptingPath_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView(ScopeType.ScriptingTemplatePath, data),
                BusinessData.ScriptingEngine.TemplatePaths);
        }

        private void menuScriptingDocument_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView(ScopeType.ScriptingTemplateDocument, data),
                BusinessData.ScriptingEngine.TemplateDocuments);
        }

        private void menuScriptingNode_Click(object sender, EventArgs e)
        {
            Activate((data) =>
                new Forms.DetailDataView(ScopeType.ScriptingTemplateNode, data), 
                BusinessData.ScriptingEngine.TemplateNodes);
        }

        private void menuScriptingAttribute_Click(object sender, EventArgs e)
        {
            Activate((data) => 
                new Forms.DetailDataView(ScopeType.ScriptingTemplateAttribute, data),
                BusinessData.ScriptingEngine.TemplateAttributes);
        }

        private void SecurityPrincipal_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Security.PrincipalManager()); }

        private void SecurityRole_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Security.RoleManager()); }
    }
}
