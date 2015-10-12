using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing.Scoping
{
    internal class ScopeManager
    {
        public Scope Current { get { return scopeStack.Peek(); } }

        private readonly Runtime runtime;
        private readonly Stack<Scope> scopeStack;
        
        public ScopeManager(Runtime runtime)
        {
            this.runtime = runtime;
            scopeStack = new Stack<Scope>();
            PushModule();
        }

        public Expression this[string name]
        {
            get
            {
                Expression result = null;

                foreach (var scope in scopeStack)
                {
                    result = scope[name];

                    if (result != null)
                    {
                        break;
                    }
                }

                return result;
            }
        }
        
        public BlockScope PushBlock()
        {
            var scope = new BlockScope(runtime);
            scopeStack.Push(scope);
            return scope;
        }

        public FunctionScope PushFunction()
        {
            var scope = new FunctionScope(runtime);
            scopeStack.Push(scope);
            return scope;
        }

        public ModuleScope PushModule()
        {
            var scope = new ModuleScope(runtime);
            scopeStack.Push(scope);
            return scope;
        }

        public ClassScope PushClass()
        {
            var scope = new ClassScope(runtime);
            scopeStack.Push(scope);
            return scope;
        }

        public void Pop()
        {
            scopeStack.Pop();
        }
    }
}
