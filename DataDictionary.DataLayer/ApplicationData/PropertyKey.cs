using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IPropertyKey
    {
        Nullable<Guid> PropertyId { get; }
    }

    public class PropertyKey: IPropertyKey
    {
        public Nullable<Guid> PropertyId { get; init; } = Guid.Empty;

        public PropertyKey(IPropertyKey source) : base()
        {
            if (source.PropertyId is Guid) { PropertyId = source.PropertyId; }
            else { PropertyId = Guid.Empty; }
        }
    }
}
