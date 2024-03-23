using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
