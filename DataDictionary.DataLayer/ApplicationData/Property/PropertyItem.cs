using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData.Property
{
    /// <summary>
    /// Interface for the Property data.
    /// </summary>
    public interface IPropertyItem : IPropertyKey, IPropertyKeyUnique, IDataItem
    {
        /// <summary>
        /// Description of the Property. How the property is used.
        /// </summary>
        String? PropertyDescription { get; }

        /// <summary>
        /// Does the Property represent a Definition.
        /// </summary>
        Nullable<Boolean> IsDefinition { get; }

        /// <summary>
        /// Does the Property represent a MS SQL Extended Property.
        /// </summary>
        Nullable<Boolean> IsExtendedProperty { get; }

        /// <summary>
        /// Does the Property represent a .Net Framework Summary document item.
        /// </summary>
        Nullable<Boolean> IsFrameworkSummary { get; }

        /// <summary>
        /// Does the Property represent a choice of items (see Choices).
        /// </summary>
        Nullable<Boolean> IsChoice { get; }

        /// <summary>
        /// The name of the MS SQL Extended Property to be populated.
        /// </summary>
        String? ExtendedProperty { get; }

        /// <summary>
        /// List of Possible Choices to be presented.
        /// </summary>
        BindingList<PropertyItem.ChoiceItem> Choices { get; }
    }

    /// <summary>
    /// Implementation of the Property data.
    /// </summary>
    [Serializable]
    public class PropertyItem : BindingTableRow, IPropertyItem, ISerializable, IValidateItem<PropertyItem>
    {
        /// <inheritdoc/>
        public Nullable<Guid> PropertyId { get { return GetValue<Guid>("PropertyId"); } protected set { SetValue<Guid>("PropertyId", value); } }

        /// <inheritdoc/>
        public String? PropertyTitle { get { return GetValue("PropertyTitle"); } set { SetValue("PropertyTitle", value); } }

        /// <inheritdoc/>
        public String? PropertyDescription { get { return GetValue("PropertyDescription"); } set { SetValue("PropertyDescription", value); } }

        /// <inheritdoc/>
        public Nullable<Boolean> IsDefinition { get { return GetValue<Boolean>("IsDefinition", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("IsDefinition", value); } }

        /// <inheritdoc/>
        public Nullable<Boolean> IsExtendedProperty { get { return GetValue<Boolean>("IsExtendedProperty", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("IsExtendedProperty", value); } }

        /// <inheritdoc/>
        public Nullable<Boolean> IsFrameworkSummary { get { return GetValue<Boolean>("IsFrameworkSummary", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("IsFrameworkSummary", value); } }

        /// <inheritdoc/>
        public Nullable<Boolean> IsChoice { get { return GetValue<Boolean>("IsChoice", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("IsChoice", value); } }

        /// <inheritdoc/>
        public String? ExtendedProperty { get { return GetValue("ExtendedProperty"); } set { SetValue("ExtendedProperty", value); } }

        /// <inheritdoc/>
        protected String? ChoiceList { get { return GetValue("ChoiceList"); } set { SetValue("ChoiceList", value); } }

        /// <summary>
        /// The Choice Item used to present a List of Choices.
        /// </summary>
        public struct ChoiceItem : INotifyPropertyChanged
        {
            /// <summary>
            /// The Choice from the Choice List.
            /// </summary>
            public String Choice
            {
                get { return choice; }
                set { choice = value; OnPropertyChanged(nameof(Choice)); }
            }
            String choice;

            /// <summary>
            /// Constructor for the Choice
            /// </summary>
            public ChoiceItem()
            { choice = String.Empty; }

            /// <inheritdoc/>
            public event PropertyChangedEventHandler? PropertyChanged;

            void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }
        }

        /// <inheritdoc/>
        public BindingList<ChoiceItem> Choices { get { return choices; } }
        BindingList<ChoiceItem> choices = new BindingList<ChoiceItem>();

        /// <summary>
        /// Constructor for the Property Data.
        /// </summary>
        public PropertyItem() : base()
        {
            PropertyId = Guid.NewGuid();

            choices.ListChanged += Choices_ListChanged;
        }

        private void Choices_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (choices.Count > 0) { ChoiceList = String.Join(", ", choices.Select(s => s.Choice)); }
            else { ChoiceList = String.Empty; }
        }

        /// <inheritdoc/>
        protected override void Table_Disposed(object? sender, EventArgs e)
        {
            base.Table_Disposed(sender, e);
            choices.ListChanged -= Choices_ListChanged;
        }

        /// <inheritdoc/>
        protected override void SetRow(DataRow row)
        {
            choices.ListChanged -= Choices_ListChanged;
            base.SetRow(row);

            choices.Clear();
            if (ChoiceList is not null)
            { choices.AddRange(ChoiceList.Split(",").Select(s => new ChoiceItem() { Choice = s })); }

            choices.ListChanged += Choices_ListChanged;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("PropertyId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("PropertyTitle", typeof(String)){ AllowDBNull = true},
            new DataColumn("PropertyDescription", typeof(String)){ AllowDBNull = true},
            new DataColumn("IsDefinition", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsExtendedProperty", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsFrameworkSummary", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsChoice", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("ExtendedProperty", typeof(String)){ AllowDBNull = true},
            new DataColumn("ChoiceList", typeof(String)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        /// <inheritdoc/>
        public Boolean Validate()
        {
            Boolean result = false;

            if (String.IsNullOrWhiteSpace(this.PropertyTitle))
            { SetRowError("[PropertyTitle] cannot be empty"); }
            else if (this.PropertyId == Guid.Empty)
            { SetRowError("[PropertyId] cannot be empty"); }
            else { result = true; }

            return result;
        }



        #region ISerializable
        /// <summary>
        /// Serialization Constructor for the Property Data.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected PropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            choices.Clear();
            if (ChoiceList is not null)
            { choices.AddRange(ChoiceList.Split(",").Select(s => new ChoiceItem() { Choice = s })); }
        }
        #endregion
    }
}
