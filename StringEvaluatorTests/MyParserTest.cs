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
        public void ParseInfixExpression_ExpectBadResult_GetError()
        {
            var expression1 = new List<string>();
            var expected1 = new List<string>() { { "5" }, { "2" }, { "3" }, { "/" }, { "+" } };
            expression1.Add("5");
            expression1.Add("+");
            expression1.Add("3");
            expression1.Add("/");
            expression1.Add("2");

            var expression2 = new List<string>();
            var expected2 = new List<string>() { { "5" }, { "3" }, { "+" }, { "2" }, { "/" } };
            expression2.Add("5");
            expression2.Add("/");
            expression2.Add("3");
            expression2.Add("+");
            expression2.Add("2");

            var myParser = new MyParser();
            var result1 = new List<string>(myParser.Execute(expression1));
            var result2 = new List<string>(myParser.Execute(expression2));

            Assert.IsFalse(ShallowListEquality.Test(expected1, result1));
            Assert.IsFalse(ShallowListEquality.Test(expected2, result2));
        }
    }
}
