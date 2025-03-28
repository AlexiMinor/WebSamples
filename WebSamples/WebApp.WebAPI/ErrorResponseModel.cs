namespace WebApp.WebAPI;

/// <summary>
/// Response for errors
/// </summary>
public class ErrorResponseModel
{
    public string Message { get; set; }
    public string? StackTrace { get; set; }
}