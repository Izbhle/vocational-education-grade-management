[System.Serializable]
public class AuthException : System.Exception
{
    public AuthException() { }
    public AuthException(string message) : base(message) { }
    public AuthException(string message, System.Exception inner) : base(message, inner) { }
    protected AuthException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}