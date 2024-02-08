using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    /// <summary>
    /// Interface for the NameSpace Key.
    /// </summary>
    public interface INameSpaceKey : IKey
    {
        /// <summary>
        /// Name of the Member
        /// </summary>
        String MemberName { get; }

        /// <summary>
        /// Path of the Member.
        /// </summary>
        /// <remarks>Formated, period delimited and square bracket qualified.</remarks>
        String MemberPath { get; }

        /// <summary>
        /// Path and Name of the Member.
        /// </summary>
        /// <remarks>Formated, period delimited and square bracket qualified.</remarks>
        String MemberFullName { get; }
    }


    /// <summary>
    /// Implementation for the NameSpace Key.
    /// </summary>
    public class NameSpaceKey : INameSpaceKey, IKeyComparable<INameSpaceKey>, IKeyComparable<NameSpaceKey>
    {
        /// <inheritdoc/>
        public String MemberName
        {
            get
            {
                if (memberParts.Count > 0) { return memberParts[memberParts.Count - 1]; }
                else { return String.Empty; }
            }
        }

        /// <inheritdoc/>
        public String MemberPath
        {
            get
            {
                if (memberParts.Count > 1)
                { return String.Join(".", memberParts.Select(s => String.Format("[{0}]", s)).SkipLast(1)); }
                else { return String.Empty; }
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
        public String MemberFullName
        { get { return String.Join(".", memberParts.Select(s => String.Format("[{0}]", s))); } }

        /// <summary>
        /// The Member Name Parts that compose the Key.
        /// </summary>
        protected List<String> memberParts = new List<String>();

        /// <summary>
        /// Parses a String into Name Parts per the rules of a NameSpace Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public List<String> NameParts(String source)
        {
            List<String> elements = new List<String>();
            String parse = source;

            // Parse the name into its pieces.
            while (!String.IsNullOrWhiteSpace(parse))
            {
                String reverse = new String(parse.ToCharArray().Reverse().ToArray());

                if (!parse.Contains(".")
                    && !parse.Contains("[")
                    && !parse.Contains("]"))
                {
                    elements.Add(String.Format("{0}", parse));
                    parse = String.Empty;
                }
                else if (!parse.Contains(".")
                    && parse.StartsWith("[")
                    && parse.EndsWith("]"))
                {
                    String value = parse.Substring(1, parse.Length - 2);
                    elements.Add(value);
                    parse = String.Empty;
                }
                else if (parse.Contains(".")
                    && !parse.Contains("[")
                    && !parse.Contains("]"))
                {
                    Int32 index = reverse.IndexOf(".");
                    String value = parse.Substring(parse.Length - index);
                    elements.Add(String.Format("{0}", value));
                    parse = parse.Substring(0, parse.Length - index - 1);
                }
                else if (parse.Contains(".[")
                    && parse.EndsWith("]"))
                {
                    Int32 index = reverse.IndexOf("[.");
                    String value = parse.Substring(parse.Length - index, index - 1);
                    elements.Add(String.Format("{0}", value));
                    parse = parse.Substring(0, parse.Length - index - 2);
                }
                else if (parse.Contains(".")
                    && parse.Contains("]")
                    && reverse.IndexOf(".") < reverse.IndexOf("]"))
                {
                    Int32 index = reverse.IndexOf(".");
                    String value = parse.Substring(parse.Length - index);
                    elements.Add(String.Format("{0}", value));
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
        protected NameSpaceKey(INameSpaceKey source) : this()
        { memberParts.AddRange(NameParts(source.MemberFullName ?? String.Empty)); }

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
        public NameSpaceKey(String source) : this()
        { memberParts.AddRange(NameParts(source)); }

        #region Constructors
        /// <summary>
        /// Constructor for NameSpace Key from Application Help
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(Help.IHelpKeyUnique source) : base()
        { memberParts.AddRange(NameParts(source.NameSpace ?? String.Empty)); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Catalog
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Catalog.IDbCatalogKeyName source) : base()
        { memberParts.Add(source.DatabaseName ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Schema
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Schema.IDbSchemaKeyName source) : this((DatabaseData.Catalog.IDbCatalogKeyName)source)
        { memberParts.Add(source.SchemaName ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Table
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Table.IDbTableKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        { memberParts.Add(source.TableName ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Table Column
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Table.IDbTableColumnKeyName source) : this((DatabaseData.Table.IDbTableKeyName)source)
        { memberParts.Add(source.ColumnName ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Constraint
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Constraint.IDbConstraintKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        { memberParts.Add(source.ConstraintName ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Domain
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Domain.IDbDomainKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        { memberParts.Add(source.DomainName ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Routine
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Routine.IDbRoutineKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        { memberParts.Add(source.RoutineName ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Database Routine Parameter
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Routine.IDbRoutineParameterKeyName source) : this((DatabaseData.Routine.IDbRoutineKeyName)source)
        { memberParts.Add(source.ParameterName ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Library Source
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(LibraryData.Source.ILibrarySourceKeyName source) : base()
        { memberParts.Add(source.AssemblyName ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Library Member
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(LibraryData.Member.ILibraryMemberKeyName source) : base()
        {
            memberParts.Add(source.NameSpace ?? String.Empty);
            memberParts.Add(source.MemberName ?? String.Empty);
        }

        /// <summary>
        /// Constructor for NameSpace Key from Model
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(ModelData.IModelItem source) : base()
        { memberParts.Add(source.ModelTitle ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Model Subject Area
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(ModelData.SubjectArea.IModelSubjectAreaItem source) : base()
        { memberParts.Add(source.SubjectAreaTitle ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Domain Entity
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DomainData.Entity.IDomainEntityItem source) : base()
        { memberParts.Add(source.EntityTitle ?? String.Empty); }

        /// <summary>
        /// Constructor for NameSpace Key from Domain Attribute
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DomainData.Attribute.IDomainAttributeItem source) : base()
        { memberParts.Add(source.AttributeTitle ?? String.Empty); }



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
                Int32 index = 0;

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
        { return HashCode.Combine(base.GetHashCode(), MemberFullName); }
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
        public virtual String Format(String pattern = "[{0}]", String delimiter = ".")
        { return String.Join(delimiter, memberParts.Select(s => String.Format(pattern, s))); }

        /// <inheritdoc/>
        public override string ToString()
        { return MemberFullName; }

    }
}
