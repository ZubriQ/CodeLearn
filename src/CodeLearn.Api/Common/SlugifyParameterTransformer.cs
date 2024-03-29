using System.Text.RegularExpressions;

namespace CodeLearn.Api.Common;

/// <summary>
/// Changes route naming style.
/// For example: 'StudentGroups' to 'student-groups'
/// </summary>
public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value == null ? null : Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}