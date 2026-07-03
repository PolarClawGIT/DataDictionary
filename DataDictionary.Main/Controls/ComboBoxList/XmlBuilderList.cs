using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using System.ComponentModel;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record class XmlBuilderList
    {
        public XmlBuilder ValueMember { get; init; }
        public String DisplayMember { get { return ValueMember.BuilderPath.MemberFullPath; } }

        XmlBuilderList(XmlBuilder builder) : base()
        { ValueMember = builder; }

        public static void Load(ComboBoxData control, IEnumerable<XmlBuilder> builders)
        {
            control.ValueMember = nameof(ValueMember);
            control.DisplayMember = nameof(DisplayMember);
            control.DataSource = BuildList(builders);
        }

        public static void Load(TreeView control, IEnumerable<XmlBuilder> builders)
        {
            control.BeginUpdate();

            foreach (XmlBuilder item in builders.
                Where(w => !builders.Any(a => a.BuilderPath.Equals(w.BuilderPath.ParentPath))).
                OrderBy(o => o.BuilderPath))
            {
                TreeNode node = new TreeNode(item.BuilderPath.Member);
                node.Tag = item;
                control.Nodes.Add(node);

                BuildChildren(node, item.BuilderPath);
            }

            control.EndUpdate();

            void BuildChildren(TreeNode parentNode, PathIndex key)
            {
                foreach (XmlBuilder item in builders.Where(w => key.Equals(w.BuilderPath.ParentPath)).OrderBy(o => o.BuilderPath))
                {
                    TreeNode childNode = new TreeNode(item.BuilderPath.Member);
                    childNode.Tag = item;
                    parentNode.Nodes.Add(childNode);

                    BuildChildren(childNode, item.BuilderPath);
                }
            }
        }

        static BindingList<XmlBuilderList> BuildList(IEnumerable<XmlBuilder> builders)
        {
            BindingList<XmlBuilderList> result = new BindingList<XmlBuilderList>();

            foreach (XmlBuilder item in builders.OrderBy(o => o.BuilderPath))
            { result.Add(new XmlBuilderList(item)); }

            return result;
        }
    }
}
