using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringEvaluator
{
    public interface IAlgorithm
    {
        IList<string> Execute(IList<string> expression);
    }
}
