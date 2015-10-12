using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang
{
    public class Class : Any
    {
        private Func<Args, Any> ctor;

        public Class(Func<Args, Any> ctor)
        {
            this.ctor = ctor;
        }
        
        internal override bool InvokeOperation(InvokeBinder binder, Args args, out Any result)
        {
            result = ctor(args);
            return true;
        }

        static Class()
        {
            New = typeof(Class).GetConstructor(new[] { typeof(Func<Args, Any>) });
        }

        public static ConstructorInfo New { get; private set; }

        public class Instance : Any
        {
            private Dictionary<string, Any> members;

            public Instance()
            {
                members = new Dictionary<string, Any>();
            }

            internal override bool GetMemberOperation(GetMemberBinder binder, out Any result)
            {
                if (members.TryGetValue(binder.Name, out result))
                {
                    return true;
                }

                return base.GetMemberOperation(binder, out result);
            }

            internal override bool SetMemberOperation(SetMemberBinder binder, Any value)
            {
                members[binder.Name] = value;
                return true;
            }

            static Instance()
            {
                New = typeof(Instance).GetConstructor(new Type[] { });
            }

            public static ConstructorInfo New { get; private set; }
        }
    }
}
