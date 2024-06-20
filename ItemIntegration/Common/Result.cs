namespace ItemIntegration.Service.Common
{
    public class Result
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
