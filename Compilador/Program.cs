using Compilador.Core;

namespace Compilador;
internal class Program {
    static void Main(string[] args) {

        Array.ForEach(args, Console.WriteLine);

        var lexer = new Lexer("Examples/Source.txt");

        List<Token> tokens = new();

        Token current;
        do {
            current = lexer.GetNextToken();

            tokens.Add(current);
        } while (current.Kind != TokenKind.EOF);

        foreach (Token token in tokens) {
            Console.WriteLine($"{token.Value} -> {token.Kind}");
        }

        var parser = new Parser(lexer);

        var @namespace = parser.Parse();

        Console.WriteLine($"{@namespace}");


    }
}