using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.AppSecurity
{
    /// <summary>
    /// List of Application Level Permission properties
    /// </summary>
    public interface IRolePermissions
    {
        /// <summary>
        /// Permission: Security Administrators can insert/update/delete any Security Table.
        /// </summary>
        Boolean IsSecurityAdmin { get; }

        /// <summary>
        /// Permission: Help Administrators can insert/update/delete any Help Table.
        /// </summary>
        Boolean IsHelpAdmin { get; }

        /// <summary>
        /// Permission: Help Owner can insert/update/delete Help item they own.
        /// </summary>
        Boolean IsHelpOwner { get; }

        /// <summary>
        /// Permission: Catalog Administrators can insert/delete any Catalog (database schema) Table.
        /// </summary>
        Boolean IsCatalogAdmin { get; }

        /// <summary>
        /// Permission: Catalog Owner can insert/delete Catalog item they own.
        /// </summary>
        Boolean IsCatalogOwner { get; }

        /// <summary>
        /// Permission: Library Administrators can insert/delete any Library (code library) Table.
        /// </summary>
        Boolean IsLibraryAdmin { get; }

        /// <summary>
        /// Permission: Library Owner can insert/delete Library item they own.
        /// </summary>
        Boolean IsLibraryOwner { get; }

        /// <summary>
        /// Permission: Model Administrators can insert/update/delete any Model (Entity, Attribute, Relationship, Process Table.
        /// </summary>
        Boolean IsModelAdmin { get; }

        /// <summary>
        /// Permission: Model Owner can insert/update/delete Model item they own.
        /// </summary>
        Boolean IsModelOwner { get; }

        /// <summary>
        /// Permission: Scripting Administrators can insert/update/delete any Scripting Table.
        /// </summary>
        Boolean IsScriptAdmin { get; }

        /// <summary>
        /// Permission: Scripting Owner can insert/update/delete Scripting item they own.
        /// </summary>
        Boolean IsScriptOwner { get; }
    }
}
