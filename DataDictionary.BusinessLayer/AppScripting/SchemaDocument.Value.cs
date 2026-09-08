using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaDocumentValue : ISchemaDocumentItem, IDocumentIndex, ITemplateObjectNameIndex, ISchemaComposite,
        IScopeType, ITemporal
    {
        /// <summary>
        /// File information to be used with the File Save/Open Dialog.
        /// </summary>
        //IFileValue SchemaFile { get; }
    }

    /// <inheritdoc/>
    public class SchemaDocumentValue : SchemaDocumentItem, ISchemaDocumentValue, IPathValue, INamedScopeSourceValue, IFileValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingDocument; } }

        FileValue SchemaFile { get; }

        /// <inheritdoc/>
        public String FileContent
        {
            get;
            set
            {
                field = value;

                if (value.TryParse(out XDocument? document, out Exception? exception))
                { field = document.Parse(); }

                OnPropertyChanged(nameof(FileContent));
                OnPropertyChanged(nameof(ContentException));
            }
        } = String.Empty;

        /// <inheritdoc/>
        public Exception? ContentException
        {
            get
            {
                if (FileContent.TryParse(out XDocument? document, out Exception? exception))
                { return null; }
                else { return exception; }
            }
        }

        /// <inheritdoc/>
        public IEnumerable<FileFormatType> FileFormats { get { return SchemaFile.FileFormats; } }

        /// <inheritdoc/>
        public SchemaDocumentValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathIndex(PathIndex.Parse(FileName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => this.FileName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(FileName),
                IsTitleChanged = (e) => e.PropertyName is nameof(FileName)
            };

            SchemaFile = new FileValue()
            {
                GetFileName = () => FileName ?? String.Empty,
                SetFileName = (value) => FileName = value,
                GetFileFormats = () => new List<FileFormatType>() { FileFormatType.XMLData },
                GetContent = () => FileContent ?? String.Empty,
                SetContent = (value) => FileContent = value
            };
        }

        /// <inheritdoc cref="SchemaDocumentItem.SchemaDocumentItem(ITemplateKey, ISchemaDefinitionKey)"/>
        public SchemaDocumentValue(ITemplateIndex template, ISchemaDefinitionIndex schema) : base(template, schema)
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathIndex(Scope),
                GetScope = () => Scope,
                GetTitle = () => this.FileName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(FileName),
                IsTitleChanged = (e) => e.PropertyName is nameof(FileName)
            };

            SchemaFile = new FileValue()
            {
                GetFileName = () => FileName ?? String.Empty,
                SetFileName = (value) => FileName = value,
                GetFileFormats = () => new List<FileFormatType>() { FileFormatType.XMLData },
                GetContent = () => FileContent ?? String.Empty,
                SetContent = (value) => FileContent = value
            };

        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Open(FileInfo file)
        { return SchemaFile.Open(file); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(FileInfo file)
        { return SchemaFile.Save(file); }

        /// <inheritdoc/>
        public Boolean IsValid([NotNullWhen(false)] out Exception? exception)
        { return SchemaFile.IsValid(out exception); }
    }
}
