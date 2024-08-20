using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.Expression
{
    class SquareRoot : IExpression
    {
        IExpression value;

        public SquareRoot(IExpression a)
        {
            this.value = a;
        }

        public Fraction Execute(VariableContext context)
        {
            return new Fraction(Math.Sqrt(value.Execute(context)));
        }

        public override string ToString()
        {
            return $"sqrt({value})";
        }
    }
}
