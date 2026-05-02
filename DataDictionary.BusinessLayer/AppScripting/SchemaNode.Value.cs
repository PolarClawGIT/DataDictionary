using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaNodeValue : ISchemaNodeItem, ISchemaNodeIndex, ISchemaComposite,
        IScopeType, ITemporal
    { }

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
        }
    }

}
