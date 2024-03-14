using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface for support of the ScriptRow functionality.
    /// </summary>
    public interface IScriptRow
    {
        /// <summary>
        /// Builds a default Row Scripting using the column definition
        /// </summary>
        IEnumerable<ScriptRow> GetScriptDataRow();

        /// <summary>
        /// Generates an XML Element from the DataRow
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        XElement GetXElement(IEnumerable<ScriptRow>? options = null);
    }

    /// <summary>
    /// Represents the XML Scripting Options for a Row.
    /// </summary>
    public class ScriptRow : INotifyPropertyChanged
    {
        DataColumn data;

        /// <inheritdoc cref="DataColumn.ColumnName"/>
        public String ColumnName { get { return data.ColumnName; } }

        /// <inheritdoc cref="DataColumn.DataType"/>
        public String DataType { get { return data.DataType.Name; } }

        /// <inheritdoc cref="DataColumn.AllowDBNull"/>
        public Boolean AllowDBNull { get { return data.AllowDBNull; } }

        /// <summary>
        /// Script row as an Element. False = as an Attribute
        /// </summary>
        public Boolean ScriptAsElement
        {
            get { return scriptAsElement; }
            set { scriptAsElement = value; OnPropertyChanged(nameof(ScriptAsElement)); }
        }
        private bool scriptAsElement = true;

        /// <summary>
        /// Script the data as CData.
        /// </summary>
        public Boolean ScriptAsCData
        {
            get { return scriptAsCData; }
            set { scriptAsCData = value; OnPropertyChanged(nameof(ScriptAsCData)); }
        }
        private bool scriptAsCData = false;

        /// <summary>
        /// Script the Data as XML Element
        /// </summary>
        public Boolean ScriptAsXml
        {
            get { return scriptAsXml; }
            set { scriptAsXml = value; OnPropertyChanged(nameof(ScriptAsXml)); }
        }
        private bool scriptAsXml = false;

        /// <summary>
        /// Script the DataType as an attribute.
        /// </summary>
        public Boolean ScriptDataType
        {
            get { return scriptDataType; }
            set { scriptDataType = value; OnPropertyChanged(nameof(ScriptDataType)); }
        }
        private bool scriptDataType = true;

        /// <summary>
        /// Script the AllowDBNull as an attribute
        /// </summary>
        public Boolean ScriptAllowDBNull
        {
            get { return scriptAllowDBNull; }
            set { scriptAllowDBNull = value; OnPropertyChanged(nameof(ScriptAllowDBNull)); }
        }
        private bool scriptAllowDBNull = true;

        /// <summary>
        /// Builds a default Row Scripting using the column definition
        /// </summary>
        /// <param name="column"></param>
        internal ScriptRow(DataColumn column) : base()
        { data = column; }

        /// <summary>
        /// Generates an XML Element from the DataRow
        /// </summary>
        /// <param name="row"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        internal static XElement GetXElement(DataRow row, IEnumerable<ScriptRow>? options = null)
        {
            XElement result = new XElement(row.Table.TableName);
            List<ScriptRow> rowOptions;

            if (options is null)
            {
                rowOptions = new List<ScriptRow>();

                foreach (DataColumn item in row.Table.Columns)
                { rowOptions.Add(new ScriptRow(item)); }
            }
            else { rowOptions = options.Where(w => row.Table.Columns.Contains(w.ColumnName)).ToList(); }

            foreach (DataColumn item in row.Table.Columns)
            {
                if (rowOptions.FirstOrDefault(w => item.ColumnName == w.ColumnName) is ScriptRow rowOption)
                {
                    if (rowOption.ScriptAsElement)
                    {
                        XElement column;
                        if (row[item] is DBNull || row[item] is null)
                        { column = new XElement(item.ColumnName); }
                        else
                        {
                            if (rowOption.ScriptAsCData)
                            {
                                column = new XElement(item.ColumnName);
                                column.Add(new XCData(row[item].ToString() ?? String.Empty));
                            }
                            if (rowOption.ScriptAsXml)
                            {
                                try
                                {
                                    column = new XElement(item.ColumnName);
                                    Object data = row[item];
                                    if (data is String value && !String.IsNullOrWhiteSpace(value))
                                    { column.Add(XElement.Parse(value)); }
                                }
                                catch (Exception)
                                { column = new XElement(item.ColumnName, row[item]); }

                            }
                            else
                            { column = new XElement(item.ColumnName, row[item]); }
                        }

                        if (rowOption.ScriptDataType)
                        { column.Add(new XAttribute(nameof(item.DataType), item.DataType)); }

                        if (rowOption.ScriptAllowDBNull)
                        { column.Add(new XAttribute(nameof(item.AllowDBNull), item.AllowDBNull)); }

                        result.Add(column);
                    }
                    else
                    {
                        if (row[item] is Object data)
                        { result.Add(new XAttribute(item.ColumnName, data)); }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a List of ScriptDataRow from a row
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal static IEnumerable<ScriptRow> GetScriptDataRow(DataRow row)
        {
            List<ScriptRow> result = new List<ScriptRow>();

            foreach (DataColumn item in row.Table.Columns)
            { result.Add(new ScriptRow(item)); }

            return result;
        }

        #region INotifyPropertyChanged
        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged is PropertyChangedEventHandler handler)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

        #endregion
    }
}
