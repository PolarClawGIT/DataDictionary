namespace DataDictionary.DataLayer.AppSecurity
{
    static class Principal
    {
        public const String GetProcedure = "[AppSecurity].[procGetPrincipal]";
        public const String IsCurrent = "@IsCurrent";
        public const String PrincipalId = "@PrincipalId";
        public const String PrincipalLogin = "@PrincipalLogin";
        public const String SetProcedure = "[AppSecurity].[procSetPrincipal]";
        public const String TableType = "[AppSecurity].[udttPrincipal]";
    }
}
