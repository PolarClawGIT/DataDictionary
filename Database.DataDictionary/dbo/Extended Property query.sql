	Select	S.name As [SchemaName],
			O.name As [ObjectName],
			F.*
	From	sys.schemas S
			Inner Join sys.Objects O
			On	S.schema_id = O.schema_id
			Cross Apply fn_listextendedproperty(N'MS_Description', 'SCHEMA', S.name, 'TABLE', O.name, default, default) F

	Select	S.name As [SchemaName],
			O.name As [ObjectName],
			F.*
	From	sys.schemas S
			Inner Join sys.Objects O
			On	S.schema_id = O.schema_id
			Inner Join sys.Columns C
			On	O.object_id = C.object_id
			Cross Apply fn_listextendedproperty(N'MS_Description', 'SCHEMA', S.name, 'TABLE', O.name, 'COLUMN', C.name) F


	Select	S.name As [SchemaName],
			O.name As [ObjectName],
			F.*
	From	sys.schemas S
			Inner Join sys.Objects O
			On	S.schema_id = O.schema_id
			Inner Join sys.objects C
			On	O.object_id = C.parent_object_id
			Cross Apply fn_listextendedproperty(N'MS_Description', 'SCHEMA', S.name, 'TABLE', O.name, 'CONSTRAINT', C.name) F

Select	*
From	sys.extended_properties