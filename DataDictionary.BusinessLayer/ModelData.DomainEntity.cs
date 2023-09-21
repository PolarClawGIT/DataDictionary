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
        /// Returns a list of Entities for a given key
        /// </summary>
        /// <param name="source">Something that implements the Attribute Key</param>
        /// <returns></returns>
        public IEnumerable<DomainEntityItem> GetEntities(IDomainAttributeKey source)
        {
            DomainAttributeKey key = new DomainAttributeKey(source);
            List<DomainEntityItem> result = new List<DomainEntityItem>();

            foreach (DomainEntityAliasKey attributeAliases in DomainAttributeAliases.Where(w => key.Equals(w)).Select(s => new DomainEntityAliasKey(s)))
            {
                foreach (DomainEntityKey entityAliases in DomainEntityAliases.Where(w => attributeAliases.Equals(w)).Select(s => new DomainEntityKey(s)))
                {
                    if (DomainEntities.FirstOrDefault(w => entityAliases.Equals(w)) is DomainEntityItem item)
                    { result.Add(item); }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the Entity Properties given the key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DomainEntityPropertyItem> GetProperties(IDomainEntityKey source)
        {
            DomainEntityKey key = new DomainEntityKey(source);
            return DomainEntityProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets the Entity Aliases given the key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<DomainEntityAliasItem> GetAliases(IDomainEntityKey source)
        {
            DomainEntityKey key = new DomainEntityKey(source);
            return DomainEntityAliases.Where(w => key.Equals(w));
        }
    }
}
