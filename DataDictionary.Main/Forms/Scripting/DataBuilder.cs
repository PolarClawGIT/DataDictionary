using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Main.Controls;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class DataBuilder : ApplicationData, IApplicationDataForm
    {
        Dictionary<TreeNode, Func<XElement>> itemSelectorValues = new Dictionary<TreeNode, Func<XElement>>();

        
        class FormData : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;

            public virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }
        }

        public DataBuilder() : base()
        {
            InitializeComponent();

            ImageList scopeImages = new ImageList();
            foreach (ScopeType item in Enum.GetValues(typeof(ScopeType)))
            { scopeImages.Images.Add(item.ToName(), item.ToImage()); }
            itemSelection.ImageList = scopeImages;

        }

        private void DetailXmlView_Load(object sender, EventArgs e)
        {
            BuildTree();
        }

        void BuildTree()
        {

            // Load up Tree
            foreach (AttributeValue attributeItem in BusinessData.DomainModel.Attributes)
            {
                AttributeIndex attributeKey = new AttributeIndex(attributeItem);
                TreeNode attributeNode = new TreeNode(attributeItem.AttributeTitle);
                attributeNode.ImageKey = attributeItem.Scope.ToName();
                attributeNode.SelectedImageKey = attributeItem.Scope.ToName();
                itemSelection.Nodes.Add(attributeNode);
                //itemSelectorValues.Add(attributeNode, () => attributeItem.GetXElement());

                foreach (AttributePropertyValue propertyItem in BusinessData.DomainModel.Attributes.Properties.Where(w => attributeKey.Equals(w)))
                {
                    PropertyIndex propertyKey = new PropertyIndex(propertyItem);

                    if (BusinessData.ApplicationData.Properties.FirstOrDefault(w => propertyKey.Equals(w)) is PropertyValue property)
                    {
                        TreeNode propertyNode = new TreeNode(property.PropertyTitle);
                        propertyNode.ImageKey = ScopeType.ModelAttributeProperty.ToName();
                        propertyNode.SelectedImageKey = ScopeType.ModelAttributeProperty.ToName();
                        attributeNode.Nodes.Add(propertyNode);

                        itemSelectorValues.Add(propertyNode, () =>
                        {
                            //XElement value = propertyItem.GetXElement();
                            XElement value = new XElement("Dummy");
                            //value.Add(property.GetXElement());

                            return value;
                        });
                    }
                }
            }
        }

        private void itemSelection_AfterCheck(object sender, TreeViewEventArgs e)
        {
            XElement root = new XElement("Root");
            doWork(itemSelection.Nodes, root);
            xmlData.Text = root.ToString();

            void doWork(TreeNodeCollection nodes, XElement parent)
            {
                foreach (TreeNode node in nodes)
                {
                    if (itemSelectorValues.ContainsKey(node) && node.Checked)
                    {
                        XElement value = itemSelectorValues[node]();
                        parent.Add(value);

                        if (node.Nodes.Count > 0)
                        { doWork(node.Nodes, value); }
                    }
                }
            }
        }

        private void itemSelection_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bindingSchema.DataSource = null;

        }
    }
}
