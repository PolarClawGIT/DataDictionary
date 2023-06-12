using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.DbMetaData;
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
        public MainNavigation()
        {
            InitializeComponent();
        }

        private void MainNavigation_Load(object sender, EventArgs e)
        {

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

            foreach (IDbSchemaItem schemaItem in data.DbSchema.OrderBy(o => o.SchemaName))
            {
                TreeNode schemaNode = new TreeNode(schemaItem.SchemaName);
                dbSchmeaTreeData.Nodes.Add(schemaNode);

                foreach (IDbTableItem tableItem in data.DbTable.
                    Where(w =>
                        w.CatalogName == schemaItem.CatalogName &&
                        w.SchemaName == schemaItem.SchemaName).
                    OrderBy(o => o.TableName))
                {
                    TreeNode tableNode = new TreeNode(tableItem.TableName);
                    schemaNode.Nodes.Add(tableNode);

                    foreach (IDbColumnItem columnItem in data.DbColumn.
                        Where(w => w.CatalogName == tableItem.CatalogName &&
                            w.SchemaName == tableItem.SchemaName &&
                            w.TableName == tableItem.TableName).
                        OrderBy(o => o.OrdinalPosition))
                    {
                        TreeNode columnNode = new TreeNode(columnItem.ColumnName);
                        tableNode.Nodes.Add(columnNode);
                    }
                }
            }
        }
    }
}
