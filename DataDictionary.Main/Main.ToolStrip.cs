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
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.Database)), BusinessData.DatabaseModel.DbCatalogs); }

        private void menuAttributes_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ModelAttribute)), BusinessData.DomainModel.Attributes); }

        private void subjectAreaToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ModelSubjectArea)), BusinessData.SubjectAreas); }

        private void browseHelpCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ApplicationHelp)), BusinessData.ApplicationData.HelpSubjects); }

        private void viewLibrarySourceCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.Library)), BusinessData.LibraryModel.LibrarySources); }

        private void viewLibraryMemberCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.LibraryType)), BusinessData.LibraryModel.LibraryMembers); }

        private void menuConstraintItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.DatabaseTableConstraint)), BusinessData.DatabaseModel.DbConstraints); }

        private void menuConstraintColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.DatabaseTableColumn)), BusinessData.DatabaseModel.DbConstraintColumns); }

        private void menuDataTypeItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.DatabaseDomain)), BusinessData.DatabaseModel.DbDomains); }

        private void menuRoutineItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.DatabaseProcedure)), BusinessData.DatabaseModel.DbRoutines); }

        private void menuRoutineParameterItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.DatabaseProcedureParameter)), BusinessData.DatabaseModel.DbRoutineParameters); }

        private void menuRoutineDependencyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.DatabaseDependency)), BusinessData.DatabaseModel.DbRoutineDependencies); }

        private void menuSchemaItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.DatabaseSchema)), BusinessData.DatabaseModel.DbSchemta); }

        private void menuTableItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.DatabaseTable)), BusinessData.DatabaseModel.DbTables); }

        private void menuColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.DatabaseTableColumn)), BusinessData.DatabaseModel.DbTableColumns); }

        private void menuPropertyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.DatabaseExtendedProperties)), BusinessData.DatabaseModel.DbExtendedProperties); }

        private void menuAttributeProperties_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ModelAttributeProperty)), BusinessData.DomainModel.Attributes.Properties); }

        private void menuAttributeAlaises_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ModelAttributeAlias)), BusinessData.DomainModel.Attributes.Aliases); }

        private void menuAttributeDefinitions_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ModelAttributeDefinition)), BusinessData.DomainModel.Attributes.Definitions); }

        private void menuEntities_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ModelEntity)), BusinessData.DomainModel.Entities); }

        private void menuEntityProperties_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ModelEntityProperty)), BusinessData.DomainModel.Entities.Properties); }

        private void menuEntityDefinitions_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ModelEntityDefinition)), BusinessData.DomainModel.Entities.Definitions); }

        private void menuEntityAlias_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ModelEntityAlias)), BusinessData.DomainModel.Entities.Aliases); }

        private void menuModelProperty_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ModelProperty)), BusinessData.DomainModel.Properties); }

        private void menuModelDefinition_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ModelDefinition)), BusinessData.DomainModel.Definitions); }

        private void manageScriptingCommand_ButtonClick(object sender, EventArgs e)
        { Activate(() => new Forms.Scripting.ScriptingTemplate(null)); }

        private void menuScriptingTemplates_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ScriptingTemplate)), BusinessData.ScriptingEngine.Templates); }

        private void menuScriptingPath_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ScriptingTemplatePath)), BusinessData.ScriptingEngine.TemplatePaths); }

        private void menuScriptingDocument_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ScriptingTemplateDocument)), BusinessData.ScriptingEngine.TemplateDocuments); }

        private void menuScriptingNode_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ScriptingTemplateNode)), BusinessData.ScriptingEngine.TemplateNodes); }

        private void menuScriptingAttribute_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, ImageEnumeration.GetIcon(ScopeType.ScriptingTemplateAttribute)), BusinessData.ScriptingEngine.TemplateAttributes); }
    }
}
