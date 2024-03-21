using Football_Insight.Infrastructure.Data.Models;

namespace Football_Insight.Core.Models.Account
{
    public class RegistrationResultViewModel
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public ApplicationUser? User { get; set; }
    }
}