using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.IExecute
{
    class Subtraction : IExecute
    {
        IExecute left;
        IExecute right;

        public Subtraction(IExecute left, IExecute right)
        {
            this.left = left;
            this.right = right;
        }

        public Fraction Execute(VariableContext context)
        {
            return left.Execute(context) - right.Execute(context);
        }
    }
}
