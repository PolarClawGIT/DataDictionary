﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbColumnName : IDbObjectName
    {
        public String? ColumnName { get; }
    }

    public class DbColumnName : IDbColumnName, IEquatable<IDbColumnName>, IComparable<IDbColumnName>, IComparable
    {
        public String? CatalogName { get; init; }
        public String? SchemaName { get; init; }
        public String? ObjectName { get; init; }
        public String? ColumnName { get; init; }

        public DbColumnName(IDbColumnName source) : base()
        {
            CatalogName = source.CatalogName;
            SchemaName = source.SchemaName;
            ObjectName = source.ObjectName;
            ColumnName = source.ColumnName;
        }

        #region IEquatable, IComparable
        public Boolean Equals(IDbColumnName? other)
        {
            return (
                other is IDbSchemaName &&
                new DbSchemaName(this).Equals(other) &&
                !String.IsNullOrEmpty(ObjectName) &&
                !String.IsNullOrEmpty(other.ObjectName) &&
                ObjectName.Equals(other.ObjectName, StringComparison.CurrentCultureIgnoreCase));
        }

        public Int32 CompareTo(IDbColumnName? other)
        {
            if (other is null) { return 1; }
            else if (new DbSchemaName(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(SchemaName, other.SchemaName, true); }
        }

        public int CompareTo(object? obj)
        { if (obj is IDbSchemaName value) { return this.CompareTo(value); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDbColumnName value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(DbColumnName left, IDbColumnName right)
        { return left.Equals(right); }

        public static bool operator !=(DbColumnName left, IDbColumnName right)
        { return !left.Equals(right); }

        public static bool operator <(DbColumnName left, IDbColumnName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbColumnName left, IDbColumnName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbColumnName left, IDbColumnName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbColumnName left, IDbColumnName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (CatalogName is String && SchemaName is String && ObjectName is String && ColumnName is String)
            { return (CatalogName, SchemaName, ObjectName, ColumnName).GetHashCode(); }
            else { return String.Empty.GetHashCode(); }
        }
        #endregion

        public static implicit operator DbSchemaName(DbColumnName value)
        { return new DbSchemaName(value); }

        public static implicit operator DbCatalogName(DbColumnName value)
        { return new DbSchemaName(value); }

        public static implicit operator DbObjectName(DbColumnName value)
        { return new DbObjectName(value); }

        public override string ToString()
        {
            if (ColumnName is String)
            { return String.Format("{0}.{1}", ((DbObjectName)this).ToString(), ColumnName); }
            else { return String.Empty; }
        }
    }
}
