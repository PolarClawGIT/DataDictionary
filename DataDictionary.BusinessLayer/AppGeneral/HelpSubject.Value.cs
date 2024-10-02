using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppGeneral;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppGeneral
{
    /// <inheritdoc/>
    public interface IHelpSubjectValue : IHelpItem, IScopeType,
        IHelpSubjectIndex, IHelpSubjectIndexNameSpace,
        ITemporalValue
    { }

    /// <inheritdoc/>
    public class HelpSubjectValue : HelpItem, IHelpSubjectValue
    {
        ITemporalValue modificationValue; // Backing field for IModificationValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return modificationValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return modificationValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ApplicationHelpPage;

        /// <inheritdoc/>
        public HelpSubjectValue() : base()
        {
            modificationValue = TemporalValue.Create(new DataValue(this)
            {
                GetIndex = () => new HelpSubjectIndex(this),
                GetTitle = () => HelpSubject ?? String.Empty,
                GetScope = () => Scope,
                IsTitleChanged = (e) => e.PropertyName is nameof(HelpSubject)
            }, this);
        }


    }
}
