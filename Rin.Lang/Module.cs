using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang
{
    public class Module : Any
    {
        public Dictionary<string, Any> Members { get; set; }

        public Module()
        {
            Members = new Dictionary<string, Any>();
        }

        internal override bool GetMemberOperation(GetMemberBinder binder, out Any result)
        {
            if (Members.TryGetValue(binder.Name, out result))
            {
                return true;
            }

            return base.GetMemberOperation(binder, out result);
        }

        internal override bool SetMemberOperation(SetMemberBinder binder, Any value)
        {
            Members[binder.Name] = value;
            return true;
        }

        static Module()
        {
            New = typeof(Module).GetConstructor(new Type[] {});
        }

        public static ConstructorInfo New { get; private set; }
    }
}
