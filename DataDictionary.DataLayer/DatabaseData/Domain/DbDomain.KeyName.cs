﻿using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Domain;

/// <summary>
/// Interface for the Database Domain (Type) Key
/// </summary>
public interface IDbDomainKeyName : IKey, IDbSchemaKeyName
{
    /// <summary>
    /// Name of the Database Domain (Type)
    /// </summary>
    String? DomainName { get; }
}

/// <summary>
/// Implementation of the Database DomainKey
/// </summary>
public class DbDomainKeyName : DbSchemaKeyName, IDbDomainKeyName,
    IKeyComparable<IDbDomainKeyName>, IKeyComparable<DbDomainKeyName>
{
    /// <inheritdoc/>
    public String DomainName { get; init; } = string.Empty;

    /// <summary>
    /// Constructor for a blank Database Domain Key
    /// </summary>
    protected internal DbDomainKeyName() : base() { }

    /// <summary>
    /// Constructor for the Database Domain Key
    /// </summary>
    /// <param name="source"></param>
    public DbDomainKeyName(IDbDomainKeyName source) : base(source)
    {
        if (source.DomainName is string) { DomainName = source.DomainName; }
        else { DomainName = string.Empty; }
    }

    #region IEquatable, IComparable
    /// <inheritdoc/>
    public Boolean Equals(DbDomainKeyName? other)
    {
        return
            other is DbSchemaKeyName &&
            new DbSchemaKeyName(this).Equals(other) &&
            !string.IsNullOrEmpty(DomainName) &&
            !string.IsNullOrEmpty(other.DomainName) &&
            DomainName.Equals(other.DomainName, KeyExtension.CompareString);
    }

    /// <inheritdoc/>
    public Boolean Equals(IDbDomainKeyName? other)
    { return other is IDbDomainKeyName value && Equals(new DbDomainKeyName(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? obj)
    { return obj is IDbDomainKeyName value && Equals(new DbDomainKeyName(value)); }

    /// <inheritdoc/>
    public Int32 CompareTo(DbDomainKeyName? other)
    {
        if (other is null) { return 1; }
        else if (new DbSchemaKeyName(this).CompareTo(other) is int value && value != 0) { return value; }
        else { return string.Compare(DomainName, other.DomainName, true); }
    }

    /// <inheritdoc/>
    public Int32 CompareTo(IDbDomainKeyName? other)
    { if (other is IDbDomainKeyName value) { return CompareTo(new DbDomainKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public override Int32 CompareTo(object? obj)
    { if (obj is IDbDomainKeyName value) { return CompareTo(new DbDomainKeyName(value)); } else { return 1; } }

    /// <inheritdoc/>
    public static Boolean operator ==(DbDomainKeyName left, DbDomainKeyName right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DbDomainKeyName left, DbDomainKeyName right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator <(DbDomainKeyName left, DbDomainKeyName right)
    { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

    /// <inheritdoc/>
    public static Boolean operator <=(DbDomainKeyName left, DbDomainKeyName right)
    { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

    /// <inheritdoc/>
    public static Boolean operator >(DbDomainKeyName left, DbDomainKeyName right)
    { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

    /// <inheritdoc/>
    public static Boolean operator >=(DbDomainKeyName left, DbDomainKeyName right)
    { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }


    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(base.GetHashCode(), DomainName.GetHashCode(KeyExtension.CompareString)); }
    #endregion

    /// <inheritdoc/>
    public override String ToString()
    { return DbObjectName.Format(DatabaseName, SchemaName, DomainName); }

}
