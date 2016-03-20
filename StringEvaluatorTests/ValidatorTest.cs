using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringEvaluator;
using System.Collections.Generic;

namespace StringEvaluatorTests
{
    

    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void ValidateString_NormalExpression_PassValidation()
        {
            var myValidator = new ExpressionValidator();

            var input1 = new List<string>();
            input1.Add("1");
            input1.Add("+");
            input1.Add("3");
            input1.Add("*");
            input1.Add("5");
            
            var result1 = new List<string>(myValidator.Execute(input1));

            Assert.IsTrue(ShallowListEquality.Test(result1, input1));
        }

        [TestMethod]
        public void ValidateString_ExpressionWithParenthesis_PassValidation()
        {
            var myValidator = new ExpressionValidator();

            var input2 = new List<string>();
            input2.Add("(");
            input2.Add("1");
            input2.Add("*");
            input2.Add("2");
            input2.Add(")");
            input2.Add("^");
            input2.Add("5");
            
            var result2 = new List<string>(myValidator.Execute(input2));
            
            Assert.IsTrue(ShallowListEquality.Test(result2, input2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidateString_MisplacedParenthesis1_ThrowError()
        {
            var myValidator = new ExpressionValidator();

            var input3 = new List<string>();
            input3.Add("1");
            input3.Add("+");
            input3.Add("3");
            input3.Add("*");
            input3.Add(")"); // <- misplaced paren is here

            var result3 = myValidator.Execute(input3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidateString_MisplacedParenthesis2_ThrowError()
        {
            var myValidator = new ExpressionValidator();


            var input4 = new List<string>();
            input4.Add("1");
            input4.Add("+");
            input4.Add("("); // <- misplaced paren is here
            input4.Add("*");
            input4.Add("5");

            var result4 = myValidator.Execute(input4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidateString_WrongNumberOfParenthesis_ThrowError()
        {
            var myValidator = new ExpressionValidator();

            var input5 = new List<string>();
            input5.Add("(");
            input5.Add("5");
            input5.Add("+");
            input5.Add("2");
            input5.Add(")");
            input5.Add(")"); // <- Bad
            
            var result5 = myValidator.Execute(input5);
        }
    }
}
