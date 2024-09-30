using DataDictionary.Main.Enumerations;
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

        private void menuCatalogItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.Database, data), BusinessData.DatabaseModel.DbCatalogs); }

        private void menuAttributes_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelAttribute, data), BusinessData.DomainModel.Attributes); }

        private void subjectAreaToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelSubjectArea, data), BusinessData.SubjectAreas); }

        private void browseHelpCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ApplicationHelp, data), BusinessData.ApplicationData.HelpSubjects); }

        private void viewLibrarySourceCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.Library, data), BusinessData.LibraryModel.LibrarySources); }

        private void viewLibraryMemberCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.LibraryType, data), BusinessData.LibraryModel.LibraryMembers); }

        private void menuConstraintItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.DatabaseTableConstraint, data), BusinessData.DatabaseModel.DbConstraints); }

        private void menuConstraintColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.DatabaseTableColumn, data), BusinessData.DatabaseModel.DbConstraintColumns); }

        private void menuDataTypeItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.DatabaseDomain, data), BusinessData.DatabaseModel.DbDomains); }

        private void menuRoutineItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.DatabaseProcedure, data), BusinessData.DatabaseModel.DbRoutines); }

        private void menuRoutineParameterItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.DatabaseProcedureParameter, data), BusinessData.DatabaseModel.DbRoutineParameters); }

        private void menuDependencyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.DatabaseDependency , data), BusinessData.DatabaseModel.DbReferences); }

        private void menuSchemaItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.DatabaseSchema, data), BusinessData.DatabaseModel.DbSchemta); }

        private void menuTableItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.DatabaseTable, data), BusinessData.DatabaseModel.DbTables); }

        private void menuColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.DatabaseTableColumn, data), BusinessData.DatabaseModel.DbTableColumns); }

        private void menuPropertyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.DatabaseExtendedProperties, data), BusinessData.DatabaseModel.DbExtendedProperties); }

        private void menuAttributeProperties_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelAttributeProperty, data), BusinessData.DomainModel.Attributes.Properties); }

        private void menuAttributeAlaises_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelAttributeAlias, data), BusinessData.DomainModel.Attributes.Aliases); }

        private void menuAttributeDefinitions_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelAttributeDefinition, data), BusinessData.DomainModel.Attributes.Definitions); }

        private void menuEntities_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelEntity, data), BusinessData.DomainModel.Entities); }

        private void menuEntityProperties_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelEntityProperty, data), BusinessData.DomainModel.Entities.Properties); }

        private void menuEntityDefinitions_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelEntityDefinition, data), BusinessData.DomainModel.Entities.Definitions); }

        private void menuEntityAlias_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelEntityAlias, data), BusinessData.DomainModel.Entities.Aliases); }

        private void menuEntityAttributes_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelEntityAttribute, data), BusinessData.DomainModel.Entities.Attributes); }

        private void menuModelProperty_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelProperty, data), BusinessData.DomainModel.Properties); }

        private void menuModelDefinition_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ModelDefinition, data), BusinessData.DomainModel.Definitions); }

        private void manageScriptingCommand_ButtonClick(object sender, EventArgs e)
        { Activate(() => new Forms.Scripting.ScriptingTemplate(null)); }

        private void menuScriptingTemplates_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ScriptingTemplate, data), BusinessData.ScriptingEngine.Templates); }

        private void menuScriptingPath_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ScriptingTemplatePath, data), BusinessData.ScriptingEngine.TemplatePaths); }

        private void menuScriptingDocument_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ScriptingTemplateDocument, data), BusinessData.ScriptingEngine.TemplateDocuments); }

        private void menuScriptingNode_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ScriptingTemplateNode, data), BusinessData.ScriptingEngine.TemplateNodes); }

        private void menuScriptingAttribute_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(ScopeType.ScriptingTemplateAttribute, data), BusinessData.ScriptingEngine.TemplateAttributes); }

    }
}
