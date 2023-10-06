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
    partial class LibraryMember : ApplicationBase, IApplicationDataForm<LibraryMemberKey>
    {
        public required LibraryMemberKey DataKey { get; init; }

        public bool IsOpenItem(object? item)
        { return DataKey.Equals(item); }

        public LibraryMember() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Class;
        }

        private void LibraryMember_Load(object sender, EventArgs e)
        { (this as IApplicationDataBind).BindData(); }

        public bool BindDataCore()
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
                        this.Icon = Resources.Icon_Method;
                        break;
                    default: break;
                }

                this.Text = memberItem.MemberName;

                memberNameSpaceData.DataBindings.Add(new Binding(nameof(memberNameSpaceData.Text), memberItem, nameof(memberItem.MemberNameSpace)));
                memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), memberItem, nameof(memberItem.MemberName)));
                memberTypeData.DataBindings.Add(new Binding(nameof(memberTypeData.Text), memberItem, nameof(memberItem.MemberType)));
                memberData.DataBindings.Add(new Binding(nameof(memberData.Text), memberItem, nameof(memberItem.MemberData)));
                assemblyNameData.DataBindings.Add(new Binding(nameof(assemblyNameData.Text), memberItem, nameof(memberItem.AssemblyName)));

                return true;
            }
            else { return false; }
        }

        public void UnbindDataCore()
        {
            memberNameSpaceData.DataBindings.Clear();
            memberNameData.DataBindings.Clear();
            memberTypeData.DataBindings.Clear();
            memberData.DataBindings.Clear();
            assemblyNameData.DataBindings.Clear();
        }

    }
}
