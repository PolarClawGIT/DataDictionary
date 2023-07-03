using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainPropertyItem
    {
        String? PropertyName { get; }
        String? ExtendedPropertyName { get; }

    }

    public class DomainPropertyItem : IDomainPropertyItem
    {
        public string? PropertyName => throw new NotImplementedException();

        public string? ExtendedPropertyName => throw new NotImplementedException();
    }
}
