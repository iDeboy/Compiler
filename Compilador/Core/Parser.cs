using System.Collections.ObjectModel;

namespace Compilador.Core;

internal sealed class Parser {

    private IReadOnlyList<Token> Tokens { get; }
    private int Index { get; set; }

    public string SourceFile { get; }

    private Token CurrentToken {
        get {
            if (Index < Tokens.Count) return Tokens[Index];

            return Token.EOF;
        }
    }

    public Parser(string sourceFile, IList<Token> tokens) {

        SourceFile = sourceFile;
        Tokens = new ReadOnlyCollection<Token>(tokens);

        Index = 0;

    }

    public Parser(Lexer lexer) {

        SourceFile = lexer.SourceFile;

        lexer.Reset();

        List<Token> tokens = new();

        Token current;
        do {
            current = lexer.GetNextToken();

            tokens.Add(current);
        } while (current.Kind != TokenKind.EOF);

        Tokens = new ReadOnlyCollection<Token>(tokens);

        Index = 0;

    }

    private bool IsMatch(TokenKind kind) {
        return CurrentToken.Kind == kind;
    }

    private Expression Match(TokenKind kind) {

        if (!IsMatch(kind)) return Expression.None;

        var current = CurrentToken;

        Index++;
        return current;

    }

    private Expression Delimiter() => Match(TokenKind.Semicolon);

    private Expression NamespaceName() {

        var ex = Match(TokenKind.Identifier);

        if (!IsMatch(TokenKind.Semicolon)) ex &= Match(TokenKind.Dot) && NamespaceName();

        if (!ex.IsValid) Index -= ex.Tokens.Count;

        return ex;
    }

    private bool MacroParamsOperator() {

        for (int i = 0; i < 3; i++)
            if (!Match(TokenKind.Dot).IsValid) return false;

        return true;
    }

    private bool MacroParams() {

        if (!Match(TokenKind.Identifier).IsValid) return false;

        if (Match(TokenKind.Comma).IsValid) return MacroParams() || MacroParamsOperator();

        return true;
    }

    private bool MacroName() {

        if (!Match(TokenKind.Identifier).IsValid) return false;

        if (!Match(TokenKind.OpenParenthesis).IsValid) return true;

        if (Match(TokenKind.CloseParenthesis).IsValid) return true;

        if (!MacroParams()) return false;

        return Match(TokenKind.CloseParenthesis).IsValid;

    }

    private Expression MacroBody() {
        Expression ex = Match(TokenKind.OpenBrace);

        if (!ex.IsValid) return Expression.None;

        if (IsMatch(TokenKind.CloseBrace)) return ex &= Match(TokenKind.CloseBrace);

        return ex;
    }

    private Expression Include() {
        var ex = Match(TokenKind.Include) && NamespaceName() && Delimiter();
        return ex;
    }

    private bool Define() {

        if (!Match(TokenKind.Define).IsValid) return false;

        return MacroName();
    }

    public Expression Parse() {
        return Include();
    }

    public void Reset() => Index = 0;
}
