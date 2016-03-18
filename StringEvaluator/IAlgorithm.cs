using System.Collections.Generic;

namespace StringEvaluator
{
    public interface IAlgorithm
    {
        IList<string> Execute(IList<string> expression);
    }
}
