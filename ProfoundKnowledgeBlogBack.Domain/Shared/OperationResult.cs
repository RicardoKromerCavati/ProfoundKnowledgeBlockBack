namespace ProfoundKnowledgeBlogBack.Domain.Shared;

public class OperationResult<T> : OperationResult where T : class
{
    public OperationResult(T value, bool isSuccessful, string errorMessage) : base(isSuccessful, errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccessful = isSuccessful;
        Value = value;
    }

    public T Value { get; set; }

    public static OperationResult<T> Success(T value) => new(value, true, "Success");
    public static new OperationResult<T> Error(string message) => new(null!, false, message);
}

public class OperationResult
{
    public OperationResult(bool isSuccessful, string errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccessful = isSuccessful;
    }

    public string ErrorMessage { get; set; }
    public bool IsSuccessful { get; set; }

    public static OperationResult Success() => new(true, "Success");
    public static OperationResult Error(string message) => new(false, message);
}