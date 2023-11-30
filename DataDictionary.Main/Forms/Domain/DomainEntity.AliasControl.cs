using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainEntity
    {
        Dictionary<ListViewItem, Object> alaisDataItems = new Dictionary<ListViewItem, Object>();
        Stack<Object> alaisDataParentItems = new Stack<Object>();

        Dictionary<ScopeType, (String imageKey, Image image)> alaisDataImages = new Dictionary<ScopeType, (String imageKey, Image image)>()
        {
            {ScopeType.Database, (ScopeType.Database.ToScopeName(), Resources.Database ) },
            {ScopeType.DatabaseSchema, (ScopeType.DatabaseSchema.ToScopeName(), Resources.Schema ) },
            {ScopeType.DatabaseSchemaTable, (ScopeType.DatabaseSchemaTable.ToScopeName(), Resources.Table ) },
            {ScopeType.DatabaseSchemaView, (ScopeType.DatabaseSchemaView.ToScopeName(), Resources.View ) },

        };

        private void AlaisDataSetup()
        {
            ImageList alaisImages = new ImageList();
            foreach (var icon in alaisDataImages.Values)
            { alaisImages.Images.Add(icon.imageKey, icon.image); }
            alaisData.SmallImageList = alaisImages;

            alaisData.View = View.Details;
            alaisData.HeaderStyle = ColumnHeaderStyle.None;
            alaisData.Columns.Add("Alias", alaisData.Width - SystemInformation.VerticalScrollBarWidth);

            foreach (DbCatalogItem item in Program.Data.DbCatalogs.OrderBy(o => o.AliasName()))
            {

                ListViewItem viewItem = new ListViewItem();
                viewItem.Text = item.AliasName();
                viewItem.ImageKey = alaisDataImages[item.ToScopeType()].imageKey;

                alaisData.Items.Add(viewItem);
                alaisDataItems.Add(viewItem, item);
            }
        }

        private void alaisData_ItemActivate(object sender, EventArgs e)
        {
            if (alaisData.SelectedItems.Count > 0)
            {
                Object item = alaisDataItems[alaisData.SelectedItems[0]];

                if(alaisDataParentItems.TryPeek(out Object? top) && top is DbCatalogItem && top == item)
                {
                    alaisDataParentItems.Pop();
                    alaisData.Items.Clear();
                    alaisDataItems.Clear();

                    foreach (DbCatalogItem catalog in Program.Data.DbCatalogs.OrderBy(o => o.AliasName()))
                    {

                        ListViewItem viewItem = new ListViewItem();
                        viewItem.Text = catalog.AliasName();
                        viewItem.ImageKey = alaisDataImages[catalog.ToScopeType()].imageKey;

                        alaisData.Items.Add(viewItem);
                        alaisDataItems.Add(viewItem, catalog);
                    }
                }

                else if (item is DbCatalogItem catalogItem)
                {
                    alaisDataParentItems.Push(item);
                    alaisData.Items.Clear();
                    alaisDataItems.Clear();

                    ListViewItem viewItem = new ListViewItem();
                    viewItem.Text = catalogItem.AliasName();
                    viewItem.ImageKey = alaisDataImages[catalogItem.ToScopeType()].imageKey;

                    alaisData.Items.Add(viewItem);
                    alaisDataItems.Add(viewItem, catalogItem);

                    foreach (DbSchemaItem schemaItem in Program.Data.DbSchemta.OrderBy(o => o.AliasName()))
                    {
                        viewItem = new ListViewItem();
                        viewItem.Text = schemaItem.AliasName();
                        viewItem.ImageKey = alaisDataImages[schemaItem.ToScopeType()].imageKey;

                        alaisData.Items.Add(viewItem);
                        alaisDataItems.Add(viewItem, schemaItem);
                    }
                }
            }

        }
    }
}
