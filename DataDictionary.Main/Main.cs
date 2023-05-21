namespace DataDictionary.Main
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void testConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var unitTest = new DataDictionary.BusinessLayer.UnitTest();
            unitTest.TestConnection();

        }

        private void appExceptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exception innerEx = new InvalidOperationException("Inner Test");
            Exception testEx = new InvalidOperationException("Test", innerEx);
            testEx.Data.Add("Key", "Value");

            throw testEx;
        }
    }
}