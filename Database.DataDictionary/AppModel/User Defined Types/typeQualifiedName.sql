-- An Qualified Name is the combination of Members delimited by period and qualified by square brackets.
-- Periods are possible within the qualified name (example [Help.ID]).
-- Length is based on 1023 is the VB.Net NameSpace definition.
-- Examples are:
-- Model: [SubjectArea].[Sub-SubjectArea].[ElementName].[AttributeName]
CREATE TYPE [AppModel].[typeQualifiedName] FROM NVarchar(1023)
