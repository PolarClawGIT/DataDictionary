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
    partial class LibraryMember : ApplicationBase, IApplicationDataForm
    {
        public LibraryMemberKey DataKey { get; private set; }

        public LibraryMember() : base()
        {
            InitializeComponent();
            //this.Icon = Resources.Icon_Attribute;
            DataKey = new LibraryMemberKey(new LibraryMemberItem());
        }

        public LibraryMember(ILibraryMemberKey libraryMemberItem) : this()
        { DataKey = new LibraryMemberKey(libraryMemberItem); }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }
    }
}
