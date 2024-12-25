using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IAttributeDefinitionValue : IAttributeDefinitionItem, IDefinitionIndex, IAttributeIndex,
        IScopeType, ITemporalValue
    { }

    /// <inheritdoc/>
    public class AttributeDefinitionValue : AttributeDefinitionItem, IAttributeDefinitionValue, IScopeType
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelAttributeDefinition; } }

        /// <inheritdoc/>
        public DataIndex Index => throw new NotImplementedException();

        /// <inheritdoc/>
        public String Title => throw new NotImplementedException();

        /// <inheritdoc/>
        public AttributeDefinitionValue() : base() { }

        /// <inheritdoc/>
        public AttributeDefinitionValue(IAttributeIndex attributeKey) : base(attributeKey) { }
    }
}
