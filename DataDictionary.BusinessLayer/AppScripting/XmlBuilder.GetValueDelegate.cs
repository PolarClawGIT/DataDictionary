using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.BusinessLayer.AppScripting
{
    partial class XmlBuilder
    {   // These are overrides to handle getting data for specific data types.


        /// <summary>
        /// GetValue function to handle ScopeType.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual String? GetValueDelegate(ScopeType value)
        { return value.GetName(); }
    }
}
