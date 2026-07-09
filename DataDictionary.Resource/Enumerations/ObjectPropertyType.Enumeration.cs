using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.Resource.Enumerations
{

    /// <summary>
    /// Enumeration support class for Object Property types.
    /// </summary>
    public interface IObjectPropertyTypeEnumeration : IEnumeration<ObjectPropertyType>
    {

    }

    /// <summary>
    /// Enumeration support class for Object Property types.
    /// </summary>
    class ObjectPropertyTypeEnumeration : Enumeration<ObjectPropertyType, ObjectPropertyTypeEnumeration>, IObjectPropertyTypeEnumeration
    {

        ObjectPropertyTypeEnumeration(ObjectPropertyType value) : base(value) { }
        ObjectPropertyTypeEnumeration(ObjectPropertyType value, String name) : base(value, name) { }

        static ObjectPropertyTypeEnumeration()
        {
            List<ObjectPropertyTypeEnumeration> data = new List<ObjectPropertyTypeEnumeration>()
            {
                new ObjectPropertyTypeEnumeration(ObjectPropertyType.Null, String.Empty) { DisplayName = "not defined" },
                new ObjectPropertyTypeEnumeration(ObjectPropertyType.String),
                new ObjectPropertyTypeEnumeration(ObjectPropertyType.StringXML,"String.XML") { DisplayName = "XML as String" },
                new ObjectPropertyTypeEnumeration(ObjectPropertyType.StringRichText,"String.RTF") { DisplayName = "Rich Text Format" },
                new ObjectPropertyTypeEnumeration(ObjectPropertyType.StringList,"String.CSV") { DisplayName = "Comma Separated Values" },
                new ObjectPropertyTypeEnumeration(ObjectPropertyType.Numeric),
                new ObjectPropertyTypeEnumeration(ObjectPropertyType.Boolean),
                new ObjectPropertyTypeEnumeration(ObjectPropertyType.DateTime),
                new ObjectPropertyTypeEnumeration(ObjectPropertyType.GUID) { DisplayName = "Globally Unique Identifier" },
                new ObjectPropertyTypeEnumeration(ObjectPropertyType.Class),
                new ObjectPropertyTypeEnumeration(ObjectPropertyType.NameSpace),
            };

            BuildDictionary(data);
        }
    }
}
