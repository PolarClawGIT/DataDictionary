using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Application
{
    /// <summary>
    /// Interface component for the Transform data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    [Obsolete("To be replaced by Scripting Objects")]
    public interface ITransformData :
        IBindingData<TransformItem>,
        ISaveData, ILoadData
    {
        /// <summary>
        /// Transform the source using the Transform Key listed into XML.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        (XDocument? result, Exception? exception) TransformToXml(ITransformKey key, XDocument source);

        /// <summary>
        /// Transform the source using the Transform Key listed into Text.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        (String? result, Exception? exception) TransformToText(ITransformKey key, XDocument source);
    }

    [Obsolete("To be replaced by Scripting Objects")]
    class TransformData : TransformCollection, ITransformData
    {
        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Transform</remarks>
        public IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }

        /// <inheritdoc/>
        public (string? result, Exception? exception) TransformToText(ITransformKey key, XDocument source)
        {
            TransformKey transformKey = new TransformKey(key);
            String? result = null;
            Exception? exception = null;

            if (this.FirstOrDefault(w => transformKey.Equals(w)) is TransformItem item && item.TransformScript is not null)
            {
                try
                {
                    using (XmlReader sourceReader = source.CreateReader())
                    using (XmlReader transformReader = item.TransformScript.CreateReader())
                    using (StringWriter resultWriter = new StringWriter())
                    {
                        XslCompiledTransform transformer = new XslCompiledTransform();
                        transformer.Load(transformReader);

                        transformer.Transform(sourceReader, null, resultWriter);

                        result = resultWriter.ToString();
                    }
                }
                catch (Exception ex)
                {
                    ex.Data.Add("XSLT", item.TransformScript);
                    ex.Data.Add("XML", source);
                    exception = ex;
                }
            }
            else
            {
                exception = new ArgumentOutOfRangeException(nameof(key));
                exception.Data.Add(nameof(key), key.ToString());
                throw exception;
            }

            return (result, exception);
        }

        /// <inheritdoc/>
        public (XDocument? result, Exception? exception) TransformToXml(ITransformKey key, XDocument source)
        {
            String? value = null;
            XDocument? result = null;
            Exception? exception = null;
            (value, exception) = TransformToText(key, source);

            if (exception is null && value is not null)
            {
                try
                {   result = XDocument.Parse(value); }
                catch (Exception ex)
                {
                    ex.Data.Add("Parse", value);
                    exception = ex;
                }
            }

            return (result, exception);
        }
    }
}
