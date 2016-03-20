using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringEvaluator;

namespace StringEvaluatorTests
{
    [TestClass]
    public class RpnSolverTest
    {
        [TestMethod]
        public void TestRpnSolver_ValidInput_ValidResult()
        {
            var parsedList1 = new List<string>() { { "8" }, { "4" }, { "2" }, { "/" }, { "+" } };

            var parsedList2 = new List<string>() { { "8" }, { "4" }, { "/" }, { "2" }, { "+" } };

            RpnSolver myRpnSolver = new RpnSolver();

            var result1 = myRpnSolver.Execute(parsedList1);

            var result2 = myRpnSolver.Execute(parsedList2);

            Assert.AreEqual("10", result1[0]);
            Assert.AreEqual((int)1, result1.Count);
            Assert.AreEqual("4", result2[0]);
            Assert.AreEqual((int)1, result2.Count);
        }
    }
}
