using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <summary>
    /// Item can be cast as a general Object Security Value
    /// </summary>
    public interface IObjectValue : IDataValue, IObjectAuthorization, IObjectAccess
    { }

    /// <summary>
    /// Implementation for an Item can be cast as a Object Security Value
    /// </summary>
    class ObjectValue : DataValue, IObjectValue
    {
        /// <inheritdoc/>
        public Boolean IsGrant { get { return GetIsGrant(); } set { SetIsGrant(value); } }

        /// <inheritdoc/>
        public Boolean IsDeny { get { return GetIsDeny(); } set { SetIsDeny(value); } }

        /// <inheritdoc/>
        public Boolean AlterValue { get { return GetAlterValue(); } }

        /// <inheritdoc/>
        public Boolean AlterSecurity { get { return GetAlterSecurity(); } }

        /// <summary>
        /// Function that returns the AlterValue of the source.
        /// </summary>
        public required Func<Boolean> GetAlterValue { get; init; }

        /// <summary>
        /// Function that returns the AlterSecurity of the source.
        /// </summary>
        public required Func<Boolean> GetAlterSecurity { get; init; }

        /// <summary>
        /// Function that returns the IsGrant of the source.
        /// </summary>
        public required Func<Boolean> GetIsGrant { get; init; }

        /// <summary>
        /// Action that sets the IsGrant of the source.
        /// </summary>
        public required Action<Boolean> SetIsGrant { get; init; }

        /// <summary>
        /// Function that returns the IsDeny of the source.
        /// </summary>
        public required Func<Boolean> GetIsDeny { get; init; }

        /// <summary>
        /// Action that sets the IsDeny of the source.
        /// </summary>
        public required Action<Boolean> SetIsDeny { get; init; }

        public ObjectValue(IBindingPropertyChanged source) : base(source)
        { }

        public static ObjectValue Create<TSource>(DataValue baseValue, TSource source)
            where TSource : IBindingPropertyChanged, IObjectValue
        {
            return new ObjectValue(source)
            {
                GetIndex = baseValue.GetIndex,
                GetScope = baseValue.GetScope,
                GetTitle = baseValue.GetTitle,
                IsTitleChanged = baseValue.IsTitleChanged,
                GetIsGrant = () => source.IsGrant,
                GetIsDeny = () => source.IsDeny,
                SetIsGrant = (value) => source.IsGrant = value,
                SetIsDeny = (value) => source.IsDeny = value,
                GetAlterValue = () => source.AlterValue,
                GetAlterSecurity = () => source.AlterSecurity,
            };
        }
    }
}
