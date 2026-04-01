using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITransformValue : ITransformItem, ITransformIndex, ITemplateIndex,
        IScopeType, ITemporal
    {
        /// <summary>
        /// Directory information to be used with the Directory Dialog.
        /// </summary>
        IDirectoryValue TransformDirectory { get; }

        /// <summary>
        /// File information to be used with the File Save/Open Dialog.
        /// </summary>
        IFileValue TransformFile { get; }
    }

    /// <inheritdoc/>
    public class TransformValue : TransformItem, ITransformValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingTransform; } }

        /// <inheritdoc/>
        public IDirectoryValue TransformDirectory { get; }

        /// <inheritdoc/>
        public IFileValue TransformFile { get; }

        /// <inheritdoc/>
        public TransformValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new TransformIndex(this),
                GetPath = () => new PathIndex(Scope),
                GetScope = () => Scope,
                GetTitle = () => TransformTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(TransformTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(TransformTitle)
            };

            TransformDirectory = new DirectoryValue()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => RelativePath ?? String.Empty,
                SetDirectory = (value) => RelativePath = value
            };
            
            TransformFile = new FileValue()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => RelativePath ?? String.Empty,
                SetDirectory = (value) => RelativePath = value,
                GetFileName = () => TransformFileName ?? String.Empty,
                SetFileName = (value) => TransformFileName = value,
                GetFileFormats = () => new List<FileFormatType>() { FileFormatType.XSLTransform }
            };
        }

    }
}
