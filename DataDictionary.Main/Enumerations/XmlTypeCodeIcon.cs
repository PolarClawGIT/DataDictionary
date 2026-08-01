using DataDictionary.Main.Properties;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace DataDictionary.Main.Enumerations
{
    static class XmlNodeTypeIcon
    {
        /// <summary>
        /// Try/Get the Icon for the Scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetIcon(this XmlNodeType scope, [NotNullWhen(true)] out Icon? value)
        {
            if (iconMap.ContainsKey(scope))
            { value = iconMap[scope]; return true; }
            else { value = null; return false; }
        }

        /// <summary>
        /// Try/Get the Icon converted to a Image (16x16) for the Scope.
        /// </summary>
        /// <param name="renderAs"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetImage(this XmlNodeType renderAs, [NotNullWhen(true)] out Image? value)
        {
            if (renderAs.TryGetIcon(out Icon? result))
            { value = result.GetSmallImage(); return true; }
            else { value = null; return false; }
        }

        /// <summary>
        /// Adds a list of Images to an Image List for the Scopes listed.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="renderTypes"></param>
        public static void AddImages(this ImageList target, params IEnumerable<XmlNodeType> renderTypes)
        {
            foreach (var item in renderTypes)
            {
                if (Enum.GetName(item) is String name
                    && !target.Images.ContainsKey(name)
                    && item.TryGetImage(out Image? value))
                { target.Images.Add(name, value); }
            }
        }

        static Dictionary<XmlNodeType, Icon> iconMap = new Dictionary<XmlNodeType, Icon>()
        {
            { XmlNodeType.None,         Resources.Icon_XMLElementNone },
            { XmlNodeType.Element,      Resources.Icon_XMLElement },
            { XmlNodeType.Text,         Resources.Icon_XMLElementText },
            { XmlNodeType.CDATA,        Resources.Icon_XMLCDataTag },
            { XmlNodeType.Attribute,    Resources.Icon_XMLAttribute },
        };

    }
}
