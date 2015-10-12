using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dyn = System.Dynamic;

namespace Rin.Lang.Binders
{
    internal class GetMemberBinder : Dyn.GetMemberBinder
    {
        public GetMemberBinder(string name)
            : base(name, false)
        {

        }

        public override T BindDelegate<T>(CallSite<T> site, object[] args)
        {
            Func<CallSite, Any, Any> func = (c, any) =>
                {
                    Any val;
                    if (!any.GetMemberOperation(this, out val))
                    {
                        throw new Exception();
                    }

                    return val;
                };
            return (T)(object)func;
        }

        public override Dyn.DynamicMetaObject FallbackGetMember(Dyn.DynamicMetaObject target, Dyn.DynamicMetaObject errorSuggestion)
        {
            throw new NotImplementedException();
        }
    }
}
