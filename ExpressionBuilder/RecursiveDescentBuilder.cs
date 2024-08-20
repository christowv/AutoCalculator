using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoCalculator.Expression;

namespace AutoCalculator.ExpressionBuilder
{
    class RecursiveDescentBuilder
    {
        private string[] tokens;
        private int pos;

        public RecursiveDescentBuilder()
        {

        }

        public IExpression GetTree(string exp)
        {
            tokens = Tokenizer.GetTokens(exp);
            pos = 0;

            IExpression result = GetExpression();

            return result;
        }

        // E -> T+- ... +-T
        private IExpression GetExpression()
        {
            IExpression a = GetTerm();

            while (!IsOutLength())
            {
                string oper = tokens[pos];

                if (oper == "+" || oper == "-")
                {
                    pos++;
                }
                else
                {
                    break;
                }

                IExpression b = GetTerm();

                if (oper == "+")
                {
                    a = new Addition(a, b);
                }
                else
                {
                    a = new Subtraction(a, b);
                }
            }

            return a;
        }

        // T -> A*/ ... */A
        private IExpression GetTerm()
        {
            IExpression a = GetAbc();

            while (!IsOutLength()) {
                string oper = tokens[pos];

                if (oper == "*" || oper == ":")
                {
                    pos++;
                }
                else
                {
                    break;
                }

                IExpression b = GetAbc();

                if (oper == "*")
                {
                    a = new Multiplication(a, b);
                }
                else
                {
                    a = new Division(a, b);
                }
            }

            return a;
        }

        // A -> abc(E,E ... ,E)
        private IExpression GetAbc()
        {
            string func = tokens[pos];

            if (func == "pow" || func == "sqrt" || func == "max" || func == "min")
            {
                pos++;
            }
            else
            {
                return GetFactor();
            }

            string next = tokens[pos];

            if (next == "(")
            {
                pos++;
                IExpression[] args = GetArgs();

                string closingBracket;
                if (!IsOutLength())
                {
                    closingBracket = tokens[pos];
                }
                else
                {
                    throw new Exception("No end expression");
                }

                if (closingBracket == ")")
                {
                    pos++;

                    if (func == "pow")
                    {
                        return new Power(args[0], args[1]);
                    }
                    else if (func == "sqrt")
                    {
                        return new SquareRoot(args[0]);
                    }
                    else if (func == "max")
                    {
                        return new Maximal(args);
                    }
                    else if (func == "min")
                    {
                        return new Minimal(args);
                    }
                }
                else
                {
                    throw new Exception("End is not a bracket");
                }
            }

            throw new Exception("Need bracket after function");
        }

        // F -> N | (E)
        private IExpression GetFactor()
        {
            bool IsMinus = false;

            if (tokens[pos] == "-")
            {
                IsMinus = true;
                pos++;
            }

            string next = tokens[pos];
            IExpression result;

            if (next == "(")
            {
                pos++;

                result = GetExpression();
                string closingBracket;
                if (!IsOutLength())
                {
                    closingBracket = tokens[pos];
                }
                else
                {
                    throw new Exception("No end expression");
                }

                if (IsOutLength() || closingBracket != ")")
                {
                    throw new Exception("End is not a bracket");
                }
            }
            else if(next.Length == 1 && Char.IsLetter(next[0]))
            {
                result = new Variable(next[0]);
            }
            else
            {
                string[] splitted = next.Split('/');
                int numerator = Convert.ToInt32(splitted[0]);
                int denominator = Convert.ToInt32(splitted[1]);

                result = new Fraction(numerator, denominator);
            }
            pos++;

            return SetUnarMinus(result, IsMinus);
        }

        private bool IsOutLength()
        {
            return pos > tokens.Length-1;
        }

        private IExpression SetUnarMinus(IExpression exp, bool IsMinus)
        {
            if (IsMinus)
                return new Multiplication(exp, new Fraction(-1));

            return exp;
        }

        private IExpression[] GetArgs()
        {
            List<IExpression> args = new List<IExpression>();

            while(!IsOutLength())
            {
                args.Add(GetExpression());

                string next;
                if (!IsOutLength())
                {
                   next = tokens[pos];
                }
                else
                {
                    break;
                }

                if (!IsOutLength() && next == ",")
                {
                    pos++;
                }
                else
                {
                    break;
                }
            }

            return args.ToArray();
        }
    }
}
