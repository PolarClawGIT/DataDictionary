using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Interface for the NameSpace Key.
    /// NameSpace is used with Alias (Entity and Attribute), Model Subject Areas, and Help Subjects.
    /// </summary>
    public interface INameSpaceKey : IKey
    {
        /// <summary>
        /// Name of the Member
        /// </summary>
        string MemberName { get; }

        /// <summary>
        /// Path of the Member.
        /// </summary>
        /// <remarks>Formated, period delimited and square bracket qualified.</remarks>
        string MemberPath { get; }

        /// <summary>
        /// Path and Name of the Member.
        /// </summary>
        /// <remarks>Formated, period delimited and square bracket qualified.</remarks>
        string MemberFullName { get; }
    }


    /// <summary>
    /// Implementation for the NameSpace Key.
    /// </summary>
    public class NameSpaceKey : INameSpaceKey, IKeyComparable<INameSpaceKey>, IKeyComparable<NameSpaceKey>
    {
        /// <inheritdoc/>
        public string MemberName
        {
            get
            {
                if (memberParts.Count > 0) { return memberParts[memberParts.Count - 1]; }
                else { return string.Empty; }
            }
        }

        /// <inheritdoc/>
        public string MemberPath
        {
            get
            {
                if (memberParts.Count > 1)
                { return string.Join(".", memberParts.Select(s => string.Format("[{0}]", s)).SkipLast(1)); }
                else { return string.Empty; }
            }
        }

        /// <summary>
        /// Parent NameSpace Key for the current item.
        /// </summary>
        public NameSpaceKey? ParentKey
        {
            get
            {
                if (memberParts.Count > 1)
                {
                    NameSpaceKey result = new NameSpaceKey();
                    result.memberParts.AddRange(memberParts.SkipLast(1));
                    return result;
                }
                else { return null; }
            }
        }

        /// <inheritdoc/>
        public string MemberFullName
        { get { return string.Join(".", memberParts.Select(s => string.Format("[{0}]", s))); } }

        /// <summary>
        /// The Member Name Parts that compose the Key.
        /// </summary>
        protected List<string> memberParts = new List<string>();

        /// <summary>
        /// Parses a String into Name Parts per the rules of a NameSpace Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public List<string> NameParts(string source)
        {
            List<string> elements = new List<string>();
            string parse = source;

            // Parse the name into its pieces.
            while (!string.IsNullOrWhiteSpace(parse))
            {
                string reverse = new string(parse.ToCharArray().Reverse().ToArray());

                if (!parse.Contains(".")
                    && !parse.Contains("[")
                    && !parse.Contains("]"))
                {
                    elements.Add(string.Format("{0}", parse));
                    parse = string.Empty;
                }
                else if (!parse.Contains(".")
                    && parse.StartsWith("[")
                    && parse.EndsWith("]"))
                {
                    string value = parse.Substring(1, parse.Length - 2);
                    elements.Add(value);
                    parse = string.Empty;
                }
                else if (parse.Contains(".")
                    && !parse.Contains("[")
                    && !parse.Contains("]"))
                {
                    int index = reverse.IndexOf(".");
                    string value = parse.Substring(parse.Length - index);
                    elements.Add(string.Format("{0}", value));
                    parse = parse.Substring(0, parse.Length - index - 1);
                }
                else if (parse.Contains(".[")
                    && parse.EndsWith("]"))
                {
                    int index = reverse.IndexOf("[.");
                    string value = parse.Substring(parse.Length - index, index - 1);
                    elements.Add(string.Format("{0}", value));
                    parse = parse.Substring(0, parse.Length - index - 2);
                }
                else if (parse.Contains(".")
                    && parse.Contains("]")
                    && reverse.IndexOf(".") < reverse.IndexOf("]"))
                {
                    int index = reverse.IndexOf(".");
                    string value = parse.Substring(parse.Length - index);
                    elements.Add(string.Format("{0}", value));
                    parse = parse.Substring(0, parse.Length - index - 1);
                }
                else
                {
                    Exception ex = new InvalidOperationException("No condition matches parse value");
                    ex.Data.Add(nameof(source), source);
                    ex.Data.Add(nameof(parse), parse);
                    throw ex;
                }
            }

            elements.Reverse();

            return elements;
        }

        /// <summary>
        /// Constructor for NameSpace Key
        /// </summary>
        protected NameSpaceKey() : base()
        { }

        /// <summary>
        /// Constructor for NameSpace Key, for internal Equals and Convert.
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(INameSpaceKey source) : this()
        { memberParts.AddRange(NameParts(source.MemberFullName ?? string.Empty)); }

        /// <summary>
        /// Constructor for NameSpace Key, Clone
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(NameSpaceKey source) : this()
        { memberParts.AddRange(source.memberParts); }

        /// <summary>
        /// Constructor for NameSpace Key from String
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(string source) : this()
        { memberParts.AddRange(NameParts(source)); }

        #region Constructors
        /// <summary>
        /// Constructor for NameSpace Key from Application Help
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(ApplicationData.Help.IHelpKeyUnique source) : base()
        { memberParts.AddRange(NameParts(source.NameSpace ?? string.Empty)); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Catalog
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Catalog.IDbCatalogKeyName source) : base()
        { memberParts.Add(source.DatabaseName ?? string.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Schema
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Schema.IDbSchemaKeyName source) : this((DatabaseData.Catalog.IDbCatalogKeyName)source)
        { memberParts.Add(source.SchemaName ?? string.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Table
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Table.IDbTableKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        { memberParts.Add(source.TableName ?? string.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Table Column
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Table.IDbTableColumnKeyName source) : this((DatabaseData.Table.IDbTableKeyName)source)
        { memberParts.Add(source.ColumnName ?? string.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Constraint
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Constraint.IDbConstraintKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        { memberParts.Add(source.ConstraintName ?? string.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Domain
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Domain.IDbDomainKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        { memberParts.Add(source.DomainName ?? string.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Routine
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Routine.IDbRoutineKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        { memberParts.Add(source.RoutineName ?? string.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Routine Parameter
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Routine.IDbRoutineParameterKeyName source) : this((DatabaseData.Routine.IDbRoutineKeyName)source)
        { memberParts.Add(source.ParameterName ?? string.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Library Source
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(LibraryData.Source.ILibrarySourceKeyName source) : base()
        { memberParts.Add(source.AssemblyName ?? string.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Library Member
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(LibraryData.Member.ILibraryMemberKeyName source) : base()
        {
            memberParts.Add(source.NameSpace ?? string.Empty);
            memberParts.Add(source.MemberName ?? string.Empty);
        }

        /// <summary>
        /// Constructor for NameSpace Key from Model
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(ModelData.IModelItem source) : base()
        { memberParts.Add(source.ModelTitle ?? string.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Model Subject Area
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(ModelData.SubjectArea.IModelSubjectAreaItem source) : base()
        { memberParts.AddRange(NameParts(source.SubjectAreaNameSpace ?? source.SubjectAreaTitle ?? string.Empty)); }

        /// <summary>
        /// Constructor for NameSpace Key from Domain Entity
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DomainData.Entity.IDomainEntityItem source) : base()
        { memberParts.Add(source.EntityTitle ?? string.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Domain Attribute
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DomainData.Attribute.IDomainAttributeItem source) : base()
        { memberParts.Add(source.AttributeTitle ?? string.Empty); }



        #endregion

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(NameSpaceKey? other)
        {
            if (other is NameSpaceKey otherKey)
            { return memberParts.SequenceEqual(otherKey.memberParts); }
            else { return false; }
        }

        /// <inheritdoc/>
        public int CompareTo(NameSpaceKey? other)
        {
            if (other is NameSpaceKey otherKey)
            {
                int index = 0;

                while (memberParts.Count > index
                    && otherKey.memberParts.Count > index
                    && memberParts[index].CompareTo(otherKey.memberParts[index]) == 0)
                { index = index + 1; }

                if (memberParts.Count == index && otherKey.memberParts.Count == index) { return 0; }
                else if (memberParts.Count > index && otherKey.memberParts.Count > index)
                { return memberParts[index].CompareTo(otherKey.memberParts[index]); }
                else { return memberParts.Count.CompareTo(otherKey.memberParts.Count); }
            }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual bool Equals(INameSpaceKey? other)
        {
            if (other is INameSpaceKey)
            {
                NameSpaceKey otherKey = new NameSpaceKey(other);
                return Equals(otherKey);
            }
            else { return false; }
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is INameSpaceKey value && Equals(new NameSpaceKey(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(INameSpaceKey? other)
        {
            if (other is null) { return 1; }
            else
            {
                NameSpaceKey otherKey = new NameSpaceKey(other);
                return CompareTo(otherKey);
            }
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        {
            if (obj is INameSpaceKey value)
            { return CompareTo(new NameSpaceKey(value)); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public static bool operator ==(NameSpaceKey left, NameSpaceKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(NameSpaceKey left, NameSpaceKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(NameSpaceKey left, NameSpaceKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(NameSpaceKey left, NameSpaceKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(NameSpaceKey left, NameSpaceKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(NameSpaceKey left, NameSpaceKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return MemberFullName.GetHashCode(); }
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
        public virtual string Format(string pattern = "[{0}]", string delimiter = ".")
        { return string.Join(delimiter, memberParts.Select(s => string.Format(pattern, s))); }

        /// <summary>
        /// Groups the NameSpaces based on Hierarchy.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<NameSpaceKey> Group(IEnumerable<NameSpaceKey> source)
        {
            List<NameSpaceKey> result = new List<NameSpaceKey>();

            List<NameSpaceKey> group = source.GroupBy(g => g).Select(s => s.Key).ToList();

            foreach (NameSpaceKey item in group)
            {
                if (!result.Contains(item))
                { result.Add(item); }

                NameSpaceKey? key = item.ParentKey;

                while (key is not null)
                {
                    if (!result.Contains(key))
                    { result.Add(key); }

                    key = key.ParentKey;
                }
            }
            return result;
        }

        /// <inheritdoc/>
        public override string ToString()
        { return MemberFullName; }

    }

}
