﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Insight.Core.Contracts
{
    public interface ICacheService
    {
        bool TryGetCachedItem(string cacheKey);
    }
}
