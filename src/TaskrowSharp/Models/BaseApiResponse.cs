using TaskrowSharp.Interfaces;

namespace TaskrowSharp.Models;

public class BaseApiResponse<T> : IBaseApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Entity { get; set; }
    public string TargetURL { get; set; }
    public string PreviousGUID { get; set; }
}
