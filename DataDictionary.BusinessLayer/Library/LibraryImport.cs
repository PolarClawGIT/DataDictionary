using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.LibraryData.Member;
using DataDictionary.DataLayer.LibraryData.Source;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Library
{
    /// <summary>
    ///  Reads a Visual Studio XML Document File and returns the data
    /// </summary>
    class LibraryImport
    {
        // TODO: The Parser is not working as expected when building NameSpaces.
        // I Think this needs to be re-written.

        public Action<Int32, Int32> Progress { get; set; } = (x, y) => { };

        public IList<LibrarySourceValue> Sources { get { return librarySources; } }
        List<LibrarySourceValue> librarySources = new List<LibrarySourceValue>();

        public IList<LibraryMemberValue> Members { get { return libraryMembers; } }
        List<LibraryMemberValue> libraryMembers = new List<LibraryMemberValue>();

        public void Import(FileInfo file)
        {
            try
            { // Parse the XML file and load to data objects
                XmlDocument xmlData = new XmlDocument();
                xmlData.Load(file.FullName);
                LibrarySourceKeyName sourceKey;

                if (xmlData.DocumentElement is XmlElement root)
                {
                    //String assemblyName = String.Empty; // Surrogate key for Library
                    LibrarySourceValue sourceItem;

                    // Parse the Assembly Node
                    if (root.ChildNodes.Cast<XmlNode>().FirstOrDefault(w => w.Name == "assembly") is XmlNode assemblyNode)
                    {
                        sourceItem = new LibrarySourceValue()
                        {
                            LibraryTitle = assemblyNode.InnerText,
                            AssemblyName = assemblyNode.InnerText,
                            SourceFile = file.Name,
                            SourceDate = file.LastWriteTime,
                        };

                        sourceKey = new LibrarySourceKeyName(sourceItem);

                        if (librarySources.FirstOrDefault(w => sourceKey.Equals(w)) is LibrarySourceValue alreadyExists)
                        {
                            sourceItem.SourceFile = file.Name;
                            sourceItem.SourceDate = file.LastWriteTime;
                        }
                        else { librarySources.Add(sourceItem); }
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
                            LibraryMemberType memberType = LibraryMemberType.Null;
                            //String memberType = String.Empty;
                            if (parseString.Split(':') is String[] types && types.Length > 0)
                            {
                                memberType = LibraryMemberTypeKey.Parse(types[0]);
                                parseString = parseString.Substring(2);
                            }

                            String memberNameSpace = String.Empty;
                            LibraryMemberValue? nameSpaceItem = null;

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
                                    if (libraryMembers.FirstOrDefault(w => sourceKey.Equals(w) && String.IsNullOrWhiteSpace(w.MemberNameSpace) && w.MemberName == parseString.Substring(0, nextPeriod)) is LibraryMemberValue existing)
                                    { nameSpaceItem = existing; }
                                    else
                                    {
                                        nameSpaceItem = new LibraryMemberValue()
                                        {
                                            LibraryId = sourceItem.LibraryId,
                                            AssemblyName = sourceItem.AssemblyName,
                                            MemberName = parseString.Substring(0, nextPeriod),
                                            MemberType = LibraryMemberType.NameSpace,
                                            MemberNameSpace = String.Empty,
                                        };

                                        libraryMembers.Add(nameSpaceItem);
                                    }

                                    memberNameSpace = parseString.Substring(0, nextPeriod);
                                }
                                else
                                {
                                    if (libraryMembers.FirstOrDefault(w => sourceKey.Equals(w) && w.MemberNameSpace == memberNameSpace && w.MemberName == parseString.Substring(0, nextPeriod)) is LibraryMemberValue existing)
                                    { nameSpaceItem = existing; }
                                    else
                                    {
                                        nameSpaceItem = new LibraryMemberValue()
                                        {
                                            LibraryId = sourceItem.LibraryId,
                                            MemberParentId = (nameSpaceItem is LibraryMemberValue) ? nameSpaceItem.MemberId : null,
                                            AssemblyName = sourceItem.AssemblyName,
                                            MemberName = parseString.Substring(0, nextPeriod),
                                            MemberType = LibraryMemberType.NameSpace,
                                            MemberNameSpace = memberNameSpace,
                                        };

                                        libraryMembers.Add(nameSpaceItem);
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
                                    LibraryMemberValue memberItem = new LibraryMemberValue()
                                    {
                                        LibraryId = sourceItem.LibraryId,
                                        MemberParentId = (nameSpaceItem is LibraryMemberValue) ? nameSpaceItem.MemberId : null,
                                        AssemblyName = sourceItem.AssemblyName,
                                        MemberName = parseString,
                                        MemberType = memberType,
                                        MemberData = memberNode.OuterXml,
                                        MemberNameSpace = memberNameSpace,
                                    };

                                    LibraryMemberIndex memberKey = new LibraryMemberIndex(memberItem);

                                    libraryMembers.Add(memberItem);
                                }
                                else if (parametersStart > 0 && parametersEnd > 0 && parametersStart < parametersEnd)
                                {
                                    //TODO: Overloaded methods look identical. Need to specify the parent/child.
                                    //Same issue in the Database.
                                    LibraryMemberValue memberItem = new LibraryMemberValue()
                                    {
                                        LibraryId = sourceItem.LibraryId,
                                        MemberParentId = (nameSpaceItem is LibraryMemberValue) ? nameSpaceItem.MemberId : null,
                                        AssemblyName = sourceItem.AssemblyName,
                                        MemberName = parseString.Substring(0, parametersStart),
                                        MemberType = memberType,
                                        MemberData = memberNode.OuterXml,
                                        MemberNameSpace = memberNameSpace,
                                    };

                                    libraryMembers.Add(memberItem);

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

                                        LibraryMemberValue parmaterItem = new LibraryMemberValue()
                                        {
                                            LibraryId = sourceItem.LibraryId,
                                            MemberParentId = memberItem.MemberId,
                                            AssemblyName = sourceItem.AssemblyName,
                                            MemberName = memberName,
                                            MemberType = LibraryMemberType.Parameter,
                                            MemberData = memberData.ToString(),
                                            MemberNameSpace = String.Format("{0}.{1}", memberItem.MemberNameSpace, memberItem.MemberName)
                                        };

                                        libraryMembers.Add(parmaterItem);
                                        paramterCount = paramterCount + 1;
                                    }
                                }
                            }

                            itemDone = itemDone + 1;
                            Progress(itemDone, itemCount);
                        }
                    }
                    else { throw new InvalidOperationException("Expected exactly one [members] node."); }
                }
            }
            catch (Exception)
            { throw; }
        }

    }
}
