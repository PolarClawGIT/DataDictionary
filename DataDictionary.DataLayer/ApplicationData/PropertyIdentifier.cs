using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IPropertyIdentifier
    {
        Nullable<Guid> PropertyId { get; }
    }

    public class PropertyIdentifier: IPropertyIdentifier
    {
        public Nullable<Guid> PropertyId { get; init; } = Guid.Empty;
    }
}
