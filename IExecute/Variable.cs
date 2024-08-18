using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.IExecute
{
    class Variable : IExecute
    {
        char ch;

        public Variable(char ch)
        {
            this.ch = ch;
        }

        public Fraction Execute(VariableContext context)
        {
            return context.GetVariable(ch);
        }
    }
}
