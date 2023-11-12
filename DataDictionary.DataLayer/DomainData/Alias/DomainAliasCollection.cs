using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Alias
{
    /// <summary>
    /// Generic Base class for Domain Alias
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <remarks>Base class, implements the Read and Write.</remarks>
    public class DomainAliasCollection<TItem> : BindingTable<TItem>
        where TItem : BindingTableRow, IDomainAliasItem, new()
    {
        /// <inheritdoc/>
        /// <remarks>
        /// This will also add parents, if needed.
        /// </remarks>
        protected override void InsertItem(int index, TItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (item.AliasName == null) throw new ArgumentNullException(nameof(item.AliasName));

            List<String> parsedValues = ParseAliasName(item.AliasName);

            foreach (String value in parsedValues)
            {
                // If in list go to next
                // if not in list, add to list
                // Look at using a SortedList as an indexer to speed up processes.
            }

            base.InsertItem(index, item);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Children may need to be removed as well.
        /// </remarks>
        protected override void RemoveItem(int index)
        {

            //If leaf node, remove it
            //if not leaf node, remove children first
            base.RemoveItem(index);
        }

        /// <summary>
        /// Parses a AlaisName (qualified DB object Name or .Net NameSpace)
        /// and returns a formated version of the name as well as all the parents of that name.
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        public List<String> ParseAliasName(String aliasName)
        {
            List<String> result = new List<String>();
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
    }

    /// <summary>
    /// Default List/Collection of Domain Alias
    /// </summary>
    public class DomainAliasCollection : DomainAliasCollection<DomainAliasItem>
    { }
}
