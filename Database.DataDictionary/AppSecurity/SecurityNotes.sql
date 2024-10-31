-- This Script file is notes and queries used to test and develop the Security objects.
-- This application uses SQL Security Policy to implement Row Level security.
-- Base Documentation: https://learn.microsoft.com/en-us/sql/relational-databases/security/row-level-security?view=sql-server-ver16
--
-- Test Query
Select	IS_SRVROLEMEMBER('sysadmin') As [IsSysAdmin],
		IS_ROLEMEMBER('db_owner') As [IsDbOwner],
		USER_NAME() As [USER_NAME],
		SUSER_NAME () As [SUSER_NAME],
		SUSER_SNAME() As [SUSER_SNAME],
		CURRENT_USER As [CURRENT_USER],
		SESSION_USER As [SESSION_USER],
		SYSTEM_USER As [SYSTEM_USER],
		ORIGINAL_LOGIN() As [ORIGINAL_LOGIN]

-- This is something new for the developer so issues may exist.
--
-- There are several pieces to this.
-- The view(s) ?? is the core security query.
-- The function(s) ?? are used for the Security Policy statement.
--   * an in-line table value function (minimize joins and no recursion)
--   * must have SCHEMABINDING 
--   * best of my knowledge returns one row with a value of "1" for permission allowed (more testing needed)
--   * best of my knowledge return no rows for permission denied (more testing needed)
--   * Filter option requires the user to have Select permissions on the function to execute Select
--   * Block option requires the user to have Select permissions on the function to execute Select/Insert/Update/Delete
--
-- The security Policy enforces the Insert/Update/Delete on a specific row.
-- The statement may be executed directly or threw a stored procedure, the Policy still applies.
--
-- Based on the Test Query, the following observations where made.
-- * Within Execute As:
--   - the USER_NAME .. takes on the value of the specified User.
--   - the ORIGINAL_LOGIN keeps the base Login value
--   - the Role Membership reflect the specified User.
-- * Within scope of an Application Role, the User_Name takes on the name of the Role.
--   - the USER_NAME .. takes on the value of the application role.
--   - the ORIGINAL_LOGIN keeps the base Login value
--   - the Role Membership reflect the application role.
--   - IS_ROLEMEMBER return 0 except for the application role itself
--
-- The goal is:
-- * All users used Windows Authenticated (other options are possible but not explored)
-- * The application uses Application Role.
-- * Only the application role can execute the application procedures.
-- * Direct (by a user) insert/update/delete will be blocked (db_owner gets an exception)
-- * Security will look to insure the command is executed by the Application Role or db_owner (hard-coded?).
-- * If executed with application role, the ORIGINAL_LOGIN is used to check application row level security.
-- * db_owner can by-pass the security logic and directly insert/update/delete as well as execute of procedures
-- * Special logic in the procedures are not needed.
--
-- The concept is that only the application can activate the Application Role.
-- To activate the role:
-- * the Application Role name and password (possibly passed in clear text of the network)
-- * the end-user must be a valid login/user for the database
-- * the end-user a member of the "public" fixed database role (most users are)
--
-- This is not easy to to test.