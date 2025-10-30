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

        /// <summary>
        /// Removes specfic elements of the given collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static void RemoveRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            foreach (T item in source.ToList())
            { target.Remove(item); }
        }

        /// <summary>
        /// Removes elements that meet the condition of the given collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="filter"></param>
        public static void RemoveRange<T>(this ICollection<T> target, Func<T,Boolean> filter)
        {
            foreach (T item in target.Where(w => filter(w)).ToList())
            { target.Remove(item); }
        }
    }
}
