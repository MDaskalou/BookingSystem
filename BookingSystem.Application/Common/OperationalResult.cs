namespace BookingSystem.Application.Common
{
    public class OperationResult<T>
    {
        public bool Success { get; private set; }
        public string? Message { get; private set; }
        public T? Data { get; private set; }

        private OperationResult(bool success, T? data = default, string? message = null)
        {
            Success = success;
            Data = data;
            Message = message;
        }

        // För lyckade operationer
        public static OperationResult<T> Ok(T data, string? message = null)
            => new OperationResult<T>(true, data, message);

        // För misslyckade operationer
        public static OperationResult<T> Fail(string message)
            => new OperationResult<T>(false, default, message);
    }
}