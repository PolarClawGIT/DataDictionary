using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.DataLayer.DatabaseData.Routine
{
    /// <summary>
    /// Interface for the Database Routine Parameter Key
    /// </summary>
    public interface IDbRoutineParameterKeyName : IKey, IDbRoutineKeyName, IToAliasName
    {
        /// <summary>
        /// Name of the Database Parameter
        /// </summary>
        String? ParameterName { get; }
    }


    /// <summary>
    /// Implementation for IDbRoutineParameterKeyName
    /// </summary>
    public static class DbRoutineParameterKeyNameExtension
    {
        /// <summary>
        /// Gets the Alias Name for the Database Routine Parameter.
        /// </summary>
        /// <returns></returns>
        public static String ToAliasName(this IDbRoutineParameterKeyName source)
        { return AliasExtension.FormatName(source.DatabaseName, source.SchemaName, source.RoutineName, source.ParameterName); }
    }

    /// <summary>
    /// Implementation for Database Routine Parameter Key
    /// </summary>
    public class DbRoutineParameterKeyName : DbRoutineKeyName, IDbRoutineParameterKeyName,
        IKeyComparable<IDbRoutineParameterKeyName>, IKeyComparable<DbRoutineParameterKeyName>
    {
        /// <inheritdoc/>
        public String ParameterName { get; set; } = string.Empty;

        /// <summary>
        /// Constructor for a blank Database Routine Parameter Key
        /// </summary>
        protected internal DbRoutineParameterKeyName() : base() { }

        /// <summary>
        /// Constructor for Database Routine Parameter Key
        /// </summary>
        /// <param name="source"></param>
        public DbRoutineParameterKeyName(IDbRoutineParameterKeyName source) : base(source)
        {
            if (source.ParameterName is string) { ParameterName = source.ParameterName; }
            else { ParameterName = string.Empty; }
        }

        /// <summary>
        /// Try to Create a Database Parameter Key from the Alias.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">A four part Alias name with a Scope of a Procedure/Function Parameter.</param>
        /// <returns>A Column Key or Null if a key could not be constructed.</returns>
        public static new DbRoutineParameterKeyName? TryCreate<T>(T source)
            where T : IAliasKeyName, IScopeType
        {
            if (source.AliasName is null) { return null; }

            List<String> parsed = AliasExtension.NameParts(source.AliasName);
            if (parsed.Count != 4) { return null; }

            if (source.Scope is ScopeType.DatabaseProcedureParameter or ScopeType.DatabaseFunctionParameter)
            {
                return new DbRoutineParameterKeyName()
                {
                    DatabaseName = parsed[0],
                    SchemaName = parsed[1],
                    RoutineName = parsed[2],
                    ParameterName = parsed[3]
                };
            }
            else { return null; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(DbRoutineParameterKeyName? other)
        {
            return
                other is IDbRoutineKeyName &&
                new DbRoutineKeyName(this).Equals(other) &&
                !string.IsNullOrEmpty(ParameterName) &&
                !string.IsNullOrEmpty(other.ParameterName) &&
                ParameterName.Equals(other.ParameterName, KeyExtension.CompareString);
        }

        /// <inheritdoc/>
        public Boolean Equals(IDbRoutineParameterKeyName? other)
        { return other is IDbRoutineParameterKeyName value && Equals(new DbRoutineParameterKeyName(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IDbRoutineParameterKeyName value && Equals(new DbRoutineParameterKeyName(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(DbRoutineParameterKeyName? other)
        {
            if (other is null) { return 1; }
            else if (new DbRoutineKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
            else { return string.Compare(ParameterName, other.ParameterName, true); }
        }

        /// <inheritdoc/>
        public Int32 CompareTo(IDbRoutineParameterKeyName? other)
        { if (other is IDbRoutineParameterKeyName value) { return CompareTo(new DbRoutineParameterKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public override Int32 CompareTo(object? obj)
        { if (obj is IDbRoutineParameterKeyName value) { return CompareTo(new DbRoutineParameterKeyName(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(DbRoutineParameterKeyName left, DbRoutineParameterKeyName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(base.GetHashCode(), ParameterName.GetHashCode(KeyExtension.CompareString)); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return this.ToAliasName(); }
    }
}
