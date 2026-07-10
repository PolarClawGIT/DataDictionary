using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using DataDictionary.Main.Properties;
using System.Xml.Schema;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record class XmlBuilderList
    {
        public XmlBuilderIndex ValueMember { get; init; }
        public String DisplayMember { get; init; }

        //TODO: Replace with Type specific icons.
        //static readonly String fieldImageName = "Field";
        //static readonly Image fieldImage = Resources.Icon_Field.GetSmallImage();

        XmlBuilderList(XmlBuilder builder) : base()
        {
            ValueMember = builder.BuilderPath;
            DisplayMember = builder.BuilderPath.MemberFullPath;
        }

        public static void Load(ComboBoxData control, IEnumerable<XmlBuilder> builders)
        {
            control.ValueMember = nameof(ValueMember);
            control.DisplayMember = nameof(DisplayMember);
            control.DataSource = BuildList(builders);
        }

        static BindingList<XmlBuilderList> BuildList(IEnumerable<XmlBuilder> builders)
        {
            BindingList<XmlBuilderList> result = new BindingList<XmlBuilderList>();

            foreach (XmlBuilder item in builders.OrderBy(o => o.BuilderPath))
            { result.Add(new XmlBuilderList(item)); }

            return result;
        }

        public static void Load(TreeView control, IEnumerable<XmlBuilder> builders)
        {
            control.BeginUpdate();
            control.ImageList =  new ImageList();
            control.ImageList.AddImages(Enum.GetValues<ScopeType>().ToList());
            control.ImageList.AddImages(Enum.GetValues<ObjectValueType>().ToList());

            foreach (XmlBuilder item in builders.
                Where(w => !builders.Any(a => a.BuilderPath.Equals(w.BuilderPath.ParentPath))).
                OrderBy(o => o.RenderOrder).
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
                    OrderBy(o => o.RenderOrder).
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
            node.Tag = item.BuilderPath;

            if (String.IsNullOrEmpty(item.ObjectProperty))
            {
                node.ImageKey = item.ObjectScope.GetName();
                node.SelectedImageKey = item.ObjectScope.GetName();
            }
            else
            {
                if(item.ObjectType.TryGetImage(out Image? _))
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
           
            return node;
        }


    }
}
