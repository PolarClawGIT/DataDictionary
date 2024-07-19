using DataDictionary.BusinessLayer.NamedScope;
namespace DataDictionary.BusinessLayer.Domain;

static class SubjectNameSpace
{
    /// <summary>
    /// Builds the SubjectNameSpace
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<NamedScopePair> GetNamedScopes(IEnumerable<NamedScopePair> source)
    {
        List<NamedScopePair> result = new List<NamedScopePair>();


        var values = source.SelectMany(
            paths => paths.Value.GetPath().Group(),
            (source, paths) => new { 
                source.ParentKey,
                source.Value,
                paths = source.Value.Source.GetPath() // Original Path, without Subject Area
            }).
            Distinct().
            OrderBy(o => o.paths.MemberFullPath.Length).
            GroupBy(g => g.ParentKey).
            ToList();

        foreach (var group in values)
        {
            if (group.Key is DataLayerIndex parentKey)
            {
                Dictionary<NamedScopePath, DataLayerIndex> items = new Dictionary<NamedScopePath, DataLayerIndex>();
                DataLayerIndex proposedKey = parentKey;
                INamedScopeSourceValue? nameSpace;

                foreach (NamedScopePath item in group.Select(s => s.paths).Distinct())
                {
                    if (item.ParentPath is NamedScopePath && items.ContainsKey(item.ParentPath))
                    { proposedKey = items[item.ParentPath]; }

                    List<NamedScopeValue> nodes = group.Where(w =>
                        item.Equals(w.Value.Source.GetPath())
                        && w.Value.Source is not SubjectNameSpaceValue).
                        Select(s => s.Value).
                        ToList();

                    if (nodes.Count == 1)
                    {
                        NamedScopeValue node = nodes.First();
                        items.Add(item, node.Source.GetIndex());
                        result.Add(new NamedScopePair(proposedKey, node));
                        proposedKey = node.Source.GetIndex();
                    }
                    else
                    {
                        nameSpace = new SubjectNameSpaceValue(item);
                        items.Add(item, nameSpace.GetIndex());
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