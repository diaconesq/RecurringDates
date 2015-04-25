using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RecurringDates.Serialization
{
    /// <summary>
    /// Search, cache and retrieve the types implementing IRule in the speciffied assemblies.
    /// </summary>
    class RuleTypeCache: IEnumerable<Type>
    {
        private readonly IDictionary<Assembly,IList<TypeInfo>> _typesByAssembly = new Dictionary<Assembly, IList<TypeInfo>>();

        public void Load(Assembly assembly)
        {
            if(_typesByAssembly.ContainsKey(assembly))
                return;

            _typesByAssembly.Add(assembly, GetRuleTypes(assembly));
        }

        private IList<TypeInfo> GetRuleTypes(Assembly assembly)
        {
            return assembly.DefinedTypes
                .Where(t => typeof (IRule).IsAssignableFrom(t))
                .ToList();
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return _typesByAssembly.Values.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}