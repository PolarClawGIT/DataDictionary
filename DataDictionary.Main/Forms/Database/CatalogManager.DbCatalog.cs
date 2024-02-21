using DataDictionary.DataLayer.DatabaseData.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Forms.Database
{
    partial class CatalogManager
    {
        class CatalogManagerItem : IDbCatalogItem
        {
            private bool inModel = false;
            private bool inDatabase = false;
            DbCatalogItem data;

            public Boolean InModel { get { return inModel; } set { inModel = value; OnPropertyChanged(nameof(InModel)); } }
            public Boolean InDatabase { get { return inDatabase; } set { inDatabase = value; OnPropertyChanged(nameof(InDatabase)); } }

            public string? CatalogTitle
            {
                get { return data.CatalogTitle; }
                set { data.CatalogTitle = value; OnPropertyChanged(nameof(CatalogTitle)); }
            }

            public string? CatalogDescription
            {
                get { return data.CatalogDescription; }
                set { data.CatalogDescription = value; OnPropertyChanged(nameof(CatalogDescription)); }
            }

            public string? SourceServerName
            { get { return data.SourceServerName; } }

            public string? SourceDatabaseName
            { get { return data.SourceDatabaseName; } }

            public DateTime? SourceDate
            { get { return data.SourceDate; } }

            public string? DatabaseName
            { get { return data.DatabaseName; } }

            public Guid? CatalogId
            { get { return data.CatalogId; } }

            public bool IsSystem
            { get { return data.IsSystem; } }

            public string? ScopeName
            { get { return data.ScopeName; } }

            public CatalogManagerItem(DbCatalogItem source) : base()
            {
                data = source;
                data.PropertyChanged += Data_PropertyChanged;
                data.RowStateChanged += Data_RowStateChanged;
            }

            private void Data_RowStateChanged(object? sender, EventArgs e)
            {
                if(RowStateChanged is EventHandler handler)
                { handler(sender, e); }
            }

            private void Data_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(sender, e); }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            public event EventHandler? RowStateChanged;

            public virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }

            public DataRowState RowState()
            { return data.RowState(); }
        }

        class CatalogManagerCollection : BindingList<CatalogManagerItem>
        {
            public void Build(IEnumerable<DbCatalogItem> modelItems, IEnumerable<DbCatalogItem> dbItems)
            {
                this.Clear();

                // List of keys all keys
                List<DbCatalogKey> catalogKeys = modelItems.Select(s => new DbCatalogKey(s))
                    .Union(dbItems.Select(s => new DbCatalogKey(s)))
                    .ToList();

                foreach (DbCatalogKey catalogKey in catalogKeys)
                {
                    CatalogManagerItem? item = this.FirstOrDefault(w => catalogKey.Equals(w));
                    DbCatalogItem? modelItem = modelItems.FirstOrDefault(w => catalogKey.Equals(w));
                    DbCatalogItem? dbItem = dbItems.FirstOrDefault(w => catalogKey.Equals(w));

                    if (item is null && dbItem is DbCatalogItem)
                    {
                        item = new CatalogManagerItem(dbItem);
                        this.Add(item);
                    }

                    if (item is null && modelItem is DbCatalogItem)
                    {
                        item = new CatalogManagerItem(modelItem);
                        this.Add(item);
                    }

                    if (item is CatalogManagerItem)
                    {// Should always have a item at this point.
                        item.InModel = (modelItem is DbCatalogItem);
                        item.InDatabase = (dbItem is DbCatalogItem);
                    }
                }
            }

            public Boolean Remove(DbCatalogItem catalogItem)
            {
                DbCatalogKey key = new DbCatalogKey(catalogItem);
                if (this.FirstOrDefault(w => key.Equals(w)) is CatalogManagerItem toRemove)
                { return base.Remove(toRemove); }
                else { return false; }
            }
        }
    }
}
