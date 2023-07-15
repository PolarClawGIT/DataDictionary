using NUnit.Framework;
using Toolbox.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Toolbox.Threading.Tests
{
    [TestFixture()]
    public class BackgroundWorkerQueueTests
    {
        WorkerQueue worker = new WorkerQueue();

        [Test()]
        public void BackgroundWorkerQueueTest()
        { Assert.IsTrue(worker is WorkerQueue); }

        [Test()]
        public void EnqueueTest()
        {
            Int32 sleepFor = 10000;

            worker.Enqueue(
                new WorkItem()
                {
                    WorkName = "Work test",
                    DoWork = () => Thread.Sleep(sleepFor)
                });
            Assert.IsTrue(worker.IsBusy);
            Assert.IsTrue(worker.WorkAdded > 0);
            Assert.IsTrue(worker.WorkComplete == 0);

            Thread.Sleep(sleepFor + 1000);

            Assert.IsFalse(worker.IsBusy);
            Assert.IsTrue(worker.WorkAdded == 0);
            Assert.IsTrue(worker.WorkComplete == 0);
        }

        [Test()]
        public void EnqueueTest_OnCompeting()
        {
            Int32 sleepFor = 10000;
            Boolean completed = false;
            Boolean starting = false;
            Boolean worked = false;
            int countProgress = 0;

            worker.ProgressChanged += Worker_ProgressChanged;

            var item = new WorkItem()
            {
                WorkName = "Work test",
                DoWork = DoWork,
                //OnCompleting = DoCompleting,
                //OnStarting = DoStarting,
            };
            worker.Enqueue(item);

            Thread.Sleep(sleepFor + 1000);

            Assert.IsTrue(completed);
            Assert.IsTrue(starting);
            Assert.IsTrue(worked);
            Assert.IsTrue(countProgress > 0);

            void DoWork()
            { Thread.Sleep(sleepFor); worked = true; }

            void Worker_ProgressChanged(object? sender, WorkerProgressChangedEventArgs e)
            { countProgress++; }
        }

        [Test()]
        public void EnqueueTest_Completed()
        {
            Int32 sleepFor = 10000;
            Boolean completed = false;
            Boolean worked = false;

            var item = new WorkItem()
            {
                WorkName = "Work test",
                DoWork = DoWork,
            };
            item.Completing += Item_Completed;

            worker.Enqueue(item);

            Thread.Sleep(sleepFor + 10000);

            Assert.IsTrue(completed);
            Assert.IsTrue(worked);

            void DoWork()
            { Thread.Sleep(sleepFor); worked = true;}

            void Item_Completed(object? sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
            {
                item.Completing -= Item_Completed;
                completed = true;
            }
        }

        [Test()]
        public void BindingListTest()
        {
            Int32 sleepFor = 10000;
            Boolean completed = false;
            Boolean worked = false;
            Boolean changed = false;
            BindingList<String> data = new BindingList<String>();
            data.ListChanged += Data_ListChanged;

            var item = new WorkItem()
            {
                WorkName = "Work test",
                DoWork = DoWork,
            };
            item.Completing += Item_Completed;

            worker.Enqueue(item);
            Thread.Sleep(sleepFor + 1000);

            Assert.IsTrue(completed);
            Assert.IsTrue(worked);
            Assert.IsTrue(changed);

            void DoWork()
            {
                data.Add("Add One");
                data.Add("Add two");
                worked = true;
                Thread.Sleep(sleepFor);
            }

            void Data_ListChanged(object? sender, ListChangedEventArgs e)
            { changed = true; }

            void Item_Completed(object? sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
            {
                item.Completing -= Item_Completed;
                completed = true;
            }
        }


        [TearDown()]
        public void TearDown()
        { worker.Dispose(); }
    }
}