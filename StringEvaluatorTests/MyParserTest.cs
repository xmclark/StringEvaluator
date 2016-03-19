using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringEvaluator;
using System.Text.RegularExpressions;

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
    
    [TestClass]
    public class MyParserTest
    {
        [TestMethod]
        public void ParseInfixExpression_ExpectGoodResults_GetCorrectPostfixNotationOut()
        {
            var expression1 = new List<string>();
            var expected1 = new List<string>() { { "5" }, { "3" }, { "2" }, { "/" }, { "+" } };
            expression1.Add("5");
            expression1.Add("+");
            expression1.Add("3");
            expression1.Add("/");
            expression1.Add("2");

            var expression2 = new List<string>();
            var expected2 = new List<string>() { { "5" }, { "3" }, { "/" }, { "2" }, { "+" } };
            expression2.Add("5");
            expression2.Add("/");
            expression2.Add("3");
            expression2.Add("+");
            expression2.Add("2");

            var myParser = new MyParser();
            var result1 = new List<string>(myParser.Execute(expression1));
            var result2 = new List<string>(myParser.Execute(expression2));
            
            Assert.IsTrue(ShallowListEquality.Test(expected1, result1));
            Assert.IsTrue(ShallowListEquality.Test(expected2, result2));
        }

        [TestMethod]
        public void ParseInfixExpression_AppropriateResultsWithParenthesis()
        {
            var expression3 = new List<string>();
            var expected3 = new List<string>() { { "1" }, { "2" }, { "3" }, { "*" }, { "+" } };
            
            expression3.Add("1");
            expression3.Add("+");
            expression3.Add("(");
            expression3.Add("2");
            expression3.Add("*");
            expression3.Add("3");
            expression3.Add(")");

            var expression4 = new List<string>();
            var expected4 = new List<string>() { { "1" }, { "2" }, { "+" }, { "3" }, { "*" } };
            expression4.Add("(");
            expression4.Add("1");
            expression4.Add("+");
            expression4.Add("2");
            expression4.Add(")");
            expression4.Add("*");
            expression4.Add("3");

            var myParser = new MyParser();
            var result3 = new List<string>(myParser.Execute(expression3));
            var result4 = new List<string>(myParser.Execute(expression4));

            Assert.IsTrue(ShallowListEquality.Test(expected3, result3));
            Assert.IsTrue(ShallowListEquality.Test(expected4, result4));
        }
    }
}
