using System;
using System.Text;

namespace Coding.Exercise {
    public abstract class ExpressionVisitor {
        // todo
        public abstract void Visit(Value ae);
        public abstract void Visit(AdditionExpression ae);
        public abstract void Visit(MultiplicationExpression me);
    }

    public abstract class Expression {
        public abstract void Accept(ExpressionVisitor ev);
    }

    public class Value : Expression {
        public readonly int TheValue;

        public Value(int value) {
            TheValue = value;
        }

        // todo
        public override void Accept(ExpressionVisitor ev) {
            ev.Visit(this);
        }
    }

    public class AdditionExpression : Expression {
        public readonly Expression LHS, RHS;

        public AdditionExpression(Expression lhs, Expression rhs) {
            LHS = lhs;
            RHS = rhs;
        }

        // todo
        public override void Accept(ExpressionVisitor ev) {
            ev.Visit(this);
        }
    }

    public class MultiplicationExpression : Expression {
        public readonly Expression LHS, RHS;

        public MultiplicationExpression(Expression lhs, Expression rhs) {
            LHS = lhs;
            RHS = rhs;
        }

        // todo
        public override void Accept(ExpressionVisitor ev) {
            ev.Visit(this);
        }
    }

    public class ExpressionPrinter : ExpressionVisitor {
        private StringBuilder sb = new StringBuilder();

        public override void Visit(Value value) {
            // todo
            sb.Append(value.TheValue);
        }

        public override void Visit(AdditionExpression ae) {
            // todo
            sb.Append("(");
            ae.LHS.Accept(this);
            sb.Append("+");
            ae.RHS.Accept(this);
            sb.Append(")");
        }

        public override void Visit(MultiplicationExpression me) {
            // todo
            me.LHS.Accept(this);
            sb.Append("*");
            me.RHS.Accept(this);
        }

        public override string ToString() {
            // todo
            return sb.ToString();
        }
    }
}