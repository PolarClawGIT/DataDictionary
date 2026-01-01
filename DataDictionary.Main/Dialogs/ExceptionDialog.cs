namespace DataDictionary.Main.Dialogs
{
    partial class ExceptionDialog : Form
    {
        FormBinding thisData;

        public ExceptionDialog(Exception ex) : base()
        {
            InitializeComponent();
            thisData = new FormBinding(ex);
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

            try // If RTF, bind to the RTF property
            { helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Rtf), thisData, nameof(thisData.HelpText))); }
            catch (Exception) // Else it is not RTF, bind to the property 
            { helpTextData.DataBindings.Add(new Binding(nameof(helpTextData.Text), thisData, nameof(thisData.HelpText))); }

            if (!thisData.HasHelp) 
            { exceptionDetailLayout.TabPages.Remove(exceptionDetailHelpText); }

            exceptionData.AutoGenerateColumns = false;
            exceptionSqlErrors.AutoGenerateColumns = false;

            exceptionData.DataSource = thisData.Data;
            exceptionSqlErrors.DataSource = thisData.SQLErrors;
        }

    }
}
