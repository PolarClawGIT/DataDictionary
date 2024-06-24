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
    public interface ITemplateDocumentValue : ITemplateIndex, INotifyPropertyChanged, IBindingPropertyChanged
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
        XDocument Source { get; }

        /// <summary>
        /// Text version of Source. For Data Binding.
        /// </summary>
        String SourceAsText { get; }

        /// <summary>
        /// The XSL Document used for the Transform
        /// </summary>
        XDocument? Transform { get; }

        /// <summary>
        /// Exceptions generated applying the Transform
        /// </summary>
        Exception? Exception { get; }

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
        public XDocument Source { get; } = new XDocument() { Declaration = new XDeclaration("1.0", null, null) };

        /// <inheritdoc/>
        public String SourceAsText { get { return String.Concat(Source.Declaration, Environment.NewLine, Source); } }

        /// <inheritdoc/>
        public XDocument? Transform { get; protected set; }

        /// <inheritdoc/>
        public Exception? Exception { get; protected set; }

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
            }
        }
        XDocument? resultsAsXml = null;

        ITemplateValue templateValue { get; set; }

        /// <summary>
        /// Constructor for Scripting Document Template.
        /// </summary>
        /// <param name="template"></param>
        public TemplateDocumentValue(ITemplateValue template)
        { templateValue = template; }

        /// <summary>
        /// Applies the XSL Transform to the Source to complete the Document
        /// </summary>
        /// <returns></returns>
        public Boolean ApplyTransform()
        { // TODO: Refine to meet the needs. 
            Boolean result = false;
            Exception = null;

            try
            {
                if (templateValue.TransformScript is String)
                { Transform = XDocument.Parse(templateValue.TransformScript); }

                if (Transform is not null)
                {
                    using (XmlReader sourceReader = Source.CreateReader())
                    using (XmlReader transformReader = Transform.CreateReader())
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
                ex.Data.Add("XML", Source);
                Exception = ex;
            }

            OnPropertyChanged(nameof(Source));
            OnPropertyChanged(nameof(Exception));
            OnPropertyChanged(nameof(ResultsAsText));
            OnPropertyChanged(nameof(ResultsAsXml));
            return result;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged is PropertyChangedEventHandler handler)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }
    }
}
