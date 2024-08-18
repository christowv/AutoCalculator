using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoCalculator.Expression;

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

        // E -> T±T±T±T± ... ±T
        private IExpression GetExpression()
        {
            IExpression a = GetTerm();

            while (pos < tokens.Length)
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

        // T -> F*/F*/F*/*/ ... */F
        private IExpression GetTerm()
        {
            IExpression a = GetFactor();

            while (pos < tokens.Length) {
                string oper = tokens[pos];

                if (oper == "*" || oper == ":")
                {
                    pos++;
                }
                else
                {
                    break;
                }

                IExpression b = GetFactor();

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
                if (pos < tokens.Length)
                {
                    closingBracket = tokens[pos];
                }
                else
                {
                    throw new Exception("No end expression");
                }

                if (pos < tokens.Length && closingBracket == ")")
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

            return ParseFraction(next);
        }

        private Fraction ParseFraction(string value)
        {
            if (Regex.IsMatch(value, @"^[0-9]+,[0-9]+$"))
            {
                string[] pieces = value.Split(',');
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
