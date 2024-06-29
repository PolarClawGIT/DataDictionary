using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface for Scripting Engine Template Document
    /// </summary>
    public interface ITemplateDocumentValue : ITemplateIndex, IBindingPropertyChanged
    {
        /// <summary>
        /// The Name of the Element for this Document
        /// </summary>
        String? ElementName { get; init; }

        /// <summary>
        /// The Document Name
        /// </summary>
        String DocumentName { get; }

        /// <summary>
        /// The Document file Name
        /// </summary>
        FileInfo? DocumentFile { get; }

        /// <summary>
        /// The Script Name
        /// </summary>
        String ScriptName { get; }

        /// <summary>
        /// The Script file Name
        /// </summary>
        FileInfo? ScriptFile { get; }

        /// <summary>
        /// The XML Document that was used as the Source
        /// </summary>
        //XDocument Source { get; }

        /// <summary>
        /// Text version of Source. For Data Binding.
        /// </summary>
        String SourceAsText { get; }

        /// <summary>
        /// Exceptions while building or transforming the document
        /// </summary>
        Exception? Exception { get; }

        /// <summary>
        /// Text version of Exception. For Data Binding.
        /// </summary>
        String ExceptionAsText { get; }

        /// <summary>
        /// The Results of the Transform as plain text (may contain formated XML).
        /// </summary>
        String? ResultsAsText { get; }

        /// <summary>
        /// The Result of the Transform as XML (null if Text transform only)
        /// </summary>
        XDocument? ResultsAsXml { get; }
    }


    /// <summary>
    /// Scripting Engine Template Document
    /// </summary>
    public class TemplateDocumentValue : ITemplateDocumentValue
    {
        /// <inheritdoc/>
        public Guid? TemplateId { get { return templateValue.TemplateId; } }

        /// <inheritdoc/>
        public String? ElementName { get; init; }

        /// <inheritdoc/>
        public String DocumentName
        {
            get
            {
                return String.Format("{0}{1}{2}.{3}",
                    templateValue.DocumentPrefix ?? String.Empty,
                    ElementName ?? templateValue.TemplateTitle,
                    templateValue.DocumentSuffix ?? String.Empty,
                    templateValue.DocumentExtension ?? "xml");
            }
        }

        /// <inheritdoc/>
        public FileInfo? DocumentFile
        {
            get
            {
                if (templateValue.RootDirectory is DirectoryType root &&
                    new DirectoryTypeKey(root).ToDirectoryInfo() is DirectoryInfo folder)
                {
                    String directoryName =
                        Path.Combine(
                            folder.FullName,
                            templateValue.DocumentDirectory ?? String.Empty);

                    return new FileInfo(Path.Combine(directoryName, DocumentName));
                }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public String ScriptName
        {
            get
            {
                return String.Format("{0}{1}{2}.{3}",
                    templateValue.ScriptPrefix ?? String.Empty,
                    ElementName ?? templateValue.TemplateTitle,
                    templateValue.ScriptSuffix ?? String.Empty,
                    templateValue.ScriptExtension ?? "txt");
            }

        }

        /// <inheritdoc/>
        public FileInfo? ScriptFile
        {
            get
            {
                if (templateValue.RootDirectory is DirectoryType root &&
                    new DirectoryTypeKey(root).ToDirectoryInfo() is DirectoryInfo folder)
                {
                    String directoryName =
                        Path.Combine(
                            folder.FullName,
                            templateValue.ScriptDirectory ?? String.Empty);

                    return new FileInfo(Path.Combine(directoryName, ScriptName));
                }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public String SourceAsText
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, };

                try
                { // Because there is no way to check if the XDocument is empty except using a try/catch.
                    using (XmlWriter writer = XmlWriter.Create(builder, settings))
                    { source.WriteTo(writer); }
                }
                catch (Exception) { }

                return builder.ToString();
            }
        }

        /// <inheritdoc/>
        public Exception? Exception
        {
            get { return documentException; }
            set
            {
                documentException = value;
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(Exception));
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(ExceptionAsText));
            }
        }
        Exception? documentException = null;

        /// <inheritdoc/>
        public String ExceptionAsText
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                if (documentException is Exception ex)
                {
                    builder.AppendLine(ex.Message);
                    foreach (var item in ex.Data.Keys)
                    {
                        if (item.ToString() is String stringKey
                            && ex.Data[item] is Object value
                            && value.ToString() is String stringValue)
                        { builder.AppendLine(String.Format("\t{0}: {1}", stringKey, stringValue)); }
                    }
                }

                return builder.ToString();
            }

        }

        /// <summary>
        /// The Results of the Transform as plain text (may contain formated XML).
        /// </summary>
        public String ResultsAsText
        {
            get { return resultsAsText; }
            protected set
            {
                if (String.IsNullOrWhiteSpace(value)) { resultsAsText = String.Empty; resultsAsXml = null; }
                else { resultsAsText = value; resultsAsXml = null; }
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(ResultsAsText));
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(ResultsAsXml));
            }
        }
        String resultsAsText = String.Empty;

        /// <summary>
        /// The Result of the Transform as XML (null if Text transform only)
        /// </summary>
        public XDocument? ResultsAsXml
        {
            get { return resultsAsXml; }
            protected set
            {
                if (value is null) { resultsAsText = String.Empty; resultsAsXml = null; }
                else { resultsAsText = String.Concat(value.Declaration, value); resultsAsXml = value; }
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(ResultsAsText));
                IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(ResultsAsXml));
            }
        }
        XDocument? resultsAsXml = null;

        ITemplateValue templateValue;
        XDocument source = new XDocument() { Declaration = new XDeclaration("1.0", null, null) };

        /// <summary>
        /// Constructor for Scripting Document Template.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="root"></param>
        public TemplateDocumentValue(ITemplateValue template, XElement root) : base()
        {
            templateValue = template;

            try
            { source.Add(root); }
            catch (Exception ex)
            { documentException = ex; }

            source.Changed += Source_Changed;
        }

        private void Source_Changed(Object? sender, XObjectChangeEventArgs e)
        { IBindingPropertyChanged.OnPropertyChanged(this, PropertyChanged, nameof(SourceAsText)); }

        /// <summary>
        /// Applies the XSL Transform to the Source to complete the Document
        /// </summary>
        /// <returns></returns>
        public Boolean ApplyTransform()
        {
            Boolean result = false;
            if (Exception is not null) { return false; }

            try
            {
                if (templateValue.TransformXml is XDocument transform)
                {
                    using (XmlReader sourceReader = source.CreateReader())
                    using (XmlReader transformReader = transform.CreateReader())
                    {
                        XslCompiledTransform transformer = new XslCompiledTransform();
                        transformer.Load(transformReader);

                        if (templateValue.ScriptAs is ScriptAsType.Text)
                        {
                            using (StringWriter resultText = new StringWriter())
                            {
                                transformer.Transform(sourceReader, null, resultText);
                                ResultsAsText = resultText.ToString();
                            }
                        }
                        else if (templateValue.ScriptAs is ScriptAsType.XML)
                        {
                            ResultsAsXml = new XDocument() { Declaration = new XDeclaration("1.0", null, null) };
                            using (XmlWriter resultAsXml = ResultsAsXml.CreateWriter())
                            { transformer.Transform(sourceReader, resultAsXml); }
                        }

                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("XSLT", templateValue.TransformScript);
                ex.Data.Add("XML", source);
                Exception = ex;
            }

            return result;
        }

        /// <summary>
        /// Returns the work items needed to Save the Source (XML) to the File.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> SaveSource()
        {
            List<WorkItem> work = new List<WorkItem>();

            if (DocumentFile is FileInfo && source.Root is XElement)
            {
                work.Add(new WorkItem()
                {
                    WorkName = String.Format("Save: {0}", DocumentFile.FullName),
                    DoWork = () => source.Save(DocumentFile.FullName)
                });
            }

            return work;
        }

        /// <summary>
        /// Returns the work items needed to Save the Result (Script) to the File.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> SaveResult()
        {
            List<WorkItem> work = new List<WorkItem>();

            if (ScriptFile is FileInfo)
            {
                if (resultsAsXml is XDocument && source.Root is XElement)
                {
                    work.Add(new WorkItem()
                    {
                        WorkName = String.Format("Save: {0}", ScriptFile.FullName),
                        DoWork = () => resultsAsXml.Save(ScriptFile.FullName)
                    });
                }
                else if (!String.IsNullOrWhiteSpace(ResultsAsText))
                {
                    work.Add(new WorkItem()
                    {
                        WorkName = String.Format("Save: {0}", ScriptFile.FullName),
                        DoWork = () => File.WriteAllText(ScriptFile.FullName, ResultsAsText)
                    });
                }
            }

            return work;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
