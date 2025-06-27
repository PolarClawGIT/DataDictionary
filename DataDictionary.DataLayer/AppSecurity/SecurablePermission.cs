namespace DataDictionary.DataLayer.AppSecurity
{
    static class SecurablePermission
    {
        public const String GetProcedure = "[AppSecurity].[procGetSecurablePermission]";
        public const String SetProcedure = "[AppSecurity].[procSetSecurablePermission]";
        public const String TableType = "[AppSecurity].[udttSecurablePermission]";
    }
}