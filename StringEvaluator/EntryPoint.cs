using System;

namespace StringEvaluator
{
    class EntryPoint
    {
        static void Main()
        {
            var solver = new RpnSolver();
            var validator = new ExpressionValidator();
            var parser = new MyParser();

            var evaluator = new Evaluator(validator, parser, solver);

            Console.WriteLine(evaluator.Evaluate("1+2"));
            Console.WriteLine(evaluator.Evaluate("(1+2)*3+(4/7)"));
            Console.WriteLine(evaluator.Evaluate("(58)/12"));

        }
    }
}
