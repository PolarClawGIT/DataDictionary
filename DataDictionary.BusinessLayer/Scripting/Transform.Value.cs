using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Transform;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITransformValue : ITransformItem, ITransformIndex
    { }

    /// <inheritdoc/>
    public class TransformValue : TransformItem, ITransformValue, INamedScopeSourceValue
    {
        /// <inheritdoc/>
        public TransformValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new DataLayerIndex(TransformId); }

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
