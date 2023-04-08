using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Compilador.Core;
internal readonly struct Expression {

    public static Expression None => default;

    private readonly List<Token> m_tokens;

    public bool IsValid { get; }

    public IReadOnlyList<Token> Tokens => new ReadOnlyCollection<Token>(m_tokens ?? new List<Token>());

    public Expression(bool isValid = true) : this(Array.Empty<Token>(), isValid) { }

    public Expression(Token token, bool isValid = true) : this(new[] { token }, isValid) { }

    public Expression(IEnumerable<Token> tokens, bool isValid = true) {
        m_tokens = tokens.ToList();
        IsValid = isValid;
    }

    public override string ToString() {

        if (!IsValid) return "Invalid";

        return $"{string.Join(' ', Tokens.Select(t => t.Value))}";
    }

    public Expression OrNext(Expression other) {

        bool valid = IsValid || other.IsValid;

        return new(Tokens.Concat(other.Tokens), valid);
    }

    public Expression AndNext(Expression other) {

        bool valid = IsValid && other.IsValid;

        return new(Tokens.Concat(other.Tokens), valid);
    }

    public static bool operator true(Expression expression) => expression.IsValid;
    public static bool operator false(Expression expression) => !expression.IsValid;

    public static implicit operator Expression(Token token)
        => new(token);

    public static Expression operator &(Expression expression1, Expression expression2) =>
        expression1.AndNext(expression2);

    public static Expression operator |(Expression expression1, Expression expression2) =>
        expression1.OrNext(expression2);



}
