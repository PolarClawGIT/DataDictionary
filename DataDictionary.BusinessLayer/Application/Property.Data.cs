using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Property;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Application
{
    /// <summary>
    /// Interface component for the Property data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    [Obsolete("Replace Domain Property", true)]
    public interface IPropertyData :
        IBindingData<PropertyValue>,
        ISaveData, ILoadData
    { }

    /// <summary>
    /// Wrapper Class for Application Properties.
    /// </summary>
    [Obsolete("Replace Domain Property", true)]
    class PropertyData : PropertyCollection<PropertyValue>, IPropertyData
    {
        public IReadOnlyList<WorkItem> Delete()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad(this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave(this).ToList(); }
    }
}
