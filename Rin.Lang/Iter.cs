using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang
{
    public class Iter : Any
    {
        protected Func<Any> State { get; set; }

        public Any GetNext()
        {
            return State();
        }

        internal override bool GetMemberOperation(GetMemberBinder binder, out Any result)
        {
            Func<Iter, Any> member;

            if (!members.TryGetValue(binder.Name, out member))
            {
                result = null;
                return false;
            }

            result = member(this);

            return true;
        }

        public static Iter Range(double start, double end)
        {
            return new Iter { State = () => start < end ? (Any)new Number(start++) : Any.None };
        }

        static Iter()
        {
            members = new Dictionary<string, Func<Iter, Any>>
                {
                    {"next", obj => new Function(args => obj.GetNext())}
                };

            New = typeof(Iter).GetConstructor(new Type[] { });
            Next = typeof(Iter).GetMethod("GetNext");
        }

        private static readonly Dictionary<string, Func<Iter, Any>> members;

        public static ConstructorInfo New { get; private set; }
        public static MethodInfo Next { get; private set; }
    }
}
