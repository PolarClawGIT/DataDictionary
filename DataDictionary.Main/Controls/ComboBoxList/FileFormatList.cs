using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record FileFormatList
    {
        public FileFormatType FileFormat { get; init; } = FileFormatType.Other;
        public String FileExtension { get; init; } = String.Empty;
        public String FileTypeName { get; init; } = String.Empty;

        public static String NullValue { get; } = String.Empty;

        protected FileFormatList() : base() { }

        FileFormatList(IFileFormatEnumeration fileFormat, String extension) : this()
        {
            FileFormat = fileFormat.Value;

            if (String.IsNullOrWhiteSpace(extension))
            { FileTypeName = fileFormat.DisplayName; }
            else
            {
                FileTypeName = String.Format("{0} ({1})", fileFormat.DisplayName, extension);
                FileExtension = extension;
            }
        }

        public static void Load(ComboBox control, params IEnumerable<FileFormatType> fileFormats)
        {
            BindingList<FileFormatList> list = BuildList(fileFormats);

            control.DataSource = list;
            control.ValueMember = nameof(FileExtension);
            control.DisplayMember = nameof(FileTypeName);

            if (list.Count == 1)
            { control.SelectedIndex = 0; }
        }

        public static void Load(ComboBoxData control, params IEnumerable<FileFormatType> fileFormats)
        {
            BindingList<FileFormatList> list = BuildList(fileFormats);

            control.DataSource = list;
            control.ValueMember = nameof(FileExtension);
            control.DisplayMember = nameof(FileTypeName);

            if (list.Count == 1)
            { control.SelectedIndex = 0; }
        }


        static BindingList<FileFormatList> BuildList(IEnumerable<FileFormatType> fileTypes, IEnumerable<IFileValue>? fileValues = null)
        {
            BindingList<FileFormatList> list = new BindingList<FileFormatList>();

            list.AddRange(fileTypes.
                Select(s => s.GetEnumeration()).
                SelectMany(s => s.Extensions, (p, c) => new FileFormatList(p, c)));

            //TODO: check for use of other extensions.

            return list;
        }
    }
}
