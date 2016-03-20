using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringEvaluator
{
    public class RpnSolver : IAlgorithm
    {
        private Stack<decimal> stack;

        private List<string> result;

        public IList<string> Execute(IList<string> expression)
        {
            stack = new Stack<decimal>();
            result = new List<string>();

            foreach (string token in expression)
            {
                var valueMatch = Regex.Match(token, @"\d+");
                var isValue = valueMatch.Success;

                var operatorMatch = Regex.Match(token, @"[\+\-\*\/\^]");
                var isOperator = operatorMatch.Success;

                if (isValue)
                {
                    stack.Push(Decimal.Parse(token));
                }

                else if (isOperator)
                {
                    decimal A = stack.Pop();
                    decimal B = stack.Pop();

                    if (token == "+") stack.Push(A + B);
                    else if (token == "-") stack.Push(B - A);
                    else if (token == "*") stack.Push(A * B);
                    else if (token == "/") stack.Push(B / A);
                    else if (token == "^") stack.Push((decimal)Math.Pow((double)B, (double)A));
                }
            }

            result.Add(stack.Pop().ToString());

            return result;
        }

        public RpnSolver()
        {
        }

    }
}
