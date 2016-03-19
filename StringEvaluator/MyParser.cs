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
            LeftScopeType,
            RightScopeType
        }

        private LinkedListNode<string> ComparePrecedence(LinkedListNode<string> CurrentToken, LinkedListNode<string> HeadToken, int localModifier)
        {
            return (GetOperatorPrecedence(CurrentToken.Value, localModifier) > GetOperatorPrecedence(HeadToken.Value)) ? CurrentToken : HeadToken;
        }

        private int GetOperatorPrecedence(string op, int localModifer)
        {
            int precedence;
            if (operatorPrecedence.TryGetValue(op, out precedence))
                return precedence + localModifer;
            else
                throw new ArgumentException("not valid operator");
        }

        private int GetOperatorPrecedence(string op)
        {
            return GetOperatorPrecedence(op, 0);
        }

        private TokenType GetTokenType(string token)
        {
            var valuePattern = @"\d+";
            var operatorPattern = @"[\+\-\*\/\^]";
            var leftScopePattern = @"\(";
            var rightScopePattern = @"\)";

            if (Regex.Match(token, valuePattern).Success)
                return TokenType.ValueType;
            else if (Regex.Match(token, operatorPattern).Success)
                return TokenType.OperatorType;
            else if (Regex.Match(token, leftScopePattern).Success)
                return TokenType.LeftScopeType;
            else if (Regex.Match(token, rightScopePattern).Success)
                return TokenType.RightScopeType;
            else
                throw new ArgumentOutOfRangeException("invalid token");
        }

        public IList<string> Execute(IList<string> expression)
        {
            var resultList = new LinkedList<string>();
            LinkedListNode<string> HEAD = null;

            var localPrecedence = 0;

            //resultList.AddFirst(HEAD);

            for (var i = 0; i < expression.Count; i++)
            {
                var token = new LinkedListNode<string>(expression[i]);

                switch (GetTokenType(expression[i]))
                {
                    // IF token is scope type (parenthesis)
                    case TokenType.LeftScopeType:
                        // elevate precedence
                        localPrecedence += 4;
                        break;

                    case TokenType.RightScopeType:
                        localPrecedence -= 4;
                        HEAD = null;
                        break;

                    // ELSE IF token is value type
                    case TokenType.ValueType:
                        if (HEAD == null)
                        {
                            resultList.AddLast(token);
                            HEAD = token;
                        }
                        else    
                            resultList.AddBefore(HEAD, token);
                        break;

                    // ELSE IF token is operator type
                    case TokenType.OperatorType:
                        
                        if (HEAD == null)
                        {
                            resultList.AddLast(token);
                            HEAD = token;
                            break;
                        }

                        switch (GetTokenType(HEAD.Value))
                        {
                            // IF HEAD is a value type
                            case TokenType.ValueType:
                                resultList.AddAfter(HEAD, token);
                                HEAD = token;  // attach HEAD to newest token
                                break;

                            // ELSE IF HEAD is an operator type
                            case TokenType.OperatorType:
                                // IF token has higher precedence than HEAD
                                if (ComparePrecedence(token, HEAD, localPrecedence) == token)
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
