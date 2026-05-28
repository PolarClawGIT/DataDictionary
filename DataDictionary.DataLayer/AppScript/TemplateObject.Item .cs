namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting TemplateObject
    /// </summary>
    public interface ITemplateObjectItem : ITemplateKey, ITemplateObjectKeyName
    {
        /// <summary>
        /// Do not include the matching Object in the results.
        /// NOT filter.
        /// </summary>
        Boolean IsExcluded { get; }

        /// <summary>
        /// Keep Orphaned is an object that is not in the CURRENT model.
        /// This allows a Template to be used in multiple models that are not exactly alike.
        /// </summary>
        Boolean KeepOrphaned { get; }
    }
}
