using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Xsl;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDocumentValue : IDocumentItem,
        IDocumentIndex, ITemplateIndex, ISchemaDefinitionIndex, ITransformIndex, 
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class DocumentValue : DocumentItem, IDocumentValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathItem IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingDocument; } }

        /// <summary>
        /// File Details for the Structured data (Input) file.
        /// </summary>
        public IFileValue StructuredFile { get; }

        /// <summary>
        /// File Details for the Transform (Process) file.
        /// </summary>
        public IFileValue TransformFile { get; }

        /// <summary>
        /// File Details for the Scripted (Output) file.
        /// </summary>
        public IFileValue ScriptedFile { get; }

        /// <inheritdoc/>
        public DocumentValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathItem(PathItem.Parse(ScriptedFileName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => DocumentTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DocumentTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(DocumentTitle)
            };

            StructuredFile = new FileValue()
            {
                GetFileName = () => StructuredFileName ?? String.Empty,
                SetFileName = (value) => StructuredFileName = value
            };

            TransformFile = new FileValue()
            {
                GetFileName = () => TransformFileName ?? String.Empty,
                SetFileName = (value) => TransformFileName = value
            };

            ScriptedFile = new FileValue()
            {
                GetFileName = () => ScriptedFileName ?? String.Empty,
                SetFileName = (value) => ScriptedFileName = value
            };

        }

        /// <inheritdoc cref="DocumentItem.DocumentItem(ITemplateKey)"/>
        public DocumentValue(ITemplateIndex template) : base(template)
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathItem(PathItem.Parse(ScriptedFileName).ToArray()),
                GetScope = () => Scope,
                GetTitle = () => DocumentTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DocumentTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(DocumentTitle)
            };

            StructuredFile = new FileValue()
            {
                GetFileName = () => StructuredFileName ?? String.Empty,
                SetFileName = (value) => StructuredFileName = value,
                FileFormats = new List<FileFormatType>() { FileFormatType.XMLData }
            };

            TransformFile = new FileValue()
            {
                GetFileName = () => TransformFileName ?? String.Empty,
                SetFileName = (value) => TransformFileName = value,
                FileFormats = new List<FileFormatType>() { FileFormatType.XSLTransform }
            };

            ScriptedFile = new FileValue()
            {
                GetFileName = () => ScriptedFileName ?? String.Empty,
                SetFileName = (value) => ScriptedFileName = value,
                FileFormats = Enum.GetValues<FileFormatType>().
                        Except(new List<FileFormatType>() { FileFormatType.Other, FileFormatType.XSLTransform })
            };
        }

        /// <summary>
        /// Tries to perform an XML Transform. If successful, the Scripted file content is updated.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public Boolean TryTransform([NotNullWhen(false)] out Exception? exception)
        {
            exception = null;

            try
            {
                // Setup the XML Transform
                XslCompiledTransform xslt = new XslCompiledTransform();
                using (StringReader xsltReader = new StringReader(TransformFile.FileContent))
                using (XmlReader xmlReader = XmlReader.Create(xsltReader))
                { xslt.Load(xmlReader); }

                // Execute the XML Transform
                using (StringReader xmlInputReader = new StringReader(StructuredFile.FileContent))
                using (XmlReader xmlReader = XmlReader.Create(xmlInputReader))
                using (StringWriter stringWriter = new StringWriter())
                {
                    xslt.Transform(xmlReader, null, stringWriter);
                    ScriptedFile.FileContent = stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add(nameof(StructuredFileName), StructuredFileName);
                ex.Data.Add(nameof(TransformFileName), TransformFileName);
                ex.Data.Add(nameof(ScriptedFileName), ScriptedFileName);
                exception = ex;
            }

            return exception is null;
        }
    }
}
