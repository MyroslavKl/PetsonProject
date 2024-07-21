namespace HashService.Options;

public class HashOptions
{
    public int SaltLength { get; init; }
    public int Iterations { get; init; }
    public int KeyLength { get; init; }
}