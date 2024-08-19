using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCalculator.Expression;
using AutoCalculator.Trees;

namespace AutoCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VariableContext context = new VariableContext();
            context.SetVariable('x', 2);

            ExpressionBuilder builder = new ExpressionBuilder();
            IExpression exp = builder.GetTree("2+2*pow(x, 1)");

            Console.WriteLine((double) exp.Execute(context));

            Console.ReadKey();
        }
    }
}
