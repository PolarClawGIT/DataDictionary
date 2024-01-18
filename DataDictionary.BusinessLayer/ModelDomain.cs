using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{

    /// <summary>
    /// Interface component for the Model Attribute
    /// </summary>
    public interface IModelAttribute
    {
        /// <summary>
        /// List of Domain Attributes within the Model.
        /// </summary>
        DomainAttributeCollection DomainAttributes { get; }

        /// <summary>
        /// List of Domain Aliases for the Attributes within the Model.
        /// </summary>
        DomainAttributeAliasCollection DomainAttributeAliases { get; }

        /// <summary>
        /// List of Domain Properties for the Attributes within the Model.
        /// </summary>
        DomainAttributePropertyCollection DomainAttributeProperties { get; }
    }

    /// <summary>
    /// Interface component for the Model Entity
    /// </summary>
    public interface IModelEntity
    {
        /// <summary>
        /// List of Domain Entities within the Model.
        /// </summary>
        DomainEntityCollection DomainEntities { get; }

        /// <summary>
        /// List of Domain Aliases for the Entities within the Model.
        /// </summary>
        DomainEntityAliasCollection DomainEntityAliases { get; }

        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        DomainEntityPropertyCollection DomainEntityProperties { get; }
    }

    /// <summary>
    /// Interface component for the Model Domain (Attribute, Entity and Subject Area)
    /// </summary>
    /// <remarks>When combined with the Extension class, this approximates multi-inheritance.</remarks>
    public interface IModelDomain : IModelAttribute, IModelEntity
    { }

    /// <summary>
    /// Implementation of Model Domain (Attribute, Entity and Subject Area)
    /// </summary>
    /// <remarks>When combined with the Extension class, this approximates multi-inheritance.</remarks>
    public static class ModelDomain
    {
        /// <summary>
        /// Creates the work items to Load the Model Domain using the Model key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadDomain(this IModelDomain data, IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            work.Add(factory.CreateWork(
                workName: "Load DomainAttributes",
                target: data.DomainAttributes,
                command: (conn) => data.DomainAttributes.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DomainAttributeAliases",
                target: data.DomainAttributeAliases,
                command: (conn) => data.DomainAttributeAliases.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DomainAttributeProperties",
                target: data.DomainAttributeProperties,
                command: (conn) => data.DomainAttributeProperties.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DomainEntities",
                target: data.DomainEntities,
                command: (conn) => data.DomainEntities.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DomainEntityAliases",
                target: data.DomainEntityAliases,
                command: (conn) => data.DomainEntityAliases.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load DomainEntityProperties",
                target: data.DomainEntityProperties,
                command: (conn) => data.DomainEntityProperties.LoadCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Save the Model Domain using the Model key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveDomain(this IModelDomain data, IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            work.Add(factory.CreateWork(
                workName: "Save DomainAttributes",
                command: (conn) => data.DomainAttributes.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DomainAttributeAliases",
                command: (conn) => data.DomainAttributeAliases.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DomainAttributeProperties",
                command: (conn) => data.DomainAttributeProperties.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DomainEntities",
                command: (conn) => data.DomainEntities.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DomainEntityAliases",
                command: (conn) => data.DomainEntityAliases.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save DomainEntityProperties",
                command: (conn) => data.DomainEntityProperties.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Removes all the Domain Data from the Model (Clear)
        /// </summary>
        /// <param name="data"></param>
        public static IReadOnlyList<WorkItem> RemoveDomain(this IModelDomain data)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Clear DomainAttributes", DoWork = data.DomainAttributes.Clear });
            work.Add(new WorkItem() { WorkName = "Clear DomainAttributeAliases", DoWork = data.DomainAttributeAliases.Clear });
            work.Add(new WorkItem() { WorkName = "Clear DomainAttributeProperties", DoWork = data.DomainAttributeProperties.Clear });
            work.Add(new WorkItem() { WorkName = "Clear DomainEntities", DoWork = data.DomainEntities.Clear });
            work.Add(new WorkItem() { WorkName = "Clear DomainEntityAliases", DoWork = data.DomainEntityAliases.Clear });
            work.Add(new WorkItem() { WorkName = "Clear DomainEntityProperties", DoWork = data.DomainEntityProperties.Clear });

            return work;
        }

        /// <summary>
        /// Returns a list of Entities for a given key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source">Something that implements the Attribute Key</param>
        /// <returns></returns>
        public static IEnumerable<DomainEntityItem> GetEntities(this IModelDomain data, IDomainAttributeKey source)
        {
            DomainAttributeKey key = new DomainAttributeKey(source);
            List<DomainEntityItem> result = new List<DomainEntityItem>();

            foreach (AliasKeyName attributeAliases in data.DomainAttributeAliases.Where(w => key.Equals(w)).Select(s => new AliasKeyName(s).Parent()))
            {
                foreach (DomainEntityKey entityAliases in data.DomainEntityAliases.Where(w => attributeAliases.Equals(w)).Select(s => new DomainEntityKey(s)))
                {
                    if (data.DomainEntities.FirstOrDefault(w => entityAliases.Equals(w)) is DomainEntityItem item)
                    { result.Add(item); }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the Entity Properties given the key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DomainEntityPropertyItem> GetProperties(this IModelDomain data, IDomainEntityKey source)
        {
            DomainEntityKey key = new DomainEntityKey(source);
            return data.DomainEntityProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets the Entity Aliases given the key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DomainEntityAliasItem> GetAliases(this IModelDomain data, IDomainEntityKey source)
        {
            DomainEntityKey key = new DomainEntityKey(source);
            return data.DomainEntityAliases.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Returns a list of Attributes for a given key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source">An item that implements the Entity Key</param>
        /// <returns></returns>
        public static IEnumerable<DomainAttributeItem> GetAttributes(this IModelDomain data, IDomainEntityKey source)
        {
            DomainEntityKey key = new DomainEntityKey(source);
            List<DomainAttributeItem> result = new List<DomainAttributeItem>();

            foreach (AliasKeyName entityAliases in data.DomainEntityAliases.Where(w => key.Equals(w)).Select(s => new AliasKeyName(s)))
            {
                foreach (DomainAttributeKey attributeAliases in data.DomainAttributeAliases.Where(w => entityAliases.Equals(new AliasKeyName(w).Parent())).Select(s => new DomainAttributeKey(s)))
                {
                    if (data.DomainAttributes.FirstOrDefault(w => attributeAliases.Equals(w)) is DomainAttributeItem item)
                    { result.Add(item); }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the Attribute Properties given the key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DomainAttributePropertyItem> GetProperties(this IModelDomain data, IDomainAttributeKey source)
        {
            DomainAttributeKey key = new DomainAttributeKey(source);
            return data.DomainAttributeProperties.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets the Attribute Aliases given the key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DomainAttributeAliasItem> GetAliases(this IModelDomain data, IDomainAttributeKey source)
        {
            DomainAttributeKey key = new DomainAttributeKey(source);
            return data.DomainAttributeAliases.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Gets an Attribute given a key
        /// </summary>
        /// <param name="source"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IDomainAttributeItem? GetAttribute(this IEnumerable<IDomainAttributeItem> source, IDomainAttributeKey item)
        { return source.FirstOrDefault(w => w.AttributeId == item.AttributeId); }

        /// <summary>
        /// Gets an Attribute given a key
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDomainAttributeItem? GetAttribute(this IDomainAttributeKey item, IEnumerable<IDomainAttributeItem> source)
        { return source.FirstOrDefault(w => w.AttributeId == item.AttributeId); }
    }
}
