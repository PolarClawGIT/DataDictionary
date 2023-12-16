﻿using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Extension Class used with Domain Aliases
    /// </summary>
    static class AliasExtension
    {
        /// <summary>
        /// Parses a AlaisName (qualified DB object Name or .Net NameSpace)
        /// and returns a formated version of the name as well as all the parents of that name.
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        public static List<String> ParseName(String aliasName)
        {
            List<String> result = new List<String>();
            List<String> elements = NameParts(aliasName);
            String newItem = String.Empty;

            // Build list of name and parents.
            foreach (String item in elements)
            {
                if (String.IsNullOrWhiteSpace(newItem)) { newItem = item; }
                else { newItem = String.Format("{0}.{1}", newItem, item); }

                result.Add(newItem);
            }

            return result;
        }

        /// <summary>
        /// Formats the Alias Name into a period delimited string with each element delimited by square brackets.
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        public static String FormatName(String? aliasName)
        {
            if (String.IsNullOrWhiteSpace(aliasName)) { return String.Empty; }

            String result = String.Empty;
            List<String> elements = NameParts(aliasName);

            // Build name
            foreach (String item in elements)
            {
                if (String.IsNullOrWhiteSpace(result)) { result = item; }
                else { result = String.Format("{0}.{1}", result, item); }
            }

            return result;
        }

        /// <summary>
        /// Formats the Alias Name into a period delimited string with each element delimited by square brackets.
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        public static String FormatName(params String?[] aliasName)
        {
            String result = String.Empty;

            // Build name
            foreach (String? item in aliasName)
            {
                if (String.IsNullOrWhiteSpace(item)) { break; }

                if (String.IsNullOrWhiteSpace(result)) { result = String.Format("[{0}]", item); }
                else { result = String.Format("{0}.[{1}]", result, item); }
            }

            return result;
        }

        static List<String> NameParts(String aliasName)
        {
            List<String> elements = new List<String>();
            String parse = aliasName;

            // Parse the name into its pieces.
            while (!String.IsNullOrWhiteSpace(parse))
            {
                String reverse = new String(parse.ToCharArray().Reverse().ToArray());

                if (!parse.Contains(".")
                    && !parse.Contains("[")
                    && !parse.Contains("]"))
                {
                    elements.Add(String.Format("[{0}]", parse));
                    parse = String.Empty;
                }
                else if (!parse.Contains(".")
                    && parse.StartsWith("[")
                    && parse.EndsWith("]"))
                {
                    elements.Add(parse);
                    parse = String.Empty;
                }
                else if (parse.Contains(".")
                    && !parse.Contains("[")
                    && !parse.Contains("]"))
                {
                    Int32 index = reverse.IndexOf(".");
                    String value = parse.Substring(parse.Length - index);
                    elements.Add(String.Format("[{0}]", value));
                    parse = parse.Substring(0, parse.Length - index - 1);
                }
                else if (parse.Contains(".[")
                    && parse.EndsWith("]"))
                {
                    Int32 index = reverse.IndexOf("[.");
                    String value = parse.Substring(parse.Length - index - 1);
                    elements.Add(String.Format("{0}", value));
                    parse = parse.Substring(0, parse.Length - index - 2);
                }
                else if (parse.Contains(".")
                    && parse.Contains("]")
                    && reverse.IndexOf(".") < reverse.IndexOf("]"))
                {
                    Int32 index = reverse.IndexOf(".");
                    String value = parse.Substring(parse.Length - index);
                    elements.Add(String.Format("[{0}]", value));
                    parse = parse.Substring(0, parse.Length - index - 1);
                }
                else
                {
                    Exception ex = new InvalidOperationException("No condition matches parse value");
                    ex.Data.Add(nameof(aliasName), aliasName);
                    ex.Data.Add(nameof(parse), parse);
                    throw ex;
                }
            }

            elements.Reverse();

            return elements;
        }
    }
}
