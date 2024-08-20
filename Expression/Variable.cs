using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator.Expression
{
    class Variable : IExpression
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

        public override string ToString()
        {
            return $"{ch}";
        }
    }
}
