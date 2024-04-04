using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public virtual string MemberName
        {
            get
            {
                if (memberParts.Count > 0) { return memberParts[memberParts.Count - 1]; }
                else { return string.Empty; }
            }
            set
            {
                if (memberParts.Count > 0) { memberParts[memberParts.Count - 1] = value; }
                else { memberParts.Add(value); }
            }
        }

        /// <inheritdoc/>
        public virtual string MemberPath
        {
            get
            {
                if (memberParts.Count > 1)
                { return string.Join(".", memberParts.Select(s => string.Format("[{0}]", s)).SkipLast(1)); }
                else { return string.Empty; }
            }
            set
            {
                String member = MemberName;
                memberParts.Clear();
                memberParts.AddRange(NameParts(value));
                memberParts.Add(member);
            }
        }

        /// <inheritdoc/>
        public virtual string MemberFullName
        {
            get { return string.Join(".", memberParts.Select(s => string.Format("[{0}]", s))); }
            set
            {
                memberParts.Clear();
                memberParts.AddRange(NameParts(value));
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

        /// <summary>
        /// The Member Name Parts that compose the Key.
        /// </summary>
        protected List<string> memberParts = new List<string>();

        /// <summary>
        /// Parses a String into Name Parts per the rules of a NameSpace Key.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<string> NameParts(string source)
        {
            List<string> elements = new List<string>();
            string parse = source;

            while (!string.IsNullOrWhiteSpace(parse))
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
        {
            if (source.MemberFullName is String)
            { memberParts.AddRange(NameParts(source.MemberFullName)); }
        }

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
        {
            if (source.NameSpace is String)
            { memberParts.AddRange(NameParts(source.NameSpace)); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Database Catalog
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Catalog.IDbCatalogKeyName source) : base()
        {
            if (source.DatabaseName is String)
            { memberParts.Add(source.DatabaseName); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Database Schema
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Schema.IDbSchemaKeyName source) : this((DatabaseData.Catalog.IDbCatalogKeyName)source)
        {
            if (source.SchemaName is String)
            { memberParts.Add(source.SchemaName); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Database Table
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Table.IDbTableKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        {
            if (source.TableName is String)
            { memberParts.Add(source.TableName); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Database Table Column
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Table.IDbTableColumnKeyName source) : this((DatabaseData.Table.IDbTableKeyName)source)
        {
            if (source.ColumnName is String)
            { memberParts.Add(source.ColumnName); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Database Constraint
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Constraint.IDbConstraintKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        {
            if (source.ConstraintName is String)
            { memberParts.Add(source.ConstraintName); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Database Domain
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Domain.IDbDomainKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        {
            if (source.DomainName is String)
            { memberParts.Add(source.DomainName); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Database Routine
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Routine.IDbRoutineKeyName source) : this((DatabaseData.Schema.IDbSchemaKeyName)source)
        {
            if (source.RoutineName is String)
            { memberParts.Add(source.RoutineName); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Database Routine Parameter
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DatabaseData.Routine.IDbRoutineParameterKeyName source) : this((DatabaseData.Routine.IDbRoutineKeyName)source)
        {
            if (source.ParameterName is String)
            { memberParts.Add(source.ParameterName); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Library Source
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(LibraryData.Source.ILibrarySourceKeyName source) : base()
        {
            if (source.AssemblyName is String)
            { memberParts.Add(source.AssemblyName); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Library Member
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(LibraryData.Member.ILibraryMemberKeyName source) : base()
        {
            if (source.MemberNameSpace is String)
            { memberParts.AddRange(NameParts(source.MemberNameSpace)); }

            if (source.MemberName is String)
            { memberParts.Add(source.MemberName); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Model
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(ModelData.IModelItem source) : base()
        {
            if (source.ModelTitle is String)
            { memberParts.Add(source.ModelTitle); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Model Subject Area
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(ModelData.SubjectArea.IModelSubjectAreaItem source) : base()
        {
            if (source.SubjectAreaNameSpace is String)
            { memberParts.AddRange(NameParts(source.SubjectAreaNameSpace)); }
            else
            if (source.SubjectAreaTitle is String)
            { memberParts.AddRange(NameParts(source.SubjectAreaTitle)); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Domain Entity
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DomainData.Entity.IDomainEntityItem source) : base()
        {
            if (source.EntityTitle is String)
            { memberParts.Add(source.EntityTitle); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Domain Attribute
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(DomainData.Attribute.IDomainAttributeItem source) : base()
        {
            if (source.AttributeTitle is String)
            { memberParts.Add(source.AttributeTitle); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Scripting Schema
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(ScriptingData.Schema.ISchemaItem source) : base()
        {
            if (source.SchemaTitle is String)
            { memberParts.Add(source.SchemaTitle); }
        }

        /// <summary>
        /// Constructor for NameSpace Key from Scripting Transform
        /// </summary>
        /// <param name="source"></param>
        public NameSpaceKey(ScriptingData.Transform.ITransformItem source) : base()
        {
            if (source.TransformTitle is String)
            { memberParts.Add(source.TransformTitle); }
        }


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
