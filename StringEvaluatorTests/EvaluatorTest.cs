using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringEvaluator;
using System.Diagnostics;

namespace StringEvaluatorTests
{
    public class ParseOnlyOneElementAlgorithm : IAlgorithm
    {
        public IList<string> Execute(IList<string> expression)
        {
            return expression;
        }
    }

    public class EvaluateOneElementAlgorithm : IAlgorithm
    {
        private IList<string> expression;

        public IList<string> Execute(IList<string> expression)
        {
            this.expression = expression;
            return expression;
        }
    }

    public class ExpressionValidateOnlyOneElement : IAlgorithm
    {
        public IList<string> Execute(IList<string> expression)
        {
            
            if (expression.Count != 1)
                throw new ArgumentOutOfRangeException("Incorrect number of arguments");
            else
                return expression;
        }
    }

    [TestClass]
    public class StringEvaluatorTests
    {
        
        [TestMethod]
        public void TestEvaluator_UsePassingNoOperationAlgorithms_ValidateOnlyOneCharacter_ExpectInputAtResult()
        {
            var myEvaluator = new Evaluator(new ExpressionValidateOnlyOneElement(), new ParseOnlyOneElementAlgorithm(), new EvaluateOneElementAlgorithm());
            var result1 = myEvaluator.Evaluate("1");
            var result2 = myEvaluator.Evaluate("24");
            var result3 = myEvaluator.Evaluate("+");

            Assert.AreEqual("1", result1);
            Assert.AreEqual("24", result2);
            Assert.AreEqual("+", result3);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestEvaluator_UsePassingAlgorithms_ValidateOnlyOneCharacter_ExpectErrorBecauseInputDoesNotValidate()
        {
            var myEvaluator = new Evaluator(new ExpressionValidateOnlyOneElement(), new ParseOnlyOneElementAlgorithm(), new EvaluateOneElementAlgorithm());
            var result1 = myEvaluator.Evaluate("12+");
            var result2 = myEvaluator.Evaluate("*39");
        }
    }
}
