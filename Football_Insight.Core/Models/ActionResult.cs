namespace Football_Insight.Core.Models
{
    public class ActionResult
    {
        public ActionResult(bool success)
        {
            Success = success;
        }

        public ActionResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
