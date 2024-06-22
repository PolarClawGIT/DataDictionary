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
    /// Scripting Engine Template Document
    /// </summary>
    public class TemplateDocumentValue : ITemplateIndex, INotifyPropertyChanged, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public Guid? TemplateId { get { return templateValue.TemplateId; } }

        /// <summary>
        /// The Name of the Element for this Document
        /// </summary>
        public required String ElementName { get; init; }

        /// <summary>
        /// The Document file Name
        /// </summary>
        public FileInfo? DocumentName
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
                    String fileName = String.Format("{0}{1}{2}.{3}",
                        templateValue.DocumentPrefix ?? String.Empty,
                        ElementName,
                        templateValue.DocumentSuffix ?? String.Empty,
                        templateValue.DocumentExtension ?? "xml");

                    return new FileInfo(Path.Combine(directoryName, fileName));
                }
                else { return null; }
            }
        }

        /// <summary>
        /// The Script file Name
        /// </summary>
        public FileInfo? ScriptName
        {
            get
            {
                if (templateValue.RootDirectory is DirectoryType root &&
                    new DirectoryTypeKey(root).ToDirectoryInfo() is DirectoryInfo folder)
                {
                    var directoryName =
                        Path.Combine(
                            folder.FullName,
                            templateValue.ScriptDirectory ?? String.Empty);
                    var fileName = String.Format("{0}{1}{2}.{3}",
                        templateValue.ScriptPrefix ?? String.Empty,
                        ElementName,
                        templateValue.ScriptSuffix ?? String.Empty,
                        templateValue.ScriptExtension ?? "txt");

                    return new FileInfo(Path.Combine(directoryName, fileName));
                }
                else { return null; }
            }
        }

        /// <summary>
        /// The XML Document that was used as the Source
        /// </summary>
        public XDocument Source { get; } = new XDocument();

        /// <summary>
        /// The XSL Document used for the Transform
        /// </summary>
        public XDocument? Transform { get; protected set; }

        /// <summary>
        /// Exceptions generated applying the Transform
        /// </summary>
        public Exception? Exception { get; protected set; }

        /// <summary>
        /// The Results of the Transform as plain text (may contain formated XML).
        /// </summary>
        public String? ResultsAsText { get; protected set; }

        /// <summary>
        /// The Result of the Transform as XML (null if Text transform only)
        /// </summary>
        public XDocument? ResultsAsXml { get; protected set; }

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
                                ResultsAsXml = null;
                            }
                        }
                        else if (templateValue.ScriptAs is ScriptAsType.XML)
                        {
                            ResultsAsXml = new XDocument();
                            using (XmlWriter resultAsXml = ResultsAsXml.CreateWriter())
                            {
                                transformer.Transform(sourceReader, resultAsXml);
                                ResultsAsText = ResultsAsXml.ToString();
                            }
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
