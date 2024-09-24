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
        /// <inheritdoc/>
        public IModificationValue AsModificationValue()
        {
            if (modificationValue is null)
            {
                modificationValue = new ModificationValue(this)
                {
                    GetIndex = () => new HelpSubjectIndex(this),
                    GetTitle = () => HelpSubject ?? String.Empty,
                    GetScope = () => Scope,
                    IsTitleChanged = (e) => e.PropertyName is nameof(HelpSubject)
                };
            }

            return modificationValue;
        }
        IModificationValue? modificationValue; // Backing field for AsModificationValue
    }
}
