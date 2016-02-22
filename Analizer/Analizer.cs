using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Analizer
{
    public class Analizer
    {
        private IEnumerable<Assembly> assembleys;

        public Analizer(IEnumerable<Assembly> assembleys)
        {
            this.assembleys = assembleys;
        }

        public Analizer(Assembly assembley) : this(new Assembly[] { assembley }) { }

        public Type GetRootForHierarhy(string ClassName)
        {
            return GetRootForHierarhy(SearchForType(ClassName));
        }

        private Type SearchForType(string ClassName)
        {
            var res = assembleys.Select(a => a.GetType(ClassName)).Where(t => t != null).ToList();
            if (res.Count==0) throw new ArgumentException("Class not found. Use valid Full Class name");
            return res.First(); 
        }

        public Type GetRootForHierarhy(Type type)
        {
            if (type == typeof(object) || type.BaseType == typeof(object)) return type;
            else return GetRootForHierarhy(type.BaseType);
        }

        public MyTypeInfo GetHierarhy(string ClassName)
        {
            return GetHierarhy(SearchForType(ClassName));
        }

        public MyTypeInfo GetHierarhy(Type type)
        {
            return new MyTypeInfo(GetRootForHierarhy(type), assembleys.SelectMany(a => a.GetTypes()));
        }
    }
}
