using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Library
{
    [Obsolete ("remove", true)]
    partial class LibraryManager
    {
        class LibraryManagerItem : ILibrarySourceItem
        {
            private Boolean inModel = false;
            private Boolean inDatabase = false;
            public LibrarySourceItem data;

            public Guid? LibraryId
            { get { return data.LibraryId; } }

            public String? LibraryTitle
            {
                get { return data.LibraryTitle; }
                set { data.LibraryTitle = value; OnPropertyChanged(nameof(LibraryTitle)); }
            }

            public String? LibraryDescription
            {
                get { return data.LibraryDescription; }
                set { data.LibraryDescription = value; OnPropertyChanged(nameof(LibraryDescription)); }
            }

            public String? SourceFile
            {
                get { return data.SourceFile; }
                set { data.SourceFile = value; OnPropertyChanged(nameof(SourceFile)); }
            }

            public DateTime? SourceDate
            {
                get { return data.SourceDate; }
                set { data.SourceDate = value; OnPropertyChanged(nameof(SourceDate)); }
            }

            public String? AssemblyName
            {
                get { return data.AssemblyName; }
                set { data.AssemblyName = value; OnPropertyChanged(nameof(AssemblyName)); }
            }

            public Boolean InModel
            {
                get { return inModel; }
                set { inModel = value; OnPropertyChanged(nameof(InModel)); }
            }

            public Boolean InDatabase
            {
                get { return inDatabase; }
                set { inDatabase = value; OnPropertyChanged(nameof(InDatabase)); }
            }

            public ScopeType Scope { get { return data.Scope; } }

            public LibraryManagerItem(LibrarySourceItem source) : base()
            {
                data = source;
                data.PropertyChanged += Data_PropertyChanged;
                data.RowStateChanged += Data_RowStateChanged;

            }

            private void Data_RowStateChanged(object? sender, EventArgs e)
            {
                if (RowStateChanged is EventHandler handler)
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

        class LibraryManagerCollection : BindingList<LibraryManagerItem>
        {
            public void Build(IEnumerable<LibrarySourceItem> modelItems, IEnumerable<LibrarySourceItem> dbItems)
            {
                this.Clear();

                // List of keys all keys
                List<LibrarySourceKey> libraryKeys = modelItems.Select(s => new LibrarySourceKey(s))
                    .Union(dbItems.Select(s => new LibrarySourceKey(s)))
                    .ToList();

                foreach (LibrarySourceKey libraryKey in libraryKeys)
                {
                    LibraryManagerItem? item = this.FirstOrDefault(w => libraryKey.Equals(w));
                    LibrarySourceItem? modelItem = modelItems.FirstOrDefault(w => libraryKey.Equals(w));
                    LibrarySourceItem? dbItem = dbItems.FirstOrDefault(w => libraryKey.Equals(w));

                    if (item is null && modelItem is LibrarySourceItem)
                    {
                        item = new LibraryManagerItem(modelItem);
                        this.Add(item);
                    }

                    if (item is null && dbItem is LibrarySourceItem)
                    {
                        item = new LibraryManagerItem(dbItem);
                        this.Add(item);
                    }

                    if (item is LibraryManagerItem)
                    {// Should always have a item at this point.
                        item.InModel = (modelItem is LibrarySourceItem);
                        item.InDatabase = (dbItem is LibrarySourceItem);
                    }
                }
            }

            public Boolean Remove(ILibrarySourceKey libraryItem)
            {
                LibrarySourceKey key = new LibrarySourceKey(libraryItem);
                if (this.FirstOrDefault(w => key.Equals(w)) is LibraryManagerItem toRemove)
                { return base.Remove(toRemove); }
                else { return false; }
            }
        }
    }
}
