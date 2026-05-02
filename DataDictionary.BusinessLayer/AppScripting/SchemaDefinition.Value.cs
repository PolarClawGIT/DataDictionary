using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaDefinitionValue : ISchemaDefinitionItem, ISchemaComposite,
        IScopeType, ITemporal
    {
        /// <summary>
        /// Directory information to be used with the Directory Dialog.
        /// </summary>
        IDirectoryValue SchemaDirectory { get; }
    }

    /// <inheritdoc/>
    public class SchemaDefinitionValue : SchemaDefinitionItem, ISchemaDefinitionValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingSchema; } }

        /// <inheritdoc/>
        public IDirectoryValue SchemaDirectory { get; }

        /// <inheritdoc />
        public SchemaDefinitionValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new SchemaDefinitionIndex(this),
                GetPath = () => new PathIndex(PathIndex.Parse(SchemaTitle).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => SchemaTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(SchemaTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(SchemaTitle)
            };

            SchemaDirectory = new DirectoryValue()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => RelativePath ?? String.Empty,
                SetDirectory = (value) => RelativePath = value
            };
        }

        /// <inheritdoc cref="SchemaDefinitionItem(ITemplateKey)"/>
        public SchemaDefinitionValue(ITemplateIndex template) : base(template)
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new SchemaDefinitionIndex(this),
                GetPath = () => new PathIndex(PathIndex.Parse(SchemaTitle).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => SchemaTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(SchemaTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(SchemaTitle)
            };

            SchemaDirectory = new DirectoryValue()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => RelativePath ?? String.Empty,
                SetDirectory = (value) => RelativePath = value
            };
        }
    }
}
