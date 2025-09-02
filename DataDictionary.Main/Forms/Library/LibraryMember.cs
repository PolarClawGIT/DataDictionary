using DataDictionary.BusinessLayer.AppLibrary;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Library
{
    partial class LibraryMember : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingMember.Current is ILibraryMemberValue current && ReferenceEquals(current, item); }

        protected LibraryMember() : base()
        {
            InitializeComponent();
            SetRowState(bindingMember, bindingChild);
            SetTitle(bindingMember);
        }

        public LibraryMember(ILibraryMemberValue libraryMember) : this()
        {
            LibraryMemberIndex key = new LibraryMemberIndex(libraryMember);

            bindingMember.DataSource = new BindingView<LibraryMemberValue>(BusinessData.LibraryModel.LibraryMembers, w => key.Equals(new LibraryMemberIndex(w)));
            bindingMember.Position = 0;

            if (bindingMember.Current is ILibraryMemberValue current)
            { bindingChild.DataSource = new BindingView<LibraryMemberValue>(BusinessData.LibraryModel.LibraryMembers, w => new LibraryMemberIndexParent(w).Equals(key)); }
        }

        private void LibraryMember_Load(object sender, EventArgs e)
        {
            memberNameSpaceData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingMember, nameof(ILibraryMemberValue.MemberNameSpace)));
            memberNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingMember, nameof(ILibraryMemberValue.MemberName)));
            scopeData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingMember, nameof(ILibraryMemberValue.MemberType)));
            memberData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingMember, nameof(ILibraryMemberValue.MemberData)));
            assemblyNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingMember, nameof(ILibraryMemberValue.AssemblyName)));

            childMemberData.AutoGenerateColumns = false;
            childMemberData.DataSource = bindingChild;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingMember.Current is not ILibraryMemberValue);
        }

        private void childMemberData_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (bindingChild.Current is ILibraryMemberValue child)
            {
                Activate(
                    () => new LibraryMember(child),
                    (form) => form.IsOpenItem(child));
            }
        }

    }
}
