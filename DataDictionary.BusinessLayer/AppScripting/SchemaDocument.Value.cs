using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Xml;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ISchemaDocumentValue : ISchemaDocumentItem, IDocumentIndex, ITemplateObjectNameIndex, ISchemaComposite,
        IScopeType, ITemporal
    {
        /// <summary>
        /// File information to be used with the File Save/Open Dialog.
        /// </summary>
        IFileValue SchemaFile { get; }

        /// <summary>
        /// XML version of the File Contents.
        /// </summary>
        XDocument Content { get; }
    }

    /// <inheritdoc/>
    public class SchemaDocumentValue : SchemaDocumentItem, ISchemaDocumentValue, IPathValue, INamedScopeSourceValue
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

        /// <inheritdoc/>
        public IFileValue SchemaFile { get; }

        /// <inheritdoc/>
        public XDocument Content
        {
            get;
            set { field = value; OnPropertyChanged(nameof(Content)); }
        } = new XDocument();

        /// <inheritdoc/>
        public Exception? ContentException
        {
            get;
            set { field = value; OnPropertyChanged(nameof(ContentException)); }
        }

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
                GetContent = () =>
                {
                    if (Content.TryParse(out String? document))
                    { return document; }
                    else { return String.Empty; }
                },
                SetContent = (value) =>
                {
                    if (value.TryParse(out XDocument? document, out Exception? exception))
                    {
                        Content = document;
                        ContentException = null;
                    }
                    else { Content = new XDocument(); ContentException = exception; }
                }
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
                GetFileFormats = () => new List<FileFormatType>() { FileFormatType.XMLData }
            };
        }


    }
}
