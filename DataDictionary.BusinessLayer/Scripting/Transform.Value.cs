using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Transform;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITransformValue : ITransformItem, ITransformIndex, ITransformIndexName
    {
        /// <summary>
        /// Transform Script as an XML Document, if possible.
        /// </summary>
        XDocument? TransformDocument { get; }

        /// <summary>
        /// Exception encountered when converting to XML Document.
        /// </summary>
        Exception? TransformException { get; }
    }

    /// <inheritdoc/>
    public class TransformValue : TransformItem, ITransformValue, INamedScopeSourceValue
    {

        /// <inheritdoc/>
        public XDocument? TransformDocument
        {
            get
            {
                if (isDocumentInitialized) { return transformDocument; }
                else
                { // Must wait until TransformScript has a value, then do this once.
                    SetTransformDocument();
                    isDocumentInitialized = true;
                    return transformDocument;
                }
            }
        }
        Boolean isDocumentInitialized = false;
        XDocument? transformDocument = null;

        /// <inheritdoc/>
        public Exception? TransformException { get; protected set; }


        /// <inheritdoc/>
        public TransformValue() : base()
        { PropertyChanged += TransformValue_PropertyChanged; }

        private void TransformValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(TransformScript))
            { SetTransformDocument(); }
        }

        private void SetTransformDocument()
        {
            // Cannot do this until after the class has been loaded.

            TransformException = null;

            try
            {
                if (String.IsNullOrWhiteSpace(TransformScript))
                { transformDocument = null; }
                else
                {
                    LoadOptions options = LoadOptions.None;
                    if (this.AsText) { options = LoadOptions.PreserveWhitespace; }

                    transformDocument = XDocument.Parse(TransformScript, options);
                    OnPropertyChanged(nameof(TransformDocument));
                    OnPropertyChanged(nameof(TransformException));
                }
            }
            catch (Exception ex)
            {
                TransformException = ex;
                transformDocument = null;
                OnPropertyChanged(nameof(TransformDocument));
                OnPropertyChanged(nameof(TransformException));
            }
        }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new TransformIndex(this); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(Scope); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return TransformTitle ?? Scope.ToName(); }

        /// <summary>
        /// Use the XSLT Transform into a String
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public (String result, Exception? exception) TransformToText(XDocument source)
        {
            String result = String.Empty;
            Exception? exception = null;

            try
            {
                if (TransformDocument is not null)
                {
                    using (XmlReader sourceReader = source.CreateReader())
                    using (XmlReader transformReader = TransformDocument.CreateReader())
                    using (StringWriter resultWriter = new StringWriter())
                    {
                        XslCompiledTransform transformer = new XslCompiledTransform();
                        transformer.Load(transformReader);

                        transformer.Transform(sourceReader, null, resultWriter);

                        result = resultWriter.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("XSLT", TransformScript);
                ex.Data.Add("XML", source);
                exception = ex;
            }

            return (result, exception);
        }

        /// <summary>
        /// Use the XSLT Transform into a XDocument
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public (XDocument result, Exception? exception) TransformToXml(XDocument source)
        {
            XDocument result = new XDocument();
            Exception? exception = null;

            try
            {
                if (TransformDocument is not null)
                {
                    using (XmlReader sourceReader = source.CreateReader())
                    using (XmlReader transformReader = TransformDocument.CreateReader())
                    using (XmlWriter resultWriter = result.CreateWriter())
                    {
                        XslCompiledTransform transformer = new XslCompiledTransform();
                        transformer.Load(transformReader);

                        transformer.Transform(sourceReader, resultWriter);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("XSLT", TransformScript);
                ex.Data.Add("XML", source);
                exception = ex;
            }

            return (result, exception);
        }
    }
}
