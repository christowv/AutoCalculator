using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoCalculator.Expression;
using static System.Net.Mime.MediaTypeNames;

namespace AutoCalculator.Trees
{
    class ExpressionBuilder
    {
        private string[] tokens;
        private int pos;

        public ExpressionBuilder()
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

            while (IsOutLength())
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

            while (IsOutLength()) {
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

            if (func == "pow")
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
                IExpression[] args = GetArgs(2);

                string closingBracket;
                if (IsOutLength())
                {
                    closingBracket = tokens[pos];
                }
                else
                {
                    throw new Exception("No end expression");
                }

                if (IsOutLength() && closingBracket == ")")
                {
                    pos++;
                    return new Power(args[0], args[1]);
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
            string next = tokens[pos];
            IExpression result = null;

            if (next == "(")
            {
                pos++;

                result = GetExpression();
                string closingBracket;
                if (IsOutLength())
                {
                    closingBracket = tokens[pos];
                }
                else
                {
                    throw new Exception("No end expression");
                }

                if (!IsOutLength() && closingBracket == ")")
                {
                    pos++;
                    return result;
                }
                else
                {
                    throw new Exception("End is not a bracket");
                }
            }
            pos++;

            if(next.Length == 1 && Char.IsLetter(next[0]))
            {
                return new Variable(next[0]);
            }

            return GetFraction(next);
        }

        private bool IsOutLength()
        {
            return pos < tokens.Length;
        }

        private IExpression[] GetArgs(int n)
        {
            IExpression[] args = new IExpression[n];

            for (int i = 0; i < n; i++)
            {
                args[i] = GetExpression();

                if (i < args.Length - 1)
                {
                    if (tokens[pos] == ",")
                    {
                        pos++;
                    }
                    else
                    {
                        throw new Exception("No comma");
                    }
                }
            }

            return args;
        }

        private Fraction GetFraction(string value)
        {
            if (Regex.IsMatch(value, @"^[0-9]+.[0-9]+$"))
            {
                string[] pieces = value.Replace('.', ',').Split(',');
                double d = double.Parse(value);

                return new Fraction(d);
            }
            else if (Regex.IsMatch(value, @"^[0-9]+/[0-9]+$"))
            {
                string[] pieces = value.Split('/');
                int a = int.Parse(pieces[0]);
                int b = int.Parse(pieces[1]);

                return new Fraction(a, b);
            }
            else if (Regex.IsMatch(value, @"^[0-9]+$"))
            {
                int a = int.Parse(value);

                return new Fraction(a);
            }
            else
            {
                throw new Exception("Unknown number format");
            }
        }
    }
}
