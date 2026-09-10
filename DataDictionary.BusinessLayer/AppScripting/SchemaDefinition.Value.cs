using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaDefinitionValue : ISchemaDefinitionItem, ISchemaComposite,
        IScopeType, ITemporal, IDirectoryValue
    { }

    /// <inheritdoc/>
    public class SchemaDefinitionValue : SchemaDefinitionItem, ISchemaDefinitionValue,
        IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue
        IDirectoryValue directory; // Backing field for IDirectoryValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingSchema; } }

        /// <inheritdoc/>
        public String InitialDirectory
        {
            get { return directory.InitialDirectory; }
            set { directory.InitialDirectory = value; }
        }

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

            directory = new DirectoryValue()
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

            directory = new DirectoryValue()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => RelativePath ?? String.Empty,
                SetDirectory = (value) => RelativePath = value
            };
        }

        /// <summary>
        /// Validates the SchemaDefinitionValue and returns exceptions if there is an issue.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public Boolean IsValid([NotNullWhen(false)] out Exception? exception)
        {   
            exception = null;
            return ((IDirectoryValue)this).IsValid(out exception);
        }
    }
}
