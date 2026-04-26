namespace ProfoundKnowledgeBlogBack.Domain.Shared;

public class OperationResult<T> where T : class
{
    public OperationResult(T value, bool isSuccessful, string errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccessful = isSuccessful;
        Value = value;
    }

    public string ErrorMessage { get; set; }
    public bool IsSuccessful { get; set; }
    public T Value { get; set; }

    public static OperationResult<T> Success(T value) => new(value, true, "Success");
    public static OperationResult<T> Error(string message) => new(null!, false, message);
}