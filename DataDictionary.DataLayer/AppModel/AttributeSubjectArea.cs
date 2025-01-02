namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Attribute Name within the scope of a Subject Area
    /// </summary>
    public interface IAttributeSubjectAreaName
    {
        /// <summary>
        /// Attribute Name within the Subject Area.
        /// </summary>
        String? AttributeName { get; set; }
    }
}