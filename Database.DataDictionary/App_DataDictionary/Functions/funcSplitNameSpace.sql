CREATE FUNCTION [App_DataDictionary].[funcSplitNameSpace](@NameSpace NVarChar(Max))
-- This takes a String that has a Delimiter of a period and creates a hierarchy of values.
-- Each level of the hierarchy is returned as separate row.
-- The example of this is a Database Fully Qualified (aka the four part name) name where the period is the delimiter.
-- This also applies to .Net NameSpaces where the depth is unknown.
-- This has the best performance found so far, even over large set of data.
-- Methods that took a list of NameSpaces performed poorly.
--
-- Columns:
--   [NameSpace]      : Fully Qualified NameSpace formated like SQL Qualified Object Names. Includes the Element Name.
--   [ParentNameSpace]: Fully Qualified NameSpaced formated like SQL Qualified Object Names without the Element Name.
--   [MemberName]     : Element of the NameSpace without formating.
--   [IsBase]         : This row is the NameSpace passed to the function (not a parent row).
--
-- In: [DatabaseName].[SchemaName].[TableName].[ColumnName] or
--     DataDictionary.SchemaName.TableName.ColumnName
-- Out: [DatabaseName]
--      [DatabaseName].[SchemaName]
--      [DatabaseName].[SchemaName].[TableName]
--      [DatabaseName].[SchemaName].[TableName].[ColumnName]
RETURNS TABLE AS RETURN (
	With [Parse] As (
		Select	@NameSpace As [NameSpaceParent],
				Convert(NVarChar(Max),Null) As [NameSpaceChild]
		Union All
		Select	N.[NameSpaceParent],
				P.[NameSpaceParent] As [NameSpaceChild]
		From	[Parse] P
				Outer Apply (
					Select	Case
						-- Empty String
						When Len(P.[NameSpaceParent]) <= 0 Then Null
						-- No more delimiters
						When CharIndex('.', Reverse(P.[NameSpaceParent])) = 0  Then Null
						-- No more brackets
						When CharIndex(']',Reverse(P.[NameSpaceParent])) = 0 And
							CharIndex('[',Reverse(P.[NameSpaceParent])) = 0  
							Then Left(P.[NameSpaceParent], Len(P.[NameSpaceParent]) - CharIndex('.',Reverse(P.[NameSpaceParent])) -0)
						-- Period is after the bracket
						When CharIndex(']',Reverse(P.[NameSpaceParent])) > 0 And
							CharIndex(']',Reverse(P.[NameSpaceParent])) > CharIndex('.', Reverse(P.[NameSpaceParent]))
							Then Left(P.[NameSpaceParent], Len(P.[NameSpaceParent]) - CharIndex('.',Reverse(P.[NameSpaceParent])) -0)
						-- Period is before the bracket
						When CharIndex(']',Reverse(P.[NameSpaceParent])) > 0 And
							CharIndex('[.', Reverse(P.[NameSpaceParent])) > 0
							Then Left(P.[NameSpaceParent], Len(P.[NameSpaceParent]) - CharIndex('[.',Reverse(P.[NameSpaceParent])) -1)
						Else Null
						End As [NameSpaceParent] ) N
		Where	NullIf(P.[NameSpaceParent],'') is Not Null),
	[Format] As (
		Select	[NameSpaceParent],
				[NameSpaceChild],
				Replace(Replace(
					IIF([NameSpaceParent] is Null,
						[NameSpaceChild],
						Right([NameSpaceChild],Len([NameSpaceChild]) - Len([NameSpaceParent]) -1)),
					'[',''),']','')
					As [MemberName]
		From	[Parse]
		Where	[NameSpaceChild] is Not Null),
	[Tree] As (
		Select	[MemberName],
				[NameSpaceParent],
				[NameSpaceChild],
				FormatMessage('[%s]',[MemberName]) As [NameSpace],
				Convert(Int,1) As [Level]
		From	[Format]
		Where	[NameSpaceParent] is Null
		Union All
		Select	F.[MemberName],
				F.[NameSpaceParent],
				F.[NameSpaceChild],
				FormatMessage('%s.[%s]',T.[NameSpace],F.[MemberName]) As [NameSpace],
				T.[Level] + 1 As [Level]
		From	[Tree] T
				Inner Join [Format] F
				On	T.[NameSpaceChild] = F.[NameSpaceParent])
Select	[NameSpace], -- Full name including Member
		IIF(CharIndex('.', Reverse([NameSpace])) = 0,
			Null,
			Left([NameSpace], Len([NameSpace]) - CharIndex('[.',Reverse([NameSpace])) -1))
			As [ParentNameSpace], -- Does not include Member
		[MemberName],
		[Level],
		IIF(Row_Number() Over (Order By [NameSpace] Desc) = 1,1,0) As [IsBase],
		Count(*) Over (Partition by Null) As [TotalElements]
From	[Tree])
GO
/*
Select	*
From	[App_DataDictionary].[funcSplitNameSpace]('[DatabaseName].[SchemaName].[TableName].[ColumnName]')
Order By [NameSpace]

Select	Distinct X.*
From	Sys.Columns C
		outer apply [App_DataDictionary].[funcSplitNameSpace](FormatMessage('[%s].[%s].[%s].[%s]',Db_Name(),Object_Schema_Name(object_id),object_name(object_id),name)) X
Order By [NameSpace]
*/
