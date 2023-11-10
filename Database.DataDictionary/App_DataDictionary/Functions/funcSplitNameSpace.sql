CREATE FUNCTION [App_DataDictionary].[funcSplitNameSpace](@NameSpace NVarChar(Max))
-- This takes a String that has a Delimiter and creates a hierarchy of values.
-- Each level of the hierarchy is returned as separate row.
-- The example of this is a Database Fully Qualified (aka the four part name) name where the period is the delimiter.
-- This also applies to .Net NameSpaces where the depth is unknown.
-- In: DatabaseName.SchemaName.TableName.ColumnName
-- Out:
--   DatabaseName
--   DatabaseName.SchemaName
--   DatabaseName.SchemaName.TableName
--   DatabaseName.SchemaName.TableName.ColumnName
RETURNS TABLE AS RETURN (
With 	[Parse] As (
		Select	@NameSpace [NameSpace],
				Convert(NVarChar(128), Null) As [ElementName]
		Union All
		Select	N.[NameSpace],
				Convert(NVarChar(128),
					Replace(Replace(
						IIF(N.[NameSpace] is Null, P.[NameSpace],
						Right(P.[NameSpace],Len(P.[NameSpace]) - Len(N.[NameSpace]) - 1)),
						'[',''),']',''))
					As [ElementName]
		From	[Parse] P
				Outer Apply (
					Select	Case
						-- Empty String
						When Len(P.[NameSpace]) <= 0 Then Null
						-- No more delimiters
						When CharIndex('.', Reverse(P.[NameSpace])) = 0  Then Null
						-- No more brackets
						When CharIndex(']',Reverse(P.[NameSpace])) = 0 And
							CharIndex('[',Reverse(P.[NameSpace])) = 0  
							Then Left(P.[NameSpace], Len(P.[NameSpace]) - CharIndex('.',Reverse(P.[NameSpace])) -0)
						-- Period is after the bracket
						When CharIndex(']',Reverse(P.[NameSpace])) > 0 And
							CharIndex(']',Reverse(P.[NameSpace])) > CharIndex('.', Reverse(P.[NameSpace]))
							Then Left(P.[NameSpace], Len(P.[NameSpace]) - CharIndex('.',Reverse(P.[NameSpace])) -0)
						-- Period is before the bracket
						When CharIndex(']',Reverse(P.[NameSpace])) > 0 And
							CharIndex('[.', Reverse(P.[NameSpace])) > 0
							Then Left(P.[NameSpace], Len(P.[NameSpace]) - CharIndex('[.',Reverse(P.[NameSpace])) -1)
						Else Null
						End As [NameSpace] ) N
		Where	NullIf(P.[NameSpace],'') is Not Null)
Select	[NameSpace],
		[ElementName]
From	[Parse])
GO