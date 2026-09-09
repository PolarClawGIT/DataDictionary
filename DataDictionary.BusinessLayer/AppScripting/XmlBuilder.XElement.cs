using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Builder Extension class containing factory method for executing the XML Builders to produce XElement.
    /// </summary>
    /// <remarks>
    /// These could me moved into several different classes.
    /// </remarks>
    public static class XmlBuilderXElement
    {   // TODO: Probably move to the specific data classes

        /// <summary>
        /// Place holder class used for the Root Value.
        /// </summary>
        class RootValue : IScopeType
        { public ScopeType Scope { get; init; } }

        /// <summary>
        /// Try/Get the correct XElement Build method for a given object.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="targetObject"></param>
        /// <param name="build"></param>
        /// <returns></returns>
        public static Boolean TryGetBuilder(this IModel model, ITemplateObjectNameIndex targetObject, [NotNullWhen(true)] out Func<IEnumerable<XmlBuilder>, XElement>? build)
        {   // Think this is the factory pattern.

            build = null;
            PathIndex key = new PathIndex(PathIndex.Parse(targetObject.ObjectPath));

            // Search the Attributes and get the Paths associated with them
            var attributes = model.Attribute.Attributes.
                    Join(model.Attribute.SubjectArea,
                        attribute => new AttributeIndex(attribute),
                        subject => new AttributeIndex(subject),
                        (Attribute, Subject) => new { Attribute, Subject }).
                    Join(model.SubjectAreas,
                        subjectKey => new SubjectAreaIndex(subjectKey.Subject),
                        subject => new SubjectAreaIndex(subject),
                        (attributeSubject, subject) => new { Path = new PathIndex(subject.SubjectAreaPath, attributeSubject.Attribute.AttributePath), attributeSubject.Attribute }).
                    Union(model.Attribute.Attributes.
                        Select(s => new { Path = s.AttributePath, Attribute = s })).
                    Where(w => key.Equals(w.Path) && w.Attribute.Scope == targetObject.ObjectScope).
                    ToList();

            // Search the Entities and get the Paths associated with them
            var entities = model.Entity.Entities.
                    Join(model.Entity.SubjectArea,
                        entity => new EntityIndex(entity),
                        subject => new EntityIndex(subject),
                        (Entity, Subject) => new { Entity, Subject }).
                    Join(model.SubjectAreas,
                        subjectKey => new SubjectAreaIndex(subjectKey.Subject),
                        subject => new SubjectAreaIndex(subject),
                        (entitySubject, subject) => new { Path = new PathIndex(subject.SubjectAreaPath, entitySubject.Entity.EntityPath), entitySubject.Entity }).
                    Union(model.Entity.Entities.
                        Select(s => new { Path = s.EntityPath, Entity = s })).
                    Where(w => key.Equals(w.Path) && w.Entity.Scope == targetObject.ObjectScope).
                    ToList();

            // Search the Processes and get the Paths associated with them
            var processes = model.Process.Processes.
                    Join(model.Process.SubjectArea,
                        process => new ProcessIndex(process),
                        subject => new ProcessIndex(subject),
                        (Process, Subject) => new { Process, Subject }).
                    Join(model.SubjectAreas,
                        subjectKey => new SubjectAreaIndex(subjectKey.Subject),
                        subject => new SubjectAreaIndex(subject),
                        (processSubject, subject) => new { Path = new PathIndex(subject.SubjectAreaPath, processSubject.Process.ProcessPath), processSubject.Process }).
                    Union(model.Process.Processes.
                        Select(s => new { Path = s.ProcessPath, Process = s })).
                    Where(w => key.Equals(w.Path) && w.Process.Scope == targetObject.ObjectScope).
                    ToList();

            // Wow, this Linq logic actually worked. Not certain how stable or east to debug it is. Consider breaking it up?

            if (attributes.Count > 0)
            {
                // TODO: Does not give results as expected when the function is actually called.
                build = (builders) => Build(builders, attributes.Select(s => s.Attribute), model.Attribute.Properties);
                return true;
            }
            // TODO: Add results for Entity and Process
            else { return false; }
        }

        /// <summary>
        /// Used to find the correct XElement Build method for a given object.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="targetObject"></param>
        /// <returns></returns>
        public static Func<IEnumerable<XmlBuilder>, XElement>? GetBuilder(this IModel model, ITemplateObjectNameIndex targetObject)
        {
            if(model.TryGetBuilder(targetObject, out Func<IEnumerable<XmlBuilder>, XElement>? builders))
            { return builders; }
            else { return null; }
        }

        static XElement Build<TRoot>(
                IEnumerable<XmlBuilder> builders,
                IEnumerable<TRoot> rootMembers,
                params IEnumerable<Func<TRoot, IEnumerable<IScopeType>>> getChildren)
            where TRoot : class, IScopeType
        {
            XElement? rootNode = null;

            var elementKeys = rootMembers.
                Select(s => new XmlBuilderIndex(s.Scope)).
                Distinct().
                OrderBy(o => o).
                ToList();

            foreach (var elementKey in elementKeys)
            {
                var rootItems = rootMembers.
                    Where(w => elementKey.Equals(new XmlBuilderIndex(w.Scope))).
                    ToList();

                foreach (var rootItem in rootItems)
                {
                    XElement? elementRoot = null;

                    var elementBuilders = builders.
                        Where(w => rootItem.Scope == w.ObjectScope).
                        OrderBy(o => o.BuilderPath.ParentPath).
                        ThenBy(o => o.RenderOrder).
                        ThenBy(o => o.NodeName).
                        ToList();

                    foreach (var builder in elementBuilders)
                    {
                        XObject? elementNode = builder.Build(rootItem);

                        // The first builder is expected to be the root.
                        if (elementRoot is null && elementNode is XElement rootElement)
                        { elementRoot = new XElement(rootElement); }
                        else if (elementRoot is null)
                        {
                            elementRoot = new XElement(builder.BuilderPath.Member);
                            elementRoot.Add(elementNode);
                        }
                        else // All the child builders.
                        { elementRoot.Add(elementNode); }
                    }

                    foreach (var getChild in getChildren)
                    {
                        var childValues = getChild(rootItem).
                            ToList();

                        var childKeys = childValues.
                            Select(s => new XmlBuilderIndex(s.Scope)).
                            Distinct().
                            OrderBy(o => o).
                            ToList();

                        foreach (var childKey in childKeys)
                        {
                            XElement? childRoot = null;

                            var childItems = childValues.
                                Where(w => childKey.Equals(new XmlBuilderIndex(w.Scope))).
                                ToList();

                            var childBuilders = builders.
                                 Where(w => w.ObjectScope.Equals(childKey.ObjectScope)).
                                 OrderBy(o => o.BuilderPath.ParentPath).
                                 ThenBy(o => o.RenderOrder).
                                 ThenBy(o => o.NodeName).
                                 ToList();

                            foreach (var childBuilder in childBuilders)
                            {
                                if (childRoot is null)
                                {   // The first builder is expected to be the root.
                                    XObject? childNode = childBuilder.Build(new RootValue() { Scope = childKey.ObjectScope });

                                    if (childNode is XElement childElement)
                                    { childRoot = childElement; }
                                    else
                                    {
                                        childRoot = new XElement(childKey.Member);
                                        childRoot.Add(childNode);
                                    }
                                }
                                else
                                {
                                    foreach (var childItem in childItems)
                                    {
                                        XObject? childNode = childBuilder.Build(childItem);
                                        if (childNode is not null)
                                        { childRoot.Add(childNode); }
                                    }
                                }
                            }


                            if (elementRoot is not null)
                            { elementRoot.Add(childRoot); }
                        }
                    }

                    if (rootNode is null && rootItems.Count == 1)
                    { rootNode = elementRoot; }
                    else if (rootNode is null)
                    {
                        rootNode = new XElement("root");
                        rootNode.Add(elementRoot);
                    }

                }
            }

            if (rootNode is null)
            { return new XElement("root"); }
            else { return rootNode; }
        }

        /// <summary>
        /// XElement builder for AttributeValue
        /// </summary>
        /// <param name="builders"></param>
        /// <param name="attributes"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static XElement Build(this IEnumerable<XmlBuilder> builders,
                IEnumerable<AttributeValue> attributes,
                IEnumerable<AttributePropertyValue> properties)
        {
            XElement result = Build<AttributeValue>(
                builders,
                attributes,
                (parent) => properties.Where(w => new AttributeIndex(parent).Equals(w))
                );



            return result;
        }
    }
}
