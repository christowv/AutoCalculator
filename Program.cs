﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCalculator.Expression;
using AutoCalculator.ExpressionBuilder;

namespace AutoCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VariableContext context = new VariableContext();
            context.SetVariable('x', 2);

            RecursiveDescentBuilder builder = new RecursiveDescentBuilder();
            IExpression exp = builder.GetTree("((2+2-3)*2:3+pow(2,2)):2");

            Console.WriteLine($"{exp} = {exp.Execute(context).Simplify()}");

            Console.ReadKey();
        }
    }
}
