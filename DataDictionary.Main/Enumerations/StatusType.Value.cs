using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Enumerations
{
    partial class StatusImage
    {
        static readonly Dictionary<StatusType, Image> statusOverlay
            = new Dictionary<StatusType, Image>()
            {
                {StatusType.Ok,          Resources.StatusOK},
                {StatusType.Error,       Resources.StatusError},
                {StatusType.Information, Resources.StatusInformation},
                {StatusType.Invalid,     Resources.StatusInvalid},
                {StatusType.No,          Resources.StatusNo},
            };
    }
}
