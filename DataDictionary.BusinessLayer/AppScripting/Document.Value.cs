using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using Microsoft.VisualBasic.FileIO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Toolbox.Threading;

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

        /// <summary>
        /// Loads the values from the files as defined by Input and Result paths.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<WorkItem> LoadFiles();

        /// <summary>
        /// Saves the values to files as defined by Input and Result paths.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<WorkItem> OpenFiles();

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
                GetDirectory = () => InputDirectory ?? RootPath,
                GetFileName = () => InputFile ?? String.Empty,
                SetDirectory = (v) => InputDirectory = v,
                SetFileName = (v) => InputFile = v
            };

            TransformValue = new DocumentFile()
            {
                GetRootFolder = () => RootFolder,
                GetContent = () => TransformScript ?? String.Empty,
                SetContent = (v) => TransformScript = v // TODO: Throwing Binding error because this occurred in a background thread.
            };

            OutputValue = new DocumentFile()
            {
                GetRootFolder = () => RootFolder,
                GetDirectory = () => OutputDirectory ?? RootPath,
                GetFileName = () => OutputFile ?? String.Empty,
                SetDirectory = (v) => OutputDirectory = v,
                SetFileName = (v) => OutputFile = v
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

                    if(String.IsNullOrWhiteSpace(OutputValue.Content))
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
            else if(inputException is Exception)
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

        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> LoadFiles()
        {
            if (String.IsNullOrEmpty(DocumentTitle))
            { throw new ArgumentNullException(nameof(DocumentTitle)); }

            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = String.Format("Load Document {0}", DocumentTitle), DoWork = LoadData });
            return work;

            void LoadData()
            {
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }



                throw new NotImplementedException();
            }
        }


        /// <inheritdoc/>
        public IReadOnlyList<WorkItem> OpenFiles()
        {
            if (String.IsNullOrEmpty(DocumentTitle))
            { throw new ArgumentNullException(nameof(DocumentTitle)); }


            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { WorkName = String.Format("Save Document {0}", DocumentTitle), DoWork = SaveData });
            return work;

            void SaveData()
            {
                try
                {

                }
                catch (Exception)
                {

                    throw;
                }
                throw new NotImplementedException();
            }
        }

    }
}
