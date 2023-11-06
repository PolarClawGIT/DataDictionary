CREATE FUNCTION [App_DataDictionary].[funcSplitNameSpace](@NameSpace NVarChar(Max), @Delimiter NVarChar(10))
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
RETURNS TABLE
AS RETURN (
		With [Parse] As (
		Select	@NameSpace As [NameSpace]
		Union All
		Select	N.[NameSpace]
		From	[Parse] V
				Outer Apply (
					Select	IIF(CharIndex(@Delimiter, V.[NameSpace])>0,
								Left(V.[NameSpace], Len(V.[NameSpace]) - CharIndex(@Delimiter, Reverse(V.[NameSpace]))),
								Null) As [NameSpace]
							) N
		Where	NullIf(N.[NameSpace],'') is Not Null)
		Select	[NameSpace],
				IIF(CharIndex(@Delimiter, [NameSpace])>0,
					Left([NameSpace], Len([NameSpace]) - CharIndex(@Delimiter, Reverse([NameSpace]))),
					Null)
					As [ParentNameSpace],
				IIF(CharIndex(@Delimiter, [NameSpace])>0,
					Right([NameSpace], CharIndex(@Delimiter, Reverse([NameSpace])) -1),
					[NameSpace])
					As [ElementName]
		From	[Parse])
GO