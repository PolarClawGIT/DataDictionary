using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    public class DbObjectEnumeration :Enumeration<DbObjectType, DbObjectEnumeration>
    {
        /// <summary>
        /// Internal Constructor for Database Object Enumeration
        /// </summary>
        /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
        DbObjectEnumeration(DbObjectType value, String name) : base(value, name) { }

        /// <summary>
        /// Static constructor, loads data.
        /// </summary>
        static DbObjectEnumeration()
        {
            List<DbObjectEnumeration> data = new List<DbObjectEnumeration>()
            {
                new DbObjectEnumeration(DbObjectType.Null, String.Empty) { DisplayName = "not defined" },

                new DbObjectEnumeration(DbObjectType.AggregateFunction,          "AGGREGATE_FUNCTION")                 { DisplayName = "Aggregate Function" },
                new DbObjectEnumeration(DbObjectType.CheckConstraint,            "CHECK_CONSTRAINT")                   { DisplayName = "Check Constraint" },
                new DbObjectEnumeration(DbObjectType.ClrScalarFunction,          "CLR_SCALAR_FUNCTION")                { DisplayName = "CLR: Scalar Function" },
                new DbObjectEnumeration(DbObjectType.ClrStoredProcedure,         "CLR_STORED_PROCEDURE")               { DisplayName = "CLR: Stored Procedure" },
                new DbObjectEnumeration(DbObjectType.ClrTableValuedFunction,     "CLR_TABLE_VALUED_FUNCTION")          { DisplayName = "CLR: Table Valued Function" },
                new DbObjectEnumeration(DbObjectType.ClrTrigger,                 "CLR_TRIGGER")                        { DisplayName = "CLR: Trigger" },
                new DbObjectEnumeration(DbObjectType.DefaultConstraint,          "DEFAULT_CONSTRAINT")                 { DisplayName = "Default Constraint" },
                new DbObjectEnumeration(DbObjectType.EdgeConstraint,             "EDGE_CONSTRAINT")                    { DisplayName = "Edge Constraint" },
                new DbObjectEnumeration(DbObjectType.ExtendedStoredProcedure,    "EXTENDED_STORED_PROCEDURE")          { DisplayName = "Extended Stored Procedure" },
                new DbObjectEnumeration(DbObjectType.ForeignKeyConstraint,       "FOREIGN_KEY_CONSTRAINT")             { DisplayName = "Foreign Key Constraint" },
                new DbObjectEnumeration(DbObjectType.InternalTable,              "INTERNAL_TABLE")                     { DisplayName = "Internal Table" },
                new DbObjectEnumeration(DbObjectType.PlanGuide,                  "PLAN_GUIDE")                         { DisplayName = "Plan Guide" },
                new DbObjectEnumeration(DbObjectType.PrimaryKeyConstraint,       "PRIMARY_KEY_CONSTRAINT")             { DisplayName = "Primary Key Constraint" },
                new DbObjectEnumeration(DbObjectType.ReplicationFilterProcedure, "REPLICATION_FILTER_PROCEDURE")       { DisplayName = "Replication Filter Procedure" },
                new DbObjectEnumeration(DbObjectType.Rule,                       "RULE")                               { DisplayName = "Rule" },
                new DbObjectEnumeration(DbObjectType.SequenceObject,             "SEQUENCE_OBJECT")                    { DisplayName = "Sequence Object" },
                new DbObjectEnumeration(DbObjectType.ServiceQueue,               "SERVICE_QUEUE")                      { DisplayName = "Service Queue" },
                new DbObjectEnumeration(DbObjectType.InlineTableValuedFunction,  "SQL_INLINE_TABLE_VALUED_FUNCTION")   { DisplayName = "Inline Table Valued Function" },
                new DbObjectEnumeration(DbObjectType.ScalarFunction,             "SQL_SCALAR_FUNCTION")                { DisplayName = "Scalar Function" },
                new DbObjectEnumeration(DbObjectType.StoredProcedure,            "SQL_STORED_PROCEDURE")               { DisplayName = "Stored Procedure" },
                new DbObjectEnumeration(DbObjectType.TableValuedFunction,        "SQL_TABLE_VALUED_FUNCTION")          { DisplayName = "Table Valued Function" },
                new DbObjectEnumeration(DbObjectType.Trigger,                    "SQL_TRIGGER")                        { DisplayName = "Trigger" },
                new DbObjectEnumeration(DbObjectType.Synonym,                    "SYNONYM")                            { DisplayName = "Synonym" },
                new DbObjectEnumeration(DbObjectType.SystemTable,                "SYSTEM_TABLE")                       { DisplayName = "System Table" },
                new DbObjectEnumeration(DbObjectType.TypeTable,                  "TYPE_TABLE")                         { DisplayName = "Type Table" },
                new DbObjectEnumeration(DbObjectType.UniqueConstraint,           "UNIQUE_CONSTRAINT")                  { DisplayName = "Unique Constraint" },
                new DbObjectEnumeration(DbObjectType.UserTable,                  "USER_TABLE")                         { DisplayName = "User Table" },
                new DbObjectEnumeration(DbObjectType.View,                       "VIEW")                               { DisplayName = "View" },
                // Additional Added for the Application
                new DbObjectEnumeration(DbObjectType.Column,                     "COLUMN")                             { DisplayName = "Column" },
            };

            BuildDictionary(data);
        }
    }
}
