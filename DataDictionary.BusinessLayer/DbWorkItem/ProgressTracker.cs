using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    /// <summary>
    /// Used to track Progress on Work Items.
    /// </summary>
    /// <example>
    /// ProgressTracker progress = new ProgressTracker();
    /// WorkItem workItem = new WorkItem() { DoWork = () => { someWorkMethod(progress); } };
    /// progress.OnProgressChanged = workItem.OnProgressChanged;
    /// </example>
    struct ProgressTracker
    {
        public ProgressTracker() { }

        public Int32 TotalWork { get; set; }
        public Int32 CompletedWork { get; set; }
        public Action<Int32, Int32> OnProgressChanged { get; set; } = (complted, total) => { };

        public void ReportProgress()
        { OnProgressChanged(CompletedWork++, TotalWork); }
    }
}
