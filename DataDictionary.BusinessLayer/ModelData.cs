using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.ApplicationData.Model.SubjectArea;
using Toolbox.BindingTable;
using Toolbox.DbContext;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Model part of the 
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// The Key of the Model that is currently opened by the application.
        /// </summary>
        ModelKey ModelKey { get; }

        /// <summary>
        /// List of Models from the Application Database.
        /// </summary>
        ModelCollection Models { get; }

        /// <summary>
        /// List of Domain Subject Areas within the Model.
        /// </summary>
        ModelSubjectAreaCollection ModelSubjectAreas { get; }
    }

    /// <summary>
    /// Represents the main data object used by the application.
    /// </summary>
    /// <remarks>
    /// The data is shared across the entire application.
    /// Forms need to connect to the Key for the data object they are presenting.
    /// If the data should change, the Forms need to reset the bindings and get new data from this object.
    /// </remarks>
    public partial class ModelData : IModel
    {
        // Model
        ModelItem defaultModel;
        //ModelKey modelKey;

        /// <inheritdoc/>
        public ModelKey ModelKey { get { return new ModelKey(Model); } }

        /// <inheritdoc/>
        public ModelCollection Models { get; } = new ModelCollection();

        /// <summary>
        /// The current Model opened by the application
        /// </summary>
        public ModelItem Model
        {
            get
            {
                if (Models.FirstOrDefault() is ModelItem item)
                { return item; }
                else { return defaultModel; }
            }
        }

        /// <summary>
        /// The File Object the Model was read from.
        /// </summary>
        public FileInfo? ModelFile { get; set; }

        #region Application Connection
        /// <summary>
        /// The Database Context of the Application for the Model.
        /// </summary>
        internal static Context ModelContext { get; private set; } = new Context();

        /// <summary>
        /// The Application Server.
        /// </summary>
        public String ServerName { get { return ModelContext.ServerName; } }

        /// <summary>
        /// The Application Database
        /// </summary>
        public String DatabaseName { get { return ModelContext.DatabaseName; } }
        #endregion

        /// <summary>
        /// Constructor for the Model Data
        /// </summary>
        protected ModelData() : base()
        {
            defaultModel = new ModelItem();
            Models.Add(defaultModel);
        }

        /// <summary>
        /// Constructor for the Model Data
        /// </summary>
        /// <param name="context"></param>
        public ModelData(Context context) : this()
        { ModelContext = context; }

        /// <summary>
        /// Initializes a New Model (does not clear the data).
        /// </summary>
        public IReadOnlyList<WorkItem> NewModel()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.AddRange(this.RemoveModel());
            work.AddRange(this.RemoveNameSpace());

            work.Add(new WorkItem()
            {
                WorkName = "New Model",
                DoWork = () =>
                {
                    defaultModel = new ModelItem();
                    Models.Add(defaultModel);
                }
            });

            work.AddRange(this.LoadNameSpace());

            return work;
        }

        /// <summary>
        /// Creates Work Items Loads the Model by Key from the Application Database
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> LoadModel(IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            work.AddRange(this.RemoveModel());
            work.AddRange(this.RemoveNameSpace());
            work.AddRange(this.LoadModelData(factory, key));
            work.AddRange(this.LoadDomain(factory, key));
            work.AddRange(this.LoadCatalog(factory, key));
            work.AddRange(this.LoadLibrary(factory, key));
            work.AddRange(this.LoadNameSpace());

            return work;
        }

        /// <summary>
        /// Creates Work Items that Save the Model to the Application Database
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> SaveModel(IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            work.AddRange(this.SaveModelData(factory, key));
            work.AddRange(this.SaveDomain(factory, key));
            work.AddRange(this.SaveCatalog(factory, key));
            work.AddRange(this.SaveLibrary(factory, key));

            return work;
        }

        /// <summary>
        /// Creates Work Items that Remove the Model (Clear).
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> RemoveModel()
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Clear Model", DoWork = this.Models.Clear });
            work.Add(new WorkItem() { WorkName = "Clear Subject Areas", DoWork = this.ModelSubjectAreas.Clear });
            work.AddRange(this.RemoveDomain());
            work.AddRange(this.RemoveCatalog());
            work.AddRange(this.RemoveLibrary());
            return work;
        }

        /// <summary>
        /// Creates Work Items that Delete the Model
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public IReadOnlyList<WorkItem> DeleteModel(IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            work.Add(factory.CreateWork(
                workName: "Delete Model",
                command: (conn) => this.Models.DeleteCommand(conn, key)));

            work.AddRange(this.RemoveModel());
            return work;
        }

    }

    /// <summary>
    /// Extension Method for the Model Data.
    /// </summary>
    public static class ModelDataExtension
    {
        /// <summary>
        /// Creates Work Items Loads the Models the Application Database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadModel<T>(this T target, IDatabaseWork factory)
            where T : IBindingTable<ModelItem>, IReadData
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(factory.CreateWork(
                workName: "Load Models",
                target: target,
                command: (conn) => target.LoadCommand(conn)));

            return work;
        }

        /// <summary>
        /// Creates Work Items that Delete a Model in the Application Database
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> DeleteModel(this IDeleteData<IModelKey> data, IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            ModelCollection empty = new ModelCollection();

            work.Add(factory.CreateWork(
                workName: "Delete Model",
                command: (conn) => empty.DeleteCommand(conn, key)));

            return work;
        }
    }


}
