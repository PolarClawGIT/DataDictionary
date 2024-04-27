<<<<<<< HEAD
﻿using DataDictionary.DataLayer.ApplicationData.Property;
=======
﻿using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Property;
using System.Xml.Linq;
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IPropertyValue : IPropertyItem, IPropertyIndex
    { }

    /// <inheritdoc/>
<<<<<<< HEAD
    public class PropertyValue : PropertyItem, IPropertyValue
    {
        /// <inheritdoc/>
        public PropertyValue() : base() { }
    }
=======
    public class PropertyValue : PropertyItem
    { }
>>>>>>> RenameIndexValue
}
