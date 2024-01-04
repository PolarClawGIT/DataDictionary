using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.ApplicationData.Model.SubjectArea;
using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.Main.Controls;
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
            {domainModelImageIndex.Entity,       ("Entity",      Resources.Entity) },
            {domainModelImageIndex.Property,     ("Property",    Resources.Property) },
            {domainModelImageIndex.Alias,        ("Alias",       Resources.Synonym) },
        };

        void SetImages(TreeView tree, IEnumerable<(String imageKey, Image image)> images)
        {
            if (tree.ImageList is null)
            { tree.ImageList = new ImageList(); }

            foreach ((string imageKey, Image image) image in images.Where(w => !tree.ImageList.Images.ContainsKey(w.imageKey)))
            { tree.ImageList.Images.Add(image.imageKey, image.image); }
        }

        List<Object> expandedDomanNode = new List<object>();
        void ClearDomainModelTree()
        {
            expandedDomanNode.Clear();
            expandedDomanNode.AddRange(dbDataNodes.Where(w => w.Key.IsExpanded).Select(s => s.Value));

            foreach (KeyValuePair<TreeNode, object> item in domainModelNodes)
            {
                if (item.Value is INotifyPropertyChanged notifyPropertyChanged)
                { notifyPropertyChanged.PropertyChanged -= DomainItem_PropertyChanged; }
            }

            Program.Data.DomainAttributes.ListChanged -= BindingTable_ListChanged;
            Program.Data.DomainEntities.ListChanged -= BindingTable_ListChanged;
            Program.Data.ModelSubjectAreas.ListChanged -= BindingTable_ListChanged;

            domainModelNavigation.Nodes.Clear();
            domainModelNodes.Clear();
        }

        void BuildDomainModelTreeByAttribute()
        {
            dataSourceNavigation.BeginUpdate();
            domainModelNavigation.TreeViewNodeSorter = new DomainTreeSort();

            foreach (DomainAttributeItem attributeItem in
                Program.Data.DomainAttributes.
                Where(w => w.SubjectAreaId is null))
            { TreeNode attributeNode = CreateTreeNode(domainModelNavigation.Nodes, attributeItem); }

            foreach (ModelSubjectAreaItem subjectItem in
                Program.Data.ModelSubjectAreas.
                OrderBy(o => o.SubjectAreaTitle))
            {
                TreeNode subjectNode = CreateTreeNode(domainModelNavigation.Nodes, domainModelImageIndex.SubjectArea, subjectItem.SubjectAreaTitle, subjectItem);
                ModelSubjectAreaKey subjectKey = new ModelSubjectAreaKey(subjectItem);

                foreach (DomainAttributeItem attributeItem in
                    Program.Data.DomainAttributes.
                    Where(w => subjectKey.Equals(w)))
                { TreeNode attributeNode = CreateTreeNode(subjectNode.Nodes, attributeItem); }

                foreach (DomainEntityItem entityItem in
                    Program.Data.DomainEntities.
                    Where(w =>
                    {
                        DomainEntityKey entityKey = new DomainEntityKey(w);
                        return subjectKey.Equals(w)
                        && domainModelNodes.Count(c => entityKey.Equals(c.Value)) == 0;
                    }))
                { CreateTreeNode(subjectNode.Nodes, entityItem); }
            }

            foreach (DomainEntityItem entityItem in
                Program.Data.DomainEntities.
                Where(w =>
                {
                    DomainEntityKey entityKey = new DomainEntityKey(w);
                    return w.SubjectAreaId is null
                    && domainModelNodes.Count(c => entityKey.Equals(c.Value)) == 0;
                }))
            { CreateTreeNode(domainModelNavigation.Nodes, entityItem); }

            Program.Data.DomainAttributes.ListChanged += BindingTable_ListChanged;
            Program.Data.DomainEntities.ListChanged += BindingTable_ListChanged;
            Program.Data.ModelSubjectAreas.ListChanged += BindingTable_ListChanged;

            domainModelNavigation.Sort();

            foreach (TreeNode item in dbDataNodes.Where(w => expandedDomanNode.Contains(w.Value)).Select(s => s.Key).ToList())
            { item.ExpandParent(); }

            dataSourceNavigation.EndUpdate();
        }

        void BuildDomainModelTreeByEntity()
        {
            dataSourceNavigation.BeginUpdate();
            domainModelNavigation.TreeViewNodeSorter = new DomainTreeSort();

            foreach (DomainEntityItem entityItem in
                Program.Data.DomainEntities.
                Where(w => w.SubjectAreaId is null))
            { TreeNode entityNode = CreateTreeNode(domainModelNavigation.Nodes, entityItem); }

            foreach (ModelSubjectAreaItem subjectItem in
                Program.Data.ModelSubjectAreas)
            {
                ModelSubjectAreaKey subjectKey = new ModelSubjectAreaKey(subjectItem);
                TreeNode subjectNode = CreateTreeNode(domainModelNavigation.Nodes, domainModelImageIndex.SubjectArea, subjectItem.SubjectAreaTitle, subjectItem);

                foreach (DomainEntityItem entityItem in
                    Program.Data.DomainEntities.
                    Where(w => subjectKey.Equals(w)).
                    OrderBy(o => o.EntityTitle))
                { CreateTreeNode(subjectNode.Nodes, entityItem); }

                foreach (DomainAttributeItem attributeItem in
                        Program.Data.DomainAttributes.
                        Where(w =>
                        {
                            DomainAttributeKey attributeKey = new DomainAttributeKey(w);
                            return subjectKey.Equals(w)
                            && domainModelNodes.Count(c => attributeKey.Equals(c.Value)) == 0;
                        }))
                { CreateTreeNode(subjectNode.Nodes, attributeItem); }
            }

            foreach (DomainAttributeItem attributeItem in
                    Program.Data.DomainAttributes.
                    Where(w =>
                    {
                        DomainAttributeKey attributeKey = new DomainAttributeKey(w);
                        return w.SubjectAreaId is null
                        && domainModelNodes.Count(c => attributeKey.Equals(c.Value)) == 0;
                    }))
            { CreateTreeNode(domainModelNavigation.Nodes, attributeItem); }

            Program.Data.DomainAttributes.ListChanged += BindingTable_ListChanged;
            Program.Data.DomainEntities.ListChanged += BindingTable_ListChanged;
            Program.Data.ModelSubjectAreas.ListChanged += BindingTable_ListChanged;

            domainModelNavigation.Sort();

            foreach (TreeNode item in dbDataNodes.Where(w => expandedDomanNode.Contains(w.Value)).Select(s => s.Key).ToList())
            { item.ExpandParent(); }

            dataSourceNavigation.EndUpdate();
        }

        /// <summary>
        /// Creates Tree Nodes for Attributes
        /// </summary>
        /// <param name="target"></param>
        /// <param name="attributeItem"></param>
        /// <returns></returns>
        TreeNode CreateTreeNode(TreeNodeCollection target, DomainAttributeItem attributeItem)
        {
            TreeNode attributeNode = CreateTreeNode(target, domainModelImageIndex.Attribute, attributeItem.AttributeTitle, attributeItem);

            DomainAttributeKey key = new DomainAttributeKey(attributeItem);

            List<DomainAttributePropertyItem> properties = Program.Data.DomainAttributeProperties.Where(w => key.Equals(w)).ToList();
            foreach (DomainAttributePropertyItem propertyItem in properties)
            {
                String propertyTitle = String.Empty;
                if (Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propertyItem.PropertyId) is PropertyItem property && property.PropertyTitle is not null)
                { propertyTitle = property.PropertyTitle; }

                CreateTreeNode(attributeNode.Nodes, domainModelImageIndex.Property, propertyTitle, propertyItem);
            }

            List<DomainEntityItem> entities = Program.Data.GetEntities(key).ToList();
            foreach (DomainEntityItem item in entities)
            { CreateTreeNode(attributeNode.Nodes, domainModelImageIndex.Entity, item.EntityTitle, item); }

            return attributeNode;
        }

        /// <summary>
        /// Create Tree Nodes for Entities
        /// </summary>
        /// <param name="target"></param>
        /// <param name="entityItem"></param>
        /// <returns></returns>
        TreeNode CreateTreeNode(TreeNodeCollection target, IDomainEntityItem entityItem)
        {
            TreeNode entityNode = CreateTreeNode(target, domainModelImageIndex.Entity, entityItem.EntityTitle, entityItem);
            DomainEntityKey key = new DomainEntityKey(entityItem);

            List<DomainEntityPropertyItem> properties = Program.Data.DomainEntityProperties.Where(w => key.Equals(w)).ToList();
            foreach (DomainEntityPropertyItem propertyItem in properties)
            {
                String propertyTitle = String.Empty;
                if (Program.Data.Properties.FirstOrDefault(w => w.PropertyId == propertyItem.PropertyId) is PropertyItem property && property.PropertyTitle is not null)
                { propertyTitle = property.PropertyTitle; }

                CreateTreeNode(entityNode.Nodes, domainModelImageIndex.Property, propertyTitle, propertyItem);
            }

            List<DomainAttributeItem> attributes = Program.Data.GetAttributes(key).ToList();
            foreach (DomainAttributeItem item in attributes)
            { CreateTreeNode(entityNode.Nodes, domainModelImageIndex.Attribute, item.AttributeTitle, item); }

            return entityNode;
        }

        /// <summary>
        /// Base CreateTreeNode for the Domain Tree.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="imageIndex"></param>
        /// <param name="nodeText"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        TreeNode CreateTreeNode(TreeNodeCollection target, domainModelImageIndex imageIndex, String? nodeText, Object? source = null)
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

        private void BindingTable_ListChanged(object? sender, ListChangedEventArgs e)
        {
            ClearDomainModelTree();

            if (sortByAttributeEntityCommand.Checked) { BuildDomainModelTreeByAttribute(); }
            else if (sortByEntityAttributeCommand.Checked) { BuildDomainModelTreeByEntity(); }
            else { return; }
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
            if (sender is null) { sender = domainModelNavigation; }

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

                if (item is IModelSubjectAreaKey subject && nameof(subject.SubjectAreaId).Equals(e.PropertyName))
                { domainModelRefreshCommand_Click(sender, EventArgs.Empty); }
            }

            String? GetTitle(Object item)
            { // Overloaded local functions are not supported. This is a work-around.
                if (item is DomainAttributeItem attribute)
                { return attribute.AttributeTitle; }

                else if (item is DomainEntityItem entity)
                { return entity.EntityTitle; }

                else if (item is ModelSubjectAreaItem subject)
                { return subject.SubjectAreaTitle; }

                else return String.Empty;
            }

            String NameOfTitle(Object item)
            { // Overloaded local functions are not supported. This is a work-around.
                if (item is DomainAttributeItem attribute)
                { return nameof(attribute.AttributeTitle); }

                else if (item is DomainEntityItem entity)
                { return nameof(entity.EntityTitle); }

                else if (item is ModelSubjectAreaItem subject)
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
            ClearDomainModelTree();

            if (sortByAttributeEntityCommand.Checked) { BuildDomainModelTreeByAttribute(); }
            else if (sortByEntityAttributeCommand.Checked) { BuildDomainModelTreeByEntity(); }
            else { return; }
        }

        private void newAttributeCommand_ButtonClick(object sender, EventArgs e)
        {
            DomainAttributeItem item = new DomainAttributeItem();
            Program.Data.DomainAttributes.Add(item);

            if (domainModelNavigation.SelectedNode is not null
                && domainModelNodes.ContainsKey(domainModelNavigation.SelectedNode)
                && domainModelNodes[domainModelNavigation.SelectedNode] is ModelSubjectAreaItem subject
                && subject.SubjectAreaId is Guid subjectId)
            {
                item.SubjectAreaId = subjectId;
                TreeNode newNode = CreateTreeNode(domainModelNavigation.SelectedNode.Nodes, domainModelImageIndex.Attribute, item.AttributeTitle, item);
                domainModelNavigation.SelectedNode = newNode;
            }
            else
            {
                TreeNode newNode = CreateTreeNode(domainModelNavigation.Nodes, domainModelImageIndex.Attribute, item.AttributeTitle, item);
                domainModelNavigation.SelectedNode = newNode;
            }

            Activate(item);
        }

        private void newEntityCommand_ButtonClick(object sender, EventArgs e)
        {
            DomainEntityItem item = new DomainEntityItem();
            Program.Data.DomainEntities.Add(item);

            if (domainModelNavigation.SelectedNode is not null
                && domainModelNodes.ContainsKey(domainModelNavigation.SelectedNode)
                && domainModelNodes[domainModelNavigation.SelectedNode] is ModelSubjectAreaItem subject
                && subject.SubjectAreaId is Guid subjectId)
            {
                item.SubjectAreaId = subjectId;
                TreeNode newNode = CreateTreeNode(domainModelNavigation.SelectedNode.Nodes, domainModelImageIndex.Entity, item.EntityTitle, item);
                domainModelNavigation.SelectedNode = newNode;
            }
            else
            {
                TreeNode newNode = CreateTreeNode(domainModelNavigation.Nodes, domainModelImageIndex.Entity, item.EntityTitle, item);
                domainModelNavigation.SelectedNode = newNode;
            }

            Activate(item);
        }

        private void newSubjectAreaCommand_ButtonClick(object sender, EventArgs e)
        {
            ModelSubjectAreaItem item = new ModelSubjectAreaItem();
            Program.Data.ModelSubjectAreas.Add(item);

            TreeNode newNode = CreateTreeNode(domainModelNavigation.Nodes, domainModelImageIndex.SubjectArea, item.SubjectAreaTitle, item);
            domainModelNavigation.SelectedNode = newNode;

            Activate(item);
        }

        void Activate(DomainAttributeItem attributeItem)
        { Activate((data) => new Forms.Domain.DomainAttribute() { DataKey = new DomainAttributeKey(attributeItem) }, attributeItem); }

        void Activate(DomainEntityItem entityItem)
        { Activate((data) => new Forms.Domain.DomainEntity() { DataKey = new DomainEntityKey(entityItem) }, entityItem); }

        void Activate(ModelSubjectAreaItem subjectItem)
        { Activate((data) => new Forms.Domain.ModelSubjectArea() { DataKey = new ModelSubjectAreaKey(subjectItem) }, subjectItem); }

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
