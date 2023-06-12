using DataDictionary.BusinessLayer;
using System.ComponentModel;
using Toolbox.Threading;
using Toolbox.Threading.WorkItem;

namespace DataDictionary.Main
{
    public partial class Main : Form
    {
        DataRepository thisData = new DataRepository();

        public Main()
        { InitializeComponent(); }

        private void Main_Load(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            Program.WorkerQueue.ProgressChanged += WorkerQueue_ProgressChanged;

            WorkBase work = thisData.Load();
            work.WorkCompleting += Work_WorkCompleting;
            Program.WorkerQueue.Enqueue(work);

            void Work_WorkCompleting(object? sender, EventArgs e)
            {
                work.WorkCompleting -= Work_WorkCompleting;
                mainNavigation.Bind(thisData);
                this.UseWaitCursor = false;
            }
        }

        private void Main_FormClosing(object? sender, FormClosingEventArgs e)
        { }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        { Program.WorkerQueue.ProgressChanged -= WorkerQueue_ProgressChanged; }

        private void WorkerQueue_ProgressChanged(object? sender, WorkerProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercent;
            toolStripWorkerTask.Text = e.ProgressText;
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

        private void getSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var unitTest = new DataDictionary.BusinessLayer.UnitTest();
            unitTest.TestGetSchema();
        }


    }
}