namespace TaskrowSharp.Interfaces;

public interface IBaseApiResponse
{
    public bool Success { get; }
    public string Message { get; }
}
