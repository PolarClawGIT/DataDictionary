using DataDictionary.BusinessLayer.Modification;
using DataDictionary.DataLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IHelpSubjectValue : IHelpItem,
        IHelpSubjectIndex, IHelpSubjectIndexNameSpace
    { }

    /// <inheritdoc/>
    public class HelpSubjectValue : HelpItem, IHelpSubjectValue,
        IModificationValue
    {
        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new HelpSubjectIndex(this); }

        /// <inheritdoc/>
        public String GetTitle()
        { return this.HelpSubject ?? String.Empty; }

        /// <inheritdoc/>
        public String? GetDescription()
        { return this.HelpToolTip; }

        /// <inheritdoc/>
        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return (eventArgs.PropertyName is nameof(HelpSubject)); }
    }
}
