using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.LibraryData;
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
using Toolbox.BindingTable;

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
        }

        private void LibraryMember_Load(object sender, EventArgs e)
        { (this as IApplicationDataBind).BindData(); }

        public bool BindDataCore()
        {
            if (Program.Data.LibraryMembers.FirstOrDefault(w => DataKey.Equals(w)) is LibraryMemberItem memberItem)
            {
                switch (new ScopeKey(memberItem).Scope)
                {
                    case ScopeType.LibraryType:
                        this.Icon = Resources.Icon_Class;
                        break;
                    case ScopeType.LibraryField or ScopeType.LibraryProperty:
                        this.Icon = Resources.Icon_Field;
                        break;
                    case ScopeType.LibraryMethod or ScopeType.LibraryEvent:
                        this.Icon = Resources.Icon_Method;
                        break;
                    case ScopeType.LibraryParameter:
                        this.Icon = Resources.Icon_Parameter;
                        break;
                    case ScopeType.LibraryNameSpace:
                        this.Icon = Resources.Icon_Namespace;
                        break;
                    default: break;
                }

                this.Text = memberItem.MemberName;

                memberNameSpaceData.DataBindings.Add(new Binding(nameof(memberNameSpaceData.Text), memberItem, nameof(memberItem.NameSpace)));
                memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), memberItem, nameof(memberItem.MemberName)));
                scopeData.DataBindings.Add(new Binding(nameof(scopeData.Text), memberItem, nameof(memberItem.ScopeName)));
                memberData.DataBindings.Add(new Binding(nameof(memberData.Text), memberItem, nameof(memberItem.MemberData)));
                assemblyNameData.DataBindings.Add(new Binding(nameof(assemblyNameData.Text), memberItem, nameof(memberItem.AssemblyName)));

                LibraryMemberKey memberKey = new LibraryMemberKey(memberItem);
                BindingView<LibraryMemberItem> childMembers = new BindingView<LibraryMemberItem>(Program.Data.LibraryMembers, w => new LibraryMemberKeyParent(w).Equals(memberKey));

                childMemberData.AutoGenerateColumns = false;
                childMemberData.DataSource = childMembers;

                return true;
            }
            else { return false; }
        }

        public void UnbindDataCore()
        {
            memberNameSpaceData.DataBindings.Clear();
            memberNameData.DataBindings.Clear();
            memberData.DataBindings.Clear();
            assemblyNameData.DataBindings.Clear();
        }

        private void childMemberData_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (childMemberData.Rows[e.RowIndex].DataBoundItem is LibraryMemberItem memberItem)
            { Activate((data) => new Forms.Library.LibraryMember() { DataKey = new LibraryMemberKey(memberItem) }, memberItem); }
        }

    }
}
