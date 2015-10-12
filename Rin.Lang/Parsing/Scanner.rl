%%{

machine scan;

action var_token {
    token = Create(TokenType.Var);
    fbreak;
}

action let_token {
    token = Create(TokenType.Let);
    fbreak;
}

action and_token {
    token = Create(TokenType.And);
    fbreak;
}

action or_token {
    token = Create(TokenType.Or);
    fbreak;
}

action as_token {
    token = Create(TokenType.As);
    fbreak;
}

action of_token {
    token = Create(TokenType.Of);
    fbreak;
}

action func_token {
    token = Create(TokenType.Func);
    fbreak;
}

action class_token {
    token = Create(TokenType.Class);
    fbreak;
}

action this_token {
    token = Create(TokenType.This);
    fbreak;
}

action return_token {
    token = Create(TokenType.Return);
    fbreak;
}

action pass_token {
    token = Create(TokenType.Pass);
    fbreak;
}

action if_token {
    token = Create(TokenType.If);
    fbreak;
}

action for_token {
    token = Create(TokenType.For);
    fbreak;
}

action in_token {
    token = Create(TokenType.In);
    fbreak;
}

action while_token {
    token = Create(TokenType.While);
    fbreak;
}

action break_token {
    token = Create(TokenType.Break);
    fbreak;
}

action elif_token {
    token = Create(TokenType.Elif);
    fbreak;
}

action else_token {
    token = Create(TokenType.Else);
    fbreak;
}

action none_token {
    token = Create(TokenType.None);
    fbreak;
}

action true_token {
    token = Create(TokenType.True);
    fbreak;
}

action false_token {
    token = Create(TokenType.False);
    fbreak;
}

action member_token {
    token = Create(TokenType.Member);
    fbreak;
}

action number_token {
    token = Create(TokenType.Number);
    fbreak;
}

action string_token {
    token = Create(TokenType.String);
    fbreak;
}

action comment_token {
    token = Create(TokenType.Comment);
    fbreak;
}

action ws_token {
    token = Create(TokenType.Ws);
    fbreak;
}

action eol_token {
    token = Create(TokenType.Eol);
    fbreak;
}

action add_token {
    token = Create(TokenType.Add);
    fbreak;
}

action sub_token {
    token = Create(TokenType.Sub);
    fbreak;
}

action mul_token {
    token = Create(TokenType.Mul);
    fbreak;
}

action div_token {
    token = Create(TokenType.Div);
    fbreak;
}

action lparen_token {
    token = Create(TokenType.LParen);
    fbreak;
}

action rparen_token {
    token = Create(TokenType.RParen);
    fbreak;
}

action lbrack_token {
    token = Create(TokenType.LBrack);
    fbreak;
}

action rbrack_token {
    token = Create(TokenType.RBrack);
    fbreak;
}

action range_token {
    token = Create(TokenType.Range);
    fbreak;
}

action dot_token {
    token = Create(TokenType.Dot);
    fbreak;
}

action comma_token {
    token = Create(TokenType.Comma);
    fbreak;
}

action colon_token {
    token = Create(TokenType.Colon);
    fbreak;
}

action semiColon_token {
    token = Create(TokenType.SemiColon);
    fbreak;
}

action equal_token {
    token = Create(TokenType.Equal);
    fbreak;
}

action less_token {
    token = Create(TokenType.Less);
    fbreak;
}

action great_token {
    token = Create(TokenType.Great);
    fbreak;
}

U1 = [a-zA-Z_];
U2 = 0xC2..0xDF 0x80..0xBF;
U3 = 0xE0 0xA0..0xBF 0x80..0xBF;
U4 = 0xE1..0xEC 0x80..0xBF 0x80..0xBF;
U5 = 0xED 0x80..0x9F 0x80..0xBF;
U6 = 0xEE..0xEF 0x80..0xBF 0x80..0xBF;
U7 = 0xF0 0x90..0xBF 0x80..0xBF 0x80..0xBF;
U8 = 0xF1..0xF3 0x80..0xBF 0x80..0xBF 0x80..0xBF;
U9 = 0xF4 0x80..0x8F 0x80..0xBF 0x80..0xBF;

letter = U1 | U2 | U3 | U4 | U5 | U6 | U7 | U8 | U9;
U = 0x0..0xFF | U2 | U3 | U4 | U5 | U6 | U7 | U8 | U9;

comment = '#' [^\r\n]*;

add = "+";
sub = "-";
mul = "*";
div = "/";
lparen = "(";
rparen = ")";
lbrack = "[";
rbrack = "]";
range = "..";
dot = ".";
comma = ",";
colon = ":";
semiColon = ";";
equal = "=";
less = "<";
great = ">";

var = "var";
let = "let";
and = "and";
or = "or";
as = "as";
of = "of";
func = "func";
class = "class";
this = "this";
return = "return";
pass = "pass";

if = "if";
for = "for";
in = "in";
while = "while";
break = "break";
elif = "elif";
else = "else";

ws = [ \t]+;
eol = (('\r'? '\n') | '\r');

none = "none";
true = "true";
false = "false";

member = letter (letter | digit)*;




nonzerodigit = "1".."9";
octdigit = "0".."7";
bindigit = "0" | "1";
hexdigit = digit | "a".."f" | "A".."F";
octinteger = "0" ("o" | "O") octdigit+ | "0" octdigit+;
hexinteger = "0" ("x" | "X") hexdigit+;
bininteger = "0" ("b" | "B") bindigit+;

intpart = digit+;
fraction = dot digit+;
exponent = ("e" | "E") ("+" | "-")? digit+;
real = ((intpart fraction?) | (intpart? fraction)) exponent?;

number = real | octinteger | hexinteger | bininteger;

stringprefix = "r" | "u" | "ur" | "R" | "U" | "UR" | "Ur" | "uR" | "b" | "B" | "br" | "Br" | "bR" | "BR";

escapeseq = "\\" ascii;

shortstringitem1 = [^\\'\r\n] | escapeseq;
shortstringitem2 = [^\\"\r\n] | escapeseq;
longstringitem = [^\\] | escapeseq | eol;
unused = ['];

shortstring = "'" shortstringitem1* "'" | '"' shortstringitem2* '"';
longstring = "'''" longstringitem* "'''" | '"""' longstringitem* '"""';

string = stringprefix? (shortstring | longstring);



main := |*
    var => var_token;
    let => let_token;
    and => and_token;
    or => or_token;
    as => as_token;
    of => of_token;
    func => func_token;
    class => class_token;
    this => this_token;
    return => return_token;
    pass => pass_token;

    if => if_token;
    for => for_token;
    in => in_token;
    while => while_token;
    break => break_token;
    elif => elif_token;
    else => else_token;
    
    none => none_token;
    true => true_token;
    false => false_token;

    member => member_token;
    number => number_token;
    string => string_token;
    comment => comment_token;

    add => add_token;
    sub => sub_token;
    mul => mul_token;
    div => div_token;
    lparen => lparen_token;
    rparen => rparen_token;
    lbrack => lbrack_token;
    rbrack => rbrack_token;
    range => range_token;
    dot => dot_token;
    comma => comma_token;
    colon => colon_token;
    semiColon => semiColon_token;
    equal => equal_token;
    less => less_token;
    great => great_token;

    ws => ws_token;
    eol => eol_token;
*|;

}%%
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing
{
    public class Scanner
    {
%% write data;

        private readonly string data;

        private int cs;
        private int act;
        private int col;
        private int line;
        private int p;
        private int pe;
        private int eof;
        private int ts;
        private int te;
        private char it;
        private Stack<int> levels;
        private Queue<Token> pending;
        private bool finished;
        private int join;
        private Token last;
        private Token error;

        public Scanner(string data)
        {
            this.data = data;

            col = 1;
            line = 1;
            p = 0;
            pe = data.Length;
            eof = data.Length;
            it = (char)0;
            levels = new Stack<int>();
            pending = new Queue<Token>();
            finished = false;
            join = 0;

            last = Create(TokenType.Dummy);
            error = Create(TokenType.Dummy);
%% write init;
            
            levels.Push(0);
        }

        public Token Scan()
        {
            while (pending.Count == 0)
            {
                var token = Next();

                if (token == error)
                {
                    return null;
                }

                if (token == null)
                {
                    token = Create(TokenType.Eof);
                }

                switch (token.Type)
                {
                    case TokenType.Comment:
                    case TokenType.Ws:
                        break;
                    case TokenType.Eol:
                        {
                            var blankLine = last.Type == TokenType.Ws && last.Col == 1;
                            var commentLine = last.Type == TokenType.Comment && last.Col == 1;
                            if (token.Col > 1 && !blankLine && !commentLine && join == 0)
                            {
                                //Console.Write("[queued {0}]", token.Type);
                                pending.Enqueue(token);
                            }
                        }
                        break;
                    case TokenType.Eof:
                        {
                            if (last.Type != TokenType.Dummy && last.Type != TokenType.Eol)
                            {
                                var eol = Create(TokenType.Eol);
                                pending.Enqueue(eol);
                            }

                            while (levels.Peek() > 0) DoDedent(token);   

                            pending.Enqueue(token);
                        }
                        break;
                    case TokenType.LParen:
                    case TokenType.LBrack:
                        ++join;
                        pending.Enqueue(token);
                        break;
                    case TokenType.RParen:
                    case TokenType.RBrack:
                        --join;
                        pending.Enqueue(token);
                        break;                        
                    default:
                        {
                            if (last.Type == TokenType.Eol) 
                            {
                                while (levels.Peek() > 0) DoDedent(token);
                            } 
                            else if (last.Type == TokenType.Ws)
                            {                       
                                if (last.Col == 1 && join == 0)
                                {
                                    var level = last.Length;
                                    var curLevel = levels.Peek();
                                    if (level > curLevel)
                                    {         
                                        DoIndent(level, last);
                                    } 
                                    else if (level < curLevel)
                                    {
                                        do
                                        {
                                            DoDedent(token);
                                        }
                                        while (level < levels.Peek());
                                    }
                                }
                            }

                            pending.Enqueue(token);
                        }
                        break;
                }

                last = token;
            }

            return pending.Dequeue();
        }

        private void DoIndent(int level, Token source)
        {
            var s = source.Value;

            if (s.Distinct().Count() > 1)
            {
                Console.WriteLine("Indent not same!");
                throw new Exception();
            }

            if (it == 0)
            {
                it = s[0];
            }
            else if (it != s[0])
            {
                Console.WriteLine("Indent not same!");
                throw new Exception();
            }

            levels.Push(level);
            var ind = new Token();
            ind.Type = TokenType.Indent;
            ind.Col = source.Col;
            ind.Line = source.Line;
            pending.Enqueue(ind);
        }

        private void DoDedent(Token source)
        {
            levels.Pop();
            var ded = new Token();
            ded.Type = TokenType.Dedent;
            ded.Col = source.Col;
            ded.Line = source.Line;
            pending.Enqueue(ded);
        }

        private Token Next()
        {
            Token token = null;
            %% write exec;

            if (cs == scan_error && data[p] != 0)
            {
                Console.WriteLine("Unexpected {0}", data[p]);   
                token = error;  
            }

            return token;
        }

        private Token Create(TokenType type)
        {
            var token = new Token();
            token.Type = type;
            token.Col = col;
            token.Line = line;

            switch (type)
            {
                case TokenType.Dummy:
                case TokenType.Indent:
                case TokenType.Dedent:
                case TokenType.Eof:
                    token.Length = -1;
                    token.Size = -1;
                    break;
                case TokenType.Ws:
                    {
                        token.Value = GetTokenString(ts, te);
                        token.Length = token.Value.Length;
                        var width = 4;
                        foreach (var c in token.Value)
                        {
                            switch (c)
                            {
                            case ' ':
                                col++;
                                break;
                            case '\t':
                                col = ((col + width) & ~(width - 1)) + 1;
                                break;
                            }
                        }
                        break;
                    }
                case TokenType.Eol:
                    col = 1;
                    line++;
                    break;
                case TokenType.String:
                    token.Value = GetTokenString(ts, te);
                    token.Length = token.Value.Length;
                    for (var i = ts; i != te; i++)
                    {
                        switch (data[i])
                        {
                            case '\r':
                                col = 1;
                                line++;
                                if (data[i + 1] == '\n') i++;  
                                break;          
                            case '\n':
                                col = 1;
                                line++;
                                break;
                            default:
                                col++;  
                                break;  
                        }           
                    }
                    break;
                default:
                    token.Value = GetTokenString(ts, te);
                    token.Length = token.Value.Length;
                    col += token.Length;
                    break;
            }

            return token;
        }

        private string GetTokenString(int start, int end)
        {
            return data.Substring(start, end - start);
        }

    }
}

