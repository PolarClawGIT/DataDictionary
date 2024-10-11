using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// Interface for the Security Principle Item.
    /// </summary>
    public interface ISecurityPrincipleItem: 
        ISecurityPrincipleKey, ISecurityPrincipleKeyName,
        IObjectSecurity
    {
        /// <summary>
        /// Display Name of the Principle. Typically the name of the individual.
        /// </summary>
        String? PrincipleName { get; }

        /// <summary>
        /// Additional information (notes) about the Principle.
        /// </summary>
        String? PrincipleAnnotation { get; }

    }

    /// <summary>
    /// Implementation of the Security Principle Item.
    /// </summary>
    [Serializable]
    public class SecurityPrincipleItem : BindingTableRow, ISecurityPrincipleItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? PrincipleId { get { return GetValue<Guid>(nameof(PrincipleId)); } protected set { SetValue(nameof(PrincipleId), value); } }

        /// <inheritdoc/>
        public String? PrincipleLogin { get { return GetValue(nameof(PrincipleLogin)); } set { SetValue(nameof(PrincipleLogin), value); } }

        /// <inheritdoc/>
        public String? PrincipleName { get { return GetValue(nameof(PrincipleName)); } set { SetValue(nameof(PrincipleName), value); } }

        /// <inheritdoc/>
        public String? PrincipleAnnotation { get { return GetValue(nameof(PrincipleAnnotation)); } set { SetValue(nameof(PrincipleAnnotation), value); } }

        /// <inheritdoc/>
        public Boolean AlterValue
        {
            get
            {
                if (GetValue<bool>(nameof(AlterValue), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <inheritdoc/>
        public Boolean AlterSecurity
        {
            get
            {
                if (GetValue<bool>(nameof(AlterSecurity), BindingItemParsers.BooleanTryParse) == true) { return true; }
                else { return false; }
            }
        }

        /// <summary>
        /// Constructor for SecurityPrincipleItem.
        /// </summary>
        public SecurityPrincipleItem() : base()
        { PrincipleId = Guid.NewGuid(); }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(PrincipleId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PrincipleLogin), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(PrincipleName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(PrincipleAnnotation), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AlterValue), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(AlterSecurity), typeof(Boolean)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for SecurityPrincipleItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected SecurityPrincipleItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return new SecurityPrincipleKeyName(this).ToString(); }

    }
}
