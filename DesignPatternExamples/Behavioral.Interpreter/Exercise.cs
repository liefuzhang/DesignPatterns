using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Coding.Exercise
{
    public class Token
    {
        public Token(Type myType, string text)
        {
            MyType = myType;
            Text = text;
        }

        public enum Type
        {
            Plus, Minus, Integer, String
        }

        public Type MyType;
        public string Text;

        public override string ToString()
        {
            return $"`{Text}`";
        }
    }

    public interface IElement
    {
        int Value { get; }
    }

    public class Integer : IElement
    {
        public Integer(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }

    public class BinaryOperation : IElement
    {
        public enum OperationType
        {
            Addition, Subtraction
        }

        public OperationType Operation;
        public IElement LeftVal;
        public IElement RightVal;

        public int Value
        {
            get
            {
                if (Operation == OperationType.Addition)
                {
                    return LeftVal.Value + RightVal.Value;
                }
                else
                {
                    return LeftVal.Value - RightVal.Value;
                }
            }
        }
    }


    public class ExpressionProcessor
    {
        public Dictionary<char, int> Variables = new Dictionary<char, int>();

        public int Calculate(string expression)
        {
            // todo
            return Parse(Lex(expression));
        }

        public int Parse(List<Token> tokens)
        {
            bool hasLHV = false;
            BinaryOperation operation = new BinaryOperation();
            for (var i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];

                switch (token.MyType)
                {
                    case Token.Type.Plus:
                        operation.Operation = BinaryOperation.OperationType.Addition;
                        break;
                    case Token.Type.Minus:
                        operation.Operation = BinaryOperation.OperationType.Subtraction;
                        break;
                    case Token.Type.Integer:
                        if (tokens.Count == 1)
                            return Int32.Parse(token.Text);

                        var val = new Integer(Int32.Parse(token.Text));
                        if (!hasLHV)
                        {
                            operation.LeftVal = val;
                            hasLHV = true;
                        }
                        else
                        {
                            operation.RightVal = val;
                            var newToken = new Token(Token.Type.Integer, operation.Value.ToString());
                            var newTokenList = new List<Token> { newToken };
                            newTokenList.AddRange(tokens.Skip(i + 1));
                            return Parse(newTokenList);
                        }
                        break;
                    case Token.Type.String:
                        if (token.Text.Length > 1 || !Variables.ContainsKey(token.Text[0]))
                        {
                            return 0;
                        }
                        var charVal = new Integer(Variables[token.Text[0]]);
                        if (!hasLHV)
                        {
                            operation.LeftVal = charVal;
                            hasLHV = true;
                        }
                        else
                        {
                            operation.RightVal = charVal;
                            var newToken = new Token(Token.Type.Integer, operation.Value.ToString());
                            var newTokenList = new List<Token> { newToken };
                            newTokenList.AddRange(tokens.Skip(i + 1));
                            return Parse(newTokenList);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return 0;
        }

        public List<Token> Lex(string expression)
        {
            var result = new List<Token>();
            for (var i = 0; i < expression.Length; i++)
            {
                var ch = expression[i];
                switch (ch)
                {
                    case '+':
                        result.Add(new Token(Token.Type.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(Token.Type.Minus, "-"));
                        break;
                    default:
                        if (char.IsDigit(ch))
                        {
                            var sb = new StringBuilder(ch.ToString());
                            var j = i + 1;
                            while (j < expression.Length && char.IsDigit(expression[j]))
                            {
                                sb.Append(expression[j]);
                                i = j++;
                            }
                            result.Add(new Token(Token.Type.Integer, sb.ToString()));
                        }
                        else if (char.IsLetter(ch))
                        {
                            var j = i + 1;
                            while (j < expression.Length && char.IsLetter(expression[j]))
                                i = j++;
                            result.Add(new Token(Token.Type.String, expression.Substring(i, j - i)));
                        }
                        break;

                }
            }

            return result;
        }

        public static void Main(string[] args)
        {
            var p = new ExpressionProcessor();
            p.Variables['x'] = 5;

            var tokens = p.Lex("1+x");
            Console.WriteLine($"value: {p.Parse(tokens)}");
        }
    }
    
}
