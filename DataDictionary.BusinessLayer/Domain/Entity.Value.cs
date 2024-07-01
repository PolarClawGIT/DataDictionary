using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntityValue : IDomainEntityItem, IEntityIndex, IEntityIndexName
    { }

    /// <inheritdoc/>
    public class EntityValue : DomainEntityItem, IEntityValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DomainEntityItem()"/>
        public EntityValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new EntityIndex(this); }

        /// <inheritdoc/>
        /// <remarks>Partial Path</remarks>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(EntityTitle); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return EntityTitle ?? Scope.ToName(); }

        /*internal XElement? GetXElement(IEnumerable<TemplateElementValue>? options = null)
        {
            XElement? result = new XElement(this.Scope.ToName());

            if (options is not null)
            {
                foreach (TemplateElementValue option in options)
                {
                    Object? value = null;

                    switch (option.PropertyName)
                    {
                        case nameof(this.EntityId): value = EntityId.ToString(); break;
                        case nameof(this.EntityTitle): value = EntityTitle; break;
                        case nameof(this.EntityDescription): value = EntityDescription; break;
                        default:
                            break;
                    }

                    result.Add(option.GetXElement(value));
                }
            }

            return result;
        }
        */
        internal static IReadOnlyList<NodePropertyValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelEntity;
            IEntityValue EntityNames;
            List<NodePropertyValue> result = new List<NodePropertyValue>()
            {
                new NodePropertyValue() {PropertyName = nameof(EntityNames.EntityId),          DataType = typeof(Guid),    AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(EntityNames.EntityTitle),       DataType = typeof(String),  AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(EntityNames.EntityDescription), DataType = typeof(String),  AllowDBNull = true,  PropertyScope = scope},
            };

            return result;
        }
    }
}
