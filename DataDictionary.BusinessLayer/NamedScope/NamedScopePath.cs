using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.NamedScope
{
    /// <summary>
    /// Interface for a NamedScope Path (aka NameSpace, Alias)
    /// </summary>
    public interface INamedScopePath : IKey
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
    /// Implementation for a NamedScope Path (aka NameSpace, Alias)
    /// </summary>
    public class NamedScopePath : INamedScopePath, IKeyComparable<INamedScopePath>
    {
        /// <summary>
        /// List of Parts of the NameScope Path.
        /// </summary>
        protected List<string> pathParts = new List<string>();

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
                { return string.Join(".", pathParts.Select(s => string.Format("[{0}]", s)).SkipLast(1)); }
                else { return string.Empty; }
            }
        }

        /// <inheritdoc/>
        public String MemberFullPath
        { get { return string.Join(".", pathParts.Select(s => string.Format("[{0}]", s))); } }

        /// <summary>
        /// Constructor for a NamedScope Path
        /// </summary>
        protected NamedScopePath() : base() { }

        /// <summary>
        /// Constructor for a NamedScope Path
        /// </summary>
        /// <param name="source"></param>
        protected NamedScopePath(String source) : this()
        { pathParts.AddRange(Parse(source)); }

        /// <summary>
        /// Constructor for a NamedScope Path
        /// </summary>
        /// <param name="source"></param>
        public NamedScopePath(INamedScopePath source) : this()
        {
            if (source is NamedScopePath value)
            { pathParts.AddRange(value.pathParts); }
            else { pathParts.AddRange(Parse(source.MemberFullPath)); }
        }

        internal NamedScopePath(ICatalogKeyName source) : this()
        {
            if (!String.IsNullOrWhiteSpace(source.DatabaseName))
            { pathParts.Add(source.DatabaseName); }
        }

        internal NamedScopePath(ISchemaKeyName source) : this((ICatalogKeyName)source)
        {
            if (!String.IsNullOrWhiteSpace(source.SchemaName))
            { pathParts.Add(source.SchemaName); }
        }



        /// <summary>
        /// Parses a String into Name Parts per the rules of a NamedScope Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<String> Parse(string source)
        {
            List<String> elements = new List<String>();
            String parse = source;

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
        public bool Equals(NamedScopePath? other)
        {
            if (other is NamedScopePath otherKey)
            { return pathParts.SequenceEqual(otherKey.pathParts); }
            else { return false; }
        }

        /// <inheritdoc/>
        public int CompareTo(NamedScopePath? other)
        {
            if (other is NamedScopePath otherKey)
            {
                int index = 0;

                while (pathParts.Count > index
                    && otherKey.pathParts.Count > index
                    && pathParts[index].CompareTo(otherKey.pathParts[index]) == 0)
                { index = index + 1; }

                if (pathParts.Count == index && otherKey.pathParts.Count == index) { return 0; }
                else if (pathParts.Count > index && otherKey.pathParts.Count > index)
                { return pathParts[index].CompareTo(otherKey.pathParts[index]); }
                else { return pathParts.Count.CompareTo(otherKey.pathParts.Count); }
            }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual bool Equals(INamedScopePath? other)
        {
            if (other is INamedScopePath)
            {
                NamedScopePath otherKey = new NamedScopePath(other);
                return Equals(otherKey);
            }
            else { return false; }
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is INamedScopePath value && Equals(new NamedScopePath(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(INamedScopePath? other)
        {
            if (other is null) { return 1; }
            else
            {
                NamedScopePath otherKey = new NamedScopePath(other);
                return CompareTo(otherKey);
            }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        {
            if (obj is INamedScopePath value)
            { return CompareTo(new NamedScopePath(value)); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public static bool operator ==(NamedScopePath left, NamedScopePath right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(NamedScopePath left, NamedScopePath right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(NamedScopePath left, NamedScopePath right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(NamedScopePath left, NamedScopePath right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(NamedScopePath left, NamedScopePath right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(NamedScopePath left, NamedScopePath right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return MemberFullPath.GetHashCode(); }
        #endregion
    }
}
