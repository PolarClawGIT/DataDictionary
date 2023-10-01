using DataDictionary.DataLayer.LibraryData;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibrarySource : ApplicationBase, IApplicationDataForm
    {
        public LibrarySourceKey DataKey { get; private set; }

        public LibrarySource() : base()
        {
            InitializeComponent();
            //this.Icon = Resources.Icon_Attribute;
            DataKey = new LibrarySourceKey(new LibrarySourceItem());
        }

        public LibrarySource(ILibrarySourceKey librarySourceItem) : this()
        { DataKey = new LibrarySourceKey(librarySourceItem); }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }
    }
}
