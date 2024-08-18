using AutoCalculator.IExecute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VariableContext context = new VariableContext();
            context.SetVariable('x', 2);

            IExecute.IExecute expr = new Addition(new Variable('x'), new Subtraction(new Fraction(5), new Fraction(3)));

            Console.WriteLine(expr.Execute(context));

            Console.ReadKey();
        }
    }
}
