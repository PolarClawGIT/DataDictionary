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
                Dictionary<NamedScopePath, INamedScopeSourceValue> items = new Dictionary<NamedScopePath, INamedScopeSourceValue>();
                DataLayerIndex proposedKey = parentKey;
                INamedScopeSourceValue? nameSpace;

                foreach (NamedScopePath item in group.Select(s => s.paths).Distinct())
                {
                    nameSpace = new SubjectNameSpaceValue(item);

                    if (item.ParentPath is NamedScopePath && items.ContainsKey(item.ParentPath))
                    { proposedKey = items[item.ParentPath].GetIndex(); }
                    items.Add(item, nameSpace);

                    result.Add(new NamedScopePair(proposedKey, new NamedScopeValue(nameSpace)));

                    //TODO: If there is only one, then only create the original node.
                    // Skip the NameSpace node.
                    // Have not figured out how.
                    List<NamedScopeValue> nodes = group.Where(w =>
                        item.Equals(w.Value.Source.GetPath())
                        && w.Value.Source is not SubjectNameSpaceValue).
                        Select(s => s.Value).
                        ToList();

                    foreach (var node in nodes)
                    { result.Add(new NamedScopePair(nameSpace.GetIndex(), node)); }
                }
            }
        }


        return result;
    }
}