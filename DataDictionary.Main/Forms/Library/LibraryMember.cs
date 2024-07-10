using DataDictionary.BusinessLayer.Library;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryMember : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingMember.Current is ILibraryMemberValue current && ReferenceEquals(current, item); }

        public LibraryMember() : base()
        {
            InitializeComponent();
        }

        public LibraryMember(ILibraryMemberValue libraryMember) : this()
        {
            LibraryMemberIndex key = new LibraryMemberIndex(libraryMember);
            this.Icon = ImageEnumeration.GetIcon(libraryMember.Scope);

            bindingMember.DataSource = new BindingView<LibraryMemberValue>(BusinessData.LibraryModel.LibraryMembers, w => key.Equals(new LibraryMemberIndex(w)));
            bindingMember.Position = 0;

            Setup(bindingMember);

            if (bindingMember.Current is ILibraryMemberValue current)
            {
                bindingChild.DataSource = new BindingView<LibraryMemberValue>(BusinessData.LibraryModel.LibraryMembers, w => new LibraryMemberIndexParent(w).Equals(key)); 
            }
        }

        private void LibraryMember_Load(object sender, EventArgs e)
        {
            ILibraryMemberValue bindingNames;

            memberNameSpaceData.DataBindings.Add(new Binding(nameof(memberNameSpaceData.Text), bindingMember, nameof(bindingNames.MemberNameSpace)));
            memberNameData.DataBindings.Add(new Binding(nameof(memberNameData.Text), bindingMember, nameof(bindingNames.MemberName)));
            scopeData.DataBindings.Add(new Binding(nameof(scopeData.Text), bindingMember, nameof(bindingNames.MemberType)));
            memberData.DataBindings.Add(new Binding(nameof(memberData.Text), bindingMember, nameof(bindingNames.MemberData)));
            assemblyNameData.DataBindings.Add(new Binding(nameof(assemblyNameData.Text), bindingMember, nameof(bindingNames.AssemblyName)));

            childMemberData.AutoGenerateColumns = false;
            childMemberData.DataSource = bindingChild;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingMember.Current is not ILibraryMemberValue);
        }

        private void childMemberData_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (bindingChild.Current is ILibraryMemberValue child)
            { Activate((data) => new LibraryMember(child), child); }
        }

    }
}
