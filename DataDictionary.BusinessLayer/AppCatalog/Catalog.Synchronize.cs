using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.AppCatalog
{

    /// <summary>
    /// Catalog Synchronize value
    /// </summary>
    [Obsolete("No used", true)]
    public class CatalogSynchronizeValue : SynchronizeValue<CatalogValue>//, ICatalog
    {
        /// <inheritdoc/>
        public String CatalogTitle
        {
            get { return Source.CatalogTitle ?? String.Empty; }
            set { Source.CatalogTitle = value; }
        }

        /// <inheritdoc/>
        public String? CatalogDescription
        {
            get { return Source.CatalogDescription ?? String.Empty; }
            set { Source.CatalogDescription = value; }
        }

        /// <inheritdoc/>
        public String ServerName
        { get { return Source.ServerName ?? String.Empty; } }

        /// <inheritdoc/>
        public String DatabaseName
        { get { return Source.DatabaseName ?? String.Empty; } }

        /// <inheritdoc/>
        public DateTime? SourceDate
        { get { return Source.SourceDate ?? DateTime.Now; } }

        /// <inheritdoc/>
        public CatalogSynchronizeValue(CatalogValue data) : base(data)
        { }

        /// <inheritdoc/>
        protected override void Source_OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            //if (e.PropertyName is nameof(CatalogTitle) or nameof(ServerName) or nameof(DatabaseName) or nameof(SourceDate))
            //{ OnPropertyChanged(e.PropertyName); }
        }
    }

    /// <summary>
    /// Catalog Synchronize to compare what Catalogs are in the Model vs the Model
    /// </summary>
    [Obsolete("No used", true)]
    public class CatalogSynchronize : SynchronizeData<CatalogSynchronizeValue, CatalogValue, CatalogIndex>
    {
        /// <summary>
        /// Concrete class for the Abstract CatalogCollection
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        class SourceCollection<TValue> : CatalogCollection<TValue>
            where TValue : CatalogValue, ICatalogValue, new()
        { }

        /// <inheritdoc/>
        protected override IBindingList<CatalogValue> ModelData { get { return dbModel.DbCatalogs; } }
        ICatalog dbModel;

        /// <inheritdoc/>
        protected override IBindingList<CatalogValue> DatabaseData { get { return sourceData; } }
        SourceCollection<CatalogValue> sourceData = new SourceCollection<CatalogValue>();

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="dbModel"></param>
        public CatalogSynchronize(ICatalog dbModel) : base()
        {
            this.dbModel = dbModel;

            foreach (CatalogValue item in dbModel.DbCatalogs)
            { Add(new CatalogSynchronizeValue(item) { InModel = true }); }
        }

        /// <inheritdoc/>
        protected override CatalogIndex GetKey(CatalogValue data)
        { return new CatalogIndex(data); }

        /// <inheritdoc/>
        protected override CatalogSynchronizeValue GetValue(CatalogValue data)
        { return new CatalogSynchronizeValue(data); }

        /// <summary>
        /// Clears then reloads the Catalog List from the Model.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> GetCatalogs(IDatabaseWork factory)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.Add(new WorkItem() { DoWork = sourceData.Clear });
            work.Add(factory.CreateLoad(sourceData));
            return work;
        }

        /// <summary>
        /// Loads a Schema from a Model into the Model
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> ImportFromSchema(DbSchemaContext source)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(dbModel.Import(source));
            return work;
        }

        /// <summary>
        /// Loads a Catalog from the Model (including all schema components)
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> OpenFromDb(IDatabaseWork factory, ICatalogIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(dbModel.Delete(key));
            work.AddRange(dbModel.Load(factory, key));
            return work;
        }

        /// <summary>
        /// Saves the Catalog to the Model (including all schema components)
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> SaveToDb(IDatabaseWork factory, ICatalogIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(dbModel.Save(factory, key));
            work.AddRange(GetCatalogs(factory));
            return work;
        }

        /// <summary>
        /// Deletes a Catalog from the Model (including all schema components).
        /// Copy in the Model is not removed.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> DeleteFromDb(IDatabaseWork factory, ICatalogIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            ICatalogKey dbKey = new CatalogKey(key);

            work.Add(new WorkItem()
            {
                DoWork = () =>
                {
                    while (sourceData.FirstOrDefault(w => dbKey.Equals(w)) is CatalogValue item)
                    { sourceData.Remove(dbKey); }
                }
            });
            work.Add(factory.CreateSave(sourceData, dbKey));
            return work;
        }



    }
}
