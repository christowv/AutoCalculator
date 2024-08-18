using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.IExecute
{
    interface IExecute
    {
        Fraction Execute(VariableContext context);
    }
}
