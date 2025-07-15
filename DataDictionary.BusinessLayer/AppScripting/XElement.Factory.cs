using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Base compoents for a XElement Builder Factory.
    /// </summary>
    public interface IXElementFactory
    {
        /// <summary>
        /// Builds a list of XElement Builders used to build XElements.
        /// </summary>
        /// <returns></returns>
        static abstract IEnumerable<XElementBuilder> CreateXElementBuilders();
    }

    /// <summary>
    /// Base compoents for a XElement Builder Factory that has a generic parameter.
    /// </summary>
    /// <typeparam name="T">generic parameter</typeparam>
    public interface IXElementFactory<T>
    {
        /// <summary>
        /// Builds a list of XElement Builders used to build XElements.
        /// </summary>
        /// <returns></returns>
        static abstract IEnumerable<XElementBuilder> CreateXElementBuilders(T paramter);
    }
}
