using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.Expression
{
    class SquareRoot : IExpression
    {
        IExpression left;

        public SquareRoot(IExpression left)
        {
            this.left = left;
        }

        public Fraction Execute(VariableContext context)
        {
            return new Fraction(Math.Sqrt(left.Execute(context)));
        }
    }
}
