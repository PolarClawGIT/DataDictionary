using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Enumerations
{
    /// <summary>
    /// Handles Icons for the XmlTypeCode
    /// </summary>
    static partial class XmlValueTypeIcon
    {
        static Dictionary<XmlValueType, Icon> typeIconMap = new Dictionary<XmlValueType, Icon>()
        {
            {XmlValueType.None, Resources.Icon_Field },
            {XmlValueType.String, Resources.Icon_String },
            {XmlValueType.Integer, Resources.Icon_Numeric },
            {XmlValueType.Long, Resources.Icon_Numeric },
            {XmlValueType.Boolean, Resources.Icon_Boolean },
            {XmlValueType.Decimal, Resources.Icon_Numeric },
            {XmlValueType.Float, Resources.Icon_Numeric },
            {XmlValueType.Double, Resources.Icon_Numeric },
            {XmlValueType.DateTime, Resources.Icon_DateTime },
            {XmlValueType.Guid, Resources.Icon_UniqueIdentifier },
            {XmlValueType.Class, Resources.Icon_Class },
            {XmlValueType.Enum, Resources.Icon_Enumeration },
            {XmlValueType.Xml, Resources.Icon_XMLElement },
            {XmlValueType.RichText, Resources.Icon_RichTextBox },
            {XmlValueType.List, Resources.Icon_RadioButtonList },
        };

        /// <summary>
        /// Try/Get the Icon for the XmlTypeCode.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetIcon(this XmlValueType type, [NotNullWhen(true)] out Icon? value)
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
        public static Boolean TryGetImage(this XmlValueType type, [NotNullWhen(true)] out Image? value)
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
        public static void AddImages(this ImageList target, params IEnumerable<XmlValueType> types)
        {
            foreach (XmlValueType item in types)
            {
                if (!target.Images.ContainsKey(item.GetName())
                    && item.TryGetImage(out Image? value))
                { target.Images.Add(item.GetName(), value); }
            }
        }
    }
}
