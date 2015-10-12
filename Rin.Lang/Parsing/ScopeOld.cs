using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing
{
    internal class ScopeOld
    {
        private readonly Type defaultReturn;

        private Frame current;
        private readonly Stack<Frame> frameStack;

        public ParameterExpression Parameters { get { return current.Args; } }
        public IEnumerable<ParameterExpression> Variables { get { return current.Variables.Values; } }

        public LabelTarget ReturnTarget { get { return current.ReturnTarget; } }
        
        public ScopeOld(Type returnTarget)
        {
            defaultReturn = returnTarget;

            current = new Frame(returnTarget);
            frameStack = new Stack<Frame>();
        }

        public void Enter(Type returnTarget = null)
        {
            frameStack.Push(current);
            current = new Frame(returnTarget ?? defaultReturn);
        }

        public void Exit()
        {
            current = frameStack.Pop();
        }

        public ParameterExpression NewVariable(string name)
        {
            if (current.Variables.ContainsKey(name) || current.Parameters.ContainsKey(name))
            {
                throw new Exception();
            }

            return current.Variables[name] = Expression.Variable(typeof(Any), name);
        }

        public Expression NewParameter(string name)
        {
            if (current.Variables.ContainsKey(name) || current.Parameters.ContainsKey(name))
            {
                throw new Exception();
            }

            var expr = Expression.MakeIndex(current.Args, Args.Indexer, new [] {Expression.Constant(current.ParameterIndex++)});

            return current.Parameters[name] = expr;
        }

        public Expression this [string name]
        {
            get
            {
                var expr = Locate(current, name);
                if (expr != null)
                {
                    return expr;
                }

                foreach (var frame in frameStack)
                {
                    expr = Locate(frame, name);
                    if (expr != null)
                    {
                        return expr;
                    }
                }

                return null;
            }            
        }

        private static Expression Locate(Frame frame, string name)
        {
            ParameterExpression variable;

            if (frame.Variables.TryGetValue(name, out variable))
            {
                return variable;
            }

            Expression parameter;

            if (frame.Parameters.TryGetValue(name, out parameter))
            {
                return parameter;
            }

            return null;  
        }

        private class Frame
        {
            public readonly ParameterExpression Args;
            public readonly Dictionary<string, ParameterExpression> Variables;
            public readonly Dictionary<string, Expression> Parameters;
            public int ParameterIndex;

            public readonly LabelTarget ReturnTarget;

            public Frame(Type returnTarget)
            {
                Args = Expression.Parameter(typeof(Args));
                Variables = new Dictionary<string, ParameterExpression>();
                Parameters = new Dictionary<string, Expression>();
                ParameterIndex = 0;

                ReturnTarget = Expression.Label(returnTarget);
            }
        }        
    }
}
