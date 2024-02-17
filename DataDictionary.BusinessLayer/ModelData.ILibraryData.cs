using DataDictionary.BusinessLayer.LibraryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer
{
    partial class ModelData
    {
        ILibraryData LibraryData { get; } = new LibraryData.LibraryData();
    }
}
