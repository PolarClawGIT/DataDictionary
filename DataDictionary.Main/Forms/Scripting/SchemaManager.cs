using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Scripting.ComboBoxList;
using DataDictionary.Main.Messages;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaManager : ApplicationData
    {
        public Boolean IsOpenItem(object? item)
        { return bindingSchema.Current is IDefinitionValue current && ReferenceEquals(current, item); }

        public SchemaManager() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(schemaToolStrip, 0);
        }

        public SchemaManager(IDefinitionValue? schemaItem) : this()
        {
            if (schemaItem is null)
            {
                schemaItem = new DefinitionValue();
                BusinessData.ScriptingEngine.Schemta.Add(schemaItem);
            }

            DataLayer.ScriptingData.Schema.SchemaKey key = new DataLayer.ScriptingData.Schema.SchemaKey(schemaItem);

            bindingSchema.DataSource = new BindingView<DefinitionValue>(BusinessData.ScriptingEngine.Schemta, w => key.Equals(w));
            bindingSchema.Position = 0;

            Setup(bindingSchema);

            bindingElement.DataSource = new BindingView<DefinitionElementValue>(BusinessData.ScriptingEngine.SchemeElements, w => key.Equals(w));
            bindingElement.SuspendBinding();
            elementOptionsLayout.Enabled = false;
        }


        private void SchemaManager_Load(object sender, EventArgs e)
        {
            SendMessage(new RefreshNavigation()); // Cannot do this in constructor because messengering is not yet hooked up.

            IDefinitionValue schemaNames;
            IDefinitionElementValue elementNames;
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

            foreach (IGrouping<ScopeType, ColumnValue> groups in BusinessData.ScriptingEngine.Columns.GroupBy(g => new ScopeKey(g).Scope))
            {
                ListViewGroup group = new ListViewGroup(groups.Key.ToName());
                elementSelection.Groups.Add(group);

                foreach (ColumnValue column in groups)
                {
                    ListViewItem newItem = new ListViewItem(column.ColumnName, group);
                    if (bindingElement.DataSource is IList<DefinitionElementValue> elements)
                    {
                        ColumnIndex key = new ColumnIndex(column);
                        if (elements.FirstOrDefault(w => key.Equals(w)) is DefinitionElementValue)
                        { newItem.Checked = true; }
                    }

                    elementSelection.Items.Add(newItem);
                    columnItems.Add(newItem, column);
                }
            }

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSchema.Current is not IDefinitionValue);
        }

        private void addSchemaCommand_Click(object sender, EventArgs e)
        {

        }

        private void removeSchemaCommand_Click(object sender, EventArgs e)
        {
            if (bindingSchema.Current is DefinitionValue item)
            {
                DefinitionIndex key = new DefinitionIndex(item);

                //bindingElement.DataSource = null;
                BusinessData.ScriptingEngine.Schemta.Remove(item);

                foreach (DefinitionElementValue element in BusinessData.ScriptingEngine.SchemeElements.Where(w => key.Equals(w)).ToList())
                { BusinessData.ScriptingEngine.SchemeElements.Remove(element); }

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


        Dictionary<ListViewItem, ColumnValue> columnItems = new Dictionary<ListViewItem, ColumnValue>(); // Used to cross reference ListViewItems to Columns
        ColumnValue? addColumn = null; // Used to pass value to bindingElement.AddNew.
        private void elementSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (elementSelection.SelectedItems.Count > 0 && columnItems.ContainsKey(elementSelection.SelectedItems[0]))
            {
                ListViewItem selected = elementSelection.SelectedItems[0];
                ColumnValue current = columnItems[selected];
                ColumnIndex column = new ColumnIndex(current);

                if (bindingElement.DataSource is IList<DefinitionElementValue> elements)
                {
                    DefinitionElementValue? element = elements.FirstOrDefault(w => column.Equals(w));

                    if (element is DefinitionElementValue)
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

            if (columnItems.ContainsKey(e.Item) && bindingElement.DataSource is IList<DefinitionElementValue> elements)
            {
                ColumnValue current = columnItems[e.Item];
                ColumnIndex key = new ColumnIndex(current);
                DefinitionElementValue? element = elements.FirstOrDefault(w => key.Equals(w));

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
            if (bindingSchema.Current is DefinitionValue schema)
            {
                DefinitionElementValue newElement = new DefinitionElementValue(schema);

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
