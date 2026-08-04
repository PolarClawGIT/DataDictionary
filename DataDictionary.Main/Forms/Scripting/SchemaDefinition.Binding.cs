using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDefinition
    {
        class FormBinding : PresenterData<SchemaDefinitionIndex>
        {
            public Func<ITemplateData> GetData { get; private set; } = () => BusinessData.Templates;
            public Action<XmlBuilderValue> OnSchemaChanged { get; init; } = (value) => { return; };

            public DataBinding<TemplateValue> TemplateData { get; }
            public DataBinding<SchemaDefinitionValue> SchemaData { get; }
            public DataBinding<XmlBuilderValue> BuilderData { get; }
            BindingView<SchemaNodeValue> schemaNodes;
            XmlBuilderData nodeValues = new XmlBuilderData();

            public FormBinding(
                BindingSource templateBinding,
                BindingSource schemaBinding,
                BindingSource nodeBinding) : base()
            {

                TemplateData = new DataBinding<TemplateValue>(templateBinding, GetData);
                SchemaData = new DataBinding<SchemaDefinitionValue>(schemaBinding, () => GetData().Schemata);
                schemaNodes = new BindingView<SchemaNodeValue>(GetData().SchemataNodes);
                BuilderData = new DataBinding<XmlBuilderValue>(nodeBinding, () => nodeValues);

                GetLocked = TemplateData.GetLocked;
                GetAuthorization = () => TemplateData.GetAuthorization(BusinessData.Authorization);

                schemaNodes.ListChanged += SchemaNodes_ListChanged;

                void SchemaNodes_ListChanged(Object? sender, ListChangedEventArgs e)
                {
                    // Reset list
                    foreach (var builder in BuilderData)
                    {
                        XmlBuilderIndex key = new XmlBuilderIndex(builder);
                        SchemaNodeValue? node = schemaNodes.FirstOrDefault(w => key.Equals(w));

                        if (builder.SchemaNode is null && node is not null)
                        { builder.SchemaNode = node; }
                        else if (builder.SchemaNode is not null && node is null)
                        { builder.SchemaNode = null; }

                        OnSchemaChanged(builder);
                    }
                }
            }

            public Boolean TrySetNode(XmlBuilderIndex key)
            { return BuilderData.TrySetValue(w => key.Equals(w)); }

            public override void LoadValue(SchemaDefinitionIndex key)
            {
                SchemaData.LoadBinding(w => key.Equals(w));
                schemaNodes = new BindingView<SchemaNodeValue>(GetData().SchemataNodes, w => key.Equals(w));

                if (SchemaData.TryGetValue(out SchemaDefinitionValue? schemaValue))
                { TemplateData.LoadBinding(w => new TemplateIndex(schemaValue).Equals(w)); }
                else
                { TemplateData.LoadBinding(w => new TemplateIndex().Equals(w)); }

                nodeValues.Load(key, GetData().SchemataNodes);
                BuilderData.LoadBinding();
            }

            protected void RemoveValue(SchemaDefinitionIndex key)
            {
                GetData().Schemata.Remove(key);
                GetData().SchemataNodes.Remove(key);
                GetData().SchemaDocuments.Remove(key);
                throw new NotImplementedException();
            }

            public IEnumerable<XmlBuilder> GetBuilders()
            { return nodeValues.Select(s => s.Builder); }
        }

        class TreeBinding
        {
            TreeView treeControl;

            Dictionary<TreeNode, XmlBuilderIndex> treeValues = new Dictionary<TreeNode, XmlBuilderIndex>();
            List<XmlBuilderIndex> expandedIndexes = new List<XmlBuilderIndex>();

            public TreeBinding(TreeView tree)
            {
                treeControl = tree;

                treeControl.ImageList = new ImageList();
                treeControl.ImageList.AddImages(Enum.GetValues<ScopeType>().ToList());
                treeControl.ImageList.AddImages(Enum.GetValues<ObjectValueType>().ToList());
            }

            public Boolean TryGetValue(TreeNode node, [NotNullWhen(true)] out XmlBuilderIndex? key)
            {
                if (treeValues.TryGetValue(node, out XmlBuilderIndex? value))
                { key = value; return true; }
                else { key = null; return false; }
            }

            public Boolean TryGetNode(XmlBuilderIndex key, [NotNullWhen(true)] out TreeNode? node)
            {
                var value = treeValues.Where(w => key.Equals(w.Value)).ToList();

                if (value.Count == 0)
                { node = null; return false; }
                else
                { node = value.First().Key; return true; }
            }

            public Boolean TryGetSelected([NotNullWhen(true)] out TreeNode? node)
            {
                node = treeControl.SelectedNode;
                return node is not null;
            }

            public Boolean TryGetSelected([NotNullWhen(true)] out XmlBuilderIndex? key)
            {
                key = null;
                return treeControl.SelectedNode is not null && treeValues.TryGetValue(treeControl.SelectedNode, out key);
            }

            public void LoadTree(IEnumerable<XmlBuilder> builders)
            {
                BeginUpdate();
                BuildNodes(builders);
                EndUpdate();


                void BeginUpdate()
                {
                    // Lock Tree
                    treeControl.Enabled = false;
                    treeControl.UseWaitCursor = true;
                    treeControl.BeginUpdate();

                    //Save Expended State
                    expandedIndexes.Clear();
                    expandedIndexes.AddRange(treeValues.Where(w =>
                                (w.Key.IsExpanded && treeValues.Any(a => w.Value.Equals(a.Value))
                                || (w.Key.Nodes.Count == 0
                                    && w.Key.Parent is not null
                                    && w.Key.Parent.IsExpanded)
                                    && treeValues.Any(a => w.Value.Equals(a.Value)))).
                                    Select(s => s.Value). // Get the NamedScope Index
                                    Distinct());
                }

                void BuildNodes(IEnumerable<XmlBuilder> builders)
                {
                    ClearNodes(treeControl.Nodes);

                    foreach (XmlBuilder item in builders.
                            Where(w => !builders.Any(a => a.BuilderPath.Equals(w.BuilderPath.ParentPath))).
                            OrderBy(o => o.RenderOrder).
                            ThenBy(o => o.BuilderPath))
                    {
                        treeControl.Invoke(() =>
                        {
                            TreeNode node = CreateNode(item);
                            treeControl.Nodes.Add(node);
                            BuildChildren(node, item.BuilderPath);
                        });
                    }
                }

                void ClearNodes(TreeNodeCollection nodes)
                {
                    while (nodes.Count > 0)
                    {
                        TreeNode item = nodes[0];
                        ClearNodes(item.Nodes);
                        item.Remove();
                    }
                }

                void BuildChildren(TreeNode parentNode, XmlBuilderIndex key)
                {
                    foreach (XmlBuilder item in builders.
                        Where(w => key.Equals(w.BuilderPath.ParentPath)).
                        OrderBy(o => o.RenderOrder).
                        ThenBy(o => o.BuilderPath))
                    {
                        TreeNode childNode = CreateNode(item);
                        parentNode.Nodes.Add(childNode);

                        BuildChildren(childNode, item.BuilderPath);
                    }
                }

                TreeNode CreateNode(XmlBuilder item)
                {
                    TreeNode node = new TreeNode(item.BuilderPath.Member);
                    node.ToolTipText = item.BuilderPath.MemberFullPath;

                    XmlBuilderIndex key = new XmlBuilderIndex(item);

                    if (String.IsNullOrEmpty(item.ObjectProperty))
                    {
                        node.ImageKey = item.ObjectScope.GetName();
                        node.SelectedImageKey = item.ObjectScope.GetName();
                    }
                    else
                    {
                        if (item.ObjectType.TryGetImage(out Image? _))
                        {
                            node.ImageKey = item.ObjectType.GetName();
                            node.SelectedImageKey = item.ObjectType.GetName();
                        }
                        else
                        {
                            node.ImageKey = ObjectValueType.Null.GetName();
                            node.SelectedImageKey = ObjectValueType.Null.GetName();
                        }
                    }

                    if (!treeValues.ContainsValue(key))
                    { treeValues.Add(node, key); }

                    return node;
                }

                void EndUpdate()
                {
                    // Restore Expanded State
                    foreach (var item in expandedIndexes.
                        Join(treeValues.
                            Where(w => !w.Key.IsExpanded),
                            index => index,
                            node => node.Value,
                        (index, node) => node.Key))
                    { item.ExpandParent(); }

                    // Unlock Tree
                    treeControl.EndUpdate();
                    treeControl.UseWaitCursor = false;
                    treeControl.Enabled = true;
                }
            }
        }
    }
}
