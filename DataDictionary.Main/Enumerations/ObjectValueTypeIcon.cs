using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Enumerations
{
    /// <summary>
    /// Handles Icons for the ObjectValueTypeIcon
    /// </summary>
    static class ObjectValueTypeIcon
    {
        static Dictionary<ObjectValueType, Icon> typeIconMap = new Dictionary<ObjectValueType, Icon>()
        {
            { ObjectValueType.Null, Resources.Icon_Field },
            { ObjectValueType.String, Resources.Icon_String },
            { ObjectValueType.StringXML, Resources.Icon_XMLElement },
            { ObjectValueType.StringRichText, Resources.Icon_RichTextBox },
            { ObjectValueType.StringList, Resources.Icon_RadioButtonList },
            { ObjectValueType.Numeric, Resources.Icon_Numeric },
            { ObjectValueType.Boolean, Resources.Icon_Boolean },
            { ObjectValueType.DateTime, Resources.Icon_DateTime },
            { ObjectValueType.GUID, Resources.Icon_UniqueIdentifier },
            { ObjectValueType.Enumeration, Resources.Icon_Enumeration },
            { ObjectValueType.NameSpace, Resources.Icon_Namespace },
            { ObjectValueType.Class, Resources.Icon_Class },
            { ObjectValueType.Structure, Resources.Icon_Structure },
            { ObjectValueType.Record, Resources.Icon_Class },
            { ObjectValueType.Interface, Resources.Icon_Interface },
        };

        /// <summary>
        /// Try/Get the Icon for the XmlTypeCode.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetIcon(this ObjectValueType type, [NotNullWhen(true)] out Icon? value)
        {
            if (typeIconMap.ContainsKey(type))
            { value = typeIconMap[type]; return true; }
            else { value = null; return false; }
        }

        /// <summary>
        /// Try/Get the Icon converted to a Image (16x16) for the XmlTypeCode.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetImage(this ObjectValueType type, [NotNullWhen(true)] out Image? value)
        {
            if (type.TryGetIcon(out Icon? result))
            { value = result.GetSmallImage(); return true; }
            else { value = null; return false; }
        }

        /// <summary>
        /// Adds a list of Images to an Image List for the XmlTypeCode listed.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="types"></param>
        public static void AddImages(this ImageList target, params IEnumerable<ObjectValueType> types)
        {
            foreach (ObjectValueType item in types)
            {
                if (!target.Images.ContainsKey(item.GetName())
                    && item.TryGetImage(out Image? value))
                { target.Images.Add(item.GetName(), value); }
            }
        }
    }
}
