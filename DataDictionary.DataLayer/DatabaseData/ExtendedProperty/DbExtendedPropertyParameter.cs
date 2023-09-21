using DataDictionary.DataLayer.DatabaseData.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DatabaseData.ExtendedProperty
{
    /// <summary>
    /// Interface structure for MS SQL ExtendedProperty parameters.
    /// </summary>
    public interface IDbExtendedPropertyParameter: IDbCatalogKeyUnique
    {
        /// <summary>
        /// Level 0 (Catalog) Type parameter
        /// </summary>
        string? Level0Type { get; }

        /// <summary>
        /// Level 0 (Catalog) Name parameter
        /// </summary>
        string? Level0Name { get; }

        /// <summary>
        /// Level 1 (Object) Type parameter
        /// </summary>
        string? Level1Type { get; }

        /// <summary>
        /// Level 1 (Object) Name parameter
        /// </summary>
        string? Level1Name { get; }

        /// <summary>
        /// Level 2 (Element) Type parameter
        /// </summary>
        string? Level2Type { get; }

        /// <summary>
        /// Level 2 (Element) Name parameter
        /// </summary>
        string? Level2Name { get; }

        /// <summary>
        /// Name of the Property.
        /// </summary>
        string? PropertyName { get; }

        /// <summary>
        /// Value of the Property.
        /// </summary>
        string? PropertyValue { get; }
    }

    /// <summary>
    /// Parameters used by MS SQL ExtendedProperty methods.
    /// </summary>
    public class DbExtendedPropertyParameter : IDbExtendedPropertyParameter
    { //TODO: Switch to using the Scope enumerations.

        /// <inheritdoc/>
        public String? CatalogName { get; set; }

        /// <inheritdoc/>
        public string? PropertyName { get; set; } // Null will return all extended properties

        /// <inheritdoc/>
        public string? PropertyValue { get; set; } // Used in Set

        /// <inheritdoc/>
        public string? Level0Type { get; set; }

        /// <inheritdoc/>
        public string? Level0Name { get; set; } // Null will return all objects of Level0 matching the Type

        /// <inheritdoc/>
        public string? Level1Type { get; set; }

        /// <inheritdoc/>
        public string? Level1Name { get; set; } // Null will return all objects of Level1 matching the Type and of Level0 Name

        /// <inheritdoc/>
        public string? Level2Type { get; set; }

        /// <inheritdoc/>
        public string? Level2Name { get; set; } // Null will return all objects of Level2 matching the Type and of Level0 & Level1 Name

    }
}
