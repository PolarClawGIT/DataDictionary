using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataDictionary.Main.Controls.ComboBoxList
{
    record DirectoryTypeList
    {
        public DirectoryType DirectoryType { get; set; } = DirectoryType.Null;
        public String DirectoryName { get; init; } = String.Empty;

        DirectoryTypeList() : base() { }

        public static void Load(ComboBoxData control, params IEnumerable<DirectoryType> directoryTypes)
        {
            DirectoryTypeList directoryTypeItem = new DirectoryTypeList();
            BindingList<DirectoryTypeList> list = new BindingList<DirectoryTypeList>();
            foreach (DirectoryType item in directoryTypes)
            {
                String name = item.GetEnumeration().DisplayName;
                if (!String.IsNullOrEmpty(name))
                { list.Add(new DirectoryTypeList() { DirectoryType = item, DirectoryName = name }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(directoryTypeItem.DirectoryType);
            control.DisplayMember = nameof(directoryTypeItem.DirectoryName);
        }

        public static void Load(ComboBoxData control)
        { Load(control, Enum.GetValues<DirectoryType>()); }

        public static void Load(DataGridViewComboBoxColumn control, params IEnumerable<DirectoryType> directoryTypes)
        {
            DirectoryTypeList directoryTypeItem = new DirectoryTypeList();
            BindingList<DirectoryTypeList> list = new BindingList<DirectoryTypeList>();
            foreach (DirectoryType item in directoryTypes)
            {
                String name = item.GetEnumeration().DisplayName;
                if (!String.IsNullOrEmpty(name))
                { list.Add(new DirectoryTypeList() { DirectoryType = item, DirectoryName = name }); }
            }

            control.DataSource = list;
            control.ValueMember = nameof(directoryTypeItem.DirectoryType);
            control.DisplayMember = nameof(directoryTypeItem.DirectoryName);
        }

        public static void Load(DataGridViewComboBoxColumn control)
        { Load(control, Enum.GetValues<DirectoryType>()); }

    }
}
