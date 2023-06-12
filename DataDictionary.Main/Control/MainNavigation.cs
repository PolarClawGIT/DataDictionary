using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Control
{
    public partial class MainNavigation : UserControl
    {
        enum DbSchmeaTreeNodeType
        {
            Schema,
            Table,
            Column,
            ColumnKey
        }

        readonly Dictionary<DbSchmeaTreeNodeType, Image> dbSchmeaTreeImages = new Dictionary<DbSchmeaTreeNodeType, Image>()
        {
            { DbSchmeaTreeNodeType.Schema, Resources.Schema },
            { DbSchmeaTreeNodeType.Table, Resources.Table },
            { DbSchmeaTreeNodeType.Column, Resources.Column },
        };

        public MainNavigation()
        {
            InitializeComponent();
        }

        private void MainNavigation_Load(object sender, EventArgs e)
        {
            // Add Image Index to Db Schema Tree
            if (dbSchmeaTreeData.ImageList is ImageList) { dbSchmeaTreeData.ImageList.Images.Clear(); }
            else { dbSchmeaTreeData.ImageList = new ImageList(); }

            foreach (DbSchmeaTreeNodeType item in Enum.GetValues(typeof(DbSchmeaTreeNodeType)))
            { dbSchmeaTreeData.ImageList.Images.Add(item.ToString(), dbSchmeaTreeImages[item]); }
        }

        public void Bind(DataRepository data)
        {
            // Unbind data as needed
            serverNameData.DataBindings.Clear();
            databaseNameData.DataBindings.Clear();
            dbSchmeaTreeData.Nodes.Clear();


            // Bind the data
            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), data, nameof(data.ServerName)));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), data, nameof(data.DatabaseName)));


            // Build Db Schema Tree Nodes
            foreach (IDbSchemaItem schemaItem in data.DbSchema.OrderBy(o => o.SchemaName))
            {
                TreeNode schemaNode = new TreeNode(schemaItem.SchemaName);
                schemaNode.ImageKey = DbSchmeaTreeNodeType.Schema.ToString() ;
                dbSchmeaTreeData.Nodes.Add(schemaNode);

                foreach (IDbTableItem tableItem in data.DbTable.
                    Where(w =>
                        w.CatalogName == schemaItem.CatalogName &&
                        w.SchemaName == schemaItem.SchemaName).
                    OrderBy(o => o.TableName))
                {
                    TreeNode tableNode = new TreeNode(tableItem.TableName);
                    tableNode.ImageKey = DbSchmeaTreeNodeType.Table.ToString();
                    schemaNode.Nodes.Add(tableNode);

                    foreach (IDbColumnItem columnItem in data.DbColumn.
                        Where(w => w.CatalogName == tableItem.CatalogName &&
                            w.SchemaName == tableItem.SchemaName &&
                            w.TableName == tableItem.TableName).
                        OrderBy(o => o.OrdinalPosition))
                    {
                        TreeNode columnNode = new TreeNode(columnItem.ColumnName);
                        columnNode.ImageKey = DbSchmeaTreeNodeType.Column.ToString();
                        tableNode.Nodes.Add(columnNode);
                    }
                }
            }
        }
    }
}
