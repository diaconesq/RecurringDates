using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace RecurringDates.Serialization
{
    public class RuleSerializer
    {
        private static readonly RuleSerializer _instance = new RuleSerializer();
        public static RuleSerializer Instance
        {
            get { return _instance; }
        }

        private RuleTypeCache _typeCache = new RuleTypeCache();

        public IRule Deserialize(string serialized)
        {
            return DeserializeInternal(serialized, _ruleAssembly);
        }

        public string Serialize(IRule rule)
        {
            return SerializeInternal(rule, _ruleAssembly);
        }

        public string Serialize(IRule rule, params Assembly[] ruleTypesAssemblies)
        {
            return SerializeInternal(rule, GetLoadList(ruleTypesAssemblies));
        }

        public string Serialize(IRule rule, params Type[] ruleTypesAssemblies)
        {
            return SerializeInternal(rule, GetLoadList(ruleTypesAssemblies));
        }

        public string Serialize(IRule rule, params object[] ruleTypesAssemblies)
        {
            return SerializeInternal(rule, GetLoadList(ruleTypesAssemblies));
        }

        private string SerializeInternal(IRule rule, params object[] ruleTypesAssemblies)
        {
            
            var serializer = GetSerializer(ruleTypesAssemblies);
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            serializer.WriteObject(xw, rule);
            return sw.ToString();
        }

        private static readonly Assembly _ruleAssembly = typeof (IRule).Assembly;

        private static ArrayList GetLoadList(object[] parameters)
        {
            var list = new ArrayList(1 + parameters.Length);
            list.Add(_ruleAssembly);
            list.AddRange(parameters);
            return list;
        }

        public IRule Deserialize(string serialized, params Assembly[] ruleTypesAssemblies)
        {
            return DeserializeInternal(serialized, GetLoadList(ruleTypesAssemblies));
        }

        public IRule Deserialize(string serialized, params Type[] ruleTypesAssemblies)
        {
            return DeserializeInternal(serialized, GetLoadList(ruleTypesAssemblies));
        }

        public IRule Deserialize(string serialized, params object[] ruleTypesAssemblies)
        {
            return DeserializeInternal(serialized, GetLoadList(ruleTypesAssemblies));
        }

        private IRule DeserializeInternal(string serialized, params object[] ruleTypesAssemblies)
        {
            var serializer = GetSerializer(ruleTypesAssemblies);

            var xmlReader = XmlReader.Create(new StringReader(serialized));
            var obj = serializer.ReadObject(xmlReader);

            return (IRule)obj;
        }

        private DataContractSerializer GetSerializer(object[] ruleTypesAssemblies)
        {
            var knownTypes = GetKnownTypes(ruleTypesAssemblies);

            DataContractSerializer serializer = new DataContractSerializer(typeof(IRule), knownTypes);
            return serializer;
        }

        private IEnumerable<Type> GetKnownTypes(object[] ruleTypesAssemblies)
        {
            var assemblies = ruleTypesAssemblies.OfType<Assembly>().ToList();
            var types = ruleTypesAssemblies.OfType<Type>().ToList();

            var others = ruleTypesAssemblies.Except(assemblies).Except(types)
                .ToList();

            if (others.Any())
            {
                throw new ArgumentException(
                    "ruleTypesAssemblies should only contain items of type System.Assembly or System.Type, but found: " +
                    others.First().GetType());
            }

            foreach (var assembly in assemblies)
            {
                _typeCache.Load(assembly);
            }

            var knownTypes = _typeCache.Concat(types);
            return knownTypes;
        }
    }

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
