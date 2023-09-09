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

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IPropertyItem : IPropertyKey, IPropertyKeyUnique
    {
        String? PropertyDescription { get; }
        Nullable<Boolean> IsExtendedProperty { get; }
        Nullable<Boolean> IsDefinition { get; }
        Nullable<Boolean> IsChoice { get; }
        String? ExtendedProperty { get; }
        //String? ChoiceList { get; }
        BindingList<PropertyItem.ChoiceItem> Choices { get; }
    }

    [Serializable]
    public class PropertyItem : BindingTableRow, IPropertyItem, ISerializable
    {
        public Nullable<Guid> PropertyId { get { return GetValue<Guid>("PropertyId"); } protected set { SetValue<Guid>("PropertyId", value); } }
        public String? PropertyTitle { get { return GetValue("PropertyTitle"); } set { SetValue("PropertyTitle", value); } }
        public String? PropertyDescription { get { return GetValue("PropertyDescription"); } set { SetValue("PropertyDescription", value); } }
        public Nullable<Boolean> IsExtendedProperty { get { return GetValue<Boolean>("IsExtendedProperty", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("IsExtendedProperty", value); } }
        public Nullable<Boolean> IsDefinition { get { return GetValue<Boolean>("IsDefinition", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("IsDefinition", value); } }
        public Nullable<Boolean> IsChoice { get { return GetValue<Boolean>("IsChoice", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("IsChoice", value); } }
        public String? ExtendedProperty { get { return GetValue("ExtendedProperty"); } set { SetValue("ExtendedProperty", value); } }
        protected String? ChoiceList { get { return GetValue("ChoiceList"); } set { SetValue("ChoiceList", value); } }
        public Nullable<Boolean> Obsolete { get { return GetValue<Boolean>("Obsolete", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("Obsolete", value); } }

        public struct ChoiceItem : INotifyPropertyChanged
        {
            String choice;
            public String Choice
            {
                get { return choice; }
                set { choice = value; OnPropertyChanged(nameof(Choice)); }
            }

            public ChoiceItem()
            { choice = String.Empty; }

            public event PropertyChangedEventHandler? PropertyChanged;
            public void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }
        }

        BindingList<ChoiceItem> choices = new BindingList<ChoiceItem>();
        public BindingList<ChoiceItem> Choices { get { return choices; } }


        public PropertyItem() : base()
        {
            PropertyId = Guid.NewGuid();
            Obsolete = false;

            choices.ListChanged += Choices_ListChanged;
        }

        private void Choices_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (choices.Count > 0) { ChoiceList = String.Join(", ", choices.Select(s => s.Choice)); }
            else { ChoiceList = String.Empty; }
        }

        protected override void Table_Disposed(object? sender, EventArgs e)
        {
            base.Table_Disposed(sender, e);
            choices.ListChanged -= Choices_ListChanged;
        }

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
            new DataColumn("IsExtendedProperty", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsDefinition", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IsChoice", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("ExtendedProperty", typeof(String)){ AllowDBNull = true},
            new DataColumn("ChoiceList", typeof(String)){ AllowDBNull = true},
            new DataColumn("Obsolete", typeof(Boolean)){ AllowDBNull = false},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},

        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection)
        { return GetData(connection, (null, null, null)); }

        static Command GetData(IConnection connection, (Guid? PropertyId, String? PropertyTitle, String? PropertyName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationProperty]";

            command.AddParameter("@PropertyId", parameters.PropertyId);
            command.AddParameter("@PropertyTitle", parameters.PropertyTitle);
            return command;
        }

        public static Command SetData(IConnection connection, IBindingTable<PropertyItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationProperty]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeApplicationProperty]", source);
            return command;
        }

        #region ISerializable
        protected PropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            choices.Clear();
            if (ChoiceList is not null)
            { choices.AddRange(ChoiceList.Split(",").Select(s => new ChoiceItem() { Choice = s })); }
        }
        #endregion
    }


}
