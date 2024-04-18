using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Interface Item/Value for DatabaseSynchronize
    /// </summary>
    public interface ISynchronizeValue<TItem> : INotifyPropertyChanged
        where TItem : class, INamedScopeValue, IBindingRowState, IBindingPropertyChanged
    {
        /// <summary>
        /// System Key for the value
        /// </summary>
        NamedScopeKey Key { get; }

        /// <summary>
        /// Title for the value
        /// </summary>
        String Title { get; }

        /// <summary>
        /// NameSpace Path for the Value
        /// </summary>
        String Path { get; }

        /// <summary>
        /// Is the value in the Model
        /// </summary>
        Boolean InModel { get; }

        /// <summary>
        /// Is the value in the Database
        /// </summary>
        Boolean InDatabase { get; }

        /// <summary>
        /// Get the Source Item (Model or Database)
        /// </summary>
        TItem Source { get; }
    }

    /// <summary>
    /// Item/Value for DatabaseSynchronize
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract class SynchronizeValue<TItem> : ISynchronizeValue<TItem>
        where TItem : class, INamedScopeValue, IBindingRowState, IBindingPropertyChanged
    {
        /// <inheritdoc/>
        public NamedScopeKey Key
        { get { return Source.GetSystemId(); } }

        /// <inheritdoc/>
        public String Title
        { get { return Source.GetTitle(); } }

        /// <inheritdoc/>
        public String Path
        { get { return Source.GetPath().MemberPath; } }

        /// <inheritdoc/>
        public Boolean InModel
        {
            get { return inModel; }
            internal set { inModel = value; OnPropertyChanged(nameof(InModel)); }
        }
        private Boolean inModel;

        /// <inheritdoc/>
        public Boolean InDatabase
        {
            get { return inDatabase; }
            internal set { inDatabase = value; OnPropertyChanged(nameof(InDatabase)); }
        }
        private Boolean inDatabase;

        /// <inheritdoc/>
        public TItem Source { get; }

        /// <summary>
        /// Constructor for SynchronizeValue
        /// </summary>
        /// <param name="data"></param>
        /// <param name="inModel"></param>
        /// <param name="inDatabase"></param>
        public SynchronizeValue(TItem data, Boolean inModel, Boolean inDatabase) : base()
        {
            Source = data;
            InModel = inModel;
            InDatabase = inDatabase;

            data.OnTitleChanged += OnTitleChanged;
        }

        private void OnTitleChanged(Object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Path));
        }

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged" />
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Triggers the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged is PropertyChangedEventHandler handler)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

        /// <summary>
        /// Builds a List of Synchronize Values from the two sources (model and database).
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="modelData"></param>
        /// <param name="databaseData"></param>
        /// <param name="newKey"></param>
        /// <param name="constructor"></param>
        /// <returns></returns>
        internal static IReadOnlyList<TResult> Build<TKey, TResult>(
            IBindingList<TItem> modelData,
            IBindingList<TItem> databaseData,
            Func<TItem, TKey> newKey,
            Func<TItem, Boolean, Boolean, TResult> constructor)
            where TResult : SynchronizeValue<TItem>
            where TKey : IKey
        {
            List<TResult> result = new List<TResult>();

            List<TKey> keys = modelData.Select(s => newKey(s))
                    .Union(databaseData.Select(s => newKey(s)))
                    .ToList();

            foreach (TKey item in keys)
            {
                if (modelData.FirstOrDefault(w => item.Equals(w)) is TItem modelItem)
                {
                    Boolean inDatabase = databaseData.FirstOrDefault(w => item.Equals(w)) is TItem;
                    TResult value = constructor(modelItem, true, inDatabase);
                    result.Add(value);
                }
                else if (databaseData.FirstOrDefault(w => item.Equals(w)) is TItem databaseItem)
                {
                    TResult value = constructor(databaseItem, false, true);
                    result.Add(value);
                }
            }

            return result;
        }
    }

}
