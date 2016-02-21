using System;
using System.Collections.Generic;
using System.Linq;

namespace Analizer
{
    public class MyTypeInfo
    {
        public Type Type { get; private set; }
        public MyTypeInfo Parent { get; private set; }
        public IEnumerable<MyTypeInfo> Children { get; private set; }

        internal MyTypeInfo(Type Type, IEnumerable<Type> OtherTypes)
        {
            this.Type = Type;
            Children = OtherTypes.Where(t => t.BaseType == Type).Select(t => new MyTypeInfo(t, this, OtherTypes));
        }

        private MyTypeInfo(Type Type, MyTypeInfo Parent, IEnumerable<Type> OtherTypes) : this(Type, OtherTypes)
        {
            this.Parent = Parent;
        }
    }
}
