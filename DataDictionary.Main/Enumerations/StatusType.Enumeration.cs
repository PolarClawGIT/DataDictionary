using DataDictionary.Main.Properties;

namespace DataDictionary.Main.Enumerations
{
    interface IStatusTypeImages
    {
        /// <summary>
        /// List of Images for the scope Type assocated with Status.
        /// </summary>
        IReadOnlyDictionary<StatusType, Func<Image>> StatusImages { get; }
    }

    static partial class NavigationExtention
    {
        partial class Enumeration: IStatusTypeImages
        {
            static readonly Dictionary<StatusType, Image> statusOverlay = new Dictionary<StatusType, Image>()
            {
                {StatusType.Ok,          Resources.StatusOK},
                {StatusType.Error,       Resources.StatusError},
                {StatusType.Information, Resources.StatusInformation},
                {StatusType.Invalid,     Resources.StatusInvalid},
                {StatusType.No,          Resources.StatusNo},
            };

            public IReadOnlyDictionary<StatusType, Func<Image>> StatusImages
            { get { return statusImages; } }
            Dictionary<StatusType, Func<Image>> statusImages { get; init; } = new Dictionary<StatusType, Func<Image>>();

        }
    }
}
