using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationData
    {
        /// <inheritdoc/>
        /// <remarks>Base Class with Database Support.</remarks>
        [Obsolete("Not needed?", true)]
        protected abstract class PresenterDatabase : PresenterData
        {
            /// <summary>
            /// Command that performs the DoWork function.
            /// </summary>
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            /// <summary>
            /// Returns the Work Items needed to Load the data from the Database.
            /// </summary>
            /// <param name="factory"></param>
            /// <returns></returns>
            protected abstract IReadOnlyList<WorkItem> LoadWork(IDatabaseWork factory);

            /// <summary>
            /// Load the data from the main data store to the local.
            /// </summary>
            public abstract void LoadValue();

            /// <summary>
            /// Returns the Work Items needed to Load the historical data from the Database.
            /// </summary>
            /// <param name="factory"></param>
            /// <param name="temporal"></param>
            /// <returns></returns>
            protected abstract IReadOnlyList<WorkItem> LoadWork(IDatabaseWork factory, TemporalIndex temporal);

            /// <summary>
            /// Returns the Work Items needed to Save the data from the Database.
            /// </summary>
            /// <param name="factory"></param>
            /// <returns></returns>
            protected abstract IReadOnlyList<WorkItem> SaveWork(IDatabaseWork factory);

            /// <summary>
            /// Loads the data from the Database by Key.
            /// </summary>
            /// <param name="onComplete"></param>
            public virtual void LoadData(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(LoadWork(factory));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    LoadValue();
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            /// <summary>
            /// Loads the historical data from the Database by Key.
            /// </summary>
            /// <param name="temporal"></param>
            /// <param name="onComplete"></param>
            public virtual void LoadData(TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(LoadWork(factory, temporal));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    LoadValue();
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            /// <summary>
            /// Saves the data to the Database by Key.
            /// </summary>
            /// <param name="onComplete"></param>
            public virtual void SaveData(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(SaveWork(factory));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    LoadValue();
                    if (onComplete is not null) { onComplete(args); }
                }
            }

        }

        /// <inheritdoc/>
        /// <remarks>Base Presenter Class with Database Support.</remarks>
        protected abstract class PresenterDatabase<TKey> : PresenterData
            where TKey : class, IKey, IKeyEquality<TKey>
        {
            /// <summary>
            /// Command that performs the DoWork function.
            /// </summary>
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            /// <summary>
            /// Returns the Work Items needed to Load the Item from the Database.
            /// </summary>
            /// <param name="factory"></param>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <example><![CDATA[
            /// protected override IReadOnlyList<WorkItem> LoadWork(IDatabaseWork factory, TemplateIndex key)
            /// {
            ///     List<WorkItem> work = new List<WorkItem>();
            ///     ITemplateData target = BusinessData.Templates;
            /// 
            ///     work.AddRange(target.Load(factory, key));
            ///     work.Add(new WorkItem() { DoWork = () => { GetData = () => target; } });
            ///     return work;
            /// }]]></example>
            protected abstract IReadOnlyList<WorkItem> LoadWork(IDatabaseWork factory, TKey key);

            /// <summary>
            /// Returns the Work Items needed to Load the historical Item from the Database.
            /// </summary>
            /// <param name="factory"></param>
            /// <param name="key"></param>
            /// <param name="temporal"></param>
            /// <returns></returns>
            /// <remarks>Be sure to create a separate data object for the temporal data.</remarks>
            /// <example><![CDATA[
            /// protected override IReadOnlyList<WorkItem> TemporalWork(IDatabaseWork factory, TemplateIndex key, TemporalIndex temporal)
            /// {
            ///   List<WorkItem> work = new List<WorkItem>();
            ///   ITemplateData target = ITemplateData.Create();
            ///   work.AddRange(target.Load(factory, key, temporal));
            ///   work.Add(new WorkItem() { DoWork = () => { GetData = () => target; } });
            ///   return work;
            /// }]]></example>
            protected abstract IReadOnlyList<WorkItem> TemporalWork(IDatabaseWork factory, TKey key, TemporalIndex temporal);

            /// <summary>
            /// Returns the Work Items needed to Save the Item to the Database.
            /// </summary>
            /// <param name="factory"></param>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <remarks>When used with DeleteWork, this becomes a Delete on the Database.</remarks>
            protected abstract IReadOnlyList<WorkItem> SaveWork(IDatabaseWork factory, TKey key);

            /// <summary>
            /// Returns the Work Items needed to Remove an Item from the data store.
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <remarks>Default wrappers the RemoveValue into a WorkItem.</remarks>
            protected virtual IReadOnlyList<WorkItem> DeleteWork(TKey key)
            { return new WorkItem() { WorkName = "Remove by Key", DoWork = () => { RemoveValue(key); } }.ToList(); }

            /// <summary>
            /// Load the data from the main data store to the local.
            /// </summary>
            /// <param name="key"></param>
            public abstract void LoadValue(TKey key);

            /// <summary>
            /// Remove an Item from the data store by Key.
            /// </summary>
            /// <param name="key"></param>
            public abstract void RemoveValue(TKey key);

            /// <summary>
            /// Loads the data from the Database by Key.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="onComplete"></param>
            /// <remarks>Calls DeleteWork and LoadWork</remarks>
            public virtual void LoadData(TKey key, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(DeleteWork(key));
                work.AddRange(LoadWork(factory, key));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    LoadValue(key);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            /// <summary>
            /// Loads the historical data from the Database by Key.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="temporal"></param>
            /// <param name="onComplete"></param>
            /// <remarks>Calls TemporalWork</remarks>
            public virtual void LoadData(TKey key, TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(TemporalWork(factory, key, temporal));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    LoadValue(key);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            /// <summary>
            /// Saves the data to the Database by Key.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="onComplete"></param>
            /// <remarks>Calls SaveWork, DeleteWork, and LoadWork</remarks>
            public virtual void SaveData(TKey key, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(SaveWork(factory, key));
                work.AddRange(DeleteWork(key));
                work.AddRange(LoadWork(factory, key));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    LoadValue(key);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            /// <summary>
            /// Deletes the data to the Database by Key.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="onComplete"></param>
            /// <remarks>Calls DeleteWork and SaveWork</remarks>
            public virtual void DeleteData(TKey key, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(DeleteWork(key));
                work.AddRange(SaveWork(factory, key));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                { if (onComplete is not null) { onComplete(args); } }
            }
        }
    }
}
