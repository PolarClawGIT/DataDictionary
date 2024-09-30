using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.ApplicationData;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IHelpSubjectValue : IHelpItem,
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
