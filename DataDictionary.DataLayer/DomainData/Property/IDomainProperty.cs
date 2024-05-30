namespace DataDictionary.DataLayer.DomainData.Property
{
    /// <summary>
    /// Interface for the Domain Property
    /// </summary>
    public interface IDomainProperty: IDomainPropertyKey
    {
        /// <summary>
        /// Property Value. Type and Format is dependent on PeropertyId.
        /// </summary>
        public String? PropertyValue { get;}
    }
}
