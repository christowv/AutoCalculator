using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.Expression
{
    class Addition : IExpression
    {
        IExpression left;
        IExpression right;

        public Addition(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public Fraction Execute(VariableContext context)
        {
            return left.Execute(context) + right.Execute(context);
        }

        public override string ToString()
        {
            return $"{left}+{right}";
        }
    }
}
