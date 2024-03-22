using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Insight.Core.Contracts
{
    public interface IStadiumService
    {
        Task<int> GetStadiumIdAsync(int teamId);
    }
}
