using DataDictionary.BusinessLayer.Application;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <summary>
    /// Interface for Importing a Catalog into a Domain
    /// </summary>
    public interface ICatalogImport
    {
        /// <summary>
        /// Imports the Catalog into the Domain
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyDefinition"></param>
        /// <param name="key"></param>
        void Import(IDatabaseModel source, IPropertyData propertyDefinition, IDbCatalogKeyName key);
    }

    /// <summary>
    /// Interface for Importing a Catalog Table into a Domain
    /// </summary>
    public interface ITableImport: ICatalogImport
    {
        /// <summary>
        /// Imports the Catalog Table into the Domain
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyDefinition"></param>
        /// <param name="key"></param>
        void Import(IDatabaseModel source, IPropertyData propertyDefinition, IDbTableKeyName key);
    }

    /// <summary>
    /// Interface for Importing a Catalog Table Column into a Domain
    /// </summary>
    public interface ITableColumnImport : ITableImport
    {
        /// <summary>
        /// Interface for Importing a Catalog Table Column into a Domain
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyDefinition"></param>
        /// <param name="key"></param>
        void Import(IDatabaseModel source, IPropertyData propertyDefinition, IDbTableColumnKeyName key);
    }
}
