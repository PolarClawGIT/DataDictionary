using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData.SubjectArea;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main
{
    partial class Main
    {

        private void manageLibrariesCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Library.LibraryManager()); }

        private void manageDatabasesCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Database.CatalogManager()); }

        private void NewAttributeCommand_ButtonClick(object? sender, EventArgs e)
        {
            AttributeItem item = new AttributeItem();

            BusinessData.DomainModel.Attributes.Add(item);
            BusinessData.NameScope.Add(new NamedScopeItem(BusinessData.Model, item));

            if (contextNodes.FirstOrDefault(w => ReferenceEquals(w.Value, item)).Key is TreeNode node)
            { contextNameNavigation.SelectedNode = node; }

            Activate(item);
        }

        private void NewEntityCommand_ButtonClick(object? sender, EventArgs e)
        {
            DomainEntityItem item = new DomainEntityItem();

            BusinessData.DomainModel.Entities.Add(item);
            BusinessData.NameScope.Add(new NamedScopeItem(BusinessData.Model, item));

            if (contextNodes.FirstOrDefault(w => ReferenceEquals(w.Value, item)).Key is TreeNode node)
            { contextNameNavigation.SelectedNode = node; }

            Activate(item);
        }

        private void NewSubjectAreaCommand_ButtonClick(object? sender, EventArgs e)
        {
            ModelSubjectAreaItem item = new ModelSubjectAreaItem();

            BusinessData.ModelSubjectAreas.Add(item);
            BusinessData.NameScope.Add(new NamedScopeItem(BusinessData.Model, item));

            if (contextNodes.FirstOrDefault(w => ReferenceEquals(w.Value, item)).Key is TreeNode node)
            { contextNameNavigation.SelectedNode = node; }

            Activate(item);
        }

        private void ManageModelCommand_ButtonClick(object? sender, EventArgs e)
        { Activate(() => new Forms.Model.ModelManager()); }

        private void menuCatalogItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Database), BusinessData.DatabaseModel.DbCatalogs); }

        private void menuAttributes_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Attribute), BusinessData.DomainModel.Attributes); }

        private void subjectAreaToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Diagram), BusinessData.ModelSubjectAreas); }

        private void browseHelpCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_HelpTableOfContent), BusinessData.ApplicationData.HelpSubjects); }

        private void viewLibrarySourceCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Library), BusinessData.LibraryModel.LibrarySources); }

        private void viewLibraryMemberCommand_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Class), BusinessData.LibraryModel.LibraryMembers); }

        private void menuConstraintItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Key), BusinessData.DatabaseModel.DbConstraints); }

        private void menuConstraintColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_KeyColumn), BusinessData.DatabaseModel.DbConstraintColumns); }

        private void menuDataTypeItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_DomainType), BusinessData.DatabaseModel.DbDomains); }

        private void menuRoutineItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Procedure), BusinessData.DatabaseModel.DbRoutines); }

        private void menuRoutineParameterItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Parameter), BusinessData.DatabaseModel.DbRoutineParameters); }

        private void menuRoutineDependencyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Dependancy), BusinessData.DatabaseModel.DbRoutineDependencies); }

        private void menuSchemaItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Schema), BusinessData.DatabaseModel.DbSchemta); }

        private void menuTableItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Table), BusinessData.DatabaseModel.DbTables); }

        private void menuColumnItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Column), BusinessData.DatabaseModel.DbTableColumns); }

        private void menuPropertyItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_ExtendedProperty), BusinessData.DatabaseModel.DbExtendedProperties); }

        private void menuAttributeProperties_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Property), BusinessData.DomainModel.Attributes.Properties); }

        private void menuAttributeAlaises_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Synonym), BusinessData.DomainModel.Attributes.Aliases); }

        private void entitiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Entities), BusinessData.DomainModel.Entities); }

        private void entityPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Property), BusinessData.DomainModel.Entities.Properties); }

        private void entityAliasToolStripMenuItem_Click(object sender, EventArgs e)
        { Activate((data) => new Forms.DetailDataView(data, Resources.Icon_Synonym), BusinessData.DomainModel.Entities.Aliases); }

        private void browseTransforms_Click(object sender, EventArgs e)
        { throw new NotImplementedException(); }

        private void xmlViewerCommand_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Scripting.DataBuilder()); }
    }
}
