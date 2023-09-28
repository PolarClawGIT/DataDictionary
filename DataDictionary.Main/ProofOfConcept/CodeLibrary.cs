using DataDictionary.DataLayer.LibraryData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DataDictionary.Main.Forms.Library
{
    /// <summary>
    /// Imports Visual Studio Documentation files.
    /// </summary>
    /// <remarks>
    /// Proof of Concept to read the XML Documentation files from Visual Studio.
    /// This would be another data source for the Model.
    /// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/
    /// This is currently limited to what is produced by the XML Documentation feature.
    /// As such inheritdoc tags are not expanded unless the developer specifically adds a cref.
    /// Example: inheritdoc cref="IDbCatalogKey.CatalogId"
    /// </remarks>
    partial class CodeLibrary : ApplicationBase
    {
        public CodeLibrary() : base()
        {
            InitializeComponent();
            openToolStripButton.Enabled = true;
            openToolStripButton.Click += OpenToolStripButton_Click;
        }

        private void OpenToolStripButton_Click(object? sender, EventArgs e)
        {
            openFileDialog.Filter = "XML VS Documentation|*.XML";
            openFileDialog.Multiselect = true;

            DirectoryInfo initDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            if (initDir.Exists && initDir.GetDirectories("source").FirstOrDefault() is DirectoryInfo sourceFolder)
            {
                initDir = sourceFolder;

                if (sourceFolder.GetDirectories("repos").FirstOrDefault() is DirectoryInfo reposFolder)
                { initDir = reposFolder; }
            }

            openFileDialog.InitialDirectory = initDir.FullName;

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult is DialogResult.OK)
            {
                try
                {
                    foreach (String files in openFileDialog.FileNames)
                    {
                        XmlDocument data = new XmlDocument();
                        data.Load(files);

                        if (data.DocumentElement is XmlElement root)
                        {
                            foreach (XmlNode node in root.ChildNodes)
                            {
                                LibraryAssemblyKey_Old? assemblyKey = null;

                                if (node.Name == "assembly")
                                {
                                    LibraryAssemblyItem_Old assemblyItem = new LibraryAssemblyItem_Old()
                                    { AssemblyName = node.InnerText };

                                    Program.Data.LibraryAssemblies_Old.Add(assemblyItem);

                                    assemblyKey = new LibraryAssemblyKey_Old(assemblyItem);
                                }
                                else if (node.Name == "members")
                                {
                                    foreach (XmlNode memberNode in node.ChildNodes)
                                    {
                                        if (memberNode.Name == "member" && memberNode.Attributes is not null)
                                        {
                                            StringBuilder memberName = new StringBuilder();
                                            StringBuilder memberSummary = new StringBuilder();

                                            foreach (XmlAttribute memberAttribute in memberNode.Attributes)
                                            {
                                                if (memberAttribute.Name == "name")
                                                { memberName.AppendLine(memberAttribute.InnerText.Trim()); }
                                            }

                                            foreach (XmlNode detailNode in memberNode.ChildNodes)
                                            {
                                                if (detailNode.Name == "summary")
                                                { memberSummary.AppendLine(detailNode.InnerText.Trim()); }
                                            }

                                            LibraryMemberItem_Old memberItem = new LibraryMemberItem_Old()
                                            { MemberName = memberName.ToString(), MemberSummary = memberSummary.ToString() };

                                            if(assemblyKey is not null) { memberItem.AssemblyId = assemblyKey.AssemblyId; }

                                            Program.Data.LibraryMembers_Old.Add(memberItem);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    libraryMemberData.DataSource = null;
                    libraryMemberData.DataSource = Program.Data.LibraryMembers_Old;
                }
                catch (Exception ex)
                { Program.ShowException(ex); }
            }
        }
    }
}
