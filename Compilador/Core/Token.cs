namespace Compilador.Core;

internal record Token(TokenKind Kind, Location Start, Location End, string Value)
{

    public static readonly Token EOF = new(TokenKind.EOF, default, default, "<EOF>");

}
