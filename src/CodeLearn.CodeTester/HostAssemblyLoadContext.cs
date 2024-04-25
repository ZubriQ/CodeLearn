using System.Reflection;
using System.Runtime.Loader;

namespace CodeLearn.CodeTester;

internal class HostAssemblyLoadContext(string pluginPath) : AssemblyLoadContext(isCollectible: true)
{
    private readonly AssemblyDependencyResolver _resolver = new(pluginPath);

    protected override Assembly? Load(AssemblyName name)
    {
        var assemblyPath = _resolver.ResolveAssemblyToPath(name);
        if (assemblyPath == null)
        {
            return null;
        }
        
        Console.WriteLine($"Loading assembly {assemblyPath} into the HostAssemblyLoadContext");
        return LoadFromAssemblyPath(assemblyPath);
    }
}