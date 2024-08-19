using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.Expression
{
    class Minimal : IExpression
    {
        IExpression[] args;

        public Minimal(params IExpression[] a)
        {
            args = a;
        }

        public Fraction Execute(VariableContext context)
        {
            Fraction min = args[0].Execute(context);

            for (int i = 1; i < args.Length; i++)
            {
                if (min > args[i].Execute(context))
                {
                    min = args[i].Execute(context);
                }
            }

            return min;
        }
    }
}
