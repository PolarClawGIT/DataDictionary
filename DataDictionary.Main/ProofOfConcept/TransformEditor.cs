using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace DataDictionary.Main.Forms.ProofOfConcept
{
    partial class TransformEditor : ApplicationBase
    {
        /// <summary>
        /// Temporary Class to use during development
        /// </summary>
        class FormData : INotifyPropertyChanged
        {
            public Guid? TransformId
            {
                get { return transformId; }
                set { transformId = value; OnPropertyChanged(nameof(TransformId)); }
            }
            private Guid? transformId;

            public String? TransformTitle
            {
                get { return transformTitle; }
                set { transformTitle = value; OnPropertyChanged(nameof(TransformTitle)); }
            }
            private String? transformTitle;

            public String? TransformDescription
            {
                get { return transformDescription; }
                set { transformDescription = value; OnPropertyChanged(nameof(TransformDescription)); }
            }
            private String? transformDescription;

            public String SourceData
            {
                get
                {
                    if (sourceXml is null) { return sourceValue; }
                    else { return sourceXml.ToString(); }
                }
                set
                {
                    sourceValue = value;

                    try
                    {
                        SourceException = null;
                        sourceXml = XDocument.Parse(value);
                    }
                    catch (Exception ex)
                    {
                        SourceException = ex;
                        sourceXml = null;
                    }

                    OnPropertyChanged(nameof(SourceException));
                    OnPropertyChanged(nameof(SourceData));
                }
            }
            public Exception? SourceException { get; private set; }
            private String sourceValue = String.Empty;
            private XDocument? sourceXml;

            public String TransformData
            {
                get
                {
                    return transformValue;
                    //if (transformXml is null) { return transformValue; }
                    //else { return transformXml.ToString(); }
                }
                set
                {
                    transformValue = value;

                    try
                    {
                        TransformException = null;
                        transformXml = XDocument.Parse(value);
                    }
                    catch (Exception ex)
                    {
                        TransformException = ex;
                        transformXml = null;
                    }

                    OnPropertyChanged(nameof(TransformException));
                    OnPropertyChanged(nameof(TransformData));
                }
            }
            public Exception? TransformException { get; private set; }
            private String transformValue = String.Empty;
            private XDocument? transformXml;

            public String ResultData { get; private set; } = String.Empty;
            public Exception? ResultException { get; private set; }

            public void Transform()
            {
                ResultException = null;
                ResultData = String.Empty;

                try
                {
                    if (transformXml is not null && sourceXml is not null)
                    {
                        using (XmlReader sourceReader = sourceXml.CreateReader())
                        using (XmlReader transformReader = transformXml.CreateReader())
                        using (StringWriter resultWriter = new StringWriter())
                        {
                            XslCompiledTransform transformer = new XslCompiledTransform();
                            transformer.Load(transformReader);

                            transformer.Transform(sourceReader, null, resultWriter);

                            ResultData = resultWriter.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ResultException = ex;
                    ResultData = String.Empty;
                }

                OnPropertyChanged(nameof(ResultException));
                OnPropertyChanged(nameof(ResultData));
            }



            #region INotifyPropertyChanged
            /// <inheritdoc/>
            public event PropertyChangedEventHandler? PropertyChanged;

            /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
            /// <remarks>
            /// The method assumes that the Property Name and the Column Name are identical.
            /// The actual property changed event is raised on the Column change event, not the Set method.
            /// This allows changes made directly to the table row to be captured.
            /// </remarks>
            public virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }

            #endregion
        }

        public TransformEditor() : base()
        {
            InitializeComponent();
            Icon = Resources.Icon_XSLTransform;
        }

        public TransformEditor(XDocument source) : this()
        {
            FormData data = new FormData() { SourceData = source.ToString(), };
            bindingTransform.DataSource = new BindingList<FormData>() { data };
            bindingTransform.Position = 0;
        }

        private void TransformEditor_Load(object sender, EventArgs e)
        {
            FormData nameOfValues;
            titleData.DataBindings.Add(new Binding(nameof(titleData.Text), bindingTransform, nameof(nameOfValues.TransformTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            descriptionData.DataBindings.Add(new Binding(nameof(descriptionData.Text), bindingTransform, nameof(nameOfValues.TransformDescription), false, DataSourceUpdateMode.OnPropertyChanged));

            sourceData.DataBindings.Add(new Binding(nameof(sourceData.Text), bindingTransform, nameof(nameOfValues.SourceData), false, DataSourceUpdateMode.OnPropertyChanged));
            sourceExceptionData.DataBindings.Add(new Binding(nameof(sourceExceptionData.Text), bindingTransform, nameof(nameOfValues.SourceException), false, DataSourceUpdateMode.OnPropertyChanged));

            transformData.DataBindings.Add(new Binding(nameof(transformData.Text), bindingTransform, nameof(nameOfValues.TransformData), false, DataSourceUpdateMode.OnPropertyChanged));
            transformExceptionData.DataBindings.Add(new Binding(nameof(transformExceptionData.Text), bindingTransform, nameof(nameOfValues.TransformException), false, DataSourceUpdateMode.OnPropertyChanged));

            resultData.DataBindings.Add(new Binding(nameof(resultData.Text), bindingTransform, nameof(nameOfValues.ResultData), false, DataSourceUpdateMode.OnPropertyChanged));
            resultExceptionData.DataBindings.Add(new Binding(nameof(resultExceptionData.Text), bindingTransform, nameof(nameOfValues.ResultException), false, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void TransformCommand_Click(object? sender, EventArgs e)
        {
            if (bindingTransform.Current is FormData current)
            { current.Transform(); }
        }
    }
}
