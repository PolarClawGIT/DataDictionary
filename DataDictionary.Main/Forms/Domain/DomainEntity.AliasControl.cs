using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Domain
{
    partial class DomainEntity
    {
        Dictionary<ListViewItem, ModelAliasKey> alaisDataItems = new Dictionary<ListViewItem, ModelAliasKey>();
        Stack<ModelAliasKey> alaisDataParentItems = new Stack<ModelAliasKey>();

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
            foreach ((string imageKey, Image image) icon in alaisDataImages.Values)
            { alaisImages.Images.Add(icon.imageKey, icon.image); }
            alaisData.SmallImageList = alaisImages;

            alaisData.View = View.Details;
            alaisData.HeaderStyle = ColumnHeaderStyle.None;
            alaisData.Columns.Add("Alias", alaisData.Width - SystemInformation.VerticalScrollBarWidth);

            AliasDataAddItems();
        }

        private void AliasDataAddItems()
        {
            alaisDataItems.Clear();
            alaisData.Items.Clear();

            if (alaisDataParentItems.TryPeek(out ModelAliasKey? parentKey) && parentKey is ModelAliasKey)
            {
                ModelAliasItem parent = Program.Data.ModelAlias[parentKey];
                ListViewItem parentItem = new ListViewItem(parent.AliasName, alaisDataImages[parent.ScopeId].imageKey);
                alaisDataItems.Add(parentItem, parentKey);
                alaisData.Items.Add(parentItem);

                foreach (var item in Program.Data.ModelAlias.Where(w => w.Key.SystemParentId == parentKey.SystemId))
                {
                    ListViewItem childItem = new ListViewItem(
                        item.Value.AliasName,
                        alaisDataImages[item.Value.ScopeId].imageKey);
                    alaisDataItems.Add(childItem, item.Key);
                    alaisData.Items.Add(childItem);
                }
            }
            else
            {
                foreach (var item in Program.Data.ModelAlias.Where(w => w.Key.SystemParentId is null))
                {
                    ListViewItem childItem = new ListViewItem(
                        item.Value.AliasName,
                        alaisDataImages[item.Value.ScopeId].imageKey);
                    alaisDataItems.Add(childItem, item.Key);
                    alaisData.Items.Add(childItem);
                }
            }
        }

        private void alaisData_ItemActivate(object sender, EventArgs e)
        {
            if (alaisData.SelectedItems.Count > 0
               && alaisDataItems.ContainsKey(alaisData.SelectedItems[0]))
            {
                ModelAliasKey selectedKey = alaisDataItems[alaisData.SelectedItems[0]];

                if (alaisDataParentItems.TryPeek(out ModelAliasKey? parentKey)
                    && selectedKey.Equals(parentKey))
                { alaisDataParentItems.Pop(); }
                else { alaisDataParentItems.Push(selectedKey); }
            }

            AliasDataAddItems();
        }
    }
}
