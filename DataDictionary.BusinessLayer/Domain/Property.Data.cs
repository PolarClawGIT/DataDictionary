using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.DomainData.Property;
using DataDictionary.DataLayer.ModelData;
using System.Xml.Linq;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface component for the Property data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IPropertyData :
        IBindingData<PropertyValue>,
        ILoadData, ILoadData<IPropertyIndex>, ISaveData<IPropertyIndex>
    {
        /// <summary>
        /// Gets the a list of XAttributes from the Properties
        /// </summary>
        /// <param name="scripting"></param>
        /// <param name="node"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <remarks>Not for use outside of BusinessLayer</remarks>
        IReadOnlyList<XAttribute> GetXAttributes(ScriptingWork scripting, TemplateNodeValue node, IEnumerable<IDomainProperty> properties);
    }

    /// <inheritdoc/>
    class PropertyData : DomainPropertyCollection<PropertyValue>, IPropertyData,
        ILoadData<IModelKey>, ISaveData<IModelKey>, IDataTableFile
    {
        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IPropertyIndex dataKey)
        { return Load(factory, (IDomainPropertyKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IDomainPropertyKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IPropertyIndex dataKey)
        { return Save(factory, (IDomainPropertyKey)dataKey); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IDomainPropertyKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Definition</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateLoad(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory, IModelKey dataKey)
        { return factory.CreateSave(this, dataKey).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<System.Data.DataTable> Export()
        { return this.ToDataTable().ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public void Import(System.Data.DataSet source)
        { this.Load(source); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Delete()
        { return new WorkItem() { WorkName = "Remove Property", DoWork = () => { this.Clear(); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Delete(IPropertyIndex dataKey)
        { return new WorkItem() { WorkName = "Remove Property", DoWork = () => { this.Remove(dataKey); } }.ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public IReadOnlyList<WorkItem> Delete(IModelKey dataKey)
        { return Delete(); }

        /// <inheritdoc/>
        public IReadOnlyList<XAttribute> GetXAttributes(ScriptingWork scripting, TemplateNodeValue node, IEnumerable<IDomainProperty> properties)
        {
            List<XAttribute> result = new List<XAttribute>();

            TemplateNodeIndex nodeKey = new TemplateNodeIndex(node);
            foreach (TemplateAttributeValue templateAttrib in scripting.Attributes.Where(w => nodeKey.Equals(w)))
            {
                XAttribute? attrib = null;
                PropertyIndex propertyKey = new PropertyIndex(templateAttrib);
                PropertyValue? propertyValue = this.FirstOrDefault(w => propertyKey.Equals(w));
                IDomainProperty? property = properties.FirstOrDefault(w => propertyKey.Equals(w));

                String newTitle = String.Empty;
                String newValue = String.Empty;

                if (!String.IsNullOrWhiteSpace(templateAttrib.AttributeName))
                { newTitle = templateAttrib.AttributeName; }
                else if (propertyValue is PropertyValue && !String.IsNullOrWhiteSpace(propertyValue.PropertyTitle))
                { { newTitle = propertyValue.PropertyTitle; } }

                if (property is IDomainProperty && !String.IsNullOrWhiteSpace(property.PropertyValue))
                { newValue = property.PropertyValue; }
                else if (!String.IsNullOrWhiteSpace(templateAttrib.AttributeValue))
                { newValue = templateAttrib.AttributeValue; }

                attrib = templateAttrib.BuildXAttribute(newTitle, newValue);

                if (attrib is XAttribute)
                { result.Add(attrib); }
            }

            return result;
        }
    }
}
