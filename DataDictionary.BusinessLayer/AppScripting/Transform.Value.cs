using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITransformValue : ITransformItem, ITransformComposite,
        IScopeType, ITemporal, IDirectoryValue, IFileValue
    { }

    /// <inheritdoc/>
    public class TransformValue : TransformItem, ITransformValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue
        IDirectoryValue directory; // Backing field for IDirectoryValue
        FileValue scriptingFile; // Backing field for IFileValue

        /// <inheritdoc/>
        PathItem IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingTransform; } }

        /// <inheritdoc/>
        String IFileValue.FileName
        {
            get { return scriptingFile.FileName; }
            set { scriptingFile.FileName = value; }
        }

        /// <inheritdoc/>
        public String FileContent
        {
            get;
            set
            {
                field = value;

                if (value.TryParse(out XDocument? document, out Exception? exception))
                { field = document.Format(); }

                OnPropertyChanged(nameof(FileContent));
                OnPropertyChanged(nameof(ContentException));
            }
        } = String.Empty;

        /// <inheritdoc/>
        public Exception? ContentException
        {
            get
            {   // It is possible that FileContent Set was by-passed.
                // As such, the content needs to be parsed independently.
                if (FileContent.TryParse(out XDocument? _, out Exception? exception))
                { return null; }
                else { return exception; }
            }
        }

        /// <inheritdoc/>
        IEnumerable<FileFormatType> IFileValue.FileFormats { get { return scriptingFile.FileFormats; } }

        /// <inheritdoc/>
        public String InitialDirectory
        {
            get { return directory.InitialDirectory; }
            set { directory.InitialDirectory = value; }
        }

        /// <inheritdoc/>
        public TransformValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new TransformIndex(this),
                GetPath = () => new PathItem(PathItem.Parse(TransformTitle).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => TransformTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(TransformTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(TransformTitle)
            };

            directory = new DirectoryValue()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => RelativePath ?? String.Empty,
                SetDirectory = (value) => RelativePath = value
            };

            scriptingFile = new FileValue()
            {
                GetFileName = () => TransformFileName ?? String.Empty,
                SetFileName = (value) => TransformFileName = value,
                GetFileFormats = () => new List<FileFormatType>() { FileFormatType.XSLTransform },
                GetContent = () => FileContent ?? String.Empty,
                SetContent = (value) => FileContent = value
            };
        }

        /// <inheritdoc cref="TransformItem.TransformItem(ITemplateKey)"/>
        public TransformValue(ITemplateIndex template): base(template)
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new TransformIndex(this),
                GetPath = () => new PathItem(PathItem.Parse(TransformTitle).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => TransformTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(TransformTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(TransformTitle)
            };

            directory = new DirectoryValue()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => RelativePath ?? String.Empty,
                SetDirectory = (value) => RelativePath = value
            };

            scriptingFile = new FileValue()
            {
                GetFileName = () => TransformFileName ?? String.Empty,
                SetFileName = (value) => TransformFileName = value,
                GetFileFormats = () => new List<FileFormatType>() { FileFormatType.XSLTransform },
                GetContent = () => FileContent ?? String.Empty,
                SetContent = (value) => FileContent = value
            };
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Open(FileInfo file)
        { return scriptingFile.Open(file); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(FileInfo file)
        { return scriptingFile.Save(file); }

        /// <inheritdoc/>
        public Boolean IsValid([NotNullWhen(false)] out Exception? exception)
        { return directory.IsValid(out exception) && scriptingFile.IsValid(out exception); }

    }
}
