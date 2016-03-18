using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringEvaluator
{
    public class MyParser : IAlgorithm
    {
        private Dictionary<string, int> operatorPrecedence = new Dictionary<string, int>()
                {
                    {"+", 2},
                    {"-", 2},
                    {"*", 3},
                    {"/", 3},
                    {"^", 4}
                };

        private enum TokenType
        {
            ValueType,
            OperatorType,
            ScopeType
        }

        private LinkedListNode<string> ComparePrecedence(LinkedListNode<string> CurrentToken, LinkedListNode<string> HeadToken)
        {
            return (GetOperatorPrecedence(CurrentToken.Value) > GetOperatorPrecedence(HeadToken.Value)) ? CurrentToken : HeadToken;
        }

        private int GetOperatorPrecedence(string op)
        {
            int precedence;
            if (operatorPrecedence.TryGetValue(op, out precedence))
                return precedence;
            else
                throw new ArgumentException("not valid operator");
        }

        private TokenType GetTokenType(string token)
        {
            var valuePattern = @"\d+";
            var operatorPattern = @"[\+\-\*\/\^]";
            var scopePattern = @"[\)\(]";

            if (Regex.Match(token, valuePattern).Success)
                return TokenType.ValueType;
            else if (Regex.Match(token, operatorPattern).Success)
                return TokenType.OperatorType;
            else if (Regex.Match(token, scopePattern).Success)
                return TokenType.ScopeType;
            else
                throw new ArgumentOutOfRangeException("invalid token");
        }

        public IList<string> Execute(IList<string> expression)
        {
            var resultList = new LinkedList<string>();
            LinkedListNode<string> HEAD = new LinkedListNode<string>(expression[0]);
            resultList.AddFirst(HEAD);

            for (var i = 1; i < expression.Count; i++)
            {
                var token = new LinkedListNode<string>(expression[i]);

                switch (GetTokenType(expression[i]))
                {
                    // IF token is value type
                    case TokenType.ValueType:
                        resultList.AddBefore(HEAD, token);
                        break;

                    // ELSE IF token is operator type
                    case TokenType.OperatorType:
                        
                        switch (GetTokenType(HEAD.Value))
                        {
                            // IF HEAD is a value type
                            case TokenType.ValueType:
                                resultList.AddAfter(HEAD, token);
                                HEAD = token;  // attach HEAD to newest token
                                break;

                            // ELSE IF HEAD is a operator type
                            case TokenType.OperatorType:
                                // IF token has higher precedence than HEAD
                                if (ComparePrecedence(token, HEAD) == token)
                                    resultList.AddBefore(HEAD, token);
                                else
                                    resultList.AddAfter(HEAD, token);
                                HEAD = token;
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }

            IList<string> result = new List<string>();

            foreach (var token in resultList)
            {
                result.Add(token);
            }

            return result;
        }
    }

}
