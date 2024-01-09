using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.ApplicationData.Model.SubjectArea;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Domain;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.NameSpace
{
    /// <summary>
    /// Interface for the Model NameSpace Key Member Name.
    /// </summary>
    public interface IModelNameSpaceKeyMember : IKey
    {
        /// <summary>
        /// Name of the Member
        /// </summary>
        String? MemberName { get; }

        /// <summary>
        /// Path of the Member.
        /// </summary>
        /// <remarks>Formated, period delimited and square bracket qualified.</remarks>
        String? MemberPath { get; }

        /// <summary>
        /// Path and Name of the Member.
        /// </summary>
        /// <remarks>Formated, period delimited and square bracket qualified.</remarks>
        String? MemberFullName { get; }
    }

    /// <summary>
    /// Implementation for the Model NameSpace Key Member Name.
    /// </summary>
    public class ModelNameSpaceKeyMember : IModelNameSpaceKeyMember, IKeyComparable<IModelNameSpaceKeyMember>
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

        /// <inheritdoc/>
        public String MemberFullName
        { get { return String.Join(".", memberParts.Select(s => String.Format("[{0}]", s))); } }

        /// <summary>
        /// The Member Name Parts that compose the Key.
        /// </summary>
        protected List<String> memberParts = new List<String>();

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(String source) : base()
        { memberParts.AddRange(NameParts(source)); }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IModelNameSpaceKeyMember source) : base()
        {
            if (source.MemberFullName is String)
            { memberParts.AddRange(NameParts(source.MemberFullName)); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Catalog
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IDbCatalogKeyName source) : base()
        {
            if (source.DatabaseName is String)
            { memberParts.Add(source.DatabaseName); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Schema
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IDbSchemaKeyName source) :this((IDbCatalogKeyName) source)
        {
            if (source.SchemaName is String)
            { memberParts.Add(source.SchemaName); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Table
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IDbTableKeyName source) : this((IDbSchemaKeyName)source)
        {
            if (source.TableName is String)
            { memberParts.Add(source.TableName); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Table Column
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IDbTableColumnKeyName source) : this((IDbTableKeyName)source)
        {
            if (source.ColumnName is String)
            { memberParts.Add(source.ColumnName); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Constraint
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IDbConstraintKeyName source) : this((IDbSchemaKeyName)source)
        {
            if (source.ConstraintName is String)
            { memberParts.Add(source.ConstraintName); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Domain
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IDbDomainKeyName source) : this((IDbSchemaKeyName)source)
        {
            if (source.DomainName is String)
            { memberParts.Add(source.DomainName); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Routine
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IDbRoutineKeyName source) : this((IDbSchemaKeyName)source)
        {
            if (source.RoutineName is String)
            { memberParts.Add(source.RoutineName); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Routine
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IDbRoutineParameterKeyName source) : this((IDbRoutineKeyName)source)
        {
            if (source.ParameterName is String)
            { memberParts.Add(source.ParameterName); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Library Member
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(ILibrarySourceKeyName source) : base()
        {
            if (source.AssemblyName is String)
            { memberParts.Add(source.AssemblyName); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Library Member
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(ILibraryMemberKeyName source) : base ()
        {
            if (source.NameSpace is String)
            { memberParts.AddRange(NameParts(source.NameSpace)); }

            if (source.MemberName is String)
            { memberParts.Add(source.MemberName); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Model
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IModelItem source) : base()
        {
            if (source.ModelTitle is String)
            { memberParts.Add(source.ModelTitle); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Subject Area
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IModelSubjectAreaItem source) : base()
        {
            if (source.SubjectAreaTitle is String)
            { memberParts.Add(source.SubjectAreaTitle); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Attribute
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IDomainAttributeItem source) : base()
        {
            if (source.AttributeTitle is String)
            { memberParts.Add(source.AttributeTitle); }
        }

        /// <summary>
        /// Constructor for Model NameSpace Key Member Name, Entity
        /// </summary>
        /// <param name="source"></param>
        public ModelNameSpaceKeyMember(IDomainEntityItem source) : base()
        {
            if (source.EntityTitle is String)
            { memberParts.Add(source.EntityTitle); }
        }


        /// <summary>
        /// Parses a String into Name Parts per the rules of a Model NameSpace Member.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<String> NameParts(String source)
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

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public virtual bool Equals(IModelNameSpaceKeyMember? other)
        {
            if (other is IModelNameSpaceKeyMember)
            {
                ModelNameSpaceKeyMember otherKey = new ModelNameSpaceKeyMember(other);
                return memberParts.SequenceEqual(otherKey.memberParts);
            }
            else { return false; }
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IModelNameSpaceKeyMember value && Equals(new ModelNameSpaceKeyMember(value)); }

        /// <inheritdoc/>
        public virtual int CompareTo(IModelNameSpaceKeyMember? other)
        {
            if (other is null) { return 1; }
            else
            {
                ModelNameSpaceKeyMember otherKey = new ModelNameSpaceKeyMember(other);
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
        }

        /// <inheritdoc/>
        public virtual int CompareTo(object? obj)
        {
            if (obj is IModelNameSpaceKeyMember value)
            { return CompareTo(new ModelNameSpaceKeyMember(value)); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public static bool operator ==(ModelNameSpaceKeyMember left, ModelNameSpaceKeyMember right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ModelNameSpaceKeyMember left, ModelNameSpaceKeyMember right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(ModelNameSpaceKeyMember left, ModelNameSpaceKeyMember right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(ModelNameSpaceKeyMember left, ModelNameSpaceKeyMember right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(ModelNameSpaceKeyMember left, ModelNameSpaceKeyMember right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(ModelNameSpaceKeyMember left, ModelNameSpaceKeyMember right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), MemberFullName); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            return MemberFullName;
        }
    }
}
