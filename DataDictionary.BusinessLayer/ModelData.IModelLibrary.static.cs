using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.LibraryData;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Implementation component for the Model Library
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    [Obsolete("To be replaced with BusinessLayerData")]
    public static class ModelLibrary
    {
        static Dictionary<ScopeType, String> scopeTypeToNetCoding = new Dictionary<ScopeType, String>
        {
            { ScopeType.LibraryNameSpace, "N"},
            { ScopeType.LibraryType,      "T"},
            { ScopeType.LibraryField,     "F"},
            { ScopeType.LibraryProperty,  "P"},
            { ScopeType.LibraryMethod,    "M"},
            { ScopeType.LibraryEvent,     "E"},
        };

        static ScopeType ToScopeType(String? value)
        {
            if (scopeTypeToNetCoding.FirstOrDefault(w => w.Value.Equals(value, KeyExtension.CompareString)) is KeyValuePair<ScopeType, String> keyValue && keyValue.Key != ScopeType.Null)
            { return keyValue.Key; }
            else { return ScopeType.Null; }
        }

        /// <summary>
        /// Creates the work items to Load the Model Library using the Model key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadLibrary(this IModelLibrary data, IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            work.Add(factory.CreateWork(
                workName: "Load LibrarySources",
                target: data.LibrarySources,
                command: (conn) => data.LibrarySources.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load LibraryMembers",
                target: data.LibraryMembers,
                command: (conn) => data.LibraryMembers.LoadCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Load the Model Library using the Library key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="libraryKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadLibrary(this IModelLibrary data, IDatabaseWork factory, ILibrarySourceKey libraryKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            LibrarySourceKey key = new LibrarySourceKey(libraryKey);

            work.Add(factory.CreateWork(
                workName: "Load LibrarySources",
                target: data.LibrarySources,
                command: (conn) => data.LibrarySources.LoadCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Load LibraryMembers",
                target: data.LibraryMembers,
                command: (conn) => data.LibraryMembers.LoadCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Load the Model Library from an XML Documents file.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadLibrary(this IModelLibrary data, FileInfo file)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            Action<Int32, Int32> progress = (x, y) => { };

            WorkItem work = new WorkItem()
            {
                WorkName = "Import Library",
                DoWork = ImportLibrary
            };

            progress = work.OnProgressChanged;
            workItems.Add(work);

            return workItems;

            void ImportLibrary()
            {
                try
                { // Parse the XML file and load to data objects
                    XmlDocument xmlData = new XmlDocument();
                    xmlData.Load(file.FullName);
                    LibrarySourceKeyName sourceKey;

                    if (xmlData.DocumentElement is XmlElement root)
                    {
                        //String assemblyName = String.Empty; // Surrogate key for Library
                        LibrarySourceItem sourceItem;

                        // Parse the Assembly Node
                        if (root.ChildNodes.Cast<XmlNode>().FirstOrDefault(w => w.Name == "assembly") is XmlNode assemblyNode)
                        {
                            sourceItem = new LibrarySourceItem()
                            {
                                LibraryTitle = assemblyNode.InnerText,
                                AssemblyName = assemblyNode.InnerText,
                                SourceFile = file.Name,
                                SourceDate = file.LastWriteTime,
                                ScopeName = ScopeType.Library.ToScopeName()
                            };

                            sourceKey = new LibrarySourceKeyName(sourceItem);

                            if (data.LibrarySources.FirstOrDefault(w => sourceKey.Equals(w)) is LibrarySourceItem alreadyExists)
                            {
                                sourceItem.SourceFile = file.Name;
                                sourceItem.SourceDate = file.LastWriteTime;
                            }
                            else { data.LibrarySources.Add(sourceItem); }
                        }
                        else { throw new InvalidOperationException("Expected exactly one [assembly] node."); }

                        // Parse the Members nodes https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/
                        if (root.ChildNodes.Cast<XmlNode>().FirstOrDefault(w => w.Name == "members") is XmlNode membersNode)
                        {
                            Int32 itemCount = membersNode.ChildNodes.Cast<XmlNode>().Count(w => w.Name == "member");
                            Int32 itemDone = 0;

                            foreach (XmlNode memberNode in membersNode.ChildNodes.Cast<XmlNode>().Where(w => w.Name == "member"))
                            {
                                String parseString = String.Empty;
                                if (memberNode.Attributes is not null && memberNode.Attributes.Cast<XmlAttribute>().FirstOrDefault(w => w.Name == "name") is XmlAttribute attribute)
                                { parseString = attribute.Value; }
                                else { throw new InvalidOperationException("Expected exactly one [name] node for each [member] node"); }

                                // Parse the Member Type
                                String memberType = String.Empty;
                                if (parseString.Split(':') is String[] types && types.Length > 0)
                                {
                                    memberType = types[0];
                                    parseString = parseString.Substring(memberType.Length + 1);
                                }

                                String memberNameSpace = String.Empty;
                                LibraryMemberItem? nameSpaceItem = null;

                                while (
                                    parseString.IndexOf(".") is int nextPeriod
                                    && parseString.IndexOf("#") is int nextHash
                                    && parseString.IndexOf("(") is int nextPern
                                    && nextPeriod >= 0
                                    && (nextHash < 0 || nextPeriod < nextHash)
                                    && (nextPern < 0 || nextPeriod < nextPern))
                                {
                                    if (String.IsNullOrWhiteSpace(memberNameSpace))
                                    {
                                        if (data.LibraryMembers.FirstOrDefault(w => sourceKey.Equals(w) && String.IsNullOrWhiteSpace(w.NameSpace) && w.MemberName == parseString.Substring(0, nextPeriod)) is LibraryMemberItem existing)
                                        { nameSpaceItem = existing; }
                                        else
                                        {
                                            nameSpaceItem = new LibraryMemberItem()
                                            {
                                                LibraryId = sourceItem.LibraryId,
                                                AssemblyName = sourceItem.AssemblyName,
                                                MemberName = parseString.Substring(0, nextPeriod),
                                                NameSpace = String.Empty,
                                                ScopeName = ScopeType.LibraryNameSpace.ToScopeName()
                                            };

                                            data.LibraryMembers.Add(nameSpaceItem);
                                        }

                                        memberNameSpace = parseString.Substring(0, nextPeriod);
                                    }
                                    else
                                    {
                                        if (data.LibraryMembers.FirstOrDefault(w => sourceKey.Equals(w) && w.NameSpace == memberNameSpace && w.MemberName == parseString.Substring(0, nextPeriod)) is LibraryMemberItem existing)
                                        { nameSpaceItem = existing; }
                                        else
                                        {
                                            nameSpaceItem = new LibraryMemberItem()
                                            {
                                                LibraryId = sourceItem.LibraryId,
                                                MemberParentId = (nameSpaceItem is LibraryMemberItem) ? nameSpaceItem.MemberId : null,
                                                AssemblyName = sourceItem.AssemblyName,
                                                MemberName = parseString.Substring(0, nextPeriod),
                                                NameSpace = memberNameSpace,
                                                ScopeName = ScopeType.LibraryNameSpace.ToScopeName()
                                            };

                                            data.LibraryMembers.Add(nameSpaceItem);
                                        }

                                        memberNameSpace = String.Format("{0}.{1}", memberNameSpace, parseString.Substring(0, nextPeriod));
                                    }

                                    parseString = parseString.Substring(nextPeriod + 1);
                                }

                                if (!String.IsNullOrWhiteSpace(parseString))
                                {
                                    Int32 parametersStart = parseString.IndexOf("(");
                                    Int32 parametersEnd = parseString.LastIndexOf(")");

                                    if (parametersStart <= 0) // No Parameters
                                    {
                                        LibraryMemberItem memberItem = new LibraryMemberItem()
                                        {
                                            LibraryId = sourceItem.LibraryId,
                                            MemberParentId = (nameSpaceItem is LibraryMemberItem) ? nameSpaceItem.MemberId : null,
                                            AssemblyName = sourceItem.AssemblyName,
                                            MemberName = parseString,
                                            MemberData = memberNode.OuterXml,
                                            NameSpace = memberNameSpace,
                                            ScopeName = ToScopeType(memberType).ToScopeName()
                                        };

                                        LibraryMemberKey memberKey = new LibraryMemberKey(memberItem);

                                        data.LibraryMembers.Add(memberItem);
                                    }
                                    else if (parametersStart > 0 && parametersEnd > 0 && parametersStart < parametersEnd)
                                    {
                                        //TODO: Overloaded methods look identical. Need to specify the parent/child.
                                        //Same issue in the Database.
                                        LibraryMemberItem memberItem = new LibraryMemberItem()
                                        {
                                            LibraryId = sourceItem.LibraryId,
                                            MemberParentId = (nameSpaceItem is LibraryMemberItem) ? nameSpaceItem.MemberId : null,
                                            AssemblyName = sourceItem.AssemblyName,
                                            MemberName = parseString.Substring(0, parametersStart),
                                            MemberData = memberNode.OuterXml,
                                            NameSpace = memberNameSpace,
                                            ScopeName = ToScopeType(memberType).ToScopeName()
                                        };

                                        data.LibraryMembers.Add(memberItem);

                                        String parameters = parseString.Substring(parametersStart + 1, parametersEnd - parametersStart - 1);
                                        Int32 paramterCount = 0;
                                        List<XmlNode>? paramterNodes = memberNode.ChildNodes.Cast<XmlNode>().Where(w => w.Name == "param").ToList();

                                        while (parameters.Length > 0)
                                        {
                                            Int32 nextSeperator = parameters.IndexOf(",");
                                            Int32 nextSubItem = parameters.IndexOf("{");
                                            String paramterType = String.Empty;
                                            String memberName = String.Format("parameter{0:00}", paramterCount); // Default name if none are provided
                                            XElement memberData = new XElement("param"); // Used to construct an XML data fragment.

                                            if (nextSeperator < 0 && nextSubItem < 0)
                                            {
                                                paramterType = parameters;
                                                parameters = String.Empty;
                                            }
                                            else if (nextSeperator < 0 && nextSubItem > 0)
                                            {
                                                paramterType = parameters;
                                                parameters = String.Empty;
                                            }
                                            else if (nextSeperator > 0 && nextSubItem < 0)
                                            {
                                                paramterType = parameters.Substring(0, nextSeperator);
                                                parameters = parameters.Substring(nextSeperator + 1);
                                            }
                                            else if (nextSeperator < nextSubItem)
                                            {
                                                paramterType = parameters.Substring(0, nextSeperator);
                                                parameters = parameters.Substring(nextSeperator + 1);
                                            }
                                            else if (nextSeperator > nextSubItem)
                                            {
                                                paramterType = parameters.Substring(0, parameters.IndexOf("}") + 1);
                                                parameters = parameters.Substring(nextSeperator + 1);
                                            }
                                            // Can only occur if the two values are equal and not zero. This should not be possible.
                                            else { throw new InvalidOperationException("Condition parsing parameters reached a condition that should not be possible."); }

                                            if (paramterNodes is not null
                                                && paramterNodes.Count > paramterCount
                                                && paramterNodes[paramterCount] is XmlNode paramterNode
                                                && paramterNode.Attributes is not null
                                                && paramterNode.Attributes.
                                                    Cast<XmlAttribute>().
                                                    FirstOrDefault(w => w.Name == "name") is XmlAttribute parameterAttribute)
                                            { // There are Parameter Node. A parameter name and description are available
                                                memberName = parameterAttribute.InnerText;

                                                memberData = new XElement("param",
                                                        new XAttribute("name", parameterAttribute.InnerText),
                                                        new XAttribute("type", paramterType),
                                                        paramterNode.InnerText);
                                            }
                                            else
                                            { //No Parameter Node, only the Type can be determined based on NameSpace
                                                memberData = new XElement("param",
                                                    new XAttribute("type", paramterType));
                                            }

                                            LibraryMemberItem parmaterItem = new LibraryMemberItem()
                                            {
                                                LibraryId = sourceItem.LibraryId,
                                                MemberParentId = memberItem.MemberId,
                                                AssemblyName = sourceItem.AssemblyName,
                                                MemberName = memberName,
                                                ScopeName = ScopeType.LibraryParameter.ToScopeName(),
                                                MemberData = memberData.ToString(),
                                                NameSpace = parseString.Substring(0, parametersStart)
                                            };

                                            data.LibraryMembers.Add(parmaterItem);
                                            paramterCount = paramterCount + 1;
                                        }
                                    }
                                }

                                itemDone = itemDone + 1;
                                progress(itemDone, itemCount);
                            }
                        }
                        else { throw new InvalidOperationException("Expected exactly one [members] node."); }
                    }
                }
                catch (Exception ex)
                {
                    ex.Data.Add(nameof(file), file.Name);
                    throw;
                }
            }
        }


        /// <summary>
        /// Creates the work items to Load the Library's from the Applications Database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadLibrary<T>(this T target, IDatabaseWork factory)
            where T : IBindingTable<LibrarySourceItem>, IReadData
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(factory.CreateWork(
                workName: "Load LibrarySources",
                target: target,
                command: (conn) => target.LoadCommand(conn)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Save the Model Library using the model key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveLibrary(this IModelLibrary data, IDatabaseWork factory, IModelKey modelKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            work.Add(factory.CreateWork(
                workName: "Save LibrarySources",
                command: (conn) => data.LibrarySources.SaveCommand(conn, key)));


            work.Add(factory.CreateWork(
                workName: "Save LibraryMembers",
                command: (conn) => data.LibraryMembers.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Save the Model Library using the Library key passed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="libraryKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveLibrary(this IModelLibrary data, IDatabaseWork factory, ILibrarySourceKey libraryKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            LibrarySourceKey key = new LibrarySourceKey(libraryKey);

            work.Add(factory.CreateWork(
                workName: "Save LibrarySources",
                command: (conn) => data.LibrarySources.SaveCommand(conn, key)));

            work.Add(factory.CreateWork(
                workName: "Save LibraryMembers",
                command: (conn) => data.LibraryMembers.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Save the Library's from the Applications Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="libraryKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveLibrary(this IWriteData<ILibrarySourceKey> data, IDatabaseWork factory, ILibrarySourceKey libraryKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            LibrarySourceKey key = new LibrarySourceKey(libraryKey);

            work.Add(factory.CreateWork(
                workName: "Save LibrarySources",
                command: (conn) => data.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Removes all the Library Data from the Model (Clear)
        /// </summary>
        /// <param name="data"></param>
        public static IReadOnlyList<WorkItem> RemoveLibrary(this IModelLibrary data)
        {
            List<WorkItem> work = new List<WorkItem>();

            work.Add(new WorkItem() { WorkName = "Clear LibrarySources", DoWork = data.LibrarySources.Clear });
            work.Add(new WorkItem() { WorkName = "Clear LibraryMembers", DoWork = data.LibraryMembers.Clear });

            return work;
        }

        /// <summary>
        /// Removes all the Library Data for the specific key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="libraryKey"></param>
        public static IReadOnlyList<WorkItem> RemoveLibrary(this IModelLibrary data, ILibrarySourceKey libraryKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            LibrarySourceKey key = new LibrarySourceKey(libraryKey);

            work.Add(new WorkItem() { WorkName = "Remove Libraries", DoWork = () => data.LibrarySources.Remove(key) });
            work.Add(new WorkItem() { WorkName = "Remove Library Members", DoWork = () => data.LibraryMembers.Remove(key) });

            return work;
        }

        /// <summary>
        /// Creates the work items to Delete the Library from the Applications Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="libraryKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> DeleteLibrary(this IModelLibrary data, IDatabaseWork factory, ILibrarySourceKey libraryKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            LibrarySourceKey key = new LibrarySourceKey(libraryKey);

            LibrarySourceCollection empty = new LibrarySourceCollection();

            work.Add(factory.CreateWork(
                workName: "Delete LibrarySources",
                command: (conn) => empty.SaveCommand(conn, key)));

            return work;
        }

        /// <summary>
        /// Creates the work items to Delete the Library from the Applications Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="factory"></param>
        /// <param name="libraryKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> DeleteLibrary(this IWriteData<ILibrarySourceKey> data, IDatabaseWork factory, ILibrarySourceKey libraryKey)
        {
            List<WorkItem> work = new List<WorkItem>();
            LibrarySourceKey key = new LibrarySourceKey(libraryKey);

            LibrarySourceCollection empty = new LibrarySourceCollection();

            work.Add(factory.CreateWork(
                workName: "Delete LibrarySources",
                command: (conn) => empty.SaveCommand(conn, key)));

            return work;
        }
    }
}
