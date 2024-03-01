using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Properties;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryMember : ApplicationBase, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingMember.Current is ILibraryMemberItem current && ReferenceEquals(current, item); }

        public LibraryMember() : base()
        {
            InitializeComponent();
        }

        public LibraryMember(ILibraryMemberItem libraryMember) : this()
        {
            LibraryMemberKeyName nameKey = new LibraryMemberKeyName(libraryMember);

            bindingMember.DataSource = new BindingView<LibraryMemberItem>(BusinessData.LibraryModel.LibraryMembers, w => nameKey.Equals(w));
            bindingMember.Position = 0;

            if (bindingMember.Current is ILibraryMemberItem current)
            {
                LibraryMemberKey key = new LibraryMemberKey(current);
                this.Icon = new ScopeKey(current).Scope.ToIcon();
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();

                bindingChild.DataSource = new BindingView<LibraryMemberItem>(BusinessData.LibraryModel.LibraryMembers, w => new LibraryMemberKeyParent(w).Equals(key));
            }
        }

        private void LibraryMember_Load(object sender, EventArgs e)
        {
            ILibraryMemberItem bindingNames;

            memberNameSpaceData.DataBindings.Add(new Binding(nameof(memberNameSpaceData.Text), bindingMember, nameof(bindingNames.NameSpace)));
            memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), bindingMember, nameof(bindingNames.MemberName)));
            scopeData.DataBindings.Add(new Binding(nameof(scopeData.Text), bindingMember, nameof(bindingNames.ScopeName)));
            memberData.DataBindings.Add(new Binding(nameof(memberData.Text), bindingMember, nameof(bindingNames.MemberData)));
            assemblyNameData.DataBindings.Add(new Binding(nameof(assemblyNameData.Text), bindingMember, nameof(bindingNames.AssemblyName)));

            childMemberData.AutoGenerateColumns = false;
            childMemberData.DataSource = bindingChild;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingMember.Current is not ILibraryMemberItem);
        }

        private void childMemberData_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (bindingChild.Current is ILibraryMemberItem child)
            { Activate((data) => new LibraryMember(child), child); }
        }

    }
}
