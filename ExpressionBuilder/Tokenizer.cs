using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoCalculator.Trees
{
    static class Tokenizer
    {
        static public string[] GetTokens(string exp)
        {
            List<string> tokenized = new List<string>();

            exp = exp.ToLower().Replace(" ", "");

            for (int i = 0; i < exp.Length; i++)
            {
                char ch = exp[i];
                string token = null;

                if (IsDigit(ch))
                {
                    token = CaptureNumber(exp, ref i);
                }
                else if (IsLetter(ch))
                {
                    token = CaptureWord(exp, ref i);
                }
                else if (IsComma(ch))
                {
                    token = ",";
                }
                else if (IsLeftBracket(ch))
                {
                    token = "(";
                }
                else if (IsRightBracket(ch))
                {
                    token = ")";
                }
                else if (IsOperator(ch))
                {
                    token = ch.ToString();
                }
                else
                {
                    throw new Exception("Unknown token");
                }

                tokenized.Add(token);
            }

            return tokenized.ToArray();
        }

        static private string CaptureNumber(string expr, ref int i)
        {
            string number = "" + expr[i];

            while (i + 1 < expr.Length && (IsDigit(expr[i + 1]) || expr[i + 1] == '.' || expr[i + 1] == '/'))
            {
                number += expr[++i];
            }

            return number;
        }

        static private string CaptureWord(string expr, ref int i)
        {
            string letter = "" + expr[i];

            while (i + 1 < expr.Length && IsLetter(expr[i + 1]))
            {
                letter += expr[++i];
            }

            return letter;
        }

        static private bool IsDigit(char ch)
        {
            return Regex.IsMatch(ch.ToString(), @"[0-9]");
        }

        static private bool IsLetter(char ch)
        {
            return Regex.IsMatch(ch.ToString(), @"[a-z]");
        }

        static private bool IsOperator(char ch)
        {
            return Regex.IsMatch(ch.ToString(), @"[+\-\*\:\=]");
        }

        static private bool IsComma(char ch)
        {
            return ch == ',';
        }

        static private bool IsLeftBracket(char ch)
        {
            return ch == '(';
        }

        static private bool IsRightBracket(char ch)
        {
            return ch == ')';
        }
    }
}
