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

namespace DataDictionary.BusinessLayer.WorkFlows
{
    /// <summary>
    /// Extension for handling working with Library work items
    /// </summary>
    public static class LibraryExtension
    {
        /// <summary>
        /// Creates work items that Loads all the Library Sources from the Database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadLibrary<T>(this T target)
            where T : IBindingTable<LibrarySourceItem>, IReadData
        {
            List<WorkItem> workItems = new List<WorkItem>();

            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Library Sources",
                Command = target.LoadCommand,
                Target = target
            });

            return workItems;
        }

        /// <summary>
        /// Creates work items that Load a Library by Model.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadLibrary(this IModelLibrary data, IModelKey modelKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Library Sources",
                Command = (conn) => data.LibrarySources.LoadCommand(conn, key),
                Target = data.LibrarySources
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Library Members",
                Command = (conn) => data.LibraryMembers.LoadCommand(conn, key),
                Target = data.LibraryMembers
            });

            return workItems;
        }

        /// <summary>
        /// Creates work items that Loads a Library by Library Key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="libraryKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> LoadLibrary(this IModelLibrary data, ILibrarySourceKey libraryKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            LibrarySourceKey key = new LibrarySourceKey(libraryKey);

            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);
            
            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Library Sources",
                Command = (conn) => data.LibrarySources.LoadCommand(conn, key),
                Target = data.LibrarySources
            });

            workItems.Add(new ExecuteReader(openConnection)
            {
                WorkName = "Load Library Members",
                Command = (conn) => data.LibraryMembers.LoadCommand(conn, key),
                Target = data.LibraryMembers
            });

            return workItems;
        }

        /// <summary>
        /// Creates work items that Loads a Library from a File.
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
                                String memberName = parseString;

                                if (!String.IsNullOrWhiteSpace(memberName))
                                {
                                    //TODO: Need to come up with a way to clean up the XML data. Remove extra returns and spacing.

                                    LibraryMemberItem memberItem = new LibraryMemberItem()
                                    {
                                        LibraryId = sourceItem.LibraryId,
                                        AssemblyName = sourceItem.AssemblyName,
                                        MemberName = memberName,
                                        MemberData = memberNode.InnerXml,
                                        MemberNameSpace = memberNameSpace,
                                        MemberType = memberType
                                    };

                                    LibraryMemberKey memberKey = new LibraryMemberKey(memberItem);

                                    if (data.LibraryMembers.FirstOrDefault(w => memberKey.Equals(w)) is LibraryMemberItem existingMember)
                                    {
                                        existingMember.MemberData = memberNode.InnerXml;
                                    }
                                    else { data.LibraryMembers.Add(memberItem); }
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
        /// Creates work items that Saves the Library for the Model to the database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modelKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveLibrary(this IModelLibrary data, IModelKey modelKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            ModelKey key = new ModelKey(modelKey);

            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Library sources",
                Command = (conn) => data.LibrarySources.SaveCommand(conn, key)
            });

            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Save Library members",
                Command = (conn) => data.LibraryMembers.SaveCommand(conn, key)
            });

            return workItems;
        }

        /// <summary>
        /// Creates work items that Saves the Library to the Database by Library Key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="libraryKey"></param>
        /// <returns></returns>
        public static IReadOnlyList<WorkItem> SaveLibrary(this IModelLibrary data, ILibrarySourceKey libraryKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            LibrarySourceKey key = new LibrarySourceKey(libraryKey);

            if (data.LibrarySources.Count(w => key.Equals(w)) != 0)
            {
                LibrarySourceCollection source = new LibrarySourceCollection();
                LibraryMemberCollection members = new LibraryMemberCollection();
                source.AddRange(data.LibrarySources.Where(w => key.Equals(w)));
                members.AddRange(data.LibraryMembers.Where(w => key.Equals(w)));

                DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(ModelData.ModelContext);
                workItems.Add(openConnection);

                workItems.Add(new ExecuteNonQuery(openConnection)
                {
                    WorkName = "Save Library sources",
                    Command = (conn) => source.SaveCommand(conn, key)
                });

                workItems.Add(new ExecuteNonQuery(openConnection)
                {
                    WorkName = "Save Library members",
                    Command = (conn) => members.SaveCommand(conn, key)
                });
            }

            return workItems;
        }

        /// <summary>
        /// Creates work items that Deletes the Library from the Database.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="libraryKey"></param>
        /// <returns></returns>
        /// <remarks>If the Library belongs to any Model, the delete will fail.</remarks>
        public static IReadOnlyList<WorkItem> DeleteLibrary(this IModelLibrary data, ILibrarySourceKey libraryKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            LibrarySourceKey key = new LibrarySourceKey(libraryKey);

            DbWorkItem.OpenConnection openConnection = new DbWorkItem.OpenConnection(ModelData.ModelContext);
            workItems.Add(openConnection);

            LibrarySourceCollection empty = new LibrarySourceCollection();

            // The Save performs a delta. As the collection is empty, anything matching that Library Key is deleted.
            workItems.Add(new ExecuteNonQuery(openConnection)
            {
                WorkName = "Delete Library (Cascade)",
                Command = (conn) => empty.SaveCommand(conn, key)
            });

            return workItems;
        }

        /// <summary>
        /// Removes all the Library Data
        /// </summary>
        /// <param name="data"></param>
        public static IReadOnlyList<WorkItem> ClearLibrary(this IModelLibrary data)
        {
            List<WorkItem> workItems = new List<WorkItem>();

            workItems.Add(new WorkItem() { WorkName = "Clear Libraries", DoWork = data.LibrarySources.Clear });
            workItems.Add(new WorkItem() { WorkName = "Clear Library Members", DoWork = data.LibraryMembers.Clear });

            return workItems;
        }

        /// <summary>
        /// Removes all the Library Data for the specific key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="libraryKey"></param>
        public static IReadOnlyList<WorkItem> RemoveLibrary(this IModelLibrary data, ILibrarySourceKey libraryKey)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            LibrarySourceKey key = new LibrarySourceKey(libraryKey);

            workItems.Add(new WorkItem() { WorkName = "Remove Libraries", DoWork = () => data.LibrarySources.Remove(key) });
            workItems.Add(new WorkItem() { WorkName = "Remove Library Members", DoWork = () => data.LibraryMembers.Remove(key) });

            return workItems;
        }

    }
}
