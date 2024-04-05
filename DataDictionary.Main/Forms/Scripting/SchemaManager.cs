using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Scripting.ComboBoxList;
using DataDictionary.Main.Messages;
using Microsoft.VisualBasic;
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

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaManager : ApplicationData
    {
        public Boolean IsOpenItem(object? item)
        { return bindingSchema.Current is ISchemaItem current && ReferenceEquals(current, item); }

        public SchemaManager() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(schemaToolStrip, 0);
        }

        public SchemaManager(ISchemaItem? schemaItem) : this()
        {
            if (schemaItem is null)
            {
                schemaItem = new SchemaItem();
                BusinessData.ScriptingEngine.Schemta.Add(schemaItem);
                BusinessData.NamedScope.Add(new NamedScopeItem(BusinessData.Model, schemaItem));
            }

            DataLayer.ScriptingData.Schema.SchemaKey key = new DataLayer.ScriptingData.Schema.SchemaKey(schemaItem);

            bindingSchema.DataSource = new BindingView<SchemaItem>(BusinessData.ScriptingEngine.Schemta, w => key.Equals(w));
            bindingSchema.Position = 0;

            Setup(bindingSchema);

            bindingElement.DataSource = new BindingView<ElementItem>(BusinessData.ScriptingEngine.Elements, w => key.Equals(w));
            bindingElement.SuspendBinding();
            elementOptionsLayout.Enabled = false;
        }


        private void SchemaManager_Load(object sender, EventArgs e)
        {
            SendMessage(new RefreshNavigation()); // Cannot do this in constructor because messengering is not yet hooked up.

            ISchemaItem schemaNames;
            IElementItem elementNames;
            this.DataBindings.Add(new Binding(nameof(this.Text), bindingSchema, nameof(schemaNames.SchemaTitle)));
            schemaTitleData.DataBindings.Add(new Binding(nameof(schemaTitleData.Text), bindingSchema, nameof(schemaNames.SchemaTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            schemaDescriptionData.DataBindings.Add(new Binding(nameof(schemaDescriptionData.Text), bindingSchema, nameof(schemaNames.SchemaDescription), false, DataSourceUpdateMode.OnPropertyChanged));

            scopeNameData.DataBindings.Add(new Binding(nameof(scopeNameData.Text), bindingElement, nameof(elementNames.ScopeName)));
            columNameData.DataBindings.Add(new Binding(nameof(columNameData.Text), bindingElement, nameof(elementNames.ColumnName)));
            elementNameData.DataBindings.Add(new Binding(nameof(elementNameData.Text), bindingElement, nameof(elementNames.ElementName), false, DataSourceUpdateMode.OnPropertyChanged));

            SupportedTypeItem.Load(elementTypeData);
            elementTypeData.DataBindings.Add(new Binding(nameof(elementTypeData.Text), bindingElement, nameof(elementNames.ElementType), false, DataSourceUpdateMode.OnPropertyChanged));

            renderAsElement.DataBindings.Add(new Binding(nameof(renderAsElement.Checked), bindingElement, nameof(elementNames.AsElement), false, DataSourceUpdateMode.OnPropertyChanged));
            renderAsAttribute.DataBindings.Add(new Binding(nameof(renderAsAttribute.Checked), bindingElement, nameof(elementNames.AsAttribute), false, DataSourceUpdateMode.OnPropertyChanged));
            renderNillableTrue.DataBindings.Add(new Binding(nameof(renderNillableTrue.Checked), bindingElement, nameof(elementNames.ElementNillable), false, DataSourceUpdateMode.OnPropertyChanged));

            renderDataAsText.DataBindings.Add(new Binding(nameof(renderDataAsText.Checked), bindingElement, nameof(elementNames.DataAsText), false, DataSourceUpdateMode.OnPropertyChanged));
            renderDataAsCData.DataBindings.Add(new Binding(nameof(renderDataAsCData.Checked), bindingElement, nameof(elementNames.DataAsCData), false, DataSourceUpdateMode.OnPropertyChanged));
            renderDataAsXml.DataBindings.Add(new Binding(nameof(renderDataAsXml.Checked), bindingElement, nameof(elementNames.DataAsXml), false, DataSourceUpdateMode.OnPropertyChanged));

            elementSelection.Groups.Clear();
            elementSelection.Items.Clear();

            foreach (IGrouping<ScopeType, ColumnItem> groups in BusinessData.ScriptingEngine.Columns.GroupBy(g => new ScopeKey(g).Scope))
            {
                ListViewGroup group = new ListViewGroup(groups.Key.ToName());
                elementSelection.Groups.Add(group);

                foreach (ColumnItem column in groups)
                {
                    ListViewItem newItem = new ListViewItem(column.ColumnName, group);
                    if (bindingElement.DataSource is IList<ElementItem> elements)
                    {
                        ColumnKey key = new ColumnKey(column);
                        if (elements.FirstOrDefault(w => key.Equals(w)) is ElementItem)
                        { newItem.Checked = true; }
                    }

                    elementSelection.Items.Add(newItem);
                    columnItems.Add(newItem, column);
                }
            }

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSchema.Current is not ISchemaItem);
        }

        private void addSchemaCommand_Click(object sender, EventArgs e)
        {

        }

        private void removeSchemaCommand_Click(object sender, EventArgs e)
        {
            if (bindingSchema.Current is SchemaItem item)
            {
                SchemaKey key = new SchemaKey(item);

                //bindingElement.DataSource = null;
                BusinessData.ScriptingEngine.Schemta.Remove(item);

                foreach (ElementItem element in BusinessData.ScriptingEngine.Elements.Where(w => key.Equals(w)).ToList())
                { BusinessData.ScriptingEngine.Elements.Remove(element); }

                SendMessage(new RefreshNavigation());
            }
        }

        protected override void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
        }

        protected override void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
        }

        protected override void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
        }


        Dictionary<ListViewItem, ColumnItem> columnItems = new Dictionary<ListViewItem, ColumnItem>(); // Used to cross reference ListViewItems to Columns
        ColumnItem? addColumn = null; // Used to pass value to bindingElement.AddNew.
        private void elementSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (elementSelection.SelectedItems.Count > 0 && columnItems.ContainsKey(elementSelection.SelectedItems[0]))
            {
                ListViewItem selected = elementSelection.SelectedItems[0];
                ColumnItem current = columnItems[selected];
                ColumnKey column = new ColumnKey(current);

                if (bindingElement.DataSource is IList<ElementItem> elements)
                {
                    ElementItem? element = elements.FirstOrDefault(w => column.Equals(w));

                    if (element is ElementItem)
                    {
                        bindingElement.ResumeBinding();
                        bindingElement.Position = elements.IndexOf(element);
                        elementOptionsLayout.Enabled = true;
                    }
                    else
                    {
                        bindingElement.SuspendBinding();
                        elementOptionsLayout.Enabled = false;
                    }
                }
            }
        }

        private void elementSelection_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            addColumn = null;

            if (columnItems.ContainsKey(e.Item) && bindingElement.DataSource is IList<ElementItem> elements)
            {
                ColumnItem current = columnItems[e.Item];
                ColumnKey key = new ColumnKey(current);
                ElementItem? element = elements.FirstOrDefault(w => key.Equals(w));

                if (e.Item.Checked && element is null)
                {
                    bindingElement.ResumeBinding();
                    addColumn = current;
                    bindingElement.AddNew();
                    elementOptionsLayout.Enabled = true;
                }

                if (!e.Item.Checked && element is not null)
                {
                    bindingElement.SuspendBinding();
                    bindingElement.Remove(element);
                    elementOptionsLayout.Enabled = false;
                }
            }

        }

        private void bindingElement_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingSchema.Current is SchemaItem schema)
            {
                ElementItem newElement = new ElementItem(schema);

                if (addColumn is not null)
                {
                    newElement.ColumnName = addColumn.ColumnName;
                    newElement.ElementName = addColumn.ColumnName;
                    newElement.Scope = addColumn.Scope;
                    newElement.AsElement = true;
                    newElement.AsAttribute = false;
                    newElement.DataAsText = true;
                    newElement.DataAsCData = false;
                    newElement.DataAsXml = false;
                    newElement.ElementNillable = true;

                    (string Name, Type NetType, bool IsSupported)? elementType = Enum.GetValues(typeof(XmlDataType)).Cast<XmlDataType>().Select(s => s.ToCrossReference()).Where(w => w.HasValue && w.Value.IsSupported && w.Value.NetType == addColumn.DataType).FirstOrDefault();
                    if (elementType is not null && elementType.HasValue)
                    { newElement.ElementType = elementType.Value.Name; }
                }
                e.NewObject = newElement;
            }
        }
    }
}
