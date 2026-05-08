using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationData
    {
        // Notes:
        //
        // This code uses a pattern like MVP (Model-View-Presenter) or MVVM (Model-View-ViewModel).
        // Strictly speaking, the code does not implement these patterns.
        //
        // The intent is to logically do the same concept.
        // The classes below performs the function of the ViewModel of MVVM or Presenter in MVP.
        // Each form has its own implementation normally called the FormBinding class (file= FormName.Binding.CS).
        // The calls to the Business Layer are localized here and specialized for the Binding of the Form.
        // The Business Layer calls the Data Layer, which turns the business logic into database calls.
        // The Database Layer (implemented in the database) turns the Data Layer into SQL operations.
        // Common functionality, when possible, is placed here to reduce copy/paste errors.
        //
        // As this was developed after many screens where created, not every screen uses these classes.
        // The developer also has a poor understanding of the pattens.
        //
        // TODO: Convert all screens to use these classes.
        // TODO: Implement MVP or MVVM pattern.

        /// <summary>
        /// DataBinding Helper.<br/>
        /// Used by forms to provide data for data binding and execute work against the Business Layer.<br/>
        /// </summary>
        /// <remarks>Base Class</remarks>
        protected abstract class DataModel<TKey>
            where TKey : class, IKey, IKeyEquality<TKey>
        {
            /// <summary>
            /// Function that gets the Authorization for the current Value.
            /// </summary>
            /// <remarks>Used by Authorize.</remarks>
            /// <example>GetAuthorization = () => {DataBinding}.GetAuthorization(BusinessData.Authorization);</example>
            protected Func<(Boolean isAdmin, Boolean isOwner, Boolean isGrant)> GetAuthorization { private get; init; } =() => (false,false,false);

            /// <summary>
            /// Function that checks if current value should be Locked (Read-only)
            /// </summary>
            /// <remarks>Set this to point to the GetLocked function of a DataBinding{TRow}.</remarks>
            public Func<Boolean> GetLocked { get; init; } = () => true;

            /// <summary>
            /// Load the data from the main data store to the local.
            /// </summary>
            /// <param name="key"></param>
            public abstract void Load(TKey key);

            /// <summary>
            /// Gets the Authorization of a Button passed.
            /// </summary>
            /// <param name="command"></param>
            /// <returns></returns>
            public virtual Boolean Authorize(Enumerations.ButtonType command)
            {
                Boolean isAdmin = false;
                Boolean isOwner = false;
                Boolean isGrant = false;

                (isAdmin, isOwner, isGrant) = GetAuthorization();

                switch (command)
                {
                    case Enumerations.ButtonType.Default: return true;
                    case Enumerations.ButtonType.Delete: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.OpenDatabase: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.SaveDatabase: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.DeleteDatabase: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.HistoryDatabase: return isAdmin || isOwner || isGrant;
                    default: return false;
                }
            }


        }

        /// <inheritdoc/>
        /// <remarks>
        /// Base Class with Database Support.</remarks>
        protected abstract class DataModelDatabase<TKey> : DataModel<TKey>
            where TKey : class, IKey, IKeyEquality<TKey>
        {
            /// <summary>
            /// Command that performs the DoWork function.
            /// </summary>
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            /// <summary>
            /// Returns the Work Items needed to Load the data from the Database.
            /// </summary>
            /// <param name="factory"></param>
            /// <param name="key"></param>
            /// <returns></returns>
            protected abstract IReadOnlyList<WorkItem> LoadWork(IDatabaseWork factory, TKey key);

            /// <summary>
            /// Returns the Work Items needed to Load the historical data from the Database.
            /// </summary>
            /// <param name="factory"></param>
            /// <param name="key"></param>
            /// <param name="temporal"></param>
            /// <returns></returns>
            protected abstract IReadOnlyList<WorkItem> LoadWork(IDatabaseWork factory, TKey key, TemporalIndex temporal);

            /// <summary>
            /// Returns the Work Items needed to Save the data from the Database.
            /// </summary>
            /// <param name="factory"></param>
            /// <param name="key"></param>
            /// <returns></returns>
            protected abstract IReadOnlyList<WorkItem> SaveWork(IDatabaseWork factory, TKey key);

            /// <summary>
            /// Loads the data from the Database by Key.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="onComplete"></param>
            public virtual void Load(TKey key, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(LoadWork(factory, key));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(key);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            /// <summary>
            /// Loads the historical data from the Database by Key.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="temporal"></param>
            /// <param name="onComplete"></param>
            public virtual void Load(TKey key, TemporalIndex temporal, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(LoadWork(factory, key, temporal));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(key);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            /// <summary>
            /// Saves the data to the Database by Key.
            /// </summary>
            /// <param name="key"></param>
            /// <param name="onComplete"></param>
            public virtual void Save(TKey key, Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(SaveWork(factory, key));

                DoWork(work, completing);

                void completing(RunWorkerCompletedEventArgs args)
                {
                    Load(key);
                    if (onComplete is not null) { onComplete(args); }
                }
            }
        }
    }
}
