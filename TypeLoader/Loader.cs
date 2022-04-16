using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TypeLoader
{
    [Serializable]
    public class Loader : MarshalByRefObject
    {
        public Loader() { }

        public void Load(byte[] assemblyData)
        {
            var assembly = Assembly.Load(assemblyData);
        }

        public string[] GetExportedTypes()
        {
            var typeNames = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetExportedTypes())
                .Select(t => t.FullName)
                .Where(t => t.Contains("Script"))
                .ToArray();
            return typeNames;
        }
    }
}
