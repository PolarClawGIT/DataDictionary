using DataDictionary.BusinessLayer.NamedScope;
namespace DataDictionary.BusinessLayer.Domain;

static class SubjectNameSpace
{
    /// <summary>
    /// Builds the SubjectNameSpace
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    [Obsolete]
    public static IEnumerable<NamedScopePair> GetNamedScopes(IEnumerable<NamedScopePair> source)
    {
        List<NamedScopePair> result = new List<NamedScopePair>();

        var values = source.SelectMany(
            paths => paths.Value.Source.GetPath().Group(), // Original Path, without Subject Area
            (source, paths) => new { 
                source.ParentKey,
                source.Value,
                paths 
            }).
            Distinct().
            OrderBy(o => o.paths.MemberFullPath.Length).
            GroupBy(g => g.ParentKey).
            ToList();

        foreach (var group in values)
        {
            if (group.Key is DataLayerIndex parentKey)
            {
                Dictionary<NamedScopePath, DataLayerIndex> nameSpaceNodes = new Dictionary<NamedScopePath, DataLayerIndex>();
                INamedScopeSourceValue? nameSpace;

                foreach (NamedScopePath item in group.Select(s => s.paths).Distinct())
                {
                    DataLayerIndex proposedKey = parentKey;

                    if (item.ParentPath is NamedScopePath && nameSpaceNodes.ContainsKey(item.ParentPath))
                    { proposedKey = nameSpaceNodes[item.ParentPath]; }

                    List<NamedScopeValue> nodes = group.Where(w =>
                        item.Equals(w.Value.Source.GetPath())
                        && w.Value.Source is not SubjectNameSpaceValue).
                        Select(s => s.Value).
                        Distinct().
                        ToList();

                    if (nodes.Count == 1)
                    { // If there is only one node for the namespace, use the node itself instead of creating a separate node.
                        NamedScopeValue node = nodes.First();
                        nameSpaceNodes.Add(item, node.Source.GetIndex());
                        result.Add(new NamedScopePair(proposedKey, node));
                        proposedKey = node.Source.GetIndex();
                    }
                    else
                    {
                        nameSpace = new SubjectNameSpaceValue(item);
                        nameSpaceNodes.Add(item, nameSpace.GetIndex());
                        result.Add(new NamedScopePair(proposedKey, new NamedScopeValue(nameSpace)));

                        foreach (var node in nodes)
                        { result.Add(new NamedScopePair(nameSpace.GetIndex(), node)); }
                    }
                }
            }
        }


        return result;
    }
}