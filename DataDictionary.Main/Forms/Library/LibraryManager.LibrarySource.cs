using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.LibraryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryManager
    {
        class LibraryManagerItem : LibrarySourceItem
        {
            private bool inModel = false;
            private bool inDatabase = false;

            public Boolean InModel { get { return inModel; } set { inModel = value; OnPropertyChanged(nameof(InModel)); } }
            public Boolean InDatabase { get { return inDatabase; } set { inDatabase = value; OnPropertyChanged(nameof(InDatabase)); } }
            public LibraryManagerItem() : base() { }

            public LibraryManagerItem(LibrarySourceItem source) : this()
            {
                this.LibraryId = source.LibraryId;
                this.LibraryTitle = source.LibraryTitle;
                this.LibraryDescription = source.LibraryDescription;
                this.SourceFile = source.SourceFile;
                this.SourceDate = source.SourceDate;
                this.AssemblyName = source.AssemblyName;
            }


            public LibrarySourceItem? Match(IEnumerable<LibrarySourceItem> items)
            {
                LibrarySourceKey key = new LibrarySourceKey(this);

                return items.FirstOrDefault(w => key.Equals(w));
            }
        }

        class LibraryManagerCollection : LibrarySourceCollection<LibraryManagerItem>
        {

            public void Build(IEnumerable<LibrarySourceItem> modelItems, IEnumerable<LibrarySourceItem> dbItems)
            {
                // List of keys all keys
                List<LibrarySourceKey> libraryKeys = modelItems.Select(s => new LibrarySourceKey(s))
                    .Union(dbItems.Select(s => new LibrarySourceKey(s)))
                    .Union(this.Select(s => new LibrarySourceKey(s)))
                    .ToList();

                foreach (LibrarySourceKey libraryKey in libraryKeys)
                {
                    LibraryManagerItem? item = this.FirstOrDefault(w => libraryKey.Equals(w));
                    LibrarySourceItem? modelItem = modelItems.FirstOrDefault(w => libraryKey.Equals(w));
                    LibrarySourceItem? dbItem = dbItems.FirstOrDefault(w => libraryKey.Equals(w));

                    if (item is null && dbItem is LibrarySourceItem)
                    {
                        item = new LibraryManagerItem(dbItem);
                        this.Add(item);
                    }

                    if (item is null && modelItem is LibrarySourceItem)
                    {
                        item = new LibraryManagerItem(modelItem);
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
