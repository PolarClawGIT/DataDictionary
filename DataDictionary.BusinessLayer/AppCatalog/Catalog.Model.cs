namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <summary>
    /// Interface used by the Model Catalog Items.
    /// Used to make a reference back to the Catalog object.
    /// </summary>
    interface ICatalogModel
    {
        /// <summary>
        /// The Wrapper Data Object that this object contained within.
        /// </summary>
        ICatalog Model { get; }
    }
}
