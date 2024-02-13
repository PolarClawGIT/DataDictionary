using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.ApplicationData.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.ApplicationData
{
    /// <summary>
    /// Interface component for the Property data
    /// </summary>
    /// <remarks>Overrides: IDatabaseData</remarks>
    public interface IPropertyData
    {
        /// <summary>
        /// List of Properties for the Application (the help system).
        /// </summary>
        PropertyCollection Properties { get; }

        /// <summary>
        /// Casts inherited type into the interface type.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Provides mechanism to map the "this" into the Interface</remarks>
        IPropertyData AsPropertyData { get; }

        /// <inheritdoc cref="DbWorkItem.ILoadData.Load(IDatabaseWork)"/>
        /// <remarks>Property</remarks>
        virtual IReadOnlyList<WorkItem> Load(IDatabaseWork factory)
        { return factory.CreateLoad("Load Properties", this.Properties).ToList(); }

        /// <inheritdoc cref="DbWorkItem.ILoadData.Load(System.Data.DataSet)"/>
        /// <remarks>Property</remarks>
        virtual IReadOnlyList<WorkItem> Load(System.Data.DataSet source)
        { return source.CreateLoad("Load Properties", this.Properties).ToList(); }

        /// <inheritdoc cref="DbWorkItem.ISaveData.Save(IDatabaseWork)"/>
        /// <remarks>Property</remarks>
        virtual IReadOnlyList<WorkItem> Save(IDatabaseWork factory)
        { return factory.CreateSave("Save Properties", this.Properties).ToList(); }
    }

    partial class ApplicationData : IPropertyData
    {
        /// <inheritdoc/>
        public PropertyCollection Properties { get; } = new PropertyCollection();

        /// <inheritdoc/>
        public IPropertyData AsPropertyData { get { return this; } }
    }
}
