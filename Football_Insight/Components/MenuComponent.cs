﻿using Football_Insight.Core.Models.League;
using Football_Insight.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Components
{
    public class MenuComponent : ViewComponent
    {
        private readonly FootballInsightDbContext context;

        public MenuComponent(FootballInsightDbContext Context)
        {
            context = Context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var leagues = await context.Leagues
                .Select(l => new LeagueTeamsViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                })
                .ToListAsync();

            return await Task.FromResult<IViewComponentResult>(View(leagues));
        }
    }
}
