using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.Main.Messages;
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
using Toolbox.Mediator;

namespace DataDictionary.Main.Control
{
    [Obsolete()]
    public partial class MainNavigation : UserControl, IColleague
    {
        enum DbSchmeaTreeNodeType
        {
            Schema,
            Table,
            Tables,
            Column,
            Columns
        }

        readonly Dictionary<DbSchmeaTreeNodeType, Image> dbSchmeaTreeImages = new Dictionary<DbSchmeaTreeNodeType, Image>()
        {
            { DbSchmeaTreeNodeType.Schema, Resources.Schema },
            { DbSchmeaTreeNodeType.Table, Resources.Table },
            { DbSchmeaTreeNodeType.Tables, Resources.TableGroup },
            { DbSchmeaTreeNodeType.Column, Resources.Column },
            { DbSchmeaTreeNodeType.Columns, Resources.ColumnGroup },
        };

        readonly Dictionary<TreeNode, Object> dbSchemaTreeObjects = new Dictionary<TreeNode, Object>();

        public MainNavigation()
        {
            InitializeComponent();
        }

        private void MainNavigation_Load(object sender, EventArgs e)
        {
            Program.Messenger.AddColleague(this);

            // Add Image Index to Db Schema Tree
            if (dbSchmeaTreeData.ImageList is ImageList) { dbSchmeaTreeData.ImageList.Images.Clear(); }
            else { dbSchmeaTreeData.ImageList = new ImageList(); }

            foreach (DbSchmeaTreeNodeType item in Enum.GetValues(typeof(DbSchmeaTreeNodeType)))
            { dbSchmeaTreeData.ImageList.Images.Add(item.ToString(), dbSchmeaTreeImages[item]); }
        }

/*        public void Bind(DataRepository data)
        {
            // Unbind data as needed
            serverNameData.DataBindings.Clear();
            databaseNameData.DataBindings.Clear();
            dbSchmeaTreeData.Nodes.Clear();
            dbSchemaTreeObjects.Clear();


            // Bind the data
            serverNameData.DataBindings.Add(new Binding(nameof(serverNameData.Text), data, nameof(data.ServerName)));
            databaseNameData.DataBindings.Add(new Binding(nameof(databaseNameData.Text), data, nameof(data.DatabaseName)));


            // Build Db Schema Tree Nodes
            foreach (IDbSchemaItem schemaItem in data.DbSchema.OrderBy(o => o.SchemaName))
            {
                TreeNode schemaNode = new TreeNode(schemaItem.SchemaName);
                schemaNode.ImageKey = DbSchmeaTreeNodeType.Schema.ToString();
                schemaNode.SelectedImageKey = DbSchmeaTreeNodeType.Schema.ToString();
                dbSchemaTreeObjects.Add(schemaNode, schemaItem);
                dbSchmeaTreeData.Nodes.Add(schemaNode);

                TreeNode? tableGroupNode = null;

                foreach (IDbTableItem tableItem in data.DbTable.
                    Where(w =>
                        w.CatalogName == schemaItem.CatalogName &&
                        w.SchemaName == schemaItem.SchemaName).
                    OrderBy(o => o.TableName))
                {
                    if (tableGroupNode is null)
                    {
                        tableGroupNode = new TreeNode("Tables");
                        tableGroupNode.ImageKey = DbSchmeaTreeNodeType.Tables.ToString();
                        tableGroupNode.SelectedImageKey = DbSchmeaTreeNodeType.Tables.ToString();
                        schemaNode.Nodes.Add(tableGroupNode);
                    }

                    TreeNode tableNode = new TreeNode(tableItem.TableName);
                    tableNode.ImageKey = DbSchmeaTreeNodeType.Table.ToString();
                    tableNode.SelectedImageKey = DbSchmeaTreeNodeType.Table.ToString();
                    dbSchemaTreeObjects.Add(tableNode, tableItem);
                    tableGroupNode.Nodes.Add(tableNode);

                    foreach (IDbColumnItem columnItem in data.DbColumn.
                        Where(w => w.CatalogName == tableItem.CatalogName &&
                            w.SchemaName == tableItem.SchemaName &&
                            w.TableName == tableItem.TableName).
                        OrderBy(o => o.OrdinalPosition))
                    {
                        TreeNode columnNode = new TreeNode(columnItem.ColumnName);
                        columnNode.ImageKey = DbSchmeaTreeNodeType.Column.ToString();
                        columnNode.SelectedImageKey = DbSchmeaTreeNodeType.Column.ToString();
                        dbSchemaTreeObjects.Add(columnNode, columnItem);
                        tableNode.Nodes.Add(columnNode);
                    }
                }
            }
        }*/

        private void dbSchmeaTreeData_DoubleClick(object sender, EventArgs e)
        {
            if (dbSchemaTreeObjects.ContainsKey(dbSchmeaTreeData.SelectedNode))
            {
                Object item = dbSchemaTreeObjects[dbSchmeaTreeData.SelectedNode];

                if (item is IDbSchemaItem schemaItem)
                { ChildFormOpening(new Forms.DbSchema(schemaItem)); }

                if (item is IDbTableItem tableItem)
                { ChildFormOpening(new Forms.DbTable(tableItem)); }

                if (item is IDbColumnItem columnItem)
                { ChildFormOpening(new Forms.DbColumn(columnItem)); }
            }
        }

        private void ChildFormOpening(Form child)
        {
            child.FormClosed += Child_FormClosed;
            SendMessage(new FormOpenMessage() { FormOpened = child });
            child.Show();

            void Child_FormClosed(object? sender, FormClosedEventArgs e)
            {
                child.FormClosed += Child_FormClosed;
            }
        }

        #region IColleague
        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public void RecieveMessage(object? sender, MessageEventArgs message)
        {
            if (message is FormOpenMessage openMessage) { }

        }
        void SendMessage(MessageEventArgs message)
        {
            if (OnSendMessage is EventHandler<MessageEventArgs> handler)
            { handler(this, message); }
        }
        #endregion

    }

}

