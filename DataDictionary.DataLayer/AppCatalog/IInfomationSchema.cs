namespace DataDictionary.DataLayer.AppCatalog
{
    /// <summary>
    /// An Object that can import the InfomationSchema (MetaData) for Collection
    /// </summary>
    /// <typeparam name="TData">This is one of the base interfaces.</typeparam>
    public interface IInfomationSchemaCollection<TData>
    {
        //Note: Catalog uses a different implementation that is not described here.

        /// <summary>
        /// Import function for InfomationSchema
        /// </summary>
        /// <param name="catalogKey"></param>
        /// <param name="source"></param>
        void Import(ICatalogKey catalogKey, IEnumerable<TData> source);
    }

    /// <summary>
    /// An Object that can import the InfomationSchema (MetaData) for Item
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public interface IInfomationSchemaItem<TData, TItem>
        where TItem : class, TData, new()
    {
        //Note: Catalog uses a different implementation that is not described here.

        /// <summary>
        /// Used to Update based on Information Schema values
        /// </summary>
        /// <param name="source"></param>
        void Update(TData source);

        /// <summary>
        /// Generic constructor of TResult
        /// </summary>
        /// <param name="catalog"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        abstract static TResult Create<TResult>(ICatalogKey catalog, TData source)
            where TResult: TItem, new();
    }
}
