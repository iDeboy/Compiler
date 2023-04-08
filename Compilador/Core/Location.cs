namespace Compilador.Core;
internal readonly struct Location {

    public static Location None => default;

    public required int Line { get; init; }
    public required int Column { get; init; }

    public override string ToString() {
        return $"{Line}:{Column}";
    }
}
