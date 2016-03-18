using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringEvaluator
{
    public class Evaluator : IEvaluator
    {
        private IAlgorithm parser;
        private IAlgorithm solver;
        private IAlgorithm cleaner;

        public Evaluator(IAlgorithm cleaner, IAlgorithm parser, IAlgorithm solver)
        {
            this.parser = parser;
            this.solver = solver;
            this.cleaner = cleaner;
        }

        public virtual string Evaluate(string expression)
        {
            IList<string> tokens = Regex.Matches(expression, @"([A-Za-z])+|([0-9])+|([\\\]\[\/\.\,\!\@\#\$\%\^\&\*\+\-]){1}")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList<string>();

            var result = solver.Execute(parser.Execute(cleaner.Execute(tokens)));

            StringBuilder resultString = new StringBuilder();
            foreach (string token in result)
            {
                resultString.Append(token);
            }

            return resultString.ToString();
        }
    }
}
