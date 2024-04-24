using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public class AttributeKeyName : DomainAttributeKeyName
    {
        /// <inheritdoc/>
        public AttributeKeyName(IDomainAttributeKeyName source) : base(source) { }

        /// <inheritdoc/>
        public AttributeKeyName(IDbTableColumnKeyName source) : base(source) { }

        /// <inheritdoc/>
        public AttributeKeyName(IDbRoutineParameterKeyName source) : base(source) { }
    }
}
