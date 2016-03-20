using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringEvaluatorTests
{
    public class ShallowListEquality
    {
        public static bool Test(List<string> List1, List<string> List2)
        {
            if (List1.Count == List2.Count)
            {
                for (int i = 0; i < List1.Count; i++)
                {
                    if (List1[i] != List2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
