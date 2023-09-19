using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData
    {
        /// <summary>
        /// Returns a list of Attributes for a given key.
        /// </summary>
        /// <param name="source">An item that implements the Entity Key</param>
        /// <returns></returns>
        public IEnumerable<DomainAttributeItem> GetAttributes(IDomainEntityKey source)
        {
            DomainEntityKey key = new DomainEntityKey(source);
            List<DomainAttributeItem> result = new List<DomainAttributeItem>();

            foreach (DomainEntityAliasKey entityAliases in DomainEntityAliases.Where(w => key.Equals(w)).Select(s => new DomainEntityAliasKey(s)))
            {
                foreach (DomainAttributeKey attributeAliases in DomainAttributeAliases.Where(w => entityAliases.Equals(w)).Select(s => new DomainAttributeKey(s)))
                {
                    if (DomainAttributes.FirstOrDefault(w => attributeAliases.Equals(w)) is DomainAttributeItem item)
                    { result.Add(item); }
                }
            }

            return result;
        }
    }
}
