using Ardalis.SmartEnum;
using System.Text.RegularExpressions;

namespace Compilador.Core;

internal sealed class TokenKind : SmartEnum<TokenKind> {

    private static int s_iota = 0;
    private static int Iota() => s_iota++;

    public static IOrderedEnumerable<TokenKind> OrderedList => List.Where(kind => kind != EOF).OrderBy(kind => kind.Value);

    public static TokenKind EOF = new("EOF", Iota(), "");

    // Private members
    internal static TokenKind Ignored = new("Ignored", Iota(), "[ \t\r\n]+");
    internal static TokenKind ScapeChar = new("ScapeChar", Iota(), "\\\\([\"'\\abtrvfn]|x[\\dA-Fa-f]{1,4}|u[\\dA-Fa-f]{4})");

    // Keywords
    public static TokenKind Include = new("Keyword_Include", Iota(), "include");
    public static TokenKind Define = new("Keyword_Define", Iota(), "define");

    public static TokenKind Namespace = new("Keyword_Namespace", Iota(), "namespace");

    public static TokenKind Public = new("Keyword_Public", Iota(), "public");
    public static TokenKind Private = new("Keyword_Private", Iota(), "private");
    public static TokenKind Protected = new("Keyword_Protected", Iota(), "protected");
    public static TokenKind Internal = new("Keyword_Internal", Iota(), "internal");

    public static TokenKind Interface = new("Keyword_Interface", Iota(), "interface");
    public static TokenKind Enum = new("Keyword_Enum", Iota(), "enum");
    public static TokenKind Struct = new("Keyword_Struct", Iota(), "struct");
    public static TokenKind Class = new("Keyword_Class", Iota(), "class");
    public static TokenKind Record = new("Keyword_Record", Iota(), "record");

    public static TokenKind Static = new("Keyword_Static", Iota(), "static");

    public static TokenKind DataType = new("DataType", Iota(), "(object|sbyte|byte|short|ushort|int|uint|long|ulong|float|double|decimal|char|bool|string)");

    // Literals
    public static TokenKind Char = new("Char", Iota(), $"'(|[^\\'\\n]|{ScapeChar.Pattern})'");
    public static TokenKind String = new("String", Iota(), $"\"([^\\\\\"\\n]|{ScapeChar.Pattern}|\\\\[^ \t\r\n])*\"");
    public static TokenKind Number = new("Number", Iota(), "\\d+(\\.\\d+)?");
    public static TokenKind Boolean = new("Boolean", Iota(), "(true|false)");

    // Punctuation
    public static TokenKind Comma = new("Comma", Iota(), ",");
    public static TokenKind Dot = new("Dot", Iota(), "\\.");
    public static TokenKind QuestionMark = new("QuestionMark", Iota(), "\\?");
    public static TokenKind Colon = new("Colon", Iota(), ":");
    public static TokenKind Semicolon = new("Semicolon", Iota(), ";");
    public static TokenKind OpenParenthesis = new("Open_Parenthesis", Iota(), "\\(");
    public static TokenKind CloseParenthesis = new("Close_Parenthesis", Iota(), "\\)");
    public static TokenKind OpenBrace = new("Open_Brace", Iota(), "{");
    public static TokenKind CloseBrace = new("Close_Brace", Iota(), "}");
    public static TokenKind OpenBracket = new("Open_Bracket", Iota(), "\\[");
    public static TokenKind CloseBracket = new("Close_Bracket", Iota(), "\\]");

    public static TokenKind EqualSymbol = new("Equal_Symbol", Iota(), "=");
    public static TokenKind PlusSymbol = new("Plus_Symbol", Iota(), "\\+");
    public static TokenKind MinusSymbol = new("Minus_Symbol", Iota(), "-");
    public static TokenKind StarSymbol = new("Star_Symbol", Iota(), "\\*");
    public static TokenKind PercentSymbol = new("Percent_Symbol", Iota(), "%");
    public static TokenKind SlashSymbol = new("Slash_Symbol", Iota(), "\\/");
    public static TokenKind AmpersandSymbol = new("Ampersand_Symbol", Iota(), "&");
    public static TokenKind PipeSymbol = new("Pipe_Symbol", Iota(), "\\|");
    public static TokenKind CaretSymbol = new("Caret_Symbol", Iota(), "\\^");
    public static TokenKind LessThanSymbol = new("LessThan_Symbol", Iota(), "<");
    public static TokenKind GreaterThanSymbol = new("GreaterThan_Symbol", Iota(), ">");
    public static TokenKind ExclamationMarkSymbol = new("ExclamationMark_Symbol", Iota(), "!");
    public static TokenKind TildeSymbol = new("Tilde_Symbol", Iota(), "~");

    // Operators
#if false
    public static TokenKind LambdaOperator = new("Lambda_Operator", Iota(), "=>");

    public static TokenKind IncrementOperator = new("Increment_Operator", Iota(), "\\+\\+");
    public static TokenKind DecrementOperator = new("Decrement_Operator", Iota(), "--");

    public static TokenKind AssignmentAdditionOperator = new("AssignmentAddition_Operator", Iota(), "\\+=");
    public static TokenKind AssignmentSubtractionOperator = new("AssignmentSubtraction_Operator", Iota(), "-=");
    public static TokenKind AssignmentExponentiationOperator = new("AssignmentExponentiation_Operator", Iota(), "\\*\\*=");
    public static TokenKind AssignmentMultiplicationOperator = new("AssignmentMultiplication_Operator", Iota(), "\\*=");
    public static TokenKind AssignmentModulusOperator = new("AssignmentModulus_Operator", Iota(), "%=");
    public static TokenKind AssignmentIntDivisionOperator = new("AssignmentIntDivision_Operator", Iota(), "\\/\\/=");
    public static TokenKind AssignmentDivisionOperator = new("AssignmentDivision_Operator", Iota(), "\\/=");

    public static TokenKind AssignmentAndBitOperator = new("AssignmentAndBit_Operator", Iota(), "&=");
    public static TokenKind AssignmentOrBitOperator = new("AssignmentOrBit_Operator", Iota(), "\\|=");
    public static TokenKind AssignmentXorBitOperator = new("AssignmentXorBit_Operator", Iota(), "\\^=");

    public static TokenKind AssignmentLeftBitwiseOperator = new("AssignmentLeftBitwise_Operator", Iota(), "<<=");
    public static TokenKind AssignmentRightBitwiseOperator = new("AssignmentRightBitwise_Operator", Iota(), ">>=");

    public static TokenKind AdditionOperator = new("Addition_Operator", Iota(), "\\+");
    public static TokenKind SubtractionOperator = new("Subtraction_Operator", Iota(), "-");
    public static TokenKind ExponentiationOperator = new("Exponentiation_Operator", Iota(), "\\*\\*");
    public static TokenKind MultiplicationOperator = new("Multiplication_Operator", Iota(), "\\*");
    public static TokenKind ModulusOperator = new("Modulus_Operator", Iota(), "%");
    public static TokenKind IntDivisionOperator = new("IntDivision_Operator", Iota(), "\\/\\/");
    public static TokenKind DivisionOperator = new("Division_Operator", Iota(), "\\/");

    public static TokenKind NotOperator = new("Not_Operator", Iota(), "!");
    public static TokenKind AndOperator = new("And_Operator", Iota(), "&&");
    public static TokenKind OrOperator = new("Or_Operator", Iota(), "\\|\\|");
    public static TokenKind XorOperator = new("Xor_Operator", Iota(), "\\^\\^");

    public static TokenKind NotBitOperator = new("NotBit_Operator", Iota(), "~");
    public static TokenKind AndBitOperator = new("AndBit_Operator", Iota(), "&");
    public static TokenKind OrBitOperator = new("OrBit_Operator", Iota(), "\\|");
    public static TokenKind XorBitOperator = new("XorBit_Operator", Iota(), "\\^");

    public static TokenKind LeftBitwiseOperator = new("LeftBitwise_Operator", Iota(), "<<");
    public static TokenKind RightBitwiseOperator = new("RightBitwise_Operator", Iota(), ">>");

    public static TokenKind EqualityOperator = new("Equality_Operator", Iota(), "==");
    public static TokenKind InequalityOperator = new("Inequality_Operator", Iota(), "!=");

    public static TokenKind GreaterOperator = new("Greater_Operator", Iota(), ">");
    public static TokenKind GreaterOrEqualsOperator = new("GreaterOrEquals_Operator", Iota(), ">=");
    public static TokenKind LessOperator = new("Less_Operator", Iota(), ">");
    public static TokenKind LessOrEqualsOperator = new("LessOrEquals_Operator", Iota(), ">=");

    public static TokenKind AssignmentOperator = new("Assignment_Operator", Iota(), "=");
#endif

    // Identifiers
    public static TokenKind Identifier = new("Identifier", Iota(), "([A-Za-z]\\w*|_[A-Za-z0-9]\\w*)");
    public static TokenKind Discard = new("Discard", Iota(), "_");

    // Any
    public static TokenKind Any = new("Any", Iota(), "[.\\S]");


    private Regex Expresion { get; }
    public string Pattern { get; }

    private TokenKind(string name, int value, string pattern)
        : base(name, value) {

        Pattern = pattern;
        Expresion = new($"^{Pattern}", RegexOptions.Compiled);
    }

    /// <inheritdoc cref="Regex.Match(string)"/>
    public Match Match(string input) => Expresion.Match(input);

    /// <inheritdoc cref="Regex.Match(string)"/>
    public Match Match(char input) => Expresion.Match(input.ToString());


    /// <inheritdoc cref="Regex.IsMatch(string)"/>
    public bool IsMatch(string input) => Expresion.IsMatch(input);

    /// <inheritdoc cref="Regex.IsMatch(string)"/>
    public bool IsMatch(char input) => Expresion.IsMatch(input.ToString());

}
