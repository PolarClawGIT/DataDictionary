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
    /// Interface for the Security Principal Item.
    /// </summary>
    public interface IPrincipalItem: 
        IPrincipalKey, IPrincipalKeyName,
        IPrincipalName
    {
        /// <summary>
        /// Additional information (notes) about the Principal.
        /// </summary>
        String? PrincipalAnnotation { get; }

    }

    /// <summary>
    /// Implementation of the Security Principal Item.
    /// </summary>
    [Serializable]
    public class PrincipalItem : BindingTableRow, IPrincipalItem, ISerializable
    {
        /// <inheritdoc/>
        public Guid? PrincipalId { get { return GetValue<Guid>(nameof(PrincipalId)); } protected set { SetValue(nameof(PrincipalId), value); } }

        /// <inheritdoc/>
        public String? PrincipalLogin { get { return GetValue(nameof(PrincipalLogin)); } set { SetValue(nameof(PrincipalLogin), value); } }

        /// <inheritdoc/>
        public String? PrincipalName { get { return GetValue(nameof(PrincipalName)); } set { SetValue(nameof(PrincipalName), value); } }

        /// <inheritdoc/>
        public String? PrincipalAnnotation { get { return GetValue(nameof(PrincipalAnnotation)); } set { SetValue(nameof(PrincipalAnnotation), value); } }

        /// <summary>
        /// Constructor for PrincipalItem.
        /// </summary>
        public PrincipalItem() : base()
        { PrincipalId = Guid.NewGuid(); }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(PrincipalId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(PrincipalLogin), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(PrincipalName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(PrincipalAnnotation), typeof(String)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        #region ISerializable
        /// <summary>
        /// Serialization Constructor for PrincipalItem.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected PrincipalItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return new PrincipalKeyName(this).ToString(); }

    }
}
