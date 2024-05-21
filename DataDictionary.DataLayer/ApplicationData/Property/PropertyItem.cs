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
    public interface IPropertyItem : IPropertyKey, IPropertyKeyExtended, IPropertyKeyName
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
        public Nullable<Guid> PropertyId { get { return GetValue<Guid>(nameof(PropertyId)); } protected set { SetValue<Guid>(nameof(PropertyId), value); } }

        /// <inheritdoc/>
        public String? PropertyTitle { get { return GetValue(nameof(PropertyTitle)); } set { SetValue(nameof(PropertyTitle), value); } }

        /// <inheritdoc/>
        public String? PropertyDescription { get { return GetValue(nameof(PropertyDescription)); } set { SetValue(nameof(PropertyDescription), value); } }

        /// <inheritdoc/>
        public Nullable<Boolean> IsDefinition { get { return GetValue<Boolean>(nameof(IsDefinition), BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>(nameof(IsDefinition), value); } }

        /// <inheritdoc/>
        public Nullable<Boolean> IsExtendedProperty { get { return GetValue<Boolean>(nameof(IsExtendedProperty), BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>(nameof(IsExtendedProperty), value); } }

        /// <inheritdoc/>
        public Nullable<Boolean> IsFrameworkSummary { get { return GetValue<Boolean>(nameof(IsFrameworkSummary), BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>(nameof(IsFrameworkSummary), value); } }

        /// <inheritdoc/>
        public Nullable<Boolean> IsChoice { get { return GetValue<Boolean>(nameof(IsChoice), BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>(nameof(IsChoice), value); } }

        /// <inheritdoc/>
        public String? ExtendedProperty { get { return GetValue(nameof(ExtendedProperty)); } set { SetValue(nameof(ExtendedProperty), value); } }

        /// <inheritdoc/>
        protected String? ChoiceList { get { return GetValue(nameof(ChoiceList)); } set { SetValue(nameof(ChoiceList), value); } }

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
            new DataColumn(nameof(PropertyId), typeof(Guid)){ AllowDBNull = false},
            new DataColumn(nameof(PropertyTitle), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(PropertyDescription), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(IsDefinition), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsExtendedProperty), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsFrameworkSummary), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(IsChoice), typeof(Boolean)){ AllowDBNull = true},
            new DataColumn(nameof(ExtendedProperty), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(ChoiceList), typeof(String)){ AllowDBNull = true},
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
