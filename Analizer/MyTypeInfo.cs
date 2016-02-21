using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizer
{
    public class MyTypeInfo 
    {
        public Type Type        { get; private set; }
        public MyTypeInfo Parent { get; private set; }
        public IEnumerable<MyTypeInfo> Children { get; private set; }
    }
}
