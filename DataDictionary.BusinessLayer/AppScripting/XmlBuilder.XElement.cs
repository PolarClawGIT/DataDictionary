using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;
using Toolbox.BindingTable;

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
        /// Used to find the correct XElement Build method.
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

        public static XElement? Build(
                this IEnumerable<XmlBuilder> builders,
                IEnumerable<IXmlBuilderIndex> items)
        {
            XElement root = new XElement("root");
            List<IXmlBuilderIndex> itemList = items.ToList(); // To Protect against add/delete in the middle of the process.


            if (itemList.Count == 0)
            { return null; }
            else if (itemList.TryGetSingle(out IXmlBuilderIndex? value))
            {
                XmlBuilderIndex rootKey = new XmlBuilderIndex(value);
                if (builders.TryGetXmlBuilder(rootKey, out XmlBuilder? builder))
                {
                    XObject? xObject = builder.Build(value);

                    if (xObject is XElement element)
                    {
                        root = element;

                        foreach (var item in builders.Where(w => rootKey.MemberFullPath.Equals(w.BuilderPath.ParentPath)))
                        { element.Add(item.Build(value)); }
                    }
                    else
                    {
                        root = new XElement(builder.NodeName);
                        root.Add(xObject);
                    }

                    return root;
                }
                else { return null; }
            }
            else
            {
                foreach (var item in items)
                {

                }
            }


            throw new NotFiniteNumberException();
        }

        /// <summary>
        /// Generic XElement that the specific builder are based on.
        /// </summary>
        /// <typeparam name="TRoot"></typeparam>
        /// <typeparam name="TChild"></typeparam>
        /// <param name="builders"></param>
        /// <param name="scope"></param>
        /// <param name="roots"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public static XElement? Build<TRoot, TChild>(this IEnumerable<XmlBuilder> builders, ScopeType scope, IEnumerable<TRoot> roots,
                params IEnumerable<(ScopeType scope, IEnumerable<TChild> values, Func<TRoot, TChild, Boolean> filter)> children)
                where TRoot : class, IScopeType
                where TChild : class
        {
            XmlBuilderIndex rootKey = new XmlBuilderIndex(scope);
            Int32 rootCount = roots.Count(w => w.Scope == scope);
            XElement root = new XElement(scope.GetName());

            // TODO: it calls the root but none of the child builders. Need to switch to path based?

            if (builders.TryGetXmlBuilder(rootKey, out XmlBuilder? builder))
            {
                if (rootCount == 0)
                { root = new XElement(builder.NodeName); }
                else
                {
                    if (rootCount == 1)
                    {
                        TRoot rootItem = roots.First();
                        XObject? node = builder.Build(rootItem);

                        if (node is XElement rootNode)
                        {
                            foreach (var child in builders.Where(w => w.ObjectScope.Equals(scope) && !rootKey.Equals(w)))
                            { rootNode.Add(child.Build(rootItem)); }

                            root = rootNode;
                        }
                        else
                        {
                            root = new XElement(builder.NodeName);
                            root.Add(node);
                        }




                        foreach (var child in children)
                        {
                            XmlBuilderIndex childKey = new XmlBuilderIndex(child.scope);

                            if (builders.TryGetXmlBuilder(childKey, out XmlBuilder? childBuilder))
                            {
                                XElement childRoot = new XElement(childBuilder.NodeName);

                                foreach (TChild item in child.values.Where(w => w is IScopeType s && child.scope == s.Scope && child.filter(rootItem, w)))
                                { childRoot.Add(childBuilder.Build(item)); }

                                root.Add(childRoot);
                            }
                        }
                    }
                    else
                    {
                        root = new XElement("Root");

                        foreach (TRoot currentItem in roots.Where(w => scope == w.Scope))
                        {
                            XObject? currentNode = builder.Build(currentItem);
                            root.Add(currentNode);

                            if (currentNode is XElement currentRoot)
                            {
                                foreach (var child in children)
                                {
                                    XmlBuilderIndex childKey = new XmlBuilderIndex(child.scope);
                                    if (builders.TryGetXmlBuilder(childKey, out XmlBuilder? childBuilder))
                                    {
                                        XElement childRoot = new XElement(childBuilder.NodeName);

                                        foreach (TChild item in child.values.Where(w => w is IScopeType s && child.scope == s.Scope && child.filter(currentItem, w)))
                                        { childRoot.Add(childBuilder.Build(item)); }

                                        currentRoot.Add(childRoot);
                                    }

                                }
                            }

                        }

                    }
                }

            }

            return root;
        }

        /// <summary>
        /// XElement build for AttributeValue.
        /// </summary>
        /// <param name="builders"></param>
        /// <param name="attributes"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static XElement Build(this IEnumerable<XmlBuilder> builders,
                IEnumerable<AttributeValue> attributes,
                IEnumerable<AttributePropertyValue> properties)
        {
            XElement? result = builders.Build<AttributeValue, IAttributeIndex>(ScopeType.ModelAttribute, attributes,
                        (ScopeType.ModelAttributeProperty, properties, (r, c) => new AttributeIndex(r).Equals(new AttributeIndex(c)))
                        );

            if (result is XElement) { return result; }
            else { return new XElement(ScopeType.ModelAttribute.GetName()); }
        }

    }
}
