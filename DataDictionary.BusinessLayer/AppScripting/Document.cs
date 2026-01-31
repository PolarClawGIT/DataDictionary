using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.ToolSet;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface representing Scripting Template data
    /// </summary>
    public interface IDocument :
        ILoadData<IDocumentIndex>, ISaveData<IDocumentIndex>, IDeleteData<IDocumentIndex>,
        ILoadData<AppModel.IModelIndex>, ISaveData<AppModel.IModelIndex>,
        IGetTemporal<AppModel.IModelIndex>, IGetTemporal<IDocumentIndex>,
        IBindListChanged, IDocumentData
    {
        /// <summary>
        /// Creates an empty instance of IDocument
        /// </summary>
        public static IDocument Create()
        { return new Document(); }
    }

    class Document : DocumentData, IDocument
    { }
}
