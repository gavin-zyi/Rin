using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Rin.Lang.Utils;
using Rin.Lang.Visitors;
using Rin.Lang.Binders;
using System.Runtime.CompilerServices;
using Rin.Lang.Parsing.Scoping;

using String = Rin.Lang.String;

namespace Rin.Lang.Parsing
{
    public class Parser
    {
        private readonly Runtime runtime;
        private readonly Scanner scanner;
        private readonly ScopeManager scopeManager;
        
        private Token token;
        private Token ahead;
                
        public Parser(Runtime runtime, Scanner scanner)
        {
            this.runtime = runtime;
            this.scanner = scanner;

            scopeManager = new ScopeManager(runtime);
        }

        public Expression<Func<Any>> Parse()
        {
            ahead = scanner.Scan();

            if (ahead.Type == TokenType.Eof)
            {
                return null;
            }

            token = null;

            var program = ProgramRule();

            if (program == null)
            {
                ExpectError(ahead.Type);
            }

            while (TryExpect(TokenType.Eol))
            {
                
            }
            
            Expect(TokenType.Eof);
            
            //var folder = new ConstantFolder(runtime);
            //result = folder.Modify(result);

            return (Expression<Func<Any>>) program;
        }

        private Expression ProgramRule()
        {           
            var list = StatementListRule().ToList();
            return scopeManager.Current.Pack(list);
            //return Expression.Block(scope.Variables, list);
        }

        private IEnumerable<Expression> StatementListRule()
        {                        
            while (true)
            {
                var i = 0;
                foreach (var stat in StatementRule())
                {
                    yield return stat;
                    i++;
                }

                if (i == 0) yield break;
            }
        }

        private IEnumerable<Expression> StatementRule()
        {
            var i = 0;
            foreach (var stat in SimpleStatementRule())
            {
                yield return stat;
                i++;
            }

            if (i > 0) yield break;

            var compound = CompoundStatementRule();
            if (compound == null)
            {
                yield break;
            }

            yield return compound;
        }

        private IEnumerable<Expression> SimpleStatementRule()
        {
            var i = 0;
            foreach (var stat in ExpressionStatementRule())
            {
                yield return stat;
                i++;
            }

            if (i == 0) yield break;

            if (ahead.Type != TokenType.SemiColon)
            {
                Expect(TokenType.Eol);
                yield break;
            }            

            do
            {
                Expect(TokenType.SemiColon);

                i = 0;
                foreach (var stat in ExpressionStatementRule())
                {
                    yield return stat;
                    i++;
                }

                if (i == 0) break;
            }
            while (ahead.Type == TokenType.SemiColon);

            Expect(TokenType.Eol);
        }

        private Expression CompoundStatementRule()
        {
            switch (ahead.Type)
            {
                case TokenType.If:
                    return IfElseStatementRule();
                case TokenType.For:
                    return ForStatementRule();
                case TokenType.While:
                    return WhileStatementRule();
                case TokenType.Func:
                    return FunctionStatementRule();
                case TokenType.Class:
                    return ClassStatementRule();
            }

            return null;
        }

        private Expression IfElseStatementRule()
        {
            Expect(TokenType.If);
            var cond = ExpressionRule();
            Expect(TokenType.Colon);

            var scope = scopeManager.PushBlock();
            var body = StatementSuiteRule().ToList();

            var comp = Expression.Dynamic(runtime.GetUnaryCallSite(ExpressionType.IsTrue), typeof(Any), cond);
            var ifElse = Expression.IfThen(Expression.Equal(comp, Expression.Constant(Bool.True)), scope.Pack(body));

            scopeManager.Pop();

            return ifElse;
        }

        private Expression ForStatementRule()
        {
            Expect(TokenType.For);
            var scope = scopeManager.PushBlock();

            Expect(TokenType.Member);
            var value = scope.NewVariable(token.Value);
            var iter = scope.NewVariable("__iter");
            var next = scope.NewVariable("__next");

            Expect(TokenType.In);
            var iterValue = ExpressionRule();
            Expect(TokenType.Colon);

            var @break = Expression.Label(typeof(void));
            var @continue = Expression.Label(typeof(void));

            var body = new List<Expression>
                {
                    Expression.Assign(value, Expression.Dynamic(runtime.InvokeCallSite, typeof(Any), next, Expression.Constant(Args.Empty))),
                    Expression.IfThen(Expression.Equal(value, Expression.Constant(Any.None)), Expression.Break(@break))
                };
            
            body.AddRange(StatementSuiteRule());
            
            var loop = Expression.Loop(Expression.Block(body), @break, @continue);

            var final = scope.Pack(Expression.Assign(iter, iterValue),
                Expression.Assign(next, Expression.Dynamic(runtime.GetMemberCallSite("next"), typeof(Any), iter)), loop);

            scopeManager.Pop();

            return final;
        }

        private Expression WhileStatementRule()
        {
            Expect(TokenType.While);
            var cond = ExpressionRule();
            Expect(TokenType.Colon);

            var scope = scopeManager.PushBlock();
            var body = StatementSuiteRule().ToList();

            var @break = Expression.Label(typeof(void));
            var @continue = Expression.Label(typeof(void));
            var comp = Expression.Dynamic(runtime.GetUnaryCallSite(ExpressionType.IsTrue), typeof(Any), cond);
            var ifElse = Expression.IfThenElse(Expression.Equal(comp, Expression.Constant(Bool.True)), scope.Pack(body), Expression.Break(@break));
            
            var final = Expression.Loop(ifElse, @break, @continue);
            scopeManager.Pop();

            return final;
        }
        
        private Expression FunctionStatementRule()
        {
            Expect(TokenType.Func);
            Expect(TokenType.Member);

            var name = token.Value;
            var funcVar = scopeManager.Current.NewVariable(name);
            var scope = scopeManager.PushFunction();

            Expect(TokenType.LParen);

            if (ahead.Type == TokenType.Member)
            {
                do
                {
                    Expect(TokenType.Member);
                    scope.NewParameter(token.Value);
                }
                while (TryExpect(TokenType.Comma));
            }

            Expect(TokenType.RParen);
            Expect(TokenType.Colon);

            var body = StatementSuiteRule().ToList();
            
            var returnLabel = Expression.Label(scope.ReturnTarget, Expression.Constant(Any.None));
            body.Add(returnLabel);

            var func = Expression.Dynamic(runtime.ConvertCallSite, typeof(Any), scope.Pack(body));

            scopeManager.Pop();

            return SafeAssign(funcVar, func);
        }

        private Expression ClassStatementRule()
        {
            Expect(TokenType.Class);
            Expect(TokenType.Member);

            var name = token.Value;
            var classVar = scopeManager.Current.NewVariable(name);
            var scope = scopeManager.PushClass();

            Expect(TokenType.LParen);

            var body = new List<Expression>();

            if (ahead.Type == TokenType.Member)
            {
                do
                {
                    Expect(TokenType.Member);
                    body.Add(SafeAssign(scope.NewVariable(token.Value), scope.LoadParameter()));
                }
                while (TryExpect(TokenType.Comma));
            }

            Expect(TokenType.RParen);
            Expect(TokenType.Colon);

            body.AddRange(StatementSuiteRule());

            //var returnLabel = Expression.Label(scope.ReturnTarget, Expression.Constant(new Class(null)));
            //body.Add(returnLabel);

            var @class = Expression.Dynamic(runtime.ConvertCallSite,
                                          typeof(Any),
                                          scope.Pack(body));

            scopeManager.Pop();

            return SafeAssign(classVar, @class);
        }

        private IEnumerable<Expression> ExpressionStatementRule()
        {
            IEnumerable<Expression> stats = null;
            switch (ahead.Type)
            {
                case TokenType.Var:
                case TokenType.Let:
                    stats = VariableStatementRule();
                    break;
                case TokenType.Return:
                    stats = ReturnStatementRule();
                    break;
                case TokenType.Pass:
                    stats = PassStatementRule();
                    break;
                default:
                    stats = AssignmentStatementRule();
                    break;
            }

            return stats;
        }

        private IEnumerable<Expression> StatementSuiteRule()
        {
            var i = 0;
            foreach (var stat in SimpleStatementRule())
            {
                yield return stat;
                i++;
            }

            if (i > 0) yield break;

            Expect(TokenType.Eol);
            Expect(TokenType.Indent);

            foreach (var stat in StatementListRule())
            {
                yield return stat;
            }

            Expect(TokenType.Dedent);
        }

        private IEnumerable<Expression> VariableStatementRule()
        {
            bool constant;
            if (ahead.Type == TokenType.Var)
            {
                Expect(TokenType.Var);
                constant = false;
            }
            else
            {
                Expect(TokenType.Let);
                constant = true;
            }

            var exprs = new List<Expression>();

            do
            {
                Expect(TokenType.Member);
                exprs.Add(scopeManager.Current.NewVariable(token.Value));
            }
            while (TryExpect(TokenType.Comma));

            if (!TryExpect(TokenType.Equal))
            {
                foreach (var expr in exprs)
                {
                    yield return Expression.Assign(expr, Expression.Constant(Any.None));
                }

                yield break;
            }

            var values = ExpressionListRule().ToList();

            foreach (var assign in PerformAssign(exprs, values))
            {
                yield return assign;
            }
        }

        private IEnumerable<Expression> ReturnStatementRule()
        {
            Expect(TokenType.Return);
            var scope = scopeManager.Current as IReturnScope;

            if (scope == null)
            {
                throw new Exception();
            }

            yield return Expression.Return(scope.ReturnTarget, ExpressionRule());
        }

        private IEnumerable<Expression> PassStatementRule()
        {
            Expect(TokenType.Pass);
            yield return Expression.Constant(Any.None, typeof(Any));
        }

        private IEnumerable<Expression> PerformAssign(List<Expression> targets, List<Expression> values)
        {
            if (targets.Count == 1 && values.Count == 1)
            {
                yield return SafeAssign(targets[0], values[0]);
            }
            else if (targets.Count > 1 && values.Count == 1)
            {
                var scope = scopeManager.PushBlock();
                var iter = scope.NewVariable("__iter");
                var next = scope.NewVariable("__next");

                var body = new List<Expression>
                {
                    Expression.Assign(iter, values[0]),
                    Expression.Assign(next, Expression.Dynamic(runtime.GetMemberCallSite("next"), typeof(Any), iter)),
                    Expression.IfThen(
                        Expression.Not(
                            Expression.TypeIs(iter, typeof(Iter))),
                        Expression.Throw(Expression.Constant(new Exception("not iter!!"))))
                };

                foreach (var expr in targets)
                {
                    body.Add(SafeAssign(expr, Expression.Dynamic(runtime.InvokeCallSite, typeof(Any), next, Expression.Constant(Args.Empty))));
                }

                yield return scope.Pack(body);
                scopeManager.Pop();
            }
            else if (targets.Count == 1 && values.Count > 1)
            {
                yield return SafeAssign(targets[0], Expression.New(Tuple.New, Expression.NewArrayInit(typeof(Any), values)));
            }
            else if (targets.Count == values.Count)
            {
                for (var i = 0; i < targets.Count; i++)
                {
                    yield return SafeAssign(targets[i], values[i]);                    
                }
            }
            else
            {
                throw new Exception();
            }
        }

        private Expression SafeAssign(Expression target, Expression value)
        {
            var dynExpr = target as DynamicExpression;

            if (dynExpr == null)
            {
                return Expression.Assign(target, value);
            }

            var getIndex = dynExpr.Binder as GetIndexBinder;
            if (getIndex != null)
            {
                return Expression.Dynamic(getIndex, typeof(Any), dynExpr.Arguments.Concat(new[] { value }));
            }
            
            var getMember = dynExpr.Binder as GetMemberBinder;
            if (getMember != null)
            {
                var newBinder = runtime.SetMemberCallSite(getMember.Name);
                return Expression.Dynamic(newBinder, typeof(Any), dynExpr.Arguments.Concat(new[] { value }));
            }

            throw new NotImplementedException();            
        }

        private IEnumerable<Expression> AssignmentStatementRule()
        {
            var expr = ExpressionRule();
            if (expr == null) yield break;

            //// expr
            //// expr = expr
            //// expr, expr = expr, expr

            if (!TryExpect(TokenType.Equal))
            {
                yield return expr;
                yield break;
            }

            var targets = ExpressionListRule(expr).ToList();
            var values = ExpressionListRule().ToList();
            // check expr

            foreach (var assign in PerformAssign(targets, values))
            {
                yield return assign;
            }
        }

        private Expression ExpressionRule()
        {
            return TupleExpressionRule();
        }

        private IEnumerable<Expression> ExpressionListRule()
        {
            return ExpressionListRule(ExpressionRule());
        }
        private IEnumerable<Expression> ExpressionListRule(Expression tuple)
        {
            if (tuple == null) yield break;

            if (tuple.Type != typeof(Tuple))
            {
                yield return tuple;
                yield break;
            }

            var exprs = ((NewArrayExpression)((NewExpression)tuple).Arguments[0]).Expressions;

            foreach (var expr in exprs)
            {
                yield return expr;
            }
        }

        private Expression TupleExpressionRule()
        {
            var expr = ConditionalExpressionRule();
            if (expr == null) return null;
                        
            if (!TryExpect(TokenType.Comma))
            {
                return expr;              
            }

            var values = new List<Expression> { expr };

            do
            {
                var value = ConditionalExpressionRule();
                if (value == null)
                {
                    break;
                }

                values.Add(value);
            }
            while (TryExpect(TokenType.Comma));     

            return Expression.New(Tuple.New, Expression.NewArrayInit(typeof(Any), values));
        }

        private Expression ConditionalExpressionRule()
        {
            var expr = OrExpressionRule();
            if (expr == null) return null;

            if (!TryExpect(TokenType.If))
            {
                return expr;
            }

            var cond = OrExpressionRule();

            Expect(TokenType.Else);

            var fall = OrExpressionRule();

            var comp = Expression.Dynamic(runtime.GetUnaryCallSite(ExpressionType.IsTrue), typeof(Any), cond);
            return Expression.Condition(Expression.Equal(comp, Expression.Constant(Bool.True)), expr, fall);
        }

        private Expression OrExpressionRule()
        {
            var expr = RelationalExpressionRule();
            if (expr == null) return null;

            while (true)
            {
                if (!TryExpect(TokenType.Or))
                {
                    return expr;
                }

                var opt = RelationalExpressionRule();
                expr = Expression.Dynamic(runtime.GetBinaryCallSite(ExpressionType.Or), typeof(Any), expr, opt);
            }
        }

        private Expression RelationalExpressionRule()
        {
            var expr = AdditiveExpressionRule();
            if (expr == null) return null;

            ExpressionType type;
            while (true)
            {
                switch (ahead.Type)
                {
                    case TokenType.Less:
                        Expect(TokenType.Less);
                        type = TryExpect(TokenType.Equal) ? ExpressionType.LessThanOrEqual : ExpressionType.LessThan;
                        break;
                    case TokenType.Great:
                        Expect(TokenType.Great);
                        type = TryExpect(TokenType.Equal) ? ExpressionType.GreaterThanOrEqual : ExpressionType.GreaterThan;
                        break;
                    default:
                        return expr;
                }

                var opt = AdditiveExpressionRule();
                expr = Expression.Dynamic(runtime.GetBinaryCallSite(type), typeof(Any), expr, opt);
            }
        }

        private Expression AdditiveExpressionRule()
        {
            var expr = MultiplicativeExpressionRule();
            if (expr == null) return null;
            
            ExpressionType type;
            while (true)
            {
                switch (ahead.Type)
                {
                    case TokenType.Add:
                        Expect(TokenType.Add);
                        type = ExpressionType.Add;
                        break;
                    case TokenType.Sub:
                        Expect(TokenType.Sub);
                        type = ExpressionType.Subtract;
                        break;
                    default:
                        return expr;
                }
                
                var opt = MultiplicativeExpressionRule();
                expr = Expression.Dynamic(runtime.GetBinaryCallSite(type), typeof(Any), expr, opt);                
            }
        }

        private Expression MultiplicativeExpressionRule()
        {
            var expr = UnaryExpressionRule();
            if (expr == null) return null;

            ExpressionType type;
            while (true)
            {
                switch (ahead.Type)
                {
                    case TokenType.Mul:
                        Expect(TokenType.Mul);
                        type = ExpressionType.Multiply;
                        break;
                    case TokenType.Div:
                        Expect(TokenType.Div);
                        type = ExpressionType.Divide;
                        break;
                    default:
                        return expr;
                }

                var opt = UnaryExpressionRule();
                expr = Expression.Dynamic(runtime.GetBinaryCallSite(type), typeof(Any), expr, opt);
            }
        }

        private Expression UnaryExpressionRule()
        {
            Expression expr = null;
            ExpressionType type;
            while (true)
            {
                switch (ahead.Type)
                {
                    case TokenType.Sub:
                        Expect(TokenType.Sub);
                        type = ExpressionType.Negate;
                        break;
                    default:
                        return expr ?? PostfixExpressionRule();
                }

                var opt = UnaryExpressionRule();
                expr = Expression.Dynamic(runtime.GetUnaryCallSite(type), typeof(Any), opt);
            }
        }

        private Expression PostfixExpressionRule()
        {
            var expr = PrimaryExpressionRule();
            if (expr == null) return null;
            
            while (true)
            {
                switch (ahead.Type)
                {
                    case TokenType.Dot:
                        {
                            Expect(TokenType.Dot);
                            Expect(TokenType.Member);

                            expr = Expression.Dynamic(runtime.GetMemberCallSite(token.Value), typeof(Any), expr);
                            break;
                        }
                    case TokenType.LParen:
                        {
                            var args = new List<Expression> { expr };
                            Expect(TokenType.LParen);
                            args.Add(Expression.New(Args.New, Expression.NewArrayInit(typeof(Any), ExpressionListRule())));
                            
                            Expect(TokenType.RParen);
                            expr = Expression.Dynamic(runtime.InvokeCallSite, typeof(Any), args);
                            break;
                        }
                    case TokenType.LBrack:
                        {
                            var args = new List<Expression> { expr };
                            Expect(TokenType.LBrack);
                            args.Add(Expression.New(Args.New, Expression.NewArrayInit(typeof(Any), ExpressionListRule())));
                            Expect(TokenType.RBrack);
                            
                            expr = Expression.Dynamic(runtime.GetIndexCallSite, typeof(Any), args);
                            break;
                        }
                    default:
                        return expr;
                }
            }
        }

        private Expression PrimaryExpressionRule()
        {
            switch (ahead.Type)
            {
                case TokenType.None:
                    return NoneExpressionRule();
                case TokenType.True:
                    return TrueExpressionRule();
                case TokenType.False:
                    return FalseExpressionRule();
                case TokenType.Member:
                    return MemberExpressionRule();
                case TokenType.Number:
                    return NumberExpressionRule();
                case TokenType.String:
                    return StringExpressionRule();
                case TokenType.LParen:
                    {
                        Expect(TokenType.LParen);
                        var expr = ExpressionRule();
                        Expect(TokenType.RParen);
                        return expr;
                    }
                case TokenType.LBrack:
                    return ListExpressionRule();
                case TokenType.This:
                    return ThisExpressionRule();
                default:
                    return null;
            }
        }

        private Expression NoneExpressionRule()
        {
            Expect(TokenType.None);
            return Expression.Constant(Any.None, typeof(Any));
        }

        private Expression TrueExpressionRule()
        {
            Expect(TokenType.True);
            return Expression.Constant(Bool.True, typeof(Any));
        }

        private Expression FalseExpressionRule()
        {
            Expect(TokenType.False);
            return Expression.Constant(Bool.False, typeof(Any));
        }

        private Expression MemberExpressionRule()
        {
            Expect(TokenType.Member);
            var member = scopeManager[token.Value];

            if (member != null)
            {
                return member;
            }
            
            if (runtime.BuiltIns.TryGetValue(token.Value, out member))
            {
                return member;
            }
            
            throw new Exception();
        }

        private IEnumerable<Expression> MemberExpressionListRule()
        {
            var expr = MemberExpressionRule();
            if (expr == null) yield break;

            yield return expr;

            while (ahead.Type == TokenType.Comma)
            {
                Expect(TokenType.Comma);

                expr = MemberExpressionRule();
                if (expr == null)
                {
                    ExpectError(ahead.Type);
                }

                yield return expr;
            }
        }

        private Expression NumberExpressionRule()
        {
            Expect(TokenType.Number);
            var start = double.Parse(token.Value);

            if (!TryExpect(TokenType.Range))
            {
                return Expression.Constant(new Number(start), typeof(Any));
            }

            Expect(TokenType.Number);
            var end = double.Parse(token.Value);

            return Expression.Constant(Iter.Range(start, end), typeof(Any));
        }

        private Expression StringExpressionRule()
        {
            Expect(TokenType.String);
            return Expression.Constant(new String(token.Value.Substring(1, token.Value.Length - 2)));
        }

        private Expression ListExpressionRule()
        {
            Expect(TokenType.LBrack);

            var values = ExpressionListRule().ToList();

            Expression listExpr;
            
            if (values.Count > 0)
            {
                listExpr = Expression.ListInit(Expression.New(List.New),
                    from expr
                    in values
                    select Expression.ElementInit(List.Init, expr));
            }
            else
            {
                listExpr = Expression.New(List.New);
            }
            
            
            Expect(TokenType.RBrack);
            return listExpr;
        }

        private Expression ThisExpressionRule()
        {
            Expect(TokenType.This);
            var member = scopeManager["__this"];

            if (member != null)
            {
                return member;
            }

            throw new Exception();
        }

        private void Get()
        {
            token = ahead;
            ahead = scanner.Scan();
        }

        private void Expect(TokenType type)
        {
            if (ahead.Type == type) Get(); else ExpectError(type);
        }

        private bool TryExpect(TokenType type)
        {
            if (ahead.Type != type) return false;
            
            Get();
            return true;
        }

        private void ExpectError(TokenType type)
        {
            throw new ArgumentException(type.ToString());
        }
    }
}
