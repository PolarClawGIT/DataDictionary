using Microsoft.Data.SqlClient;

namespace Toolbox.DbContext
{
    public interface IContext
    {
        String ServerName { get; }
        String DatabaseName { get; }
        IConnection CreateConnection();
        Boolean ValidateCommand { get; set; }
    }

    /// <summary>
    /// Class containing the data needed to create a Database Connection.
    /// </summary>
    /// <remarks>This wrappers a Connection String.</remarks>
    public class Context : IContext
    {
        internal SqlConnectionStringBuilder ConnectionBuilder { get; set; } = new SqlConnectionStringBuilder()
        { ApplicationName = GetApplicationName(), TrustServerCertificate = true, IntegratedSecurity = true };

        /// <summary>
        /// Used to set the Application Name of the Connection String.
        /// </summary>
        /// <returns></returns>
        private static String GetApplicationName()
        {
            System.Diagnostics.FileVersionInfo value =
                System.Diagnostics.FileVersionInfo.GetVersionInfo(
                    (System.Reflection.Assembly.GetEntryAssembly() ??
                    System.Reflection.Assembly.GetExecutingAssembly()).
                    Location);
            return String.Format("{0}: {1}", value.ProductName, value.FileVersion);
        }

        /// <summary>
        /// Name of the Database Server of the Connection
        /// </summary>
        public String ServerName { get { return ConnectionBuilder.DataSource; } init { ConnectionBuilder.DataSource = value; } }

        /// <summary>
        /// Name of the Database of the Connection
        /// </summary>
        public String DatabaseName
        {
            get { return ConnectionBuilder.InitialCatalog; }

            init
            {
                if (String.IsNullOrWhiteSpace(value)) { ConnectionBuilder.InitialCatalog = "master"; }
                else { ConnectionBuilder.InitialCatalog = value; }
            }
        }

        public Boolean IntegratedSecurity { get { return ConnectionBuilder.IntegratedSecurity; } set { ConnectionBuilder.IntegratedSecurity = value; } }
        public Boolean TrustServerCertificate { get { return ConnectionBuilder.TrustServerCertificate; } set { ConnectionBuilder.TrustServerCertificate = value; } }

        public String ServerUserName { get { return ConnectionBuilder.UserID; } init { ConnectionBuilder.UserID = value; } }
        public String ServerUserPassword { get { return ConnectionBuilder.Password; } init { ConnectionBuilder.Password = value; } }

        public String? ApplicationRole { get; init; }
        public String? ApplicationRolePassword { get; init; }

        /// <summary>
        /// Attempt to validate the command before executing it. Any issues are thrown as exceptions.
        /// </summary>
        public Boolean ValidateCommand { get; set; } = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Context() { }

        public IConnection CreateConnection()
        {
            if (ConnectionBuilder == null || String.IsNullOrWhiteSpace(ConnectionBuilder.ConnectionString))
            { throw new ArgumentException("ConnectionString has not been defined"); }

            return new Connection() { DbContext = this };
        }

        public override string ToString()
        { return ConnectionBuilder.ConnectionString; }
    }
}
