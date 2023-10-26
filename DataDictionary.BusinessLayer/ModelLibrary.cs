using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{
    /// <summary>
    /// Interface component for the Model Library
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public interface IModelLibrary
    {
        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        LibrarySourceCollection LibrarySources { get; }

        /// <summary>
        /// List of Domain Properties for the Entities within the Model.
        /// </summary>
        LibraryMemberCollection LibraryMembers { get; }
    }

    /// <summary>
    /// Implementation component for the Model Library
    /// </summary>
    /// <remarks>When combined with the Extension class, this implements multi-inheritance.</remarks>
    public static class ModelLibrary
    {
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

            workItems.Add(new WorkItem()
            {
                WorkName = "Import Library",
                DoWork = ImportLibrary
            });

            return workItems;

            void ImportLibrary()
            {
                try
                { // Parse the XML file and load to data objects
                    XmlDocument xmlData = new XmlDocument();
                    xmlData.Load(file.FullName);

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
                                SourceDate = file.LastWriteTime
                            };

                            LibrarySourceKeyUnique newKey = new LibrarySourceKeyUnique(sourceItem);

                            if (data.LibrarySources.FirstOrDefault(w => newKey.Equals(w)) is LibrarySourceItem alreadyExists)
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

                                while (
                                    parseString.IndexOf(".") is int nextPeriod
                                    && parseString.IndexOf("#") is int nextHash
                                    && parseString.IndexOf("(") is int nextPern
                                    && nextPeriod >= 0
                                    && (nextHash < 0 || nextPeriod < nextHash)
                                    && (nextPern < 0 || nextPeriod < nextPern))
                                {
                                    if (String.IsNullOrWhiteSpace(memberNameSpace))
                                    { memberNameSpace = parseString.Substring(0, nextPeriod); }
                                    else
                                    { memberNameSpace = String.Format("{0}.{1}", memberNameSpace, parseString.Substring(0, nextPeriod)); }

                                    parseString = parseString.Substring(nextPeriod + 1);
                                }

                                // What is left must be the Member Name?
                                //String memberName = parseString;

                                if (!String.IsNullOrWhiteSpace(parseString))
                                {
                                    Int32 parametersStart = parseString.IndexOf("(");
                                    Int32 parametersEnd = parseString.LastIndexOf(")");

                                    if (parametersStart <= 0) // No Parameters
                                    {
                                        LibraryMemberItem memberItem = new LibraryMemberItem()
                                        {
                                            LibraryId = sourceItem.LibraryId,
                                            AssemblyName = sourceItem.AssemblyName,
                                            MemberName = parseString,
                                            MemberData = memberNode.InnerXml,
                                            MemberNameSpace = memberNameSpace,
                                            MemberType = memberType
                                        };

                                        LibraryMemberKey memberKey = new LibraryMemberKey(memberItem);

                                        data.LibraryMembers.Add(memberItem);
                                    }
                                    else if (parametersStart > 0 && parametersEnd > 0 && parametersStart < parametersEnd)
                                    {
                                        List<String> parameterList = parseString.Substring(parametersStart + 1, parametersEnd - parametersStart - 1).Split(",").ToList();

                                        LibraryMemberItem memberItem = new LibraryMemberItem()
                                        {
                                            LibraryId = sourceItem.LibraryId,
                                            AssemblyName = sourceItem.AssemblyName,
                                            MemberName = parseString.Substring(0, parametersStart),
                                            MemberData = memberNode.InnerXml,
                                            MemberNameSpace = memberNameSpace,
                                            MemberType = memberType
                                        };

                                        data.LibraryMembers.Add(memberItem);

                                        foreach (String paramter in parameterList)
                                        {
                                            LibraryMemberItem parmaterItem = new LibraryMemberItem()
                                            {
                                                LibraryId = sourceItem.LibraryId,
                                                AssemblyName = sourceItem.AssemblyName,
                                                MemberName = paramter,
                                                //MemberData = memberNode.InnerXml,
                                                MemberNameSpace = String.Format("{0}.{1}",memberNameSpace , parseString.Substring(0, parametersStart)),
                                                //MemberType = memberType
                                            };

                                            data.LibraryMembers.Add(parmaterItem);
                                        }

                                    }
                                }
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
