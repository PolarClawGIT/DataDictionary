using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData.Definition
{
    /// <summary>
    /// Interface for the Domain Definition
    /// </summary>
    public interface IDomainDefinition : IDomainDefinitionKey
    {
        /// <summary>
        /// Definition Summary (Plain Text, limited length)
        /// </summary>
        public String? DefinitionSummary { get; }

        /// <summary>
        /// Definition Text (Rich Text)
        /// </summary>
        public String? DefinitionText { get; }

    }
}
