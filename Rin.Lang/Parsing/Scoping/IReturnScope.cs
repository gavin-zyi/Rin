using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing.Scoping
{
    internal interface IReturnScope
    {
        LabelTarget ReturnTarget { get; }
    }
}
