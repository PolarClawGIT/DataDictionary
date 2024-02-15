using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.ApplicationData
{
    /// <summary>
    /// Interface component for the Property data
    /// </summary>
    /// <remarks>Used to hide the DataLayer methods from the Application Layer.</remarks>
    public interface IPropertyData :
        IBindingTable<PropertyItem>,
        ISaveData, ILoadData
    { }

    /// <summary>
    /// Wrapper Class for Application Properties.
    /// </summary>
    public class PropertyData : PropertyCollection, IPropertyData
    {
        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad("Load Properties", this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        public virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave("Save Properties", this).ToList(); }

        /// <inheritdoc/>
        /// <remarks>Property</remarks>
        internal virtual new IReadOnlyList<WorkItem> Load(System.Data.DataSet source)
        { return new WorkItem() { WorkName = "Load Properties", DoWork = () => base.Load(source) }.ToList(); }
    }


}
