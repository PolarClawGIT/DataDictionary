using DataDictionary.BusinessLayer.Modification;
using DataDictionary.DataLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IHelpSubjectValue : IHelpItem
    { }

    /// <inheritdoc/>
    public class HelpSubjectValue : HelpItem, IHelpItem,
        IHelpSubjectIndex, IHelpSubjectIndexNameSpace,
        IModificationValue
    {
        /// <inheritdoc/>
        public String GetDescription()
        { return this.HelpToolTip ?? String.Empty; }

        /// <inheritdoc/>
        public String GetTitle()
        { return this.HelpSubject ?? String.Empty; }
    }
}
