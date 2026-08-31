using System.Diagnostics.CodeAnalysis;

namespace Toolbox.BindingTable
{
    /// <summary>
    /// LINQ Extension methods
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// Similar to IEnumerable{TSource}.Single except the statement is pre-wrapped in a Try/Catch block.
        /// Exceptions are not thrown.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetSingle<TSource>(this IEnumerable<TSource> source, [NotNullWhen(true)] out TSource? value)
        {   //TODO: Need to find all the places where this should be used.
            //Most will use an If(IEnumerable{TSource}.FirstOrDefault() is {TSource} value).
            value = default;

            try
            { value = source.Single(); return value is TSource; }
            catch (Exception)
            { return false; }
        }

        /// <summary>
        /// Similar to IEnumerable{TSource}.Single except the statement is pre-wrapped in a Try/Catch block.
        /// Exceptions are not thrown.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetSingle<TSource>(this IEnumerable<TSource> source, Func<TSource, Boolean> predicate, [NotNullWhen(true)] out TSource? value)
        {   //TODO: Need to find all the places where this should be used.
            //Most will use an If(IEnumerable{TSource}.FirstOrDefault() is {TSource} value).
            value = default;

            try
            { value = source.Single(w => predicate(w)); return value is TSource; }
            catch (Exception)
            { return false; }
        }

    }
}
