CREATE VIEW [AppScript].[TemplateNode_Old] As
-- Element and Attribute are Sub-Types of Node.
-- This builds Super-Type of Node with hiearachy.
With [Data] As (
	Select	[ElementId] as [NodeId],
			[ParentElementId] As [ParentNodeId],
			[ElementName] As [NodeName],
			[SysStart],
			[SysEnd]
	From	[AppScript].[TemplateElement]
	Union
	Select	A.[AttributeId] as [NodeId],
			O.[ElementId] As [ParentNodeId],
			A.[AttributeName] As [NodeName],
			A.[SysStart],
			A.[SysEnd]
	From	[AppScript].[TemplateAttribute] A
			Left Join [AppScript].[TemplateNodeOwner] O
			On	A.[AttributeId] = O.[AttributeId]),
[Tree] As (
	Select	D.[NodeId],
			D.[NodeName],
			D.[ParentNodeId],
			N.[QualifiedName] As [NodePath],
			Convert(NVarChar(Max),
				FormatMessage('/%I64d/', -- Under documented BigInt. See C++ PrintF
					Dense_Rank() Over (Order By [NodeName])))
				As [HierarchyId],
			D.[SysStart],
			D.[SysEnd]
	From	[Data] D
			Cross Apply [AppGeneral].[funcParseName](D.[NodeName]) N
	Where	D.[ParentNodeId] is Null And
			N.[IsBase] = 1
	Union All
	Select	D.[NodeId],
			D.[NodeName],
			D.[ParentNodeId],
			FormatMessage('%s.%s', T.[NodePath], N.[QualifiedName]) As [NodePath],
			Convert(NVarChar(Max), FormatMessage('%s%I64d/', T.[HierarchyId],
				Row_Number() Over (Partition By D.[NodeId] Order By D.[NodeName])))
				As [HierarchyId],
			Greatest(D.[SysStart], T.[SysStart]) As [SysStart],
			Least(D.[SysEnd], T.[SysEnd]) As [SysEnd]
	From	[Data] D
			Cross Apply [AppGeneral].[funcParseName](D.[NodeName]) N
			Inner Join [Tree] T
			On	D.[ParentNodeId] = T.[NodeId] And
			-- Temporal, multiple rows could be returned. Do not have confidence in this.
			((D.[SysStart] >= T.[SysStart] And D.[SysStart] < T.[SysEnd]) Or
			 (T.[SysStart] >= D.[SysStart] And T.[SysStart] < D.[SysEnd]))
	Where	N.[IsBase] = 1)
Select	IsNull(T.[NodeId], 0x0) As [NodeId],
		IsNull(T.[NodeName], '') As [NodeName],
		T.[ParentNodeId],
		IsNull(T.[NodePath], '') As [NodePath],
		Convert(HierarchyId, T.[HierarchyId]) As [HierarchyId],  -- Values is not guaranteed between executions.
		IsNull(IsNull(E.[RenderOrder], A.[RenderOrder]), 0) As [RenderOrder],
		IsNull(E.[RenderValueAs], A.[RenderValueAs]) As [RenderValueAs],
		IsNull(E.[FixedValue], A.[FixedValue]) As [FixedValue],
		IsNull(E.[ObjectScope], A.[ObjectScope]) As [ObjectScope],
		IsNull(E.[ObjectProperty], A.[ObjectProperty]) As [ObjectProperty],
		IsNull(E.[ModelPropertyId], A.[ModelPropertyId]) As [ModelPropertyId],
		IsNull(T.[SysStart], '1/1/0001') As [SysStart],
		IsNull(T.[SysEnd], '12/31/9999') As [SysEnd]
From	[Tree] T
		Left Join [AppScript].[TemplateElement] E
		On	T.[NodeId] = E.[ElementId] And
			T.[SysStart] = E.[SysStart]
		Left Join [AppScript].[TemplateAttribute] A
		On	T.[NodeId] = A.[AttributeId] And
			T.[SysStart] = A.[SysStart]
GO
