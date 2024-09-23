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
                    GetIndex = () => new HelpSubjectIndex(this).AsDataIndex(),
                    GetTitle = () => HelpSubject ?? String.Empty,
                    GetScope = () => Scope,
                    IsTitleChanged = (e) => e.PropertyName is nameof(HelpSubject)
                };
            }

            return modificationValue;
        }

        IDataValue IDataValue.AsDataValue()
        { return AsModificationValue(); }

        // Backing field so the object is not recreated every time AsModificationValue is executed.
        IModificationValue? modificationValue;
    }
}
