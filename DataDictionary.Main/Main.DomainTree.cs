using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.DomainData.SubjectArea;
using DataDictionary.Main.Properties;
using System.Collections;
using System.ComponentModel;

namespace DataDictionary.Main
{
    partial class Main
    {
        Dictionary<TreeNode, Object> domainModelNodes = new Dictionary<TreeNode, Object>();
        enum domainModelImageIndex
        {
            SubjectArea,
            Attribute,
            Entity,
            Property,
            Alias
        }

        static Dictionary<domainModelImageIndex, (String imageKey, Image image)> domainModelImageItems = new Dictionary<domainModelImageIndex, (String imageKey, Image image)>()
        {
            {domainModelImageIndex.SubjectArea,  ("SubjectArea", Resources.Diagram) },
            {domainModelImageIndex.Attribute,    ("Attribute",   Resources.Attribute) },
            {domainModelImageIndex.Entity,       ("Entity",      Resources.Relationship) },
            {domainModelImageIndex.Property,     ("Property",    Resources.Property) },
            {domainModelImageIndex.Alias,        ("Alias",       Resources.Synonym) },
        };

        void ClearDomainModelTree()
        {
            foreach (KeyValuePair<TreeNode, object> item in domainModelNodes)
            {
                if (item.Value is INotifyPropertyChanged notifyPropertyChanged)
                { notifyPropertyChanged.PropertyChanged -= DomainItem_PropertyChanged; }
            }

            domainModelNavigation.Nodes.Clear();
            domainModelNodes.Clear();
        }

        void BuildDomainModelTreeByAttribute()
        {
            domainModelNavigation.TreeViewNodeSorter = new DomainTreeSort();

            foreach (DomainAttributeItem attributeItem in
                Program.Data.DomainAttributes.
                Where(w => w.SubjectAreaId is null))
            { TreeNode attributeNode = CreateAttribute(domainModelNavigation.Nodes, attributeItem); }

            foreach (DomainSubjectAreaItem subjectItem in
                Program.Data.DomainSubjectAreas.
                OrderBy(o => o.SubjectAreaTitle))
            {
                TreeNode subjectNode = CreateNode(domainModelNavigation.Nodes, subjectItem.SubjectAreaTitle, domainModelImageIndex.SubjectArea, subjectItem);
                DomainSubjectAreaKey subjectKey = new DomainSubjectAreaKey(subjectItem);
                domainModelNavigation.Nodes.Add(subjectNode);

                foreach (DomainAttributeItem attributeItem in
                    Program.Data.DomainAttributes.
                    Where(w => subjectKey.Equals(w)))
                { TreeNode attributeNode = CreateAttribute(subjectNode.Nodes, attributeItem); }
            }

            domainModelNavigation.Sort();

            TreeNode CreateAttribute(TreeNodeCollection target, DomainAttributeItem attributeItem)
            {
                TreeNode attributeNode = CreateNode(target, attributeItem.AttributeTitle, domainModelImageIndex.Attribute, attributeItem);

                DomainAttributeKey key = new DomainAttributeKey(attributeItem);

                List<DomainAttributePropertyItem> properties = Program.Data.DomainAttributeProperties.Where(w => key.Equals(w)).ToList();
                foreach (DomainAttributePropertyItem propertyItem in properties)
                {
                    String propertyTitle = String.Empty;
                    if (Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propertyItem.PropertyId) is PropertyItem property && property.PropertyTitle is not null)
                    { propertyTitle = property.PropertyTitle; }

                    CreateNode(attributeNode.Nodes, propertyTitle, domainModelImageIndex.Property, propertyItem);
                }

                List<DomainEntityItem> entities = Program.Data.GetEntities(key).OrderBy(o => o.EntityTitle).ToList();
                foreach (DomainEntityItem item in entities)
                { CreateNode(attributeNode.Nodes, item.EntityTitle, domainModelImageIndex.Entity, item); }

                return attributeNode;
            }
        }

        void BuildDomainModelTreeByEntity()
        {
            domainModelNavigation.TreeViewNodeSorter = new DomainTreeSort();

            foreach (DomainEntityItem entityItem in
                Program.Data.DomainEntities.
                Where(w => w.SubjectAreaId is null))
            { TreeNode entityNode = CreateEntity(domainModelNavigation.Nodes, entityItem); }

            foreach (DomainSubjectAreaItem subjectItem in
                Program.Data.DomainSubjectAreas)
            {
                DomainSubjectAreaKey subjectKey = new DomainSubjectAreaKey(subjectItem);
                TreeNode subjectNode = CreateNode(domainModelNavigation.Nodes, subjectItem.SubjectAreaTitle, domainModelImageIndex.SubjectArea, subjectItem);

                foreach (DomainEntityItem entityItem in
                    Program.Data.DomainEntities.
                    Where(w => subjectKey.Equals(w)).
                    OrderBy(o => o.EntityTitle))
                { CreateEntity(subjectNode.Nodes, entityItem); }
            }

            domainModelNavigation.Sort();

            TreeNode CreateEntity(TreeNodeCollection target, IDomainEntityItem entityItem)
            {
                TreeNode entityNode = CreateNode(target, entityItem.EntityTitle, domainModelImageIndex.Entity, entityItem);
                DomainEntityKey key = new DomainEntityKey(entityItem);

                List<DomainEntityPropertyItem> properties = Program.Data.DomainEntityProperties.Where(w => key.Equals(w)).ToList();
                foreach (DomainEntityPropertyItem propertyItem in properties)
                {
                    String propertyTitle = String.Empty;
                    if (Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propertyItem.PropertyId) is PropertyItem property && property.PropertyTitle is not null)
                    { propertyTitle = property.PropertyTitle; }

                    CreateNode(entityNode.Nodes, propertyTitle, domainModelImageIndex.Property, propertyItem);
                }

                List<DomainEntityAliasItem> alias = Program.Data.DomainEntityAliases.Where(w => key.Equals(w)).ToList();
                foreach (DomainEntityAliasItem aliasItem in alias)
                { CreateNode(entityNode.Nodes, aliasItem.ToString(), domainModelImageIndex.Alias); }

                List<DomainAttributeItem> attributes = Program.Data.GetAttributes(key).ToList();
                foreach (DomainAttributeItem item in attributes)
                { CreateNode(entityNode.Nodes, item.AttributeTitle, domainModelImageIndex.Attribute, item); }

                return entityNode;
            }

        }

        TreeNode CreateNode(TreeNodeCollection target, String? nodeText, domainModelImageIndex imageIndex, Object? source = null)
        {
            if (String.IsNullOrWhiteSpace(nodeText)) { throw new ArgumentNullException(nameof(nodeText)); }
            TreeNode result = new TreeNode(nodeText);

            result.ImageKey = domainModelImageItems[imageIndex].imageKey;
            result.SelectedImageKey = domainModelImageItems[imageIndex].imageKey;

            if (source is not null) { domainModelNodes.Add(result, source); }
            if (source is INotifyPropertyChanged notifyPropertyChanged)
            { notifyPropertyChanged.PropertyChanged += DomainItem_PropertyChanged; }

            target.Add(result);

            return result;
        }

        private void domainModelNavigation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (domainModelNavigation.SelectedNode is TreeNode node && domainModelNodes.ContainsKey(node))
            {
                dynamic dataNode = domainModelNodes[node];
                Activate(dataNode);
            }
        }

        void DomainItem_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (domainModelNodes.FirstOrDefault(w => w.Value == sender) is KeyValuePair<TreeNode, object> value)
            {
                Object item = value.Value;
                TreeNode node = value.Key;

                if (NameOfTitle(item).Equals(e.PropertyName))
                {
                    node.Text = GetTitle(item);
                    domainModelNavigation.Sort();
                    domainModelNavigation.SelectedNode = node;
                }
            }

            String? GetTitle(Object item)
            { // Overloaded local functions are not supported. This is a work-around.
                if (item is DomainAttributeItem attribute)
                { return attribute.AttributeTitle; }

                else if (item is DomainEntityItem entity)
                { return entity.EntityTitle; }

                else if (item is DomainSubjectAreaItem subject)
                { return subject.SubjectAreaTitle; }

                else return String.Empty;
            }

            String NameOfTitle(Object item)
            { // Overloaded local functions are not supported. This is a work-around.
                if (item is DomainAttributeItem attribute)
                { return nameof(attribute.AttributeTitle); }

                else if (item is DomainEntityItem entity)
                { return nameof(entity.EntityTitle); }

                else if (item is DomainSubjectAreaItem subject)
                { return nameof(subject.SubjectAreaTitle); }

                else return String.Empty;
            }
        }

        private void sortByAttributeEntityCommand_Click(object sender, EventArgs e)
        {
            sortByAttributeEntityCommand.Checked = true;
            sortByEntityAttributeCommand.Checked = false;

            ClearDomainModelTree();
            BuildDomainModelTreeByAttribute();
        }

        private void sortByEntityAttributeCommand_Click(object sender, EventArgs e)
        {
            sortByAttributeEntityCommand.Checked = false;
            sortByEntityAttributeCommand.Checked = true;

            ClearDomainModelTree();
            BuildDomainModelTreeByEntity();
        }

        private void domainModelRefreshCommand_Click(object sender, EventArgs e)
        {
            if (sortByAttributeEntityCommand.Checked) { sortByAttributeEntityCommand_Click(sender, e); }
            else if (sortByEntityAttributeCommand.Checked) { sortByEntityAttributeCommand_Click(sender, e); }
            else { return; }
        }

        void Activate(DomainAttributeItem attributeItem)
        { Activate((data) => new Forms.Domain.DomainAttribute() { DataKey = new DomainAttributeKey(attributeItem) }, attributeItem); }

        void Activate(DomainEntityItem entityItem)
        { Activate((data) => new Forms.Domain.DomainEntity() { DataKey = new DomainEntityKey(entityItem) }, entityItem); }

        void Activate(DomainSubjectAreaItem subjectItem)
        { Activate((data) => new Forms.Domain.DomainSubjectArea() { DataKey = new DomainSubjectAreaKey(subjectItem) }, subjectItem); }

        /// <summary>
        /// Used by Tree Control to do sorting
        /// </summary>
        class DomainTreeSort : IComparer
        {
            public int Compare(object? x, object? y)
            {
                if (x is TreeNode firstNode
                    && y is TreeNode lastNode
                    && domainModelImageItems.FirstOrDefault(w => firstNode.ImageKey.Equals(w.Value.imageKey)).Value.imageKey is String firstKey
                    && domainModelImageItems.FirstOrDefault(w => lastNode.ImageKey.Equals(w.Value.imageKey)).Value.imageKey is String lastKey
                    && Enum.TryParse(firstKey, out domainModelImageIndex firstImageIndex)
                    && Enum.TryParse(lastKey, out domainModelImageIndex lastImageIndex)
                    )
                {
                    Int32 imageCompare = firstImageIndex.CompareTo(lastImageIndex);

                    if (imageCompare == 0) { return String.Compare(firstNode.Text, lastNode.Text); }
                    else { return imageCompare; }
                }
                else { return -1; }
            }
        }
    }
}
