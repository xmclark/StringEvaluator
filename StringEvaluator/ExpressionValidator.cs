using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace StringEvaluator
{
    public class ExpressionValidator : IAlgorithm
    {
        public IList<string> Execute(IList<string> expression)
        {
            var leftParens =
                from token in expression
                where token == "("
                select token;

            var rightParens =
                from token in expression
                where token == ")"
                select token;

            var numberLeftParens = new List<string>(leftParens);
            var numberRightParens = new List<string>(rightParens);

            if (numberLeftParens.Count != numberRightParens.Count)
                throw new ArgumentOutOfRangeException("Invalid number of parenthesis");
            
            for (var i = 0; i < expression.Count; i++)
            {
                if (Regex.Match(expression[i], @"\+\-\^\*\\").Success)
                {
                    if (Regex.Match(expression[i - 1], @"[\)\d]").Success || expression[i-1] == null)
                    {
                        if (Regex.Match(expression[i + 1], @"[\(\d]").Success || expression[i + 1] == null)
                        {
                            break;
                        }
                        else
                            throw new ArgumentOutOfRangeException("Missing right operand");
                    }
                    else
                        throw new ArgumentOutOfRangeException("Missing left operand");
                }
            }

            return expression;
        }
    }
}
