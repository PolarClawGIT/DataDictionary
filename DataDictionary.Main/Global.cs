using DataDictionary.BusinessLayer;
using DataDictionary.Main.Properties;
using System.Security.Principal;
using Toolbox.Mediator;
using Toolbox.Threading;

namespace DataDictionary.Main
{
    /// <summary>
    /// Global Scope items
    /// </summary>
    /// <remarks>
    /// So they do not get put into the Program class.
    /// Setup in the Global Using part of the Project Definition.
    /// Remember to keep this list as small as possible.
    /// </remarks>
    internal static class Global
    {
        /// <summary>
        /// Worker Queue for Background work. Singleton access point.
        /// </summary>
        public static WorkerQueue Worker { get; } = new WorkerQueue();

        /// <summary>
        /// Mediator to allow messages to be sent between related forms. Messenger
        /// </summary>
        public static Mediator Messenger { get; } = new Mediator();

        /// <summary>
        /// Main Data used by the Application
        /// </summary>
        public static BusinessLayerData BusinessData { get; } =
            new BusinessLayerData(
                identity: WindowsIdentity.GetCurrent(),
                serverName: Settings.Default.AppServer,
                databaseName: Settings.Default.AppDatabase,
                applicationRole: Settings.Default.AppDbRole,
                ApplicationRolePassword: Settings.Default.AppDbRolePassword);
    }
}
