using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Resource.Enumerations
{
    public class DbModificationEnumeration : Enumeration<DbModificationType, DbModificationEnumeration>
    {
        /// <summary>
        /// Internal Constructor for Database Row Modification Enumeration
        /// </summary>
        /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
        DbModificationEnumeration(DbModificationType value, String name) : base(value, name) { }

        /// <summary>
        /// Static constructor, loads data.
        /// </summary>
        static DbModificationEnumeration()
        {
            List<DbModificationEnumeration> data = new List<DbModificationEnumeration>()
            {
                new DbModificationEnumeration(DbModificationType.Null,       String.Empty)  { DisplayName = "not defined" },
                new DbModificationEnumeration(DbModificationType.Inserted,   "Inserted")    { DisplayName = "Inserted" },
                new DbModificationEnumeration(DbModificationType.Updated,    "Updated")     { DisplayName = "Updated" },
                new DbModificationEnumeration(DbModificationType.Deleted,    "Deleted")     { DisplayName = "Deleted" },
            };

            BuildDictionary(data);
        }
    }
}
