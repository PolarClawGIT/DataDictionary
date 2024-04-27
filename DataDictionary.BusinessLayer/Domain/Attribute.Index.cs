<<<<<<< HEAD
﻿using DataDictionary.DataLayer.DomainData.Attribute;
=======
﻿using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.DomainData.Attribute;
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeIndex : IDomainAttributeKey
    { }

    /// <inheritdoc/>
    public class AttributeIndex : DomainAttributeKey, IAttributeIndex
    {
        /// <inheritdoc cref="DomainAttributeKey(IDomainAttributeKey)"/>
        public AttributeIndex(IAttributeIndex source) : base(source) { }
<<<<<<< HEAD

        /// <inheritdoc/>
        public AttributeIndex(IDomainAttributeKey source) : base(source) { }
=======
    }

    /// <inheritdoc/>
    public interface IAttributeIndexName : IDomainAttributeKeyName
    { }

    /// <inheritdoc/>
    public class AttributeIndexName : DomainAttributeKeyName
    {
        /// <inheritdoc cref="DomainAttributeKeyName(IDomainAttributeKeyName)"/>
        public AttributeIndexName(IAttributeIndexName source) : base(source) { }

        /// <inheritdoc cref="DomainAttributeKeyName(DataLayer.DatabaseData.Table.IDbTableColumnKeyName)"/>
        internal AttributeIndexName(ITableColumnIndexName source): base(source) { }

        /// <inheritdoc cref="DomainAttributeKeyName(DataLayer.DatabaseData.Routine.IDbRoutineParameterKeyName)"/>
        internal AttributeIndexName(IRoutineParameterIndexName source) : base(source) { }
>>>>>>> RenameIndexValue
    }
}
