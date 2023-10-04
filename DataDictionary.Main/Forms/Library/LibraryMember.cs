using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.Main.Messages;
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
using System.Xml;
using System.Xml.Linq;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryMember : ApplicationBase, IApplicationDataForm
    {
        public LibraryMemberKey DataKey { get; private set; }

        public LibraryMember() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Class;
            DataKey = new LibraryMemberKey(new LibraryMemberItem());
        }

        public LibraryMember(ILibraryMemberKey libraryMemberItem) : this()
        { DataKey = new LibraryMemberKey(libraryMemberItem); }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }


        private void LibraryMember_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            if (Program.Data.LibraryMembers.FirstOrDefault(w => DataKey.Equals(w)) is LibraryMemberItem memberItem)
            {
                switch (memberItem.MemberItemType().type)
                {
                    case LibraryMemberType.Type:
                        this.Icon = Resources.Icon_Class;
                        break;
                    case LibraryMemberType.Field or LibraryMemberType.Property:
                        this.Icon = Resources.Icon_Field;
                        break;
                    case LibraryMemberType.Method or LibraryMemberType.Event:
                        this.Icon = Resources.Icon_Assembly; //TODO: Change to Method
                        break;
                    default: break;
                }

                this.Text = memberItem.MemberName;

                memberNameSpaceData.DataBindings.Add(new Binding(nameof(memberNameSpaceData.Text), memberItem, nameof(memberItem.MemberNameSpace)));
                memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), memberItem, nameof(memberItem.MemberName)));
                memberTypeData.DataBindings.Add(new Binding(nameof(memberTypeData.Text), memberItem, nameof(memberItem.MemberType)));
                memberData.DataBindings.Add(new Binding(nameof(memberData.Text), memberItem, nameof(memberItem.MemberData)));
                assemblyNameData.DataBindings.Add(new Binding(nameof(assemblyNameData.Text), memberItem, nameof(memberItem.AssemblyName)));
            }
        }

        void UnBindData()
        {
            memberNameSpaceData.DataBindings.Clear();
            memberNameData.DataBindings.Clear();
            memberTypeData.DataBindings.Clear();
            memberData.DataBindings.Clear();
            assemblyNameData.DataBindings.Clear();
        }
        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion

    }
}
