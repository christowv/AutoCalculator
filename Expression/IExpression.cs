using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.Expression
{
    interface IExpression
    {
        Fraction Execute(VariableContext context);
        string ToString();
    }
}
