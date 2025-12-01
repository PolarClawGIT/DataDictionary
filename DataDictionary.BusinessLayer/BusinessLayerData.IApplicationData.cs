using DataDictionary.BusinessLayer.AppGeneral;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
	{
		/// <summary>
		/// Wrapper for Application Data
		/// </summary>
		public IApplicationData ApplicationData { get { return applicationValues; } }
        private readonly ApplicationData applicationValues;
    }
}
