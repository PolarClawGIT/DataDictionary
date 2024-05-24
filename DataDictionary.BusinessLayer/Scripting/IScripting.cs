using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Data Object that support XML Scripting
    /// </summary>
    public interface IScripting
    {
        /// <summary>
        /// Returns the data as XElement.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        XElement GetXElement(IBusinessLayerData data, IEnumerable<SchemaElementValue>? options);
    }

    /// <inheritdoc/>
    public interface IScripting<TSource> : IScripting
        where TSource : class
    {   // This allows the UI layer to reference the generic IBusinessLayerData
        // but call the type specific implementation of GetXElement.

        XElement IScripting.GetXElement(IBusinessLayerData data, IEnumerable<SchemaElementValue>? options)
        {   // Override the base GetXElement to cast the call into TSource

            if (data is TSource value)
            { return GetXElement(value, options); }
            else
            {
                Exception ex = new InvalidCastException("Could not to cast IBusinessLayerData to TSource");
                ex.Data.Add(nameof(TSource), typeof(TSource).Name);
                ex.Data.Add(nameof(IBusinessLayerData), data.GetType().Name);

                throw ex;
            }
        }

        /// <inheritdoc cref="IScripting.GetXElement"/>
        XElement GetXElement(TSource data, IEnumerable<SchemaElementValue>? options);
    }
}
