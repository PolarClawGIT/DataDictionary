using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Default Builders for the different supported objects.
    /// </summary>
    public static class XmlBuilderExtension
    {
        /// <summary>
        /// Try/Get a specific XmlBuilder from the list.
        /// </summary>
        /// <param name="builders"></param>
        /// <param name="rootKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetXmlBuilder(this IEnumerable<XmlBuilder> builders, XmlBuilderIndex rootKey, [NotNullWhen(true)] out XmlBuilder? value)
        {
            value = null;

            if (builders.Count(w => rootKey.Equals(w)) == 1)
            { value = builders.Single(w => rootKey.Equals(w)); return true; }
            else { return false; }
        }

        /// <summary>
        /// Gets the NamedScopeSource for the xmlBuilder
        /// </summary>
        /// <param name="namedScope"></param>
        /// <param name="templateObject"></param>
        /// <returns></returns>
        public static IEnumerable<INamedScopeSourceValue> GetData(this INamedScopeData namedScope, ITemplateObjectNameIndex templateObject)
        {
            TemplateObjectNameIndex key = new TemplateObjectNameIndex(templateObject);
            PathIndex path = new PathIndex(key.ObjectPath);

            return namedScope.PathKeys(path).Select(s => namedScope.GetData(s));
        }

        /// <summary>
        /// Try to Parse a String into an XDocument.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="document"></param>
        /// <param name="exception"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        /// <remarks>Do not use XDocument.ToString. Use <see cref="Parse(XDocument)"/></remarks>
        public static Boolean TryParse(this String source, [NotNullWhen(true)] out XDocument? document, [NotNullWhen(false)] out Exception? exception, LoadOptions option = LoadOptions.PreserveWhitespace)
        {
            document = null;
            exception = null;

            try
            {
                document = XDocument.Parse(source, option);
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        /// <summary>
        /// Parse an XDocument into a String.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <remarks>XDocument.ToString removes the xml declaration. This keeps the declaration.</remarks>
        public static String Parse(this XDocument source)
        {
            //Note: Online Sources use StringWriter to convert an XDocument to String.
            //This alters the Declaration of the XDocument and forces it to UTF-16, which is the format of Windows Strings.
            //Other solutions run the XDocument thru several more steps that also alter the Declaration or require
            //that the correct Declaration to be known and that is be compatible with a String Encoding.
            //This approach is to add the Declaration using the StringBuilder as a simple string.

            StringBuilder result = new StringBuilder();

            // XDocument.ToString() does not contain the Header, put that back in.
            if (source.Declaration is XDeclaration declaration)
            { result.Append(declaration.ToString()); }
            //else { result.AppendLine(new XDeclaration(null, null, null).ToString()); }

            result.AppendLine(source.ToString());

            return result.ToString();
        }

    }
}

