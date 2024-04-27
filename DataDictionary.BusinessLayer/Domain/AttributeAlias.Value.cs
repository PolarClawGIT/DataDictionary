<<<<<<< HEAD
﻿using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.DomainData.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
=======
﻿using DataDictionary.DataLayer.DomainData.Attribute;
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public interface IAttributeAliasValue: IDomainAttributeAliasItem
=======
    public interface IAttributeAliasValue: IDomainAttributeAliasItem, IAttributeIndex
>>>>>>> RenameIndexValue
    { }

    /// <inheritdoc/>
    public class AttributeAliasValue : DomainAttributeAliasItem, IAttributeAliasValue
    {
        /// <inheritdoc/>
        public AttributeAliasValue() : base() { }

<<<<<<< HEAD
        /// <inheritdoc/>
        public AttributeAliasValue(IAttributeIndex key) : base(key) { }
=======
        /// <inheritdoc cref="DomainAttributeAliasItem(IDomainAttributeKey)"/>
        public AttributeAliasValue(IAttributeIndex key) : base(key) { }

        /// <inheritdoc/>
        internal AttributeAliasValue(IDomainAttributeKey key) : base(key) { }
>>>>>>> RenameIndexValue
    }
}
