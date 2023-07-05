using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;


namespace DataDictionary.Main.Dialog
{
    partial class ExceptionDialog : Form
    {
        FormData thisData;

        class FormData
        {
            public class ExceptionItem
            {
                Exception baseException;

                public String Type { get { return baseException.GetType().ToString(); } }
                public String Message { get { return baseException.Message; } }
                public IList<KeyValuePair<String, String>> Data
                {
                    get
                    {
                        List<KeyValuePair<String, String>> result = new List<KeyValuePair<String, String>>();

                        foreach (DictionaryEntry item in baseException.Data)
                        {
                            if (item.Key is object key &&
                                key.ToString() is String keyString &&
                                item.Value is Object value &&
                                value.ToString() is String valueString)
                            { result.Add(new KeyValuePair<string, string>(keyString, valueString)); }
                        }

                        return result;
                    }
                }

                public IList<SqlError> Errors
                {
                    get
                    {
                        List<SqlError> result = new List<SqlError>();

                        if (baseException is SqlException sqlEx)
                        {
                            foreach (SqlError item in sqlEx.Errors)
                            { result.Add(item); }
                        }

                        return result;
                    }
                }

                public ExceptionItem(Exception ex)
                { baseException = ex; }
            }

            Exception baseException;
            DateTime eventDate;

            public String Type { get { return baseException.GetType().ToString(); } }
            public String? Source { get { return baseException.Source; } }
            public String Message { get { return baseException.Message; } }
            public String StackTrace { get { if (baseException.StackTrace is String valueString) { return valueString; } else { return String.Empty; } } }

            public DateTime EventDate { get { return eventDate; } }
            public String OsVersion { get { return System.Environment.OSVersion.ToString(); } }
            public String UserName { get { return String.Format("{0}\\{1}", System.Environment.UserDomainName, System.Environment.UserName); } }
            public String WorkStation { get { return System.Environment.MachineName; } }
            public String ApplicationName
            {
                get
                {
                    System.Reflection.AssemblyName result = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                    if (result.Name is string value) { return value; } else { return String.Empty; }
                }
            }

            public String ApplicationVersion
            {
                get
                {
                    System.Reflection.AssemblyName result = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                    if (result.Version is object valueObject && valueObject.ToString() is String valueString)
                    { return valueString; }
                    else { return string.Empty; }
                }
            }

            public IReadOnlyList<KeyValuePair<String, String>> Data
            {
                get
                {
                    List<KeyValuePair<String, String>> result = new List<KeyValuePair<String, String>>();

                    Exception currentEx = baseException;
                    while (currentEx is Exception workEx)
                    {
                        foreach (DictionaryEntry item in workEx.Data)
                        {
                            if (item.Key.ToString() is String keyString && item.Value is Object valueObject && valueObject.ToString() is String valueString)
                            { result.Add(new KeyValuePair<String, String>(keyString, valueString)); }
                        }

                        currentEx = workEx.InnerException!;
                    }

                    return result;
                }
            }

            public IReadOnlyList<SqlError> SqlErrors
            {
                get
                {
                    List<SqlError> result = new List<SqlError>();
                    Exception currentEx = baseException;

                    while (currentEx is Exception workEx)
                    {
                        if (workEx is SqlException sqlEx)
                        { foreach (SqlError item in sqlEx.Errors) { result.Add(item); } }

                        currentEx = workEx.InnerException!;
                    }

                    return result;
                }
            }

            public String AsXml
            {
                get
                {
                    XmlDocument result = new XmlDocument();
                    XmlElement root = result.CreateElement("Root");
                    result.AppendChild(root);

                    XmlElement summaryNode = result.CreateElement("Summary");
                    summaryNode.Attributes.Append(CreateAttribute(nameof(this.ApplicationName), this.ApplicationName));
                    summaryNode.Attributes.Append(CreateAttribute(nameof(this.ApplicationVersion), this.ApplicationVersion));
                    summaryNode.Attributes.Append(CreateAttribute(nameof(this.EventDate), this.EventDate.ToShortDateString()));
                    summaryNode.Attributes.Append(CreateAttribute(nameof(this.OsVersion), this.OsVersion));
                    summaryNode.Attributes.Append(CreateAttribute(nameof(this.UserName), this.UserName));
                    summaryNode.Attributes.Append(CreateAttribute(nameof(this.WorkStation), this.WorkStation));
                    root.AppendChild(summaryNode);

                    Exception currentEx = baseException;
                    while (currentEx is Exception workEx)
                    {
                        root.AppendChild(CreateExceptionElement(workEx));
                        currentEx = workEx.InnerException!;
                    }
                    return XDocument.Parse(result.InnerXml).ToString();

                    XmlAttribute CreateAttribute(String name, String value)
                    {
                        XmlAttribute attribute = result.CreateAttribute(name);
                        attribute.Value = value;
                        return attribute;
                    }

                    XmlElement CreateExceptionElement(Exception ex)
                    {
                        XmlElement exceptionNode = result.CreateElement("Exception");
                        exceptionNode.Attributes.Append(CreateAttribute("Type", ex.GetType().ToString()));
                        exceptionNode.Attributes.Append(CreateAttribute(nameof(ex.Message), ex.Message));
                        if (baseException.StackTrace is String stackTrace)
                        { exceptionNode.Attributes.Append(CreateAttribute(nameof(ex.StackTrace), stackTrace)); }

                        if (ex.Data.Count > 0)
                        {

                            XmlElement dataNode = result.CreateElement("Data");

                            foreach (DictionaryEntry dataItem in ex.Data)
                            {
                                if (dataItem.Key.ToString() is String keyString && dataItem.Value is Object valueObject && valueObject.ToString() is String valueString)
                                {
                                    XmlElement itemNode = result.CreateElement("Item");
                                    itemNode.Attributes.Append(CreateAttribute(nameof(dataItem.Key), keyString));
                                    itemNode.Attributes.Append(CreateAttribute(nameof(dataItem.Value), valueString));
                                    dataNode.AppendChild(itemNode);
                                }
                            }
                            exceptionNode.AppendChild(dataNode);
                        }

                        if (ex is SqlException sqlEx)
                        {
                            XmlElement sqlExceptionNode = result.CreateElement("SqlException");

                            foreach (SqlError errorsItem in sqlEx.Errors)
                            {
                                XmlElement sqlErrorNode = result.CreateElement("SqlError");
                                sqlErrorNode.Attributes.Append(CreateAttribute(nameof(errorsItem.Number), errorsItem.Number.ToString()));
                                sqlErrorNode.Attributes.Append(CreateAttribute(nameof(errorsItem.Class), errorsItem.Class.ToString()));
                                sqlErrorNode.Attributes.Append(CreateAttribute(nameof(errorsItem.State), errorsItem.State.ToString()));
                                sqlErrorNode.Attributes.Append(CreateAttribute(nameof(errorsItem.Message), errorsItem.Message));
                                sqlErrorNode.Attributes.Append(CreateAttribute(nameof(errorsItem.Procedure), errorsItem.Procedure));
                                sqlErrorNode.Attributes.Append(CreateAttribute(nameof(errorsItem.LineNumber), errorsItem.LineNumber.ToString()));
                                sqlExceptionNode.AppendChild(sqlErrorNode);
                            }
                            exceptionNode.AppendChild(sqlExceptionNode);
                        }

                        return exceptionNode;
                    }
                }
            }

            public FormData(Exception ex)
            {
                baseException = ex;
                eventDate = DateTime.Now;
            }
        }


        public ExceptionDialog(Exception ex) : base()
        {
            InitializeComponent();
            thisData = new FormData(ex);
        }

        private void ExceptionDialog_Load(object sender, EventArgs e)
        {
            exceptionTypeData.DataBindings.Add(new Binding(nameof(exceptionTypeData.Text), thisData, nameof(thisData.Type)));
            exceptionMessageData.DataBindings.Add(new Binding(nameof(exceptionMessageData.Text), thisData, nameof(thisData.Message)));
            exceptionApplicationData.DataBindings.Add(new Binding(nameof(exceptionApplicationData.Text), thisData, nameof(thisData.ApplicationName)));
            exceptionApplicationVersionData.DataBindings.Add(new Binding(nameof(exceptionApplicationVersionData.Text), thisData, nameof(thisData.ApplicationVersion)));
            exceptionWorkstationData.DataBindings.Add(new Binding(nameof(exceptionWorkstationData.Text), thisData, nameof(thisData.WorkStation)));
            excpetionOsVersionData.DataBindings.Add(new Binding(nameof(excpetionOsVersionData.Text), thisData, nameof(thisData.OsVersion)));
            exceptionUserNameData.DataBindings.Add(new Binding(nameof(exceptionUserNameData.Text), thisData, nameof(thisData.UserName)));
            exceptionStackTraceData.DataBindings.Add(new Binding(nameof(exceptionStackTraceData.Text), thisData, nameof(thisData.StackTrace)));
            exceptionAsXmlData.DataBindings.Add(new Binding(nameof(exceptionAsXmlData.Text), thisData, nameof(thisData.AsXml)));
            exceptionSourceData.DataBindings.Add(new Binding(nameof(exceptionSourceData.Text), thisData, nameof(thisData.Source)));

            exceptionData.AutoGenerateColumns = false;
            exceptionSqlErrors.AutoGenerateColumns = false;

            exceptionData.DataSource = thisData.Data;
            exceptionSqlErrors.DataSource = thisData.SqlErrors;
        }

    }
}
