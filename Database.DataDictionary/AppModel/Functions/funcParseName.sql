CREATE FUNCTION [AppModel].[funcParseName](@QualifiedName NVarChar(Max))
-- This takes a String that has a Delimiter of a period and creates a hierarchy of values.
-- Each level of the hierarchy is returned as separate row.
-- The example of this is a Database Fully Qualified (aka the four part name) name where the period is the delimiter.
-- This also applies to .Net NameSpaces where the depth is unknown.
--
-- Columns:
--   [QualifiedName]  : Fully Qualified QualifiedName formated like SQL Qualified Object Names. Includes the Element Name.
--   [ParentName]     : Fully Qualified QualifiedNamed formated like SQL Qualified Object Names without the Element Name.
--   [MemberName]     : Element of the QualifiedName without formating.
--   [IsBase]         : This row is the QualifiedName passed to the function (not a parent row).
--
-- In: [DatabaseName].[SchemaName].[TableName].[ColumnName] or
--     DataDictionary.SchemaName.TableName.ColumnName
-- Out: [DatabaseName]
--      [DatabaseName].[SchemaName]
--      [DatabaseName].[SchemaName].[TableName]
--      [DatabaseName].[SchemaName].[TableName].[ColumnName]
RETURNS TABLE AS RETURN (
	With [Parse] As (
		Select	@QualifiedName As [ParentName],
				Convert(NVarChar(Max),Null) As [ChildName]
		Union All
		Select	N.[ParentName],
				P.[ParentName] As [ChildName]
		From	[Parse] P
				Outer Apply (
					Select	Case
						-- Empty String
						When Len(P.[ParentName]) <= 0 Then Null
						-- No more delimiters
						When CharIndex('.', Reverse(P.[ParentName])) = 0  Then Null
						-- No more brackets
						When CharIndex(']',Reverse(P.[ParentName])) = 0 And
							CharIndex('[',Reverse(P.[ParentName])) = 0  
							Then Left(P.[ParentName], Len(P.[ParentName]) - CharIndex('.',Reverse(P.[ParentName])) -0)
						-- Period is after the bracket
						When CharIndex(']',Reverse(P.[ParentName])) > 0 And
							CharIndex(']',Reverse(P.[ParentName])) > CharIndex('.', Reverse(P.[ParentName]))
							Then Left(P.[ParentName], Len(P.[ParentName]) - CharIndex('.',Reverse(P.[ParentName])) -0)
						-- Period is before the bracket
						When CharIndex(']',Reverse(P.[ParentName])) > 0 And
							CharIndex('[.', Reverse(P.[ParentName])) > 0
							Then Left(P.[ParentName], Len(P.[ParentName]) - CharIndex('[.',Reverse(P.[ParentName])) -1)
						Else Null
						End As [ParentName] ) N
		Where	NullIf(P.[ParentName],'') is Not Null),
	[Format] As (
		Select	[ParentName],
				[ChildName],
				Replace(Replace(
					IIF([ParentName] is Null,
						[ChildName],
						Right([ChildName],Len([ChildName]) - Len([ParentName]) -1)),
					'[',''),']','')
					As [MemberName]
		From	[Parse]
		Where	[ChildName] is Not Null),
	[Tree] As (
		Select	[MemberName],
				[ParentName],
				[ChildName],
				FormatMessage('[%s]',[MemberName]) As [QualifiedName],
				Convert(Int,1) As [Level]
		From	[Format]
		Where	[ParentName] is Null
		Union All
		Select	F.[MemberName],
				F.[ParentName],
				F.[ChildName],
				FormatMessage('%s.[%s]',T.[QualifiedName],F.[MemberName]) As [QualifiedName],
				T.[Level] + 1 As [Level]
		From	[Tree] T
				Inner Join [Format] F
				On	T.[ChildName] = F.[ParentName])
Select	[QualifiedName], -- Full name including Member
		IIF(CharIndex('.', Reverse([QualifiedName])) = 0,
			Null,
			Left([QualifiedName], Len([QualifiedName]) - CharIndex('[.',Reverse([QualifiedName])) -1))
			As [ParentName], -- Does not include Member
		[MemberName],
		[Level],
		IIF(Row_Number() Over (Order By [QualifiedName] Desc) = 1,1,0) As [IsBase],
		Count(*) Over (Partition by Null) As [TotalElements]
From	[Tree])
GO
/*
Select	*
From	[App_DataDictionary].[funcSplitQualifiedName]('[DatabaseName].[SchemaName].[TableName].[ColumnName]')
Order By [QualifiedName]

Select	Distinct X.*
From	Sys.Columns C
		outer apply [App_DataDictionary].[funcSplitQualifiedName](FormatMessage('[%s].[%s].[%s].[%s]',Db_Name(),Object_Schema_Name(object_id),object_name(object_id),name)) X
Order By [QualifiedName]
*/
