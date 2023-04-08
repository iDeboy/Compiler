using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compilador.Core;
internal sealed class Lexer {

    private string SourceCode { get; }
    private int Index { get; set; }

    public string SourceFile { get; }

    public Lexer(string sourceFile) {
        SourceFile = sourceFile;

        SourceCode = File.ReadAllText(sourceFile);

        Index = 0;

    }

    public Token GetNextToken() {

        // Saltea los tokens ignorados
        while (Index < SourceCode.Length && TokenKind.Ignored.IsMatch(SourceCode[Index].ToString())) {
            Index++;
        }

        if (Index >= SourceCode.Length) return Token.EOF;

        foreach (var kind in TokenKind.OrderedList) {

            var match = kind.Match(SourceCode[Index..]);

            if (!match.Success) continue;

            var subSourceCode = SourceCode[..Index];

            var line = subSourceCode.Count(c => c == '\n') + 1;

            int startColumn;
            int endColumn;

            if (line is 1) startColumn = Index + 1;

            else startColumn = Index - subSourceCode.LastIndexOf('\n');

            endColumn = startColumn + match.Length - 1;

            Index += match.Length;

            string value = match.Value;

            Location startLocation = new() { Line = line, Column = startColumn };
            Location endLocation = new() { Line = line, Column = endColumn };


            return new(kind, startLocation, endLocation, value);

        }

        throw new TokenNotFoundException();

    }

    public void Reset() => Index = 0;

}
