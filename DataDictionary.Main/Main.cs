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
    }
}