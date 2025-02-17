using System;
using System.Collections.Generic;

namespace Core
{
    public static class AssemblyHelper
    {
        public static IEnumerable<Type> FilterAllTypes(Func<Type, bool> filter)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (filter(type))
                    {
                        yield return type;
                    }
                }
            }
        }
    }
}