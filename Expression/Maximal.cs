using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.Expression
{
    class Maximal : IExpression
    {
        IExpression[] args;

        public Maximal(params IExpression[] a)
        {
            args = a;
        }

        public Fraction Execute(VariableContext context)
        {
            Fraction max = args[0].Execute(context);

            for (int i = 1; i < args.Length; i++)
            {
                if (max < args[i].Execute(context))
                {
                    max = args[i].Execute(context);
                }
            }

            return max;
        }

        public override string ToString()
        {
            string list = ExpressionString.ListArgs(args);
            return $"max({list})";
        }
    }
}
