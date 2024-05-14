using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Entity;
using System.ComponentModel;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IEntityValue : IDomainEntityItem, IEntityIndex, IEntityIndexName
    { }

    /// <inheritdoc/>
    public class EntityValue : DomainEntityItem, IEntityValue, INamedScopeValue
    {
        /// <inheritdoc cref="DomainEntityItem()"/>
        public EntityValue() : base()
        { PropertyChanged += CatalogValue_PropertyChanged; }

        /// <inheritdoc/>
        public virtual NamedScopeIndex GetKey()
        { return new NamedScopeIndex(EntityId); }

        /// <inheritdoc/>
        public virtual NamedScopePath GetPath()
        { return new NamedScopePath(EntityTitle); }

        /// <inheritdoc/>
        public virtual String GetTitle()
        { return EntityTitle ?? String.Empty; }

        /// <inheritdoc/>
        public event EventHandler? OnTitleChanged;
        private void CatalogValue_PropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(EntityTitle)
                && OnTitleChanged is EventHandler handler)
            { handler(this, EventArgs.Empty); }
        }

        internal XElement? GetXElement(IEnumerable<SchemaElementValue>? options = null)
        {
            XElement? result = new XElement(this.Scope.ToName());

            if (options is not null)
            {
                foreach (SchemaElementValue option in options)
                {
                    Object? value = null;

                    switch (option.ColumnName)
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

        internal static IReadOnlyList<ColumnValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelEntity;
            IEntityValue EntityNames;
            List<ColumnValue> result = new List<ColumnValue>()
            {
                new ColumnValue() {ColumnName = nameof(EntityNames.EntityId),          DataType = typeof(Guid),    AllowDBNull = false, Scope = scope},
                new ColumnValue() {ColumnName = nameof(EntityNames.EntityTitle),       DataType = typeof(String),  AllowDBNull = false, Scope = scope},
                new ColumnValue() {ColumnName = nameof(EntityNames.EntityDescription), DataType = typeof(String),  AllowDBNull = true,  Scope = scope},
            };

            return result;
        }
    }
}
