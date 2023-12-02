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


        }

        private void alaisData_ItemActivate(object sender, EventArgs e)
        {

        }
    }
}
