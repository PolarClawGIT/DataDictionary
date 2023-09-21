using DataDictionary.DataLayer.ApplicationData.Property;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
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

        /// <summary>
        /// Gets the Attribute Properties given the key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DomainAttributePropertyItem> GetProperties(IDomainAttributeKey source)
        {
            DomainAttributeKey key = new DomainAttributeKey(source);
            return DomainAttributeProperties.Where(w => key.Equals(w)); 
        }

        /// <summary>
        /// Gets the Attribute Aliases given the key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DomainAttributeAliasItem> GetAliases(IDomainAttributeKey source)
        {
            DomainAttributeKey key = new DomainAttributeKey(source);
            return DomainAttributeAliases.Where(w => key.Equals(w)); 
        }
    }
}
