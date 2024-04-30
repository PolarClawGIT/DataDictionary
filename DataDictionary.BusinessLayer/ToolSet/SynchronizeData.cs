using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Base Class Used to Synchronize data between the Model and the Database.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class SynchronizeData<TValue, TItem, TKey> : BindingList<TValue>
        where TValue : SynchronizeValue<TItem>
        where TItem : class, IBindingRowState, IBindingPropertyChanged
        where TKey : class
    {
        /// <summary>
        /// Reference to the existing Model data.
        /// </summary>
        protected abstract IBindingList<TItem> ModelData { get; }

        /// <summary>
        /// Reference to the data from the Db.
        /// </summary>
        protected abstract IBindingList<TItem> DatabaseData { get; }

        /// <summary>
        /// Returns the Key for the data item.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <remarks>Use the Key constructor</remarks>
        protected abstract TKey GetKey(TItem data);

        /// <summary>
        /// Returns the SynchronizeValue for the data item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <remarks>Use the SynchronizeValue constructor</remarks>
        protected abstract TValue GetValue(TItem data);

        /// <summary>
        /// Updates the data based on sources (Model and Database)
        /// </summary>
        public void Refresh()
        {
            List<TKey> keys = ModelData.Select(s => GetKey(s))
                    .Union(DatabaseData.Select(s => GetKey(s)))
                    .ToList();

            foreach (TValue item in this.ToList())
            {
                Boolean inDatabase = DatabaseData.FirstOrDefault(w => item.Equals(w)) is TItem;
                Boolean inModel = ModelData.FirstOrDefault(w => item.Equals(w)) is TItem;

                if (inDatabase || inModel)
                {
                    item.InDatabase = inDatabase;
                    item.InModel = inModel;
                }
                else { this.Remove(item); }
            }

            foreach (TKey item in keys)
            {
                if (this.FirstOrDefault(w => item.Equals(w)) is TValue existing)
                {
                    Boolean inDatabase = DatabaseData.FirstOrDefault(w => item.Equals(w)) is TItem;
                    Boolean inModel = ModelData.FirstOrDefault(w => item.Equals(w)) is TItem;
                }
                else if (ModelData.FirstOrDefault(w => item.Equals(w)) is TItem modelItem)
                {
                    Boolean inDatabase = DatabaseData.FirstOrDefault(w => item.Equals(w)) is TItem;
                    TValue value = GetValue(modelItem);
                    value.InDatabase = inDatabase;
                    value.InModel = true;
                    this.Add(value);
                }
                else if (DatabaseData.FirstOrDefault(w => item.Equals(w)) is TItem databaseItem)
                {
                    TValue value = GetValue(databaseItem);
                    value.InDatabase = true;
                    value.InModel = false;
                    this.Add(value);
                }
            }
        }
    }
}
