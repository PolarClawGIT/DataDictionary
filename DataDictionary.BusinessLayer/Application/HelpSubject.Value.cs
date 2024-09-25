using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.ApplicationData;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IHelpSubjectValue : IHelpItem,
        IHelpSubjectIndex, IHelpSubjectIndexNameSpace,
        IModificationValue
    { }

    /// <inheritdoc/>
    public class HelpSubjectValue : HelpItem, IHelpSubjectValue
    {
        IModificationValue modificationValue; // Backing field for IModificationValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return modificationValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return modificationValue.Title; } }

        /// <inheritdoc/>
        public HelpSubjectValue() : base()
        {
            modificationValue = new ModificationValue(this)
            {
                GetIndex = () => new HelpSubjectIndex(this),
                GetTitle = () => HelpSubject ?? String.Empty,
                GetScope = () => Scope,
                GetModification = () => Modification,
                GetModifiedBy = () => ModifiedBy ?? String.Empty,
                GetModifiedOn = () => ModifiedOn?? DateTime.MaxValue,
                IsTitleChanged = (e) => e.PropertyName is nameof(HelpSubject)
            };
        }


    }
}
