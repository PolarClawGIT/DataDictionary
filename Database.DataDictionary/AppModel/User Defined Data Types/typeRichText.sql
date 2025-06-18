-- Intended to store RTF (Rich Text Formated). Pattern: Like '{\rtf1\ansi %}'
-- This is not enforced.
CREATE TYPE [AppModel].[typeRichText] FROM NVarChar(Max) NULL
