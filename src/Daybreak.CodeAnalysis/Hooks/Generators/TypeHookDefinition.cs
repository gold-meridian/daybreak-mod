using System.Collections.Generic;
using System.Linq;

namespace Daybreak.CodeAnalysis.Hooks.Generators;

public sealed class TypeHookDefinition(string typeMetadataName)
{
    public string TypeMetadataName { get; } = typeMetadataName;
    
    public string TypeName => TypeMetadataName.Split('.').Last();
    
    public List<string> Exclusions { get; } = [];

    public List<TypeHookDefinition> SuperTypes { get; } = [];

    public TypeHookDefinition WithExclusions(params string[] exclusions)
    {
        Exclusions.AddRange(exclusions);
        return this;
    }

    public TypeHookDefinition WithSuperTypes(params TypeHookDefinition[] superTypes)
    {
        SuperTypes.AddRange(superTypes);
        return this;
    }
}
