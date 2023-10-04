using DataDictionary.DataLayer.DatabaseData.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Database
{
    partial class DatabaseManager
    {
        class CatalogManagerItem : DbCatalogItem
        {
            private bool inModel = false;
            private bool inDatabase = false;

            public Boolean InModel { get { return inModel; } set { inModel = value; OnPropertyChanged(nameof(InModel)); } }
            public Boolean InDatabase { get { return inDatabase; } set { inDatabase = value; OnPropertyChanged(nameof(InDatabase)); } }
            public CatalogManagerItem() : base() { }

            public CatalogManagerItem(DbCatalogItem source) : this()
            {
                this.CatalogId = source.CatalogId;
                this.CatalogName = source.CatalogName;
                this.SourceServerName = source.SourceServerName;
                this.SourceDatabaseName = source.SourceDatabaseName;
            }
        }
    }
}
