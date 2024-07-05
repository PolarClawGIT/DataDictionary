using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for the Database Routine Key
    /// </summary>
    public interface IDbRoutineKeyName : IKey, IDbSchemaKeyName, IToAliasName
    {
        /// <summary>
        /// Name of the Database Routine (Procedure or Function)
        /// </summary>
        String? RoutineName { get; }
    }

    /// <summary>
    /// Implementation for IDbRoutineKeyName
    /// </summary>
    public static class DbRoutineKeyNameExtension
    {
        /// <summary>
        /// Gets the Alias Name for the Database Routine.
        /// </summary>
        /// <returns></returns>
        public static String ToAliasName(this IDbRoutineKeyName source)
        { return AliasExtension.FormatName(source.DatabaseName, source.SchemaName, source.RoutineName); }
    }

    /// <summary>
    /// Implementation of the Database Routine Key
    /// </summary>
    public class DbRoutineKeyName : DbSchemaKeyName, IDbRoutineKeyName, IKeyComparable<IDbRoutineKeyName>
    {
        /// <inheritdoc/>
        public String RoutineName { get; set; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Database Routine Key
        /// </summary>
        protected internal DbRoutineKeyName() : base() { }

        /// <summary>
        /// Constructor for the Database Routine Key
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineKeyName(IDbRoutineKeyName source) : base(source)
        {
            if (source.RoutineName is string) { RoutineName = source.RoutineName; }
            else { RoutineName = string.Empty; }
        }

        /// <summary>
        /// Try to Create a Database Routine Key from the Alias.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">A four part Alias name with a Scope of a Function or Procedure.</param>
        /// <returns>A Table Key or Null if a key could not be constructed.</returns>
        public static DbRoutineKeyName? TryCreate<T>(T source)
            where T : IAliasKeyName, IScopeType
        {
            if (source.AliasName is null) { return null; }

            List<String> parsed = AliasExtension.NameParts(source.AliasName);
            if (parsed.Count != 3) { return null; }

            if (source.Scope is ScopeType.DatabaseFunction or ScopeType.DatabaseProcedure)
            {
                return new DbRoutineKeyName()
                {
                    DatabaseName = parsed[0],
                    SchemaName = parsed[1],
                    RoutineName = parsed[2]
                };
            }
            else { return null; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public bool Equals(IDbRoutineKeyName? other)
        {
            return 
                other is IDbSchemaKeyName &&
                new DbSchemaKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(RoutineName) &&
                !string.IsNullOrEmpty(other.RoutineName) &&
                RoutineName.Equals(other.RoutineName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IDbRoutineKeyName value && Equals(new DbRoutineKeyName(value)); }

        /// <inheritdoc/>
        public int CompareTo(IDbRoutineKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(RoutineName, other.RoutineName, true); }
        }

        /// <inheritdoc/>
        public override int CompareTo(object? obj)
        { if (obj is IDbRoutineKeyName value) { return CompareTo(new DbRoutineKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static bool operator ==(DbRoutineKeyName left, DbRoutineKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(DbRoutineKeyName left, DbRoutineKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator <(DbRoutineKeyName left, DbRoutineKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static bool operator <=(DbRoutineKeyName left, DbRoutineKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static bool operator >(DbRoutineKeyName left, DbRoutineKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static bool operator >=(DbRoutineKeyName left, DbRoutineKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), RoutineName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        { return this.ToAliasName(); }
    }
}
