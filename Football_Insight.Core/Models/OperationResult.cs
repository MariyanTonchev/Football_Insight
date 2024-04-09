namespace Football_Insight.Core.Models
{
    public class OperationResult
    {
        public OperationResult(bool success)
        {
            Success = success;
        }

        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public OperationResult(bool success, string message, int? objectId) : this(success, message)
        {
            ObjecId = objectId;
        }

        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public int? ObjecId { get; set; }
    }
}
