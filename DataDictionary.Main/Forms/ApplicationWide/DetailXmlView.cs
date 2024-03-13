using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Main.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DataDictionary.Main.Forms.ApplicationWide
{
    partial class DetailXmlView : ApplicationBase, IApplicationDataForm
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

        public DetailXmlView() : base()
        {
            InitializeComponent();

            ImageList scopeImages = new ImageList();
            foreach (ScopeType item in Enum.GetValues(typeof(ScopeType)))
            { scopeImages.Images.Add(item.ToScopeName(), item.ToImage()); }
            itemSelection.ImageList = scopeImages;
        }

        private void DetailXmlView_Load(object sender, EventArgs e)
        {

            // Load up Tree
            foreach (DomainAttributeItem attributeItem in BusinessData.DomainModel.Attributes)
            {
                DomainAttributeKey attributeKey = new DomainAttributeKey(attributeItem);
                TreeNode attributeNode = new TreeNode(attributeItem.AttributeTitle);
                attributeNode.ImageKey = ScopeType.ModelAttribute.ToScopeName();
                attributeNode.SelectedImageKey = ScopeType.ModelAttribute.ToString();
                itemSelection.Nodes.Add(attributeNode);
                itemSelectorValues.Add(attributeNode, attributeItem.ToXElement);


                foreach (DomainAttributePropertyItem propertyItem in BusinessData.DomainModel.Attributes.Properties.Where(w => attributeKey.Equals(w)))
                {
                    PropertyKey propertyKey = new PropertyKey(propertyItem);

                    if (BusinessData.ApplicationData.Properties.FirstOrDefault(w => propertyKey.Equals(w)) is PropertyItem property)
                    {
                        TreeNode propertyNode = new TreeNode(property.PropertyTitle);
                        propertyNode.ImageKey = ScopeType.ModelAttributeProperty.ToScopeName();
                        propertyNode.SelectedImageKey = ScopeType.ModelAttributeProperty.ToScopeName();
                        attributeNode.Nodes.Add(propertyNode);

                        itemSelectorValues.Add(propertyNode, () => {
                            XElement value = propertyItem.ToXElement();
                            value.Add(property.ToXElement());

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
    }
}
