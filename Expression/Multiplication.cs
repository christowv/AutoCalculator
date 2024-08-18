using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.Expression
{
    class Multiplication : IExpression
    {
        IExpression left;
        IExpression right;

        public Multiplication(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public Fraction Execute(VariableContext context)
        {
            return left.Execute(context) * right.Execute(context);
        }
    }
}
