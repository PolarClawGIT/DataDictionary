CREATE FUNCTION [AppGeneral].[funcConcatPath] (
	@LeftPath	[AppGeneral].[uddtPath] = Null,
	@RightPath	[AppGeneral].[uddtPath] = Null)
RETURNS [AppGeneral].[uddtPath]
As
Begin
	/* Combines two Paths/Members into a combined path.
	** 
	** Null for both paths results in Null
	** Null in either path is treated as an empty string.
	** If the path does not starts/ends with square brackets, square brackets are added.
	** If both a paths are provided, a period is added between them.
	**
	** Deals with FormatMessage limitation.
	** Formate Message is limited to 2000 characters. By using Concsat instad.
	**
	** Deals with Message 243, 'Type %s is not a defined system type.'
	** The function avoid this by implictly casting the results back to the correct type.
	*/
	Return 
		Case
			When @LeftPath is Null And @RightPath is Null Then Null
			Else
				Concat(
					Case
						When @LeftPath is Null Then ''
						When @LeftPath is Not Null And Left(@LeftPath,1) = '[' And Right(@LeftPath,1) = ']' Then @LeftPath
						Else Concat('[',@LeftPath,']')
						End,
					Case When @LeftPath is Not Null And @RightPath is Not Null Then '.' Else '' End,
					Case
						When @RightPath is Null Then ''
						When @RightPath is Not Null And Left(@RightPath,1) = '[' And Right(@RightPath,1) = ']' Then @RightPath
						Else Concat('[',@RightPath,']')
						End)
		End
End
GO

