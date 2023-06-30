﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DbMetaData
{
    public interface IDbSchemaName : IDbCatalogName
    {
        String? SchemaName { get; }
    }

    public class DbSchemaName : DbCatalogName, IDbSchemaName, IEquatable<IDbSchemaName>, IComparable<IDbSchemaName>, IComparable
    {
        public String SchemaName { get; init; } = String.Empty;

        public DbSchemaName() : base() { }

        public DbSchemaName(IDbSchemaName source) : base(source)
        {
            if (source.SchemaName is String) { SchemaName = source.SchemaName; }
            else { SchemaName = String.Empty; }
        }

        #region IEquatable, IComparable
        public Boolean Equals(IDbSchemaName? other)
        {
            return (
                other is IDbSchemaName &&
                new DbCatalogName(this).Equals(other) &&
                !String.IsNullOrEmpty(SchemaName) &&
                !String.IsNullOrEmpty(other.SchemaName) &&
                SchemaName.Equals(other.SchemaName, StringComparison.CurrentCultureIgnoreCase));
        }

        public Int32 CompareTo(IDbSchemaName? other)
        {
            if (other is null) { return 1; }
            else if (new DbCatalogName(this).CompareTo(other) is Int32 value && value != 0) { return value; }
            else { return String.Compare(SchemaName, other.SchemaName, true); }
        }

        public override int CompareTo(object? obj)
        { if (obj is IDbSchemaName value) { return this.CompareTo(value); } else { return 1; } }

        public override bool Equals(object? obj)
        { if (obj is IDbSchemaName value) { return this.Equals(value); } else { return false; } }

        public static bool operator ==(DbSchemaName left, IDbSchemaName right)
        { return left.Equals(right); }

        public static bool operator !=(DbSchemaName left, IDbSchemaName right)
        { return !left.Equals(right); }

        public static bool operator <(DbSchemaName left, IDbSchemaName right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        public static bool operator <=(DbSchemaName left, IDbSchemaName right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        public static bool operator >(DbSchemaName left, IDbSchemaName right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        public static bool operator >=(DbSchemaName left, IDbSchemaName right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        public override Int32 GetHashCode()
        {
            if (CatalogName is String && SchemaName is String)
            { return (CatalogName, SchemaName).GetHashCode(); }
            else { return base.GetHashCode(); }
        }
        #endregion

        public override string ToString()
        {
            if (SchemaName is String)
            { return String.Format("{0}.{1}", base.ToString(), SchemaName); }
            else { return String.Empty; }
        }
    }
}
