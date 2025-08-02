namespace DataDictionary.Resource
{
    public static class CollectionExtension
    {
        /// <summary>
        /// Adds the elements of the given collection to the end of this collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <remarks>
        /// ICollection (and direved types, does not include AddRange).
        /// List has a speciallized AddRange that is better then this generic version.
        /// </remarks>
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            foreach (T item in source.ToList())
            { target.Add(item); }
        }
    }
}
