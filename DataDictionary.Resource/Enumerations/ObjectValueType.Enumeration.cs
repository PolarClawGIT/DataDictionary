using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.Resource.Enumerations
{

    /// <summary>
    /// Enumeration support class for Object Property types.
    /// </summary>
    public interface IObjectValueTypeEnumeration : IEnumeration<ObjectValueType>
    {

    }

    /// <summary>
    /// Enumeration support class for Object Property types.
    /// </summary>
    class ObjectValueTypeEnumeration : Enumeration<ObjectValueType, ObjectValueTypeEnumeration>, IObjectValueTypeEnumeration
    {

        ObjectValueTypeEnumeration(ObjectValueType value) : base(value) { }
        ObjectValueTypeEnumeration(ObjectValueType value, String name) : base(value, name) { }

        static ObjectValueTypeEnumeration()
        {
            List<ObjectValueTypeEnumeration> data = new List<ObjectValueTypeEnumeration>()
            {
                new ObjectValueTypeEnumeration(ObjectValueType.Null, String.Empty) { DisplayName = "not defined" },
                new ObjectValueTypeEnumeration(ObjectValueType.String),
                new ObjectValueTypeEnumeration(ObjectValueType.StringXML,"String.XML") { DisplayName = "XML as String" },
                new ObjectValueTypeEnumeration(ObjectValueType.StringRichText,"String.RTF") { DisplayName = "Rich Text Format" },
                new ObjectValueTypeEnumeration(ObjectValueType.StringList,"String.CSV") { DisplayName = "Comma Separated Values" },
                new ObjectValueTypeEnumeration(ObjectValueType.Numeric),
                new ObjectValueTypeEnumeration(ObjectValueType.Boolean),
                new ObjectValueTypeEnumeration(ObjectValueType.DateTime),
                new ObjectValueTypeEnumeration(ObjectValueType.GUID) { DisplayName = "Globally Unique Identifier" },
                new ObjectValueTypeEnumeration(ObjectValueType.NameSpace),
                new ObjectValueTypeEnumeration(ObjectValueType.Enumeration),
                new ObjectValueTypeEnumeration(ObjectValueType.Class),
                new ObjectValueTypeEnumeration(ObjectValueType.Structure),
                new ObjectValueTypeEnumeration(ObjectValueType.Record),
                new ObjectValueTypeEnumeration(ObjectValueType.Interface),
            };

            BuildDictionary(data);
        }
    }
}
