using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.Main.Properties;

namespace DataDictionary.Main
{
    partial class Main
    {

        #region domainModelNavigation
        Dictionary<TreeNode, Object> domainModelNodes = new Dictionary<TreeNode, Object>();
        enum domainModelImageIndex
        {
            Attribute,
            Attributes,
            Property,
            Alias,
            Entity
        }

        static Dictionary<domainModelImageIndex, (String imageKey, Image image)> domainModelImageItems = new Dictionary<domainModelImageIndex, (String imageKey, Image image)>()
        {
            {domainModelImageIndex.Attribute,    ("Attribute",   Resources.Attribute) },
            {domainModelImageIndex.Attributes,   ("Attributes",  Resources.Parameter) },
            {domainModelImageIndex.Property,     ("Property",    Resources.Property) },
            {domainModelImageIndex.Alias,        ("Alias",       Resources.Synonym) },
            {domainModelImageIndex.Entity,       ("Entity",      Resources.Relationship) },
        };


        void BuildDomainModelTreeByAttribute()
        {
            Object? selected = null;
            if (dataSourceNavigation.SelectedNode is not null && dbDataNodes.ContainsKey(dataSourceNavigation.SelectedNode))
            { selected = dbDataNodes[dataSourceNavigation.SelectedNode]; }

            domainModelNavigation.Nodes.Clear();
            domainModelNodes.Clear();

            foreach (IDomainAttributeItem attributeItem in
                Program.Data.DomainAttributes.
                OrderBy(o => o.AttributeTitle))
            {
                TreeNode attributeNode = CreateAttribute(attributeItem, null);
                domainModelNavigation.Nodes.Add(attributeNode);
            }

            if (domainModelNodes.FirstOrDefault(w => ReferenceEquals(w.Value, selected)) is KeyValuePair<TreeNode, object> selectedNode)
            { domainModelNavigation.SelectedNode = selectedNode.Key; }

            TreeNode CreateAttribute(IDomainAttributeItem attributeItem, TreeNode? parent)
            {
                TreeNode attributeNode = CreateNode(attributeItem.AttributeTitle, domainModelImageIndex.Attribute, attributeItem);
                DomainAttributeKey key = new DomainAttributeKey(attributeItem);

                List<DomainAttributePropertyItem> properties = Program.Data.DomainAttributeProperties.Where(w => key.Equals(w)).ToList();
                foreach (DomainAttributePropertyItem propertyItem in properties)
                {
                    String propertyTitle = String.Empty;
                    if (Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propertyItem.PropertyId) is PropertyItem property && property.PropertyTitle is not null)
                    { propertyTitle = property.PropertyTitle; }

                    CreateNode(propertyTitle, domainModelImageIndex.Property, propertyItem, attributeNode);
                }

                List<DomainAttributeAliasItem> alias = Program.Data.DomainAttributeAliases.Where(w => key.Equals(w)).ToList();
                foreach (DomainAttributeAliasItem aliasItem in alias)
                { CreateNode(aliasItem.ToString(), domainModelImageIndex.Alias, aliasItem, attributeNode); }

                // TODO: Children not yet supported
                //var children = Program.Data.DomainAttributes.Where(w => w.AttributeId == attributeItem.ParentAttributeId).ToList();
                //foreach (DomainAttributeItem childAttributeItem in Program.Data.DomainAttributes.Where(w => w.AttributeId == attributeItem.ParentAttributeId))
                //{ attributeNode.Nodes.Add(CreateAttribute(childAttributeItem, attributeNode)); }

                List<DomainEntityItem> entities = Program.Data.GetEntities(key).OrderBy(o => o.EntityTitle).ToList();
                foreach (DomainEntityItem item in entities)
                { CreateNode(item.EntityTitle, domainModelImageIndex.Entity, item, attributeNode); }

                if (parent is not null) { parent.Nodes.Add(attributeNode); }
                return attributeNode;
            }

            TreeNode CreateNode(String? nodeText, domainModelImageIndex imageIndex, Object? source = null, TreeNode? parentNode = null)
            {
                if (String.IsNullOrWhiteSpace(nodeText)) { throw new ArgumentNullException(nameof(nodeText)); }

                TreeNode result = new TreeNode(nodeText);
                result.ImageKey = domainModelImageItems[imageIndex].imageKey;
                result.SelectedImageKey = domainModelImageItems[imageIndex].imageKey;

                if (parentNode is not null) { parentNode.Nodes.Add(result); }
                if (source is not null) { domainModelNodes.Add(result, source); }

                return result;
            }
        }

        void BuildDomainModelTreeByEntity()
        {
            Object? selected = null;
            if (dataSourceNavigation.SelectedNode is not null && dbDataNodes.ContainsKey(dataSourceNavigation.SelectedNode))
            { selected = dbDataNodes[dataSourceNavigation.SelectedNode]; }

            domainModelNavigation.Nodes.Clear();
            domainModelNodes.Clear();

            foreach (IDomainEntityItem entityItem in
                Program.Data.DomainEntities.
                OrderBy(o => o.EntityTitle))
            {
                TreeNode entityNode = CreateEntity(entityItem, null);
                domainModelNavigation.Nodes.Add(entityNode);
            }

            if (domainModelNodes.FirstOrDefault(w => ReferenceEquals(w.Value, selected)) is KeyValuePair<TreeNode, object> selectedNode)
            { domainModelNavigation.SelectedNode = selectedNode.Key; }

            TreeNode CreateEntity(IDomainEntityItem entityItem, TreeNode? parent)
            {
                TreeNode entityNode = CreateNode(entityItem.EntityTitle, domainModelImageIndex.Entity, entityItem);
                DomainEntityKey key = new DomainEntityKey(entityItem);

                List<DomainEntityPropertyItem> properties = Program.Data.DomainEntityProperties.Where(w => key.Equals(w)).ToList();
                foreach (DomainEntityPropertyItem propertyItem in properties)
                {
                    String propertyTitle = String.Empty;
                    if (Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propertyItem.PropertyId) is PropertyItem property && property.PropertyTitle is not null)
                    { propertyTitle = property.PropertyTitle; }

                    CreateNode(propertyTitle, domainModelImageIndex.Property, propertyItem, entityNode);
                }

                List<DomainEntityAliasItem> alias = Program.Data.DomainEntityAliases.Where(w => key.Equals(w)).ToList();
                foreach (DomainEntityAliasItem aliasItem in alias)
                { CreateNode(aliasItem.ToString(), domainModelImageIndex.Alias, aliasItem, entityNode); }

                List<DomainAttributeItem> attributes = Program.Data.GetAttributes(key).OrderBy(o => o.AttributeTitle).ToList();
                foreach (DomainAttributeItem item in attributes)
                { CreateNode(item.AttributeTitle, domainModelImageIndex.Attribute, item, entityNode); }

                if (parent is not null) { parent.Nodes.Add(entityNode); }
                return entityNode;
            }

            TreeNode CreateNode(String? nodeText, domainModelImageIndex imageIndex, Object? source = null, TreeNode? parentNode = null)
            {
                if (String.IsNullOrWhiteSpace(nodeText)) { throw new ArgumentNullException(nameof(nodeText)); }

                TreeNode result = new TreeNode(nodeText);
                result.ImageKey = domainModelImageItems[imageIndex].imageKey;
                result.SelectedImageKey = domainModelImageItems[imageIndex].imageKey;

                if (parentNode is not null) { parentNode.Nodes.Add(result); }
                if (source is not null) { domainModelNodes.Add(result, source); }

                return result;
            }
        }

        private void domainModelNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (domainModelNavigation.SelectedNode is TreeNode node && domainModelNodes.ContainsKey(node))
            {
                Object dataNode = domainModelNodes[node];

                if (dataNode is IDomainAttributeItem attributeItem)
                { Activate((data) => new Forms.Domain.DomainAttribute() { DataKey = new DomainAttributeKey(attributeItem) }, attributeItem); }

                if (dataNode is IDomainEntityItem entityItem)
                { Activate((data) => new Forms.Domain.DomainEntity() { DataKey = new DomainEntityKey(entityItem) }, entityItem); }
            }
        }
        private void sortByAttributeEntityCommand_Click(object sender, EventArgs e)
        {
            sortByAttributeEntityCommand.Checked = true;
            sortByEntityAttributeCommand.Checked = false;
            BuildDomainModelTreeByAttribute();
        }

        private void sortByEntityAttributeCommand_Click(object sender, EventArgs e)
        {
            sortByAttributeEntityCommand.Checked = false;
            sortByEntityAttributeCommand.Checked = true;
            BuildDomainModelTreeByEntity();
        }
        #endregion
    }
}
