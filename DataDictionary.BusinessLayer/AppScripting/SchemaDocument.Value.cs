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
    public interface ISchemaDocumentValue : ISchemaDocumentItem, IDocumentIndex, ITemplateObjectIndex, ISchemaComposite,
        IScopeType, ITemporal, IFileValue
    {
        /// <summary>
        /// File information to be used with the File Save/Open Dialog.
        /// </summary>
        //IFileValue SchemaFile { get; }
    }

    /// <inheritdoc/>
    public class SchemaDocumentValue : SchemaDocumentItem, ISchemaDocumentValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue
        FileValue schemaFile; // Backing field for IFileValue

        /// <inheritdoc/>
        PathItem IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingDocument; } }


        /// <inheritdoc/>
        public String FileName
        {
            get { return schemaFile.FileName; }
            set { schemaFile.FileName = value; }
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
        public IEnumerable<FileFormatType> FileFormats { get { return schemaFile.FileFormats; } }

        /// <inheritdoc/>
        public override String? ObjectPath
        {
            get { return base.ObjectPath; }
            set
            {
                PathItem path = new PathItem(PathItem.Parse(value));
                base.ObjectPath = path.MemberFullPath;
            }
        }


        /// <inheritdoc/>
        public SchemaDocumentValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathItem(PathItem.Parse(SchemaFileName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => this.SchemaFileName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(SchemaFileName),
                IsTitleChanged = (e) => e.PropertyName is nameof(SchemaFileName)
            };

            schemaFile = new FileValue()
            {
                GetFileName = () => SchemaFileName ?? String.Empty,
                SetFileName = (value) => SchemaFileName = value,
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
                GetPath = () => new PathItem(Scope),
                GetScope = () => Scope,
                GetTitle = () => this.SchemaFileName ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(SchemaFileName),
                IsTitleChanged = (e) => e.PropertyName is nameof(SchemaFileName)
            };

            schemaFile = new FileValue()
            {
                GetFileName = () => SchemaFileName ?? String.Empty,
                SetFileName = (value) => SchemaFileName = value,
                GetFileFormats = () => new List<FileFormatType>() { FileFormatType.XMLData },
                GetContent = () => FileContent ?? String.Empty,
                SetContent = (value) => FileContent = value
            };

        }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Open(FileInfo file)
        { return schemaFile.Open(file); }

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> Save(FileInfo file)
        { return schemaFile.Save(file); }

        /// <inheritdoc/>
        public Boolean IsValid([NotNullWhen(false)] out Exception? exception)
        { return schemaFile.IsValid(out exception); }

        /// <summary>
        /// Builds the XDocument and sets the FileContent to the value.
        /// </summary>
        /// <param name="getBuilders"></param>
        /// <param name="nodeValues"></param>
        /// <remarks>Use <see cref="XmlBuilderXElement.GetBuilder(AppModel.IModel, ITemplateObjectIndex)"/> to get the builders.</remarks>
        public void BuildContent(Func<ITemplateObjectIndex, Func<IEnumerable<XmlBuilder>, XElement>?> getBuilders, IEnumerable<XmlBuilderNode> nodeValues)
        {
            var builder = getBuilders(this);

            if (builder is not null)
            { FileContent = new XDocument(new XDeclaration("1.0", "utf-8", null), builder(nodeValues)).Format(); }

        }
    }
}
