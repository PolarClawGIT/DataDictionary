namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Interface used by the Catalog Items.
    /// Used to make a reference back to the Catalog object.
    /// </summary>
    interface ICatalogReference
    {
        /// <summary>
        /// The Wrapper Data Object that this object contained within.
        /// </summary>
        ICatalog Catalog { get; }
    }
}
