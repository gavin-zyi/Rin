using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dyn = System.Dynamic;

namespace Rin.Lang.Binders
{
    class SetMemberBinder : Dyn.SetMemberBinder
    {
        public SetMemberBinder(string name)
            : base(name, false)
        {

        }

        public override T BindDelegate<T>(CallSite<T> site, object[] args)
        {
            Func<CallSite, Any, Any, Any> func = (c, any, val) =>
            {
                if (!any.SetMemberOperation(this, val))
                {
                    throw new Exception();
                }

                return val;
            };
            return (T)(object)func;
        }

        public override Dyn.DynamicMetaObject FallbackSetMember(Dyn.DynamicMetaObject target, Dyn.DynamicMetaObject value, Dyn.DynamicMetaObject errorSuggestion)
        {
            throw new NotImplementedException();
        }
    }
}
