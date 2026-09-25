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
    public interface ITransformDocumentValue : ITransformDocumentItem, IDocumentIndex, ITransformComposite,
        IScopeType, ITemporal, IFileValue
    { }

    /// <inheritdoc/>
    public class TransformDocumentValue : TransformDocumentItem, ITransformDocumentValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue
        FileValue scriptedFile; // Backing field for IFileValue

        /// <inheritdoc/>
        PathItem IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingDocument; } }

        /// <inheritdoc/>
        String IFileValue.FileName
        {
            get { return scriptedFile.FileName; }
            set { scriptedFile.FileName = value; }
        }

        /// <inheritdoc/>
        public String FileContent
        {
            get;
            set
            {
                field = value;

                if ((FileFormatType.XMLData.IsFileFormat(ScriptedFileName)
                   || FileFormatType.XSLTransform.IsFileFormat(ScriptedFileName))
                   && value.TryParse(out XDocument? document, out Exception? exception))
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
                if ((FileFormatType.XMLData.IsFileFormat(ScriptedFileName)
                   || FileFormatType.XSLTransform.IsFileFormat(ScriptedFileName))
                   && !FileContent.TryParse(out XDocument? document, out Exception? exception))
                { return exception; }
                else { return null; }
            }
        }

        /// <inheritdoc cref="IFileValue.FileFormats"/>
        public static IEnumerable<FileFormatType> FileFormats
        { get; } = Enum.GetValues<FileFormatType>().
            Except(new List<FileFormatType>() { FileFormatType.Other, FileFormatType.XSLTransform });

        /// <inheritdoc/>
        IEnumerable<FileFormatType> IFileValue.FileFormats { get { return FileFormats; } }

        /// <inheritdoc/>
        public TransformDocumentValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathItem(PathItem.Parse(ScriptedFileName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => this.ScriptedFileName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(ScriptedFileName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ScriptedFileName)
            };

            scriptedFile = new FileValue()
            {
                GetFileName = () => ScriptedFileName ?? String.Empty,
                SetFileName = (value) => ScriptedFileName = value,
                GetContent = () => FileContent ?? String.Empty,
                SetContent = (value) => FileContent = value
            };
        }

        /// <inheritdoc cref="TransformDocumentItem.TransformDocumentItem(ITemplateKey, ITransformKey)"/>
        public TransformDocumentValue(ITransformComposite transform) : base(transform, transform)
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathItem(PathItem.Parse(ScriptedFileName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => this.ScriptedFileName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(ScriptedFileName),
                IsTitleChanged = (e) => e.PropertyName is nameof(ScriptedFileName)
            };

            scriptedFile = new FileValue()
            {
                GetFileName = () => ScriptedFileName ?? String.Empty,
                SetFileName = (value) => ScriptedFileName = value,
                GetContent = () => FileContent ?? String.Empty,
                SetContent = (value) => FileContent = value
            };
        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Open(FileInfo file)
        { return scriptedFile.Open(file); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(FileInfo file)
        { return scriptedFile.Save(file); }

        /// <inheritdoc/>
        public Boolean IsValid([NotNullWhen(false)] out Exception? exception)
        { return scriptedFile.IsValid(out exception); }
    }
}
