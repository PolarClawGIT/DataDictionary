using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryManager
    {
        class LibraryManagerItem : ILibrarySourceItem
        {
            private bool inModel = false;
            private bool inDatabase = false;
            public LibrarySourceItem data;

            public Guid? LibraryId
            { get { return data.LibraryId; } }

            public string? LibraryTitle
            {
                get { return data.LibraryTitle; }
                set { data.LibraryTitle = value; OnPropertyChanged(nameof(LibraryTitle)); }
            }

            public string? LibraryDescription
            {
                get { return data.LibraryDescription; }
                set { data.LibraryDescription = value; OnPropertyChanged(nameof(LibraryDescription)); }
            }

            public string? SourceFile
            {
                get { return data.SourceFile; }
                set { data.SourceFile = value; OnPropertyChanged(nameof(SourceFile)); }
            }

            public DateTime? SourceDate
            {
                get { return data.SourceDate; }
                set { data.SourceDate = value; OnPropertyChanged(nameof(SourceDate)); }
            }

            public string? AssemblyName
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

            public string? MemberType { get { return data.MemberType; } }

            public LibraryManagerItem(LibrarySourceItem source) : base()
            {
                data = source;
                data.PropertyChanged += Data_PropertyChanged;
            }

            private void Data_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (!String.IsNullOrWhiteSpace(e.PropertyName))
                { OnPropertyChanged(e.PropertyName); }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            public virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }

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
