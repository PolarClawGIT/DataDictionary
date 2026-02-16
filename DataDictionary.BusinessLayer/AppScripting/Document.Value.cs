using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface IDocumentValue : IDocumentItem, IDocumentIndex, ITemplateIndex,
        IScopeType, ITemporal
    {
        /// <summary>
        /// Root Path determined by the RootFolder value.
        /// </summary>
        String RootPath { get; }

        /// <summary>
        /// Input Data, XML is expected.
        /// </summary>
        DocumentFile InputValue { get; }

        /// <summary>
        /// XML Transform Data, XSL expected.
        /// </summary>
        DocumentFile TransformValue { get; }

        /// <summary>
        /// Output Data, Plain Text or XML is expected.
        /// </summary>
        DocumentFile OutputValue { get; }

        /// <summary>
        /// Executes the XML Transform, filling Results and Exception.
        /// </summary>
        Boolean TryTransform([NotNullWhen(false)] out Exception? exception);
    }

    /// <inheritdoc/>
    public class DocumentValue : DocumentItem, IDocumentValue, INamedScopeSourceValue
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
        public String RootPath
        {
            get
            {

                if (RootFolder.GetEnumeration().Directory is DirectoryInfo directory)
                { return directory.FullName; }
                else { return String.Empty; }
            }
        }

        /// <inheritdoc/>
        public DocumentFile InputValue { get; }

        /// <inheritdoc/>
        public DocumentFile TransformValue { get; }

        /// <inheritdoc/>
        public DocumentFile OutputValue { get; }

        /// <inheritdoc/>
        public String? DocumentException
        {
            get
            {
                if (exceptions.Count == 0)
                { return null; }

                StringBuilder result = new StringBuilder();

                foreach (Exception exceptionItem in exceptions)
                {
                    result.AppendLine(exceptionItem.Message);

                    foreach (var exceptionKey in exceptionItem.Data.Keys)
                    {
                        if (exceptionKey.ToString() is String stringKey
                            && exceptionItem.Data[exceptionKey] is Object value
                            && value.ToString() is String stringValue)
                        { result.AppendLine(String.Format("\t{0}: {1}", stringKey, stringValue)); }

                    }
                }

                return result.ToString();
            }
        }

        Collection<Exception> exceptions = new Collection<Exception>();

        /// <inheritdoc/>
        public DocumentValue() : base()
        {
            InputValue = new DocumentFile()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => InputPath ?? RootPath,
                GetFileName = () => InputFile ?? String.Empty,
                SetDirectory = (v) => InputPath = v,
                SetFileName = (v) => InputFile = v,
                GetFileFormats = () => new List<FileFormatType>() { FileFormatType.XMLData }
            };
            
            TransformValue = new DocumentFile()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => ProcessPath ?? RootPath,
                GetFileName = () => ProcessFile ?? String.Empty,
                SetDirectory = (v) => ProcessPath = v,
                SetFileName = (v) => ProcessFile = v,
                GetFileFormats = () => new List<FileFormatType>() { FileFormatType.XSLTransform }
            };

            OutputValue = new DocumentFile()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => OutputPath ?? RootPath,
                GetFileName = () => OutputFile ?? String.Empty,
                SetDirectory = (v) => OutputPath = v,
                SetFileName = (v) => OutputFile = v,
                GetFileFormats = () => new List<FileFormatType>()
                { FileFormatType.PlainText,
                  FileFormatType.XMLData,
                  FileFormatType.SQLScript,
                  FileFormatType.CSharp,
                  FileFormatType.VisualBasic,
                  FileFormatType.Mermaid,
                  FileFormatType.Markdown,
                  FileFormatType.Other
                }
            };

            PropertyChanged += DocumentValue_PropertyChanged;

            pathValue = new PathValue(this)
            {
                GetIndex = () => new DocumentIndex(this),
                GetPath = () => new PathIndex(DocumentTitle),
                GetScope = () => Scope,
                GetTitle = () => DocumentTitle ?? Scope.GetEnumeration().Name,
                IsPathChanged = (e) => e.PropertyName is nameof(DocumentTitle),
                IsTitleChanged = (e) => e.PropertyName is nameof(DocumentTitle)
            };

            void DocumentValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName is nameof(RootFolder))
                { OnPropertyChanged(nameof(RootPath)); }
            }
        }


        /// <inheritdoc/>
        public Boolean TryTransform([NotNullWhen(false)] out Exception? exception)
        {
            Boolean isSourceXml = InputValue.TryParse(out XDocument? source, out Exception? inputException);
            Boolean isTransformXml = TransformValue.TryParse(out XDocument? transform, out Exception? transformException);

            if (source is XDocument && transform is XDocument)
            {
                try
                {
                    using (XmlReader sourceReader = source.CreateReader())
                    using (XmlReader transformReader = transform.CreateReader())
                    {
                        XslCompiledTransform transformer = new XslCompiledTransform();
                        transformer.Load(transformReader);

                        using (StringWriter resultText = new StringWriter())
                        {   // Transform to Text
                            transformer.Transform(sourceReader, null, resultText);
                            OutputValue.Content = resultText.ToString();
                        }
                    }

                    if (String.IsNullOrWhiteSpace(OutputValue.Content))
                    {
                        OutputValue.Content = String.Empty;
                        exception = new InvalidDataException("No results returned");
                        exception.Data.Add(nameof(InputValue), InputValue.Content);
                        exception.Data.Add(nameof(TransformValue), TransformValue.Content);
                        return false;
                    }

                    exception = null; return true;
                }
                catch (Exception ex)
                { exception = ex; return false; }
            }
            else if (inputException is Exception)
            { exception = inputException; return false; }
            else if (transformException is Exception)
            { exception = transformException; return false; }
            else
            {   // This should be un-reachable.
                exception = new InvalidOperationException("Data is Not XML");
                exception.Data.Add(nameof(InputValue), InputValue.Content);
                exception.Data.Add(nameof(isSourceXml), isSourceXml);
                exception.Data.Add(nameof(TransformValue), TransformValue.Content);
                exception.Data.Add(nameof(isTransformXml), isTransformXml);
                return false;
            }
        }

    }
}
