using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.Expression
{
    class Power : IExpression
    {
        IExpression left;
        IExpression right;

        public Power(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public Fraction Execute(VariableContext context)
        {
            return new Fraction(Math.Pow(left.Execute(context), right.Execute(context)));
        }

        public override string ToString()
        {
            return $"pow({left}, {right})";
        }
    }
}
