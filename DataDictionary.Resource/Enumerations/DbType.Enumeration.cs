using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Enumeration support class for System.Data.DbType
    /// </summary>
    public class DbTypeEnumeration : Enumeration<DbType, DbTypeEnumeration>
    {
        public IEnumerable<String> EngineType { get; init; }

        /// <summary>
        /// Is the DataType a subset of Alpha Numeric data (strings)
        /// </summary>
        public Boolean IsAlphaNumeric { get; init; } = false;

        /// <summary>
        /// Is the DataType a subset of Numeric Data
        /// </summary>
        public Boolean IsNumeric { get; init; } = false;

        /// <summary>
        /// Is the DataType a Subset of Numeric Data that contains a Floating Point Value.
        /// </summary>
        public Boolean IsFloatingPoint { get; init; } = false;

        /// <summary>
        /// Is the DataType a Subset of Date or DateTime.
        /// </summary>
        public Boolean IsDate { get; init; } = false;

        // References:
        // DbType: https://learn.microsoft.com/en-us/dotnet/api/system.data.dbtype?view=net-9.0
        // SQL to .Net Mapping: https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings

        /// <summary>
        /// Internal Constructor for Database Constraint Enumeration
        /// </summary>
        /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
        DbTypeEnumeration(DbType value, String name, params String[] engineNames) : base(value, name)
        { EngineType = engineNames.Distinct().ToList(); }

        /// <summary>
        /// Static constructor, loads data.
        /// </summary>
        static DbTypeEnumeration()
        {
            List<DbTypeEnumeration> data = new List<DbTypeEnumeration>()
            {
                new DbTypeEnumeration(DbType.AnsiString, nameof(SqlDbType.VarChar),
                    nameof(SqlDbType.VarChar))
                    { IsAlphaNumeric = true },
                new DbTypeEnumeration(DbType.Binary, nameof(DbType.Binary),
                    nameof(SqlDbType.VarBinary), nameof(SqlDbType.Binary), nameof(SqlDbType.Timestamp), nameof(SqlDbType.Image),
                    "RowVersion"),
                new DbTypeEnumeration(DbType.Byte, nameof(DbType.Byte),
                    nameof(SqlDbType.TinyInt))
                    { IsNumeric = true },
                new DbTypeEnumeration(DbType.Boolean, nameof(DbType.Boolean), 
                    nameof(SqlDbType.Bit)),
                new DbTypeEnumeration(DbType.Currency, nameof(DbType.Currency),
                    nameof(SqlDbType.Money), nameof(SqlDbType.SmallMoney)) 
                    { IsNumeric = true, IsFloatingPoint=true },
                new DbTypeEnumeration(DbType.Date, nameof(DbType.Date),
                    nameof(SqlDbType.Date))
                    {IsDate = true },
                new DbTypeEnumeration(DbType.DateTime, nameof(DbType.DateTime),
                    nameof(SqlDbType.DateTime))
                    {IsDate = true },
                new DbTypeEnumeration(DbType.Decimal, nameof(DbType.Decimal),
                    nameof(SqlDbType.Decimal), "numeric")
                    { IsNumeric = true, IsFloatingPoint=true },
                new DbTypeEnumeration(DbType.Double, nameof(DbType.Double),
                    nameof(SqlDbType.Float))
                    { IsNumeric = true, IsFloatingPoint=true },
                new DbTypeEnumeration(DbType.Guid, nameof(DbType.Guid),
                    nameof(SqlDbType.UniqueIdentifier)),
                new DbTypeEnumeration(DbType.Int16, nameof(DbType.Int16),
                    nameof(SqlDbType.SmallInt))
                    { IsNumeric = true },
                new DbTypeEnumeration(DbType.Int32, nameof(DbType.Int32),
                    nameof(SqlDbType.Int))
                    { IsNumeric = true },
                new DbTypeEnumeration(DbType.Int64, nameof(DbType.Int64),
                    nameof(SqlDbType.BigInt))
                    { IsNumeric = true },
                new DbTypeEnumeration(DbType.Object, nameof(DbType.Object),
                    nameof(SqlDbType.Variant), nameof(SqlDbType.Structured)),
                new DbTypeEnumeration(DbType.SByte, nameof(DbType.SByte)),
                new DbTypeEnumeration(DbType.Single, nameof(DbType.Single),
                    nameof(SqlDbType.Real))
                    { IsNumeric = true, IsFloatingPoint=true },
                new DbTypeEnumeration(DbType.String, nameof(DbType.String),
                    nameof(SqlDbType.NVarChar), nameof(SqlDbType.NText), nameof(SqlDbType.Text))
                    { IsAlphaNumeric = true },
                new DbTypeEnumeration(DbType.Time, nameof(DbType.Time),
                    nameof(SqlDbType.Time)),
                new DbTypeEnumeration(DbType.UInt16, nameof(DbType.UInt16)),
                new DbTypeEnumeration(DbType.UInt32, nameof(DbType.UInt32)),
                new DbTypeEnumeration(DbType.UInt64, nameof(DbType.UInt64)),
                new DbTypeEnumeration(DbType.VarNumeric, nameof(DbType.VarNumeric)),
                new DbTypeEnumeration(DbType.AnsiStringFixedLength, nameof(DbType.AnsiStringFixedLength),
                    nameof(SqlDbType.Char))
                    { IsAlphaNumeric = true },
                new DbTypeEnumeration(DbType.StringFixedLength, nameof(DbType.StringFixedLength),
                    nameof(SqlDbType.NChar))
                    { IsAlphaNumeric = true },
                new DbTypeEnumeration(DbType.Xml, nameof(DbType.Xml),
                    nameof(SqlDbType.Xml))
                    { IsAlphaNumeric = true },
                new DbTypeEnumeration(DbType.DateTime2, nameof(DbType.DateTime2),
                    nameof(SqlDbType.DateTime2))
                    {IsDate = true },
                new DbTypeEnumeration(DbType.DateTimeOffset, nameof(DbType.DateTimeOffset),
                    nameof(SqlDbType.DateTimeOffset)),
            };

            BuildDictionary(data);
        }

        #region IParsable
        // This implements a IParsable<TSelf>.
        // CA2260 prevent IParsable from being declared.

        /// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)" />
        public static new DbTypeEnumeration Parse(String source, IFormatProvider? format)
        {
            if (Members.Values.FirstOrDefault(w => w.EngineType.Any(a => String.Equals(a, source, StringComparison.OrdinalIgnoreCase))) is DbTypeEnumeration item)
            { return item; }
            else
            {
                Exception ex = new ArgumentException("Could not parse value", nameof(source));
                ex.Data.Add("Type", typeof(DbTypeEnumeration).ToString());
                ex.Data.Add(nameof(source), source);
                throw ex;
            }
        }

        /// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)" />
        public static new Boolean TryParse([NotNullWhen(true)] String? source, IFormatProvider? format, [MaybeNullWhen(false)] out DbTypeEnumeration result)
        {
            if (Members is null) { result = null; return false; }

            if (Members.Values.FirstOrDefault(w => w.EngineType.Any(a => String.Equals(a, source, StringComparison.OrdinalIgnoreCase))) is DbTypeEnumeration item)
            { result = item; return true; }
            else { result = null; return false; }
        }
        #endregion
    }
}
