using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface for a Path (aka NameSpace, Alias)
    /// </summary>
    public interface IPathItem : IKey
    {
        /// <summary>
        /// Name of the Member
        /// </summary>
        String Member { get; }

        /// <summary>
        /// Path of the Member.
        /// </summary>
        /// <remarks>Formated, period delimited and square bracket qualified.</remarks>
        String MemberPath { get; }

        /// <summary>
        /// Path and Name of the Member.
        /// </summary>
        /// <remarks>Formated, period delimited and square bracket qualified.</remarks>
        String MemberFullPath { get; }
    }

    /// <summary>
    /// Interface for a class that has a Path
    /// </summary>
    public interface IPathIndex
    {
        /// <summary>
        /// The NamedPath for the Value
        /// </summary>
        PathIndex Path { get; }
    }

    /// <summary>
    /// Implementation for a Path (aka NameSpace, Alias)
    /// </summary>
    public class PathIndex : IPathItem, IKeyComparable<IPathItem>
    {
        /// <summary>
        /// List of Parts of the Path.
        /// </summary>
        protected List<String> pathParts = new List<String>();

        /// <inheritdoc/>
        public String Member
        {
            get
            {
                if (pathParts.Count > 0) { return pathParts[pathParts.Count - 1]; }
                else { return string.Empty; }
            }
        }

        /// <inheritdoc/>
        public String MemberPath
        {
            get
            {
                if (pathParts.Count > 1)
                { return String.Join(".", pathParts.Select(s => string.Format("[{0}]", s)).SkipLast(1)); }
                else { return string.Empty; }
            }
        }

        /// <inheritdoc/>
        public String MemberFullPath
        { get { return string.Join(".", pathParts.Select(s => string.Format("[{0}]", s))); } }

        /// <summary>
        /// Parent NameSpace Key for the current item.
        /// </summary>
        public PathIndex? ParentPath
        {
            get
            {
                if (pathParts.Count > 1)
                {
                    PathIndex result = new PathIndex();
                    result.pathParts.AddRange(pathParts.SkipLast(1));
                    return result;
                }
                else { return null; }
            }
        }

        /// <summary>
        /// Constructor for a Path
        /// </summary>
        /// <remarks>This is blank.</remarks>
        public PathIndex() : base() { }

        /// <summary>
        /// Constructor for a Path
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>This version takes pre-parsed strings to build the Path.</remarks>
        /// <example>
        /// var x new PathIndex(PathIndex.Parse(sourceString).ToArray())
        /// </example>
        public PathIndex(params String?[] source)
        {
            foreach (String? item in source)
            {
                if (!String.IsNullOrWhiteSpace(item))
                { pathParts.Add(String.Join(".", Parse(item).Select(s => s))); }
            }
        }

        /// <summary>
        /// Constructor for a Path (Combine)
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>This version allows multiple paths to be combined.</remarks>
        public PathIndex(params IPathItem[] source)
        {
            foreach (PathIndex item in source)
            { pathParts.AddRange(item.pathParts); }
        }

        /// <summary>
        /// Constructor for a NamedScope Path (Append)
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="member"></param>
        public PathIndex(IPathItem basePath, String member) : this(basePath)
        { pathParts.AddRange(Parse(member)); }

        /// <summary>
        /// Constructor for a Path
        /// </summary>
        /// <param name="source"></param>
        internal PathIndex(ScopeType source) : this()
        { pathParts.AddRange(Parse(ScopeEnumeration.Cast(source).Name)); }

        /// <summary>
        /// Parses a String into Name Parts per the rules of a Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<String> Parse(String? source)
        {
            List<String> elements = new List<String>();
            String? parse = source;

            // Names must be Letter or Digit or a narrow list of separators
            if (!String.IsNullOrWhiteSpace(parse))
            {
                parse = parse.Trim();

                foreach (Char item in parse.ToCharArray().Where(w => !(
                    Char.IsLetterOrDigit(w) ||
                    w is '[' or ']' or '.' || // Characters used for formating
                    w is ' ' or '_' or '-' or ':' or ';' or '/' or '\\'))) // allowed separators & delimiter characters
                { parse = parse.Replace(item.ToString(), String.Empty); }
            }

            while (!String.IsNullOrWhiteSpace(parse))
            {
                if (String.IsNullOrWhiteSpace(parse))
                { parse = String.Empty; }
                else if (!parse.Contains(".")
                    && !parse.Contains("[")
                    && !parse.Contains("]"))
                {
                    elements.Add(string.Format("{0}", parse));
                    parse = string.Empty;
                }
                else if (parse.StartsWith(".") || parse.StartsWith("]"))
                {
                    parse = parse.Substring(1);
                }
                else if (parse.StartsWith("[") && parse.Contains("]"))
                {
                    String value = parse.Substring(1, parse.IndexOf("]") - 1);
                    value = Clean(value);

                    if (!String.IsNullOrWhiteSpace(value))
                    { elements.Add(value); }

                    parse = parse.Substring(parse.IndexOf("]") + 1);
                }
                else if (parse.StartsWith("[") && !parse.Contains("]") && parse.Contains("."))
                {
                    String value = parse.Substring(1, parse.IndexOf(".") - 1);
                    value = Clean(value);

                    if (!String.IsNullOrWhiteSpace(value))
                    { elements.Add(value); }

                    parse = parse.Substring(parse.IndexOf(".") + 1);
                }
                else if (parse.StartsWith("[") && !parse.Contains("]") && !parse.Contains("."))
                {
                    String value = parse.Substring(1);
                    value = Clean(value);

                    if (!String.IsNullOrWhiteSpace(value))
                    { elements.Add(value); }

                    parse = String.Empty;
                }
                else if (parse.Contains("."))
                {
                    String value = parse.Substring(0, parse.IndexOf("."));
                    value = Clean(value);

                    if (!String.IsNullOrWhiteSpace(value))
                    { elements.Add(value); }

                    parse = parse.Substring(parse.IndexOf(".") + 1);
                }
                else if (parse.Contains("["))
                {
                    String value = parse.Substring(0, parse.IndexOf("["));
                    value = Clean(value);

                    if (!String.IsNullOrWhiteSpace(value))
                    { elements.Add(value); }

                    parse = parse.Substring(parse.IndexOf("[") + 1);
                }
                else if (parse.Contains("]"))
                {
                    String value = parse.Substring(0, parse.IndexOf("]"));
                    value = Clean(value);

                    if (!String.IsNullOrWhiteSpace(value))
                    { elements.Add(value); }

                    parse = parse.Substring(parse.IndexOf("]") + 1);
                }
                else
                {
                    Exception ex = new InvalidOperationException("No condition matches parse value");
                    ex.Data.Add(nameof(source), source);
                    ex.Data.Add(nameof(parse), parse);
                    throw ex;
                }
            }

            return elements;

            String Clean(String source)
            {
                if (String.IsNullOrWhiteSpace(source)) { return source; }

                // Trim off any leading non alpha numerics (exceptions for # and @)
                source = source.Substring(source.IndexOf(source.FirstOrDefault(w => char.IsLetterOrDigit(w) || w is '#' or '@')));

                // .Net CTOR methods
                if (source.StartsWith('#'))
                { source = String.Format("#{0}", source.Replace("#", String.Empty)); }

                // SQL Parameter
                if (source.StartsWith('@'))
                { source = String.Format("@{0}", source.Replace("@", String.Empty)); }

                // Trim off any trailing  non alpha numerics
                Int32 endAt = source.Length - Reverse(source).IndexOf(Reverse(source).FirstOrDefault(w => char.IsLetterOrDigit(w)));
                source = source.Substring(0, endAt);

                return source;
            }

            String Reverse(String source)
            { return new String(source.ToCharArray().Reverse().ToArray()); }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(PathIndex? other)
        {
            if (other is PathIndex otherKey)
            {
                return pathParts.SequenceEqual(
                    otherKey.pathParts,
                    EqualityComparer<String>.Create(
                        (first, second) => String.Equals(first, second, KeyExtension.CompareString),
                        (hash) => String.GetHashCode(hash, KeyExtension.CompareString)
                    ));
            }
            else { return false; }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(PathIndex? other)
        {
            if (other is PathIndex otherKey)
            {
                int index = 0;

                while (pathParts.Count > index
                    && otherKey.pathParts.Count > index
                    && String.Compare(pathParts[index], otherKey.pathParts[index], KeyExtension.CompareString) == 0)
                { index = index + 1; }

                if (pathParts.Count == index && otherKey.pathParts.Count == index) { return 0; }
                else if (pathParts.Count > index && otherKey.pathParts.Count > index)
                { return pathParts[index].CompareTo(otherKey.pathParts[index]); }
                else { return pathParts.Count.CompareTo(otherKey.pathParts.Count); }
            }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IPathItem? other)
        {
            if (other is IPathItem)
            {
                PathIndex otherKey = new PathIndex(other);
                return Equals(otherKey);
            }
            else { return false; }
        }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IPathItem value && Equals(new PathIndex(value)); }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IPathItem? other)
        {
            if (other is null) { return 1; }
            else
            {
                PathIndex otherKey = new PathIndex(other);
                return CompareTo(otherKey);
            }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        {
            if (obj is IPathItem value)
            { return CompareTo(new PathIndex(value)); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public static Boolean operator ==(PathIndex left, PathIndex right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(PathIndex left, PathIndex right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(PathIndex left, PathIndex right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(PathIndex left, PathIndex right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(PathIndex left, PathIndex right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(PathIndex left, PathIndex right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return MemberFullPath.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <summary>
        /// Allows formating of the NameSpace into a String.
        /// </summary>
        /// <param name="pattern">
        /// Pattern for String.Format with a single parameter.
        /// This qualifies each element of the name.
        /// Default is "[{0}]"</param>
        /// <param name="delimiter">
        /// Delimiter placed between names.
        /// Default is "."</param>
        /// <returns></returns>
        public virtual String Format(string pattern = "[{0}]", string delimiter = ".")
        { return string.Join(delimiter, pathParts.Select(s => string.Format(pattern, s))); }

        /// <summary>
        /// Groups the Path based on Hierarchy.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<PathIndex> Group(IEnumerable<PathIndex> source)
        {
            List<PathIndex> result = new List<PathIndex>();

            List<PathIndex> group = source.GroupBy(g => g).Select(s => s.Key).ToList();

            foreach (PathIndex item in group)
            { result.AddRange(item.Group()); }

            return result;
        }

        /// <summary>
        /// Groups the Path based on Hierarchy.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PathIndex> Group()
        {
            List<PathIndex> result = new List<PathIndex>() { this };

            PathIndex? key = ParentPath;

            while (key is not null)
            {
                if (!result.Contains(key))
                { result.Add(key); }

                key = key.ParentPath;
            }

            return result;
        }

        /// <summary>
        /// Combines this Path with the parent Path provided.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public PathIndex Merge(PathIndex parent)
        {
            List<String> parts = new List<String>();
            Int32 childIndex = 0;
            Int32 parentIndex = 0;

            while (childIndex < pathParts.Count || parentIndex < parent.pathParts.Count)
            {
                if (childIndex < pathParts.Count
                    && parentIndex < parent.pathParts.Count
                    && pathParts[childIndex].Equals(parent.pathParts[parentIndex], KeyExtension.CompareString))
                {
                    parts.Add(pathParts[childIndex]);
                    childIndex = childIndex + 1;
                    parentIndex = parentIndex + 1;
                }
                else
                {
                    if (parentIndex < parent.pathParts.Count)
                    {
                        parts.Add(parent.pathParts[parentIndex]);
                        parentIndex = parentIndex + 1;
                    }
                    else if (childIndex < pathParts.Count)
                    {
                        parts.Add(pathParts[childIndex]);
                        childIndex = childIndex + 1;
                    }
                }
            }

            return new PathIndex(parts.ToArray());
        }

        /// <inheritdoc/>
        public override String ToString()
        { return MemberFullPath; }
    }
}
