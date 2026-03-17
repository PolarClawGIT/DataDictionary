CREATE FUNCTION [AppGeneral].[funcCreatePath] (
	@Path	[AppGeneral].[uddtPath],
	@Member [AppGeneral].[uddtMember] = Null)
RETURNS [AppGeneral].[uddtPath]
As
Begin
	/* Used to Merge MembersNames into the Path.
	** This is an alternate to using FormatMessage.
	** Formate Message is limited to 2000 characters.
	** @Path is required. If not supplied, the function returns Null.
	** @Member is optional. If not supplied the @Path is returned with [].
	** Otherwise the @Member is concatinated to @Path with [] and period between them.
	**
	** TODO: Replace With [AppGeneral].[funcConcatPath]. 
	*/
	Return 
		Case
			When @Path is Null Then Null
			Else Concat(
				Case
					When Left(@Path,1) = '[' And Right(@Path,1) = ']' Then @Path
					Else Concat('[',@Path,']')
					End,
				Case
					When @Member is Null Then ''
					When Left(@Member,1) = '[' And Right(@Member,1) = ']' Then @Member
					Else Concat('.[',@Member,']') 
					End)
			End
End