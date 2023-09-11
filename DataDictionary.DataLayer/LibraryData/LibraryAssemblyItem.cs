using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.LibraryData
{
    public interface ILibraryAssemblyItem : ILibraryAssemblyKey
    {
        String? AssemblyName { get; }
    }

    [Serializable]
    public class LibraryAssemblyItem :BindingTableRow, ILibraryAssemblyItem, ISerializable 
    {
        /// <inheritdoc/>
        public Nullable<Guid> AssemblyId
        { get { return GetValue<Guid>("AssemblyId"); } private set { SetValue<Guid>("AssemblyId", value); } }

        /// <inheritdoc/>
        public String? AssemblyName { get { return GetValue("AssemblyName"); } set { SetValue("AssemblyName", value); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AssemblyId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("AssemblyName", typeof(String)){ AllowDBNull = true},
        };

        public LibraryAssemblyItem() : base()
        {
            AssemblyId = Guid.NewGuid();
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }
}
