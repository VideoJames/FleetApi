namespace WorkflowApi.Exceptions
{
    public class ConcurrencyConflictException(string message) : Exception(message)
    {
    }
}
