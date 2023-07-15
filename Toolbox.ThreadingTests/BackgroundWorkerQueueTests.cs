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
        public void EnqueueTest_Completed()
        {
            Int32 sleepFor = 10000;
            Boolean completed = false;
            Boolean worked = false;
            Int32 baseThread = Thread.CurrentThread.ManagedThreadId;
            Int32 workThread = -1;
            Int32 completeThread = -1;

            var item = new WorkItem()
            {
                WorkName = "Work test",
                DoWork = DoWork,
            };
            item.Completing += Item_Completed;

            worker.Enqueue(item);

            Thread.Sleep(sleepFor + 10000);

            Assert.IsTrue(completed,"The completed Event is expected to have fired");
            Assert.IsTrue(worked,"Worker is expected to have completed work");
            Assert.IsTrue(baseThread == completeThread, "Event is expected to be on the same thread that created the queue");
            Assert.IsFalse(baseThread == workThread, "Worker is expected to be on the background thread");

            void DoWork()
            {
                Thread.Sleep(sleepFor);
                worked = true;
                workThread = Thread.CurrentThread.ManagedThreadId;
            }

            void Item_Completed(object? sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
            {
                item.Completing -= Item_Completed;
                completed = true;
                completeThread = Thread.CurrentThread.ManagedThreadId;
            }
        }

        [Test()]
        public void BindingListTest()
        {
            //This should fail with a cross thread exception but it does not.
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

                data.Add("Add Three");
                data.Add("Add Four");

                completed = true;
            }
        }


        [TearDown()]
        public void TearDown()
        { worker.Dispose(); }
    }
}