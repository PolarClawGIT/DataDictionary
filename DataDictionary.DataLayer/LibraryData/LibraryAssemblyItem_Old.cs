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
    [Obsolete]
    public interface ILibraryAssemblyItem_Old : ILibraryAssemblyKey_old
    {
        String? AssemblyName { get; }
    }

    [Serializable, Obsolete]
    public class LibraryAssemblyItem_Old :BindingTableRow, ILibraryAssemblyItem_Old, ISerializable 
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

        public LibraryAssemblyItem_Old() : base()
        {
            AssemblyId = Guid.NewGuid();
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }
}
