using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppGeneral;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppGeneral
{
    /// <inheritdoc/>
    public interface IHelpSubjectValue : IHelpSubjectItem, IScopeType,
        IHelpSubjectIndex, IHelpSubjectIndexNameSpace,
        IDataValue, ITemporalValue
    { }

    /// <inheritdoc/>
    public class HelpSubjectValue : HelpSubjectItem, IHelpSubjectValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ApplicationHelpPage;

        /// <inheritdoc/>
        public HelpSubjectValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new HelpSubjectIndex(this),
                GetTitle = () => HelpSubject ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(HelpSubject)
            };
        }
    }
}
