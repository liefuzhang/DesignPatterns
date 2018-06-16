using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Coding.Exercise {
    public class Token {
        public int Value = 0;

        public Token(int value) {
            this.Value = value;
        }
    }

    public class Memento {
        private readonly List<Token> tokenList;

        public Memento(List<Token> tokens) {
            tokenList = tokens.Select(t => new Token(t.Value)).ToList();
        }

        public List<Token> TokenList => tokenList.Select(t => new Token(t.Value)).ToList();
    }

    public class TokenMachine {
        public List<Token> Tokens = new List<Token>();

        public Memento AddToken(int value) {
            // todo
            Tokens.Add(new Token(value));
            return new Memento(Tokens);
        }

        public Memento AddToken(Token token) {
            // todo (yes, please do both overloads)
            // todo
            Tokens.Add(token);
            return new Memento(Tokens);
        }

        public void Revert(Memento m) {
            // todo
            Tokens = m.TokenList;
        }
    }
}
