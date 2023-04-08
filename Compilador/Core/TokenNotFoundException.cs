namespace Compilador.Core;

[Serializable]
public class TokenNotFoundException : Exception {
    public TokenNotFoundException() { }
    public TokenNotFoundException(string message) : base(message) { }
    public TokenNotFoundException(string message, Exception inner) : base(message, inner) { }
    protected TokenNotFoundException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
