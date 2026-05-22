using DataDictionary.Resource;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationData
    {
        // Notes:
        //
        // This code uses a pattern like MVP (Model-View-Presenter) 
        // Strictly speaking, the code does not implement these patterns.
        //
        // The intent is to logically do the same concept.
        // The classes below performs the function of the Presenter in MVP.
        // Each form has its own implementation normally called the FormBinding class (file= FormName.Binding.CS).
        // The calls to the Business Layer are localized here and specialized for the Binding of the Form.
        // The Business Layer calls the Data Layer, which turns the business logic into database calls.
        // The Database Layer (implemented in the database) turns the Data Layer into SQL operations.
        // Common functionality, when possible, is placed here to reduce copy/paste errors.
        //
        // As this was developed after many screens where created, not every screen uses these classes.
        // The developer also has a poor understanding of the pattens.
        //
        //  Form (inherits from ApplicationData)
        //    FormBinding (inherits from Presenter, group of BindingData)
        //    BindingData (manages business layer items)
        //  Business Layer (Data, Index, and Value, inherits from Data Layer)
        //  Data Layer (Collection, Key, Item)
        //  Database Layer (Table, stored procedure)
        //
        // TODO: Convert all screens to use these classes.
        // TODO: Implement MVP pattern.

        /// <summary>
        /// DataBinding Helper.<br/>
        /// Used by forms to provide data for data binding and execute work against the Business Layer.<br/>
        /// </summary>
        protected abstract class PresenterData
        {
            /// <summary>
            /// Function that gets the Authorization for the current Value.
            /// </summary>
            /// <remarks>Used by Authorize.</remarks>
            /// <example>GetAuthorization = () => {DataBinding}.GetAuthorization(BusinessData.Authorization);</example>
            protected Func<(Boolean isAdmin, Boolean isOwner, Boolean isGrant)> GetAuthorization { get; init; } = () => (false, false, false);

            /// <summary>
            /// Function that checks if current value should be Locked (Read-only)
            /// </summary>
            /// <remarks>Set this to point to the GetLocked function of a DataBinding{TRow}.</remarks>
            public Func<Boolean> GetLocked { get; init; } = () => true;

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
                    case Enumerations.ButtonType.Browse: return true;
                    case Enumerations.ButtonType.Select: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.Add: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.Delete: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.Save: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.Open: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.Refresh: return true;
                    case Enumerations.ButtonType.Sync: return true;
                    case Enumerations.ButtonType.Import: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.Export: return true;
                    case Enumerations.ButtonType.OpenDatabase: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.SaveDatabase: return isAdmin || isOwner || isGrant;
                    case Enumerations.ButtonType.DeleteDatabase: return isAdmin || isOwner;
                    case Enumerations.ButtonType.HistoryDatabase: return isAdmin || isOwner;
                    case Enumerations.ButtonType.SecurityDatabase: return isAdmin;
                    default: return false;
                }
            }
        }

        /// <inheritdoc/>
        protected abstract class PresenterData<TKey>: PresenterData
            where TKey : class, IKey, IKeyEquality<TKey>
        {   // This is the simple form of the Presenter.
            // This is used with Child Forms where anouther form provides the data.

            /// <summary>
            /// Load the data from the main data store to the local.
            /// </summary>
            /// <param name="key"></param>
            public abstract void LoadValue(TKey key);

            /// <summary>
            /// Remove the item by Key from the data store.
            /// </summary>
            /// <param name="key"></param>
            public abstract void RemoveValue(TKey key);
        }


    }
}
