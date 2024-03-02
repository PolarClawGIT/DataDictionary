using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.DataLayer.DomainData.Alias;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.DataLayer.DomainData.Entity;
using DataDictionary.DataLayer.ModelData;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer
{

    /// <summary>
    /// Interface component for the Model Domain (Attribute, Entity and Subject Area)
    /// </summary>
    /// <remarks>When combined with the Extension class, this approximates multi-inheritance.</remarks>
    public interface IModelDomain : IModelAttribute, IModelEntity
    { }
}
