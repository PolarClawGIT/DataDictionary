using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IEntityValue : IEntityItem, IEntityIndex, IEntityIndexName,
        IScopeType, ITemporalValue
    { }

    /// <inheritdoc/>
    public class EntityValue : EntityItem, IEntityValue, IPathValue, INamedScopeSourceValue
    {
        IPathValue pathValue; // Backing field for IPathValue

        /// <inheritdoc/>
        PathIndex IPathIndex.Path { get { return pathValue.Path; } }

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return pathValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return pathValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelEntity; } }

        /// <summary>
        /// Path Index version of the EntityName
        /// </summary>
        public PathIndex EntityPath
        {
            get
            { return new PathIndex(new PathIndex(PathIndex.Parse(EntityName).ToArray())); }
            set
            {
                EntityName = value.MemberFullPath;
                OnPropertyChanged(nameof(EntityPath));
            }
        }

        /// <inheritdoc/>
        public EntityValue() : base()
        {
            pathValue = new PathValue(this)
            {
                GetIndex = () => new EntityIndex(this),
                GetPath = () =>
                {
                    if (String.IsNullOrWhiteSpace(EntityName))
                    { return new PathIndex(EntityTitle); }
                    else { return new PathIndex(new PathIndex(PathIndex.Parse(EntityName).ToArray())); }
                },
                GetScope = () => Scope,
                GetTitle = () => EntityTitle ?? ScopeEnumeration.Cast(Scope).Name,
                IsPathChanged = (e) => e.PropertyName is nameof(EntityTitle) or nameof(EntityName),
                IsTitleChanged = (e) => e.PropertyName is nameof(EntityTitle)
            };
        }

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
