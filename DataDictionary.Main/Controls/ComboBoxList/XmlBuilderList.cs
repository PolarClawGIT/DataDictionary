using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using DataDictionary.Main.Properties;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record class XmlBuilderList
    {
        public XmlBuilder ValueMember { get; init; }
        public String DisplayMember { get { return ValueMember.BuilderPath.MemberFullPath; } }
        static readonly String fieldImageName = "Field";
        static readonly Image fieldImage = Resources.Icon_Field.GetSmallImage();

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
            control.ImageList =  new ImageList();
            control.ImageList.AddImages(Enum.GetValues<ScopeType>().ToList());
            control.ImageList.Images.Add(fieldImageName, fieldImage);

            foreach (XmlBuilder item in builders.
                Where(w => !builders.Any(a => a.BuilderPath.Equals(w.BuilderPath.ParentPath))).
                OrderBy(o => o.NodeOrder).
                ThenBy(o => o.BuilderPath))
            {
                TreeNode node = CreateNode(item);
                control.Nodes.Add(node);

                BuildChildren(node, item.BuilderPath);
            }

            control.EndUpdate();

            void BuildChildren(TreeNode parentNode, PathIndex key)
            {
                foreach (XmlBuilder item in builders.
                    Where(w => key.Equals(w.BuilderPath.ParentPath)).
                    OrderBy(o => o.NodeOrder).
                    ThenBy(o => o.BuilderPath))
                {
                    TreeNode childNode = CreateNode(item);
                    parentNode.Nodes.Add(childNode);

                    BuildChildren(childNode, item.BuilderPath);
                }
            }
        }

        private static TreeNode CreateNode(XmlBuilder item)
        {
            TreeNode node = new TreeNode(item.BuilderPath.Member);
            node.Tag = item;

            //TODO: Consider using type to drive the icon?
            // Would need a new set of Icons for each major type. String, Number, Enum, Guid
            if (String.IsNullOrEmpty(item.ObjectProperty))
            {
                node.ImageKey = item.ObjectScope.GetEnumeration().Name;
                node.SelectedImageKey = item.ObjectScope.GetEnumeration().Name;
            }
            else
            {
                node.ImageKey = fieldImageName;
                node.SelectedImageKey = fieldImageName;
            }
           
            return node;
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
