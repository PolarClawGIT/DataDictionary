using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ScriptingData.Transform;
<<<<<<< HEAD
using System.ComponentModel;
=======
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> RenameIndexValue
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public interface ITransformValue : ITransformItem, ITransformIndex
=======
    public interface ITransformValue : ITransformItem
>>>>>>> RenameIndexValue
    { }

    /// <inheritdoc/>
    public class TransformValue : TransformItem, ITransformValue, INamedScopeValue
    {
        /// <inheritdoc cref="TransformItem()"/>
        public TransformValue() : base()
        { PropertyChanged += OnPropertyChanged; }

        /// <inheritdoc/>
<<<<<<< HEAD
        public NamedScopeKey GetSystemId()
=======
        public TransformValue() : base()
        { PropertyChanged += SchemaValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeKey GetSystemId()
>>>>>>> RenameIndexValue
        { return new NamedScopeKey(TransformId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
<<<<<<< HEAD
        { return new NamedScopePath(this); }
=======
        { return new NamedScopePath(Scope); }
>>>>>>> RenameIndexValue

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return TransformTitle ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
<<<<<<< HEAD
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
=======
        private void SchemaValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
>>>>>>> RenameIndexValue
        {
            if (e.PropertyName is nameof(TransformTitle)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }

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
