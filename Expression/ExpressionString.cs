using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.Expression
{
    static class ExpressionString
    {
        static public string ListArgs(IExpression[] args)
        {
            string argString = "";

            for (int i = 0; i < args.Length; i++)
            {
                argString += $"{args[i]}";

                if (i < args.Length - 1)
                    argString += ",";
            }

            return argString;
        }

        static public string ShowSignificantBrackets(IExpression operation, IExpression arg)
        {
            if (operation is Multiplication || operation is Division)
            {
                if (arg is Addition || arg is Subtraction)
                    return $"({arg})";
            }

            return $"{arg}";
        }
    }
}
