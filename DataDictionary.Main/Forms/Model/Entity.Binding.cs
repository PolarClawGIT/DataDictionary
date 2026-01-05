using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Model
{
    partial class Entity
    {
        class FormBinding
        {
            public required Action<IEnumerable<WorkItem>, Action<RunWorkerCompletedEventArgs>?> DoWork { get; init; }

            public required BindingSource BindingEntity { private get; init; }

            public BindingView<EntityValue> Entity { get; private set; } =
                new BindingView<EntityValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingAlias { private get; init; }

            public BindingView<EntityAliasValue> Aliases { get; private set; } =
                new BindingView<EntityAliasValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingSubjectArea { private get; init; }

            public BindingView<EntitySubjectAreaValue> SubjectAreas { get; private set; } =
                new BindingView<EntitySubjectAreaValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingProperty { private get; init; }

            public BindingView<EntityPropertyValue> Properties { get; private set; } =
                new BindingView<EntityPropertyValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingDefinition { private get; init; }

            public BindingView<EntityDefinitionValue> Definitions { get; private set; } =
                new BindingView<EntityDefinitionValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingAttribute { private get; init; }

            public BindingView<EntityAttributeValue> Attributes { get; private set; } =
                new BindingView<EntityAttributeValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            public required BindingSource BindingAttributeDetail { private get; init; }

            public BindingView<AttributeValue> AttributeDetails { get; private set; } =
                new BindingView<AttributeValue>([])
                { AllowEdit = false, AllowNew = false, AllowRemove = false };

            EntityIndex entityIndex = new EntityIndex();
            TemporalIndex? temporalIndex = null;
            IEntity entityData = BusinessData.Model.Entity;

            public FormBinding()
            { }

            public void Load(IEntityIndex entity)
            {
                entityIndex = new EntityIndex(entity);

                Entity.ListChanged -= OnListChanged;
                BindingEntity.RaiseListChangedEvents = false;
                BindingProperty.RaiseListChangedEvents = false;
                BindingAlias.RaiseListChangedEvents = false;
                BindingSubjectArea.RaiseListChangedEvents = false;
                BindingDefinition.RaiseListChangedEvents = false;
                BindingAttribute.RaiseListChangedEvents = false;

                Entity.RaiseListChangedEvents = false;
                Properties.RaiseListChangedEvents = false;
                Aliases.RaiseListChangedEvents = false;
                SubjectAreas.RaiseListChangedEvents = false;
                Definitions.RaiseListChangedEvents = false;

                Entity = new BindingView<EntityValue>(entityData.Entities, w => entityIndex.Equals(w));
                Properties = new BindingView<EntityPropertyValue>(entityData.Properties, w => entityIndex.Equals(w));
                Aliases = new BindingView<EntityAliasValue>(entityData.Aliases, w => entityIndex.Equals(w));
                SubjectAreas = new BindingView<EntitySubjectAreaValue>(entityData.SubjectArea, w => entityIndex.Equals(w));
                Definitions = new BindingView<EntityDefinitionValue>(entityData.Definitions, w => entityIndex.Equals(w));
                Attributes = new BindingView<EntityAttributeValue>(entityData.Attributes, w => entityIndex.Equals(w));

                if (Entity.Count > 0)
                {
                    BindingEntity.DataSource = Entity;
                    BindingProperty.DataSource = Properties;
                    BindingAlias.DataSource = Aliases;
                    BindingSubjectArea.DataSource = SubjectAreas;
                    BindingDefinition.DataSource = Definitions;
                    BindingAttribute.DataSource = Attributes;

                    BindingEntity.RaiseListChangedEvents = true;
                    BindingProperty.RaiseListChangedEvents = true;
                    BindingAlias.RaiseListChangedEvents = true;
                    BindingSubjectArea.RaiseListChangedEvents = true;
                    BindingDefinition.RaiseListChangedEvents = true;
                    BindingAttribute.RaiseListChangedEvents = true;

                    Entity.RaiseListChangedEvents = true;
                    Properties.RaiseListChangedEvents = true;
                    Aliases.RaiseListChangedEvents = true;
                    SubjectAreas.RaiseListChangedEvents = true;
                    Definitions.RaiseListChangedEvents = true;
                    Attributes.RaiseListChangedEvents = true;
                }

                Entity.ResetBindings();
                Properties.ResetBindings();
                Aliases.ResetBindings();
                SubjectAreas.ResetBindings();
                Definitions.ResetBindings();
                Attributes.ResetBindings();

                BindingEntity.MoveFirst(); // For some reason this must be done last or it does not work.
            }

            public void Load(IEntityIndex entity, ITemporalIndex temporal)
            {
                Load(entity);
                temporalIndex = new TemporalIndex(temporal);
            }

            private void OnListChanged(Object? sender, ListChangedEventArgs e)
            {
                if (e.ListChangedType is ListChangedType.ItemDeleted
                    && sender is IBindingList values
                    && values.Count is 0)
                {
                    // This addresses invalid operation exception fired by CurrencyManager.FindGoodRow.
                    // The exception occurs on empty list and is triggered by the ListChanged Event.
                    // When this event occurs, all BindingSources need to set RaiseListChangedEvents to false.
                    // A related error can occur with DataGridViews when the BindingList has an empty list.

                    BindingEntity.RaiseListChangedEvents = false;
                }
            }

            public EntityValue NewValue()
            {
                EntityValue newValue = new EntityValue();
                entityData.Entities.Add(newValue);
                Load(newValue);

                return newValue;
            }

            public void Load(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());

                if (temporalIndex is null)
                {
                    entityData = BusinessData.Model.Entity;
                    work.AddRange(entityData.Delete(entityIndex));
                    work.AddRange(entityData.Load(factory, entityIndex));
                }
                else
                {
                    entityData = IEntity.Create();
                    work.AddRange(entityData.Load(factory, entityIndex, temporalIndex));
                }

                DoWork(work, StartBinding);

                void StartBinding(RunWorkerCompletedEventArgs args)
                {
                    Load(entityIndex);
                    if (onComplete is not null) { onComplete(args); }
                }
            }

            public void Save(Action<RunWorkerCompletedEventArgs>? onComplete = null)
            {
                IDatabaseWork factory = BusinessData.GetDbFactory();
                List<WorkItem> work = new List<WorkItem>();

                work.Add(factory.OpenConnection());
                work.AddRange(entityData.Save(factory, entityIndex));

                DoWork(work, onComplete);
            }

            public ITemporalData GetTemporal()
            { return entityData.GetTemporal(entityIndex); }

            public Boolean TryGetValue([NotNullWhen(true)] out EntityValue? result)
            {
                if (BindingEntity.Position >= 0
                    && BindingEntity.Current is EntityValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetAlias([NotNullWhen(true)] out EntityAliasValue? result)
            {
                if (BindingAlias.Position >= 0
                    && BindingAlias.Current is EntityAliasValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public Boolean TryGetAttribute([NotNullWhen(true)] out EntityAttributeValue? result)
            {
                if (BindingAttribute.Position >= 0
                    && BindingAttribute.Current is EntityAttributeValue value)
                { result = value; return true; }
                else { result = null; return false; }
            }

            public void RemoveValue()
            {
                if (TryGetValue(out EntityValue? value))
                {
                    entityData.RaiseListChangedEvents = false;
                    entityData.Remove(value);
                    Load(value);
                }
            }

            public Boolean GetAuthorization(Enumerations.CommandType command)
            {
                Boolean isGrant = false;
                SecurableIndex securable = BusinessData.Model.ModelIndex;
                isGrant = BusinessData.Authorization.IsGrant(securable);

                switch (command)
                {
                    case Enumerations.CommandType.Default: return true;
                    case Enumerations.CommandType.Delete: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.CommandType.OpenDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.CommandType.SaveDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.CommandType.DeleteDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    case Enumerations.CommandType.HistoryDatabase: return BusinessData.Authorization.IsModelAdmin || isGrant;
                    default: return false;
                }
            }

            public Boolean GetLocked()
            {
                if (TryGetValue(out EntityValue? value))
                {
                    return value.RowState() is DataRowState.Detached
                        or DataRowState.Deleted;
                }
                else return true;
            }

            public void AddAttributes(IEnumerable<AttributeValue> attributes)
            {
                if (TryGetValue(out EntityValue? entity))
                {
                    foreach (AttributeValue attribute in attributes)
                    {
                        if (!Attributes.Any(w => attribute.AttributePath.Equals(w.AttributePath)))
                        {
                            Attributes.Add(
                                new EntityAttributeValue(entity)
                                {
                                    AttributeKnownAs = attribute.AttributeTitle,
                                    AttributePath = attribute.AttributePath,
                                    IsNullable = attribute.IsNullable,
                                    OrdinalPosition = Attributes.Count + 1,
                                });
                        }
                    }
                }
            }

            public void AddAttribute()
            {
                if (TryGetValue(out EntityValue? entity))
                {
                    Attributes.Add(new EntityAttributeValue(entity)
                    {
                        AttributeKnownAs = "{new attribute}",
                        OrdinalPosition = Attributes.Count + 1
                    });
                }
            }

            public void AddAlias()
            {
                if (TryGetValue(out EntityValue? entity))
                {
                    Aliases.Add(new EntityAliasValue(entity)
                    { AliasScope = ScopeType.Null });
                }
            }

            public void AddAlias(IEnumerable<INamedScopeValue> namedScopes)
            {
                if (TryGetValue(out EntityValue? entity))
                {
                    foreach (INamedScopeValue namedScope in namedScopes)
                    {
                        if (!Aliases.Any(w => namedScope.Path.Equals(w.AliasName)))
                        {
                            Aliases.Add(new EntityAliasValue(entity)
                            {
                                AliasName = namedScope.Path,
                                AliasScope = namedScope.Scope
                            });
                        }
                    }
                }
            }

            public EntityDefinitionValue NewDefinition()
            {
                if (TryGetValue(out EntityValue? value))
                { return new EntityDefinitionValue(value); }
                else { throw new InvalidOperationException("Current EntityValue not defined"); }
            }

            public EntityPropertyValue NewProperty()
            {
                if (TryGetValue(out EntityValue? value))
                { return new EntityPropertyValue(value); }
                else { throw new InvalidOperationException("Current EntityValue not defined"); }
            }

            internal IAliasSubType NewAlias()
            {
                if (TryGetValue(out EntityValue? value))
                { return new EntityAliasValue(value); }
                else { throw new InvalidOperationException("Current EntityValue not defined"); }
            }


            public void AddSubjectArea(ISubjectAreaIndex index)
            {
                if (TryGetValue(out EntityValue? entity))
                { SubjectAreas.Add(new EntitySubjectAreaValue(entity, index)); }
            }

            public void RemoveSubjectArea(ISubjectAreaIndex subject)
            {
                SubjectAreaIndex key = new SubjectAreaIndex(subject);

                while (SubjectAreas.FirstOrDefault(w => key.Equals(w)) is EntitySubjectAreaValue item)
                { SubjectAreas.Remove(item); }
            }


        }
    }
}
