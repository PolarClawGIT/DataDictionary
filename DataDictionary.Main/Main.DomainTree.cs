using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.DomainData.SubjectArea;
using DataDictionary.Main.Properties;
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
            Attributes,
            Property,
            Alias,
            Entity
        }

        static Dictionary<domainModelImageIndex, (String imageKey, Image image)> domainModelImageItems = new Dictionary<domainModelImageIndex, (String imageKey, Image image)>()
        {
            {domainModelImageIndex.SubjectArea,  ("SubjectArea", Resources.Diagram) },
            {domainModelImageIndex.Attribute,    ("Attribute",   Resources.Attribute) },
            {domainModelImageIndex.Attributes,   ("Attributes",  Resources.Parameter) },
            {domainModelImageIndex.Property,     ("Property",    Resources.Property) },
            {domainModelImageIndex.Alias,        ("Alias",       Resources.Synonym) },
            {domainModelImageIndex.Entity,       ("Entity",      Resources.Relationship) },
        };

        void ClearDomainModelTreeByAttribute()
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
            foreach (DomainAttributeItem attributeItem in
                Program.Data.DomainAttributes.
                Where(w => w.SubjectAreaId is null))
            {
                TreeNode attributeNode = CreateAttribute(attributeItem, null);
                domainModelNavigation.Nodes.Add(attributeNode);
            }

            foreach (DomainSubjectAreaItem subjectItem in
                Program.Data.DomainSubjectAreas.
                OrderBy(o => o.SubjectAreaTitle))
            {
                TreeNode subjectNode = CreateNode(subjectItem.SubjectAreaTitle, domainModelImageIndex.SubjectArea, subjectItem, null);
                DomainSubjectAreaKey subjectKey = new DomainSubjectAreaKey(subjectItem);
                domainModelNavigation.Nodes.Add(subjectNode);

                foreach (DomainAttributeItem attributeItem in
                    Program.Data.DomainAttributes.
                    Where(w => subjectKey.Equals(w)))
                { TreeNode attributeNode = CreateAttribute(attributeItem, subjectNode); }
            }

            domainModelNavigation.Sort();

            TreeNode CreateAttribute(DomainAttributeItem attributeItem, TreeNode? parent)
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
                if (source is INotifyPropertyChanged notifyPropertyChanged)
                { notifyPropertyChanged.PropertyChanged += DomainItem_PropertyChanged; }

                return result;
            }
        }

        void BuildDomainModelTreeByEntity()
        {
            domainModelNavigation.Nodes.Clear();
            domainModelNodes.Clear();

            foreach (DomainEntityItem entityItem in
                Program.Data.DomainEntities.
                Where(w => w.SubjectAreaId is null))
            {
                TreeNode entityNode = CreateEntity(entityItem, null);
                domainModelNavigation.Nodes.Add(entityNode);
            }

            foreach (DomainSubjectAreaItem subjectItem in
                Program.Data.DomainSubjectAreas)
            {
                TreeNode subjectNode = CreateNode(subjectItem.SubjectAreaTitle, domainModelImageIndex.SubjectArea, subjectItem, null);
                DomainSubjectAreaKey subjectKey = new DomainSubjectAreaKey(subjectItem);
                domainModelNavigation.Nodes.Add(subjectNode);

                foreach (DomainEntityItem entityItem in
                    Program.Data.DomainEntities.
                    Where(w => subjectKey.Equals(w)).
                    OrderBy(o => o.EntityTitle))
                {
                    TreeNode attributeNode = CreateEntity(entityItem, subjectNode);
                    domainModelNavigation.Nodes.Add(attributeNode);
                }
            }

            domainModelNavigation.Sort();

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
                dynamic dataNode = domainModelNodes[node];
                Activate(dataNode);
            }
        }


        void DomainItem_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (domainModelNodes.FirstOrDefault(w => w.Value == sender) is KeyValuePair<TreeNode, object> value)
            {
                dynamic item = value.Value;
                TreeNode node = value.Key;

                if (NameOfTitle(item).Equals(e.PropertyName))
                {
                    node.Text = GetTitle(item);
                    domainModelNavigation.Sort();
                    domainModelNavigation.SelectedNode = node;
                }
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

        private void domainModelRefreshCommand_Click(object sender, EventArgs e)
        {
            ClearDomainModelTreeByAttribute();
            BuildDomainModelTreeByAttribute();
        }

        void Activate(DomainAttributeItem attributeItem)
        { Activate((data) => new Forms.Domain.DomainAttribute() { DataKey = new DomainAttributeKey(attributeItem) }, attributeItem); }

        void Activate(DomainEntityItem entityItem)
        { Activate((data) => new Forms.Domain.DomainEntity() { DataKey = new DomainEntityKey(entityItem) }, entityItem); }

        void Activate(DomainSubjectAreaItem subjectItem)
        { Activate((data) => new Forms.Domain.DomainSubjectArea() { DataKey = new DomainSubjectAreaKey(subjectItem) }, subjectItem); }

        String? GetTitle(object unkown)
        { throw new InvalidOperationException(String.Format("Could not determine dynamic type for GetTitle: {0}", unkown.GetType().Name)); }

        String? GetTitle(DomainAttributeItem attribute)
        { return attribute.AttributeTitle; }

        String? GetTitle(DomainEntityItem entity)
        { return entity.EntityTitle; }

        String? GetTitle(DomainSubjectAreaItem subject)
        { return subject.SubjectAreaTitle; }

        String NameOfTitle(object unkown)
        { throw new InvalidOperationException(String.Format("Could not determine dynamic type for NameOfTitle: {0}", unkown.GetType().Name)); }

        String NameOfTitle(DomainAttributeItem attribute)
        { return nameof(attribute.AttributeTitle); }

        String NameOfTitle(DomainEntityItem entity)
        { return nameof(entity.EntityTitle); }

        String NameOfTitle(DomainSubjectAreaItem subject)
        { return nameof(subject.SubjectAreaTitle); }
    }
}
