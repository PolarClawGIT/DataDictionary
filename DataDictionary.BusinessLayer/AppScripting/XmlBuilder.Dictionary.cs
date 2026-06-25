using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{

    // TODO: Need to return a list including children so a tree structure can be built.
    // TODO: Need a way to load and save to the database. Rebuild into SchemaNode?

    public class XmlBuilderDictionary : Dictionary<PathIndex, XmlBuilder>
    {
        //public required TryGetProperty GetProperty { get; init; } 

        //public required TryGetDefinition GetDefinition { get; init; }



        public XmlBuilderDictionary(IEnumerable<XmlBuilder> builders) : base()
        {
            foreach (XmlBuilder item in builders)
            { Add(item.ObjectPath, item); }
        }

        public XElement Build(ScopeType scope, IEnumerable<IScopeType> values)
        {
            PathIndex key = new PathIndex(scope);

            if (TryGetValue(key, out XmlBuilder? builder))
            {
                var valueCount = values.Count();

                if (valueCount == 0)
                {
                    return new XElement(builder.NodeName);
                }
                else if (valueCount == 1)
                {
                    XObject? first = builder.Build(values.First());

                    if (first is XElement element)
                    { return element; }
                    else
                    {
                        XElement root = new XElement(builder.NodeName);
                        root.Add(first);
                        return root;
                    }
                }
                else
                {
                    XElement root = new XElement(builder.NodeName);
                    foreach (var item in values.Where(w => w.Scope == scope))
                    { root.Add(builder.Build(item)); }

                    return root;
                }
            }
            else { return new XElement(new XmlBuilder(scope).NodeName); }

        }

    }
}
