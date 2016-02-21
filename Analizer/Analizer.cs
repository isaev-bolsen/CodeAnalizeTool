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
            return GetRootForHierarhy(assembleys.Select(a => a.GetType(ClassName)).Single());
        }

        public Type GetRootForHierarhy(Type type)
        {
            if (type == typeof(object) || type.BaseType == typeof(object)) return type;
            else return GetRootForHierarhy(type.BaseType);
        }
    }
}
