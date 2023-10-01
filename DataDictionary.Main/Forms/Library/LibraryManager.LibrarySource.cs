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
            public Boolean InModel { get; set; } = false;
            public Boolean InDatabase { get; set; } = false;

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
        }

        class LibraryManagerCollection : LibrarySourceCollection<LibraryManagerItem>
        {

            public void Build(LibrarySourceCollection<LibrarySourceItem> data)
            {
                this.Clear();

                // List of keys in both sources
                List<LibrarySourceKey> libraryKeys = data.Select(s => new LibrarySourceKey(s))
                    .Union(Program.Data.LibrarySources.Select(s => new LibrarySourceKey(s)))
                    .ToList();

                foreach (LibrarySourceKey libraryKey in libraryKeys)
                {
                    LibraryManagerItem? newItem = null;

                    if (Program.Data.LibrarySources.FirstOrDefault(w => libraryKey.Equals(w)) is LibrarySourceItem source)
                    {
                        if (newItem is null)
                        { newItem = new LibraryManagerItem(source); }

                        newItem.InModel = true;
                    }

                    if (data.FirstOrDefault(w => libraryKey.Equals(w)) is LibrarySourceItem dbsource)
                    {
                        if (newItem is null)
                        { newItem = new LibraryManagerItem(dbsource); }

                        newItem.InDatabase = true;
                    }

                    if (newItem is LibraryManagerItem)
                    { this.Add(newItem); }
                }
            }

            public void RefreshFromModel()
            {
                this.Clear();

                List<LibrarySourceKey> libraryKeys = this.Select(s => new LibrarySourceKey(s))
                    .Union(Program.Data.LibrarySources.Select(s => new LibrarySourceKey(s)))
                    .ToList();

                foreach (LibrarySourceKey libraryKey in libraryKeys)
                {
                    LibraryManagerItem? newItem = null;

                    if (this.FirstOrDefault(w => libraryKey.Equals(w)) is LibrarySourceItem alreadyHere)
                    { } // Do not think I need to do anything with it
                    else if (Program.Data.LibrarySources.FirstOrDefault(w => libraryKey.Equals(w)) is LibrarySourceItem source)
                    {
                        if (newItem is null)
                        { newItem = new LibraryManagerItem(source); }

                        newItem.InModel = true;
                        this.Add(newItem);
                    }
                }
            }
        }

    }
}
