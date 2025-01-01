namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Entity Name within the scope of a Subject Area
    /// </summary>
    public interface IEntitySubjectAreaName
    {
        /// <summary>
        /// Entity Name within the Subject Area.
        /// </summary>
        String? EntityName { get; set; }
    }
}