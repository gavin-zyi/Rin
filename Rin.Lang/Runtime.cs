using Rin.Lang.Binders;
using Rin.Lang.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang
{
    public class Runtime
    {
        private readonly Dictionary<ExpressionType, UnaryOperationBinder> unaryOps;
        private readonly Dictionary<ExpressionType, BinaryOperationBinder> binaryOps;

        internal CallSiteBinder InvokeCallSite { get; private set; }
        internal CallSiteBinder ConvertCallSite { get; private set; }
        internal CallSiteBinder GetIndexCallSite { get; private set; }
        internal CallSiteBinder SetIndexCallSite { get; private set; }      

        public Dictionary<string, Expression> BuiltIns { get; private set; }
                
        public Runtime()
        {
            unaryOps = new Dictionary<ExpressionType, UnaryOperationBinder>();
            binaryOps = new Dictionary<ExpressionType, BinaryOperationBinder>();
            InvokeCallSite = new InvokeBinder();
            ConvertCallSite = new ConvertBinder();
            GetIndexCallSite = new GetIndexBinder();
            SetIndexCallSite = new SetIndexBinder();

            BuiltIns = new Dictionary<string, Expression>();

            RegisterClass("Foo", null);
            RegisterFunction("range", args =>
                {
                    return Iter.Range(((Number)args[0]).Value, ((Number)args[1]).Value);
                });
            RegisterFunction("using", args =>
                {
                    var filename = args[0].ToString();
                    var scanner = new Scanner(File.ReadAllText(filename));
                    var parser = new Parser(this, scanner);
                    var expr = parser.Parse();

                    var func = expr.Compile();
                    return func();
                });
            RegisterFunction("print", args => Console.WriteLine(string.Join(" ", args)));

            RegisterModule("maths", new Dictionary<string, Any>
                {
                    {"PI", new Number(Math.PI)},
                    {"cos", new Function(args => new Number(Math.Cos(((Number) args[0]).Value)))},
                    {"sin", new Function(args => new Number(Math.Sin(((Number) args[0]).Value)))},


                });
        }

        public void RegisterModule(string name, Dictionary<string, Any> members)
        {
            BuiltIns[name] = Expression.Constant(new Module { Members = members });
        }

        public void RegisterClass(string name, Action<Args> func)
        {
            BuiltIns[name] = Expression.Constant(new Function(func));
        }

        public void RegisterFunction(string name, Action<Args> func)
        {
            BuiltIns[name] = Expression.Constant(new Function(func));
        }

        public void RegisterFunction(string name, Func<Args, Any> func)
        {
            BuiltIns[name] = Expression.Constant(new Function(func));
        }
        
        internal CallSiteBinder GetUnaryCallSite(ExpressionType type)
        {
            UnaryOperationBinder unaryOp;

            if (!unaryOps.TryGetValue(type, out unaryOp))
            {
                unaryOp = new UnaryOperationBinder(type);
                unaryOps[type] = unaryOp;
            }

            return unaryOp;
        }

        internal CallSiteBinder GetBinaryCallSite(ExpressionType type)
        {
            BinaryOperationBinder binaryOp;

            if (!binaryOps.TryGetValue(type, out binaryOp))
            {
                binaryOp = new BinaryOperationBinder(type);
                binaryOps[type] = binaryOp;
            }

            return binaryOp;
        }

        internal CallSiteBinder GetMemberCallSite(string name)
        {
            return new GetMemberBinder(name);
        }

        internal CallSiteBinder SetMemberCallSite(string name)
        {
            return new SetMemberBinder(name);
        }
    }
}
