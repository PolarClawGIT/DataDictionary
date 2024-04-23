using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DatabaseData.Catalog;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Database
{

    /// <summary>
    /// Catalog Synchronize value
    /// </summary>
    public class CatalogSynchronizeValue : SynchronizeValue<CatalogValue>
    {
        /// <inheritdoc cref="DbCatalogItem.CatalogTitle"/>
        public String CatalogTitle
        {
            get { return Source.CatalogTitle ?? String.Empty; }
            set { Source.CatalogTitle = value; }
        }

        /// <inheritdoc cref="DbCatalogItem.DatabaseName"/>
        public String DatabaseName
        { get { return Source.DatabaseName ?? String.Empty; } }

        /// <inheritdoc/>
        public CatalogSynchronizeValue(CatalogValue data) : base(data)
        { }

        /// <inheritdoc/>
        protected override void Source_OnPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName is nameof(CatalogTitle) or nameof(DatabaseName))
            { OnPropertyChanged(e.PropertyName); }
        }
    }

    /// <summary>
    /// Catalog Synchronize to compare what Catalogs are in the Database vs the Model
    /// </summary>
    public class CatalogSynchronize : SynchronizeData<CatalogSynchronizeValue, CatalogValue, CatalogIndex>
    {
        /// <summary>
        /// Concrete class for the Abstract DbCatalogCollection
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        class SourceCollection<TValue> : DbCatalogCollection<TValue>
            where TValue : CatalogValue, ICatalogValue, new()
        { }

        /// <inheritdoc/>
        protected override IBindingList<CatalogValue> ModelData { get { return dbModel.DbCatalogs; } }
        IDatabaseModel dbModel;

        /// <inheritdoc/>
        protected override IBindingList<CatalogValue> DatabaseData { get { return sourceData; } }
        SourceCollection<CatalogValue> sourceData = new SourceCollection<CatalogValue>();

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="dbModel"></param>
        public CatalogSynchronize(IDatabaseModel dbModel) : base()
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
        /// Clears then reloads the Catalog List from the Database.
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
        /// Loads a Schema from a Database into the Model
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
        /// Loads a Catalog from the Database (including all schema components)
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> OpenFromDb(IDatabaseWork factory, ICatalogIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            work.AddRange(dbModel.Remove(key));
            work.AddRange(dbModel.Load(factory, key));
            return work;
        }

        /// <summary>
        /// Saves the Catalog to the Database (including all schema components)
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
        /// Deletes a Catalog from the Database (including all schema components).
        /// Copy in the Model is not removed.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> DeleteFromDb(IDatabaseWork factory, ICatalogIndex key)
        {
            List<WorkItem> work = new List<WorkItem>();
            IDbCatalogKey dbKey = new DbCatalogKey(key);

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
