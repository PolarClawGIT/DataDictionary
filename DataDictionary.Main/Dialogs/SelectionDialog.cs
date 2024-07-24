using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Dialogs
{
    partial class SelectionDialog : Form
    {
        public SelectionDialog() : base()
        {
            InitializeComponent();
        }

        private void AcceptCommand_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelCommand_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SelectionLayout_SizeChanged(object sender, EventArgs e)
        { }


    }

    class SelectionDialog<TValue, TIndex> : SelectionDialog
        where TValue : INamedScopeSourceValue
        where TIndex : class, IKey//, IKeyEquality<TIndex>
    {
        public IEnumerable<TValue> DataSource { get; init; } = new List<TValue>();
        public IList<TIndex> Selected { get; init; } = new List<TIndex>();

        public Func<TValue, TIndex?> AsIndex { get; init; } = (value) => { return null; };

        public Func<TValue, String> GetDescription { get; init; } = (value) => { return String.Empty; };

        public Func<TValue, String> GetTitle { get; init; } = (value) => { return value.Title; };

        Dictionary<ListViewItem, TValue> listViewItems = new Dictionary<ListViewItem, TValue>();


        public SelectionDialog() : base()
        {
            selectionData.SmallImageList = ImageEnumeration.AsImageList();

            this.Load += SelectionDialog_Load;
            selectionData.ItemChecked += SelectionData_ItemChecked;
            selectionData.SelectedIndexChanged += SelectionData_SelectedIndexChanged;
        }



        private void SelectionDialog_Load(Object? sender, EventArgs e)
        {
            foreach (TValue item in DataSource)
            {
                ListViewItem newItem = new ListViewItem(item.Title);
                selectionData.Items.Add(newItem);
                listViewItems.Add(newItem, item);
                newItem.ImageKey = ImageEnumeration.Cast(item.Scope).Name;

                if (Selected.FirstOrDefault(w => w.Equals(AsIndex(item))) is TIndex)
                { newItem.Checked = true; }
            }
        }

        private void SelectionData_ItemChecked(Object? sender, ItemCheckedEventArgs e)
        {
            if (listViewItems.ContainsKey(e.Item))
            {
                if (e.Item.Checked
                    && AsIndex(listViewItems[e.Item]) is TIndex addKey
                    && !Selected.Contains(addKey))
                { Selected.Add(addKey); }

                if(!e.Item.Checked
                    && AsIndex(listViewItems[e.Item]) is TIndex removeKey
                    && Selected.Contains(removeKey))
                { Selected.Remove(removeKey); }
            }
        }

        private void SelectionData_SelectedIndexChanged(Object? sender, EventArgs e)
        {
            if(selectionData.SelectedItems.Count > 0 
                && listViewItems.ContainsKey(selectionData.SelectedItems[0]))
            {
                titleData.Text = listViewItems[selectionData.SelectedItems[0]].Title;
                pathData.Text = listViewItems[selectionData.SelectedItems[0]].Path.MemberFullPath;
            }
            
        }

    }
}
