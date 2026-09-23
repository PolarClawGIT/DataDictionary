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

        FileFormatList(String extension) : this()
        {
            FileFormat = FileFormatType.Other;
            FileExtension = extension;
            FileTypeName = String.Format("{0} ({1})", FileFormatType.Other.GetName(), extension);
        }


        /// <summary>
        /// Loads the ComboBox with a list of FileFormatTypes.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="fileFormats"></param>
        public static void Load(ComboBox control, params IEnumerable<FileFormatType> fileFormats)
        {
            BindingList<FileFormatList> list = BuildList(fileFormats);

            control.DataSource = list;
            control.ValueMember = nameof(FileExtension);
            control.DisplayMember = nameof(FileTypeName);

            if (list.Count == 1)
            { control.SelectedIndex = 0; }
        }

        /// <summary>
        /// Loads the ComboBox with a list of FileFormatTypes. Allows existing
        /// </summary>
        /// <param name="control"></param>
        /// <param name="fileFormats"></param>
        /// <param name="otherExtensions"></param>
        public static void Load(ComboBoxData control, IEnumerable<FileFormatType>? fileFormats = null, IEnumerable<String?>? otherExtensions = null)
        {
            if (fileFormats is null)
            { fileFormats = Enum.GetValues<FileFormatType>(); }

            BindingList<FileFormatList> list = BuildList(fileFormats);

            control.DataSource = list;
            control.ValueMember = nameof(FileExtension);
            control.DisplayMember = nameof(FileTypeName);

            if (list.Count == 1)
            { control.SelectedIndex = 0; }

            if (control.DropDownStyle is not ComboBoxStyle.DropDownList)
            { control.TextUpdated += Control_TextUpdated; }

            if (otherExtensions is not null)
            {
                foreach (String item in otherExtensions.
                    Select(s => s ?? String.Empty).
                    Where(w => !list.Any(a => a.FileExtension.Equals(w, StringComparison.InvariantCultureIgnoreCase))).
                    Where(w => !String.IsNullOrWhiteSpace(w)).
                    Distinct())
                { list.Add(new FileFormatList(item)); }
            }

            void Control_TextUpdated(Object? sender, EventArgs e)
            {
                if (control.SelectedItem is null && !String.IsNullOrWhiteSpace(control.Text))
                {
                    FileFormatList? matched = list.
                        FirstOrDefault(w => 
                            w.FileFormat is not FileFormatType.Other
                            && w.FileExtension.Equals(control.Text, StringComparison.InvariantCultureIgnoreCase));

                    if (matched is null)
                    {
                        FileFormatList newItem = new FileFormatList(control.Text);

                        list.Add(new FileFormatList(control.Text));
                        control.SelectedItem = newItem;
                    }
                    else
                    { control.SelectedItem = matched; }
                }
            }
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
