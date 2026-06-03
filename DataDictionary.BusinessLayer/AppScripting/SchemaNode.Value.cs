using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaNodeValue : ISchemaNodeItem, ISchemaNodeIndex, ISchemaComposite,
        IScopeType, ITemporal
    {
        /// <inheritdoc cref="SchemaNodeFixedValue"/>
        SchemaNodeFixedValue FixedNodeValue { get; }

        /// <inheritdoc cref="SchemaNodeObjectValue"/>
        SchemaNodeObjectValue ObjectNodeValue { get; }

        /// <inheritdoc cref="SchemaNodeObjectValue"/>
        SchemaNodePropertyValue PropertyNodeValue { get; }
    }

    /// <inheritdoc/>
    public class SchemaNodeValue : SchemaNodeItem, ISchemaNodeValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingNode; } }

        /// <inheritdoc/>
        public SchemaNodeFixedValue FixedNodeValue { get; }

        /// <inheritdoc/>
        public SchemaNodeObjectValue ObjectNodeValue { get; }

        /// <inheritdoc/>
        public SchemaNodePropertyValue PropertyNodeValue { get; }

        /// <inheritdoc/>
        public SchemaNodeValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new SchemaNodeIndex(this),
                GetPath = () => new PathIndex(PathIndex.Parse(NodeName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => NodeName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(NodeName),
                IsTitleChanged = (e) => e.PropertyName is nameof(NodeName)
            };

            FixedNodeValue = new SchemaNodeFixedValue(this);
            ObjectNodeValue = new SchemaNodeObjectValue(this);
            PropertyNodeValue = new SchemaNodePropertyValue(this);
        }

        /// <inheritdoc cref="SchemaNodeItem.SchemaNodeItem(ITemplateKey, ISchemaDefinitionKey)"/>
        public SchemaNodeValue(ISchemaComposite schema) : base(schema, schema)
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new SchemaNodeIndex(this),
                GetPath = () => new PathIndex(Scope),
                GetScope = () => Scope,
                GetTitle = () => NodeName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(NodeName),
                IsTitleChanged = (e) => e.PropertyName is nameof(NodeName)
            };

            FixedNodeValue = new SchemaNodeFixedValue(this);
            ObjectNodeValue = new SchemaNodeObjectValue(this);
            PropertyNodeValue = new SchemaNodePropertyValue(this);
        }
    }

}
