﻿using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data;
using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Controllers
{
    public class MatchController : BaseController
    {
        private readonly FootballInsightDbContext context;
        private readonly ILogger<MatchController> logger;

        public MatchController(FootballInsightDbContext Context, ILogger<MatchController> Logger)
        {
            logger = Logger;
            context = Context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int leagueId)
        {
            var viewModel = new MatchCreateViewModel();
            viewModel.Teams = await GetTeamsAsync(leagueId);
            viewModel.LeagueId = leagueId;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MatchCreateViewModel model)
        {
            if (model.HomeTeamId == model.AwayTeamId)
            {
                ModelState.AddModelError(string.Empty, "The home team and away team cannot be the same.");
            }

            if (!ModelState.IsValid)
            {
                var viewModel = new MatchCreateViewModel();
                viewModel.Teams = await GetTeamsAsync(model.LeagueId);
                viewModel.LeagueId = model.LeagueId;

                return View(viewModel);
            }

            var match = new Match
            {
                Date = model.DateTime,
                HomeTeamId = model.HomeTeamId,
                AwayTeamId = model.AwayTeamId,
                StadiumId = await GetStadiumId(model.HomeTeamId),
                HomeScore = 0,
                AwayScore = 0,
                LeagueId = model.LeagueId,
                Status = MatchStatus.Scheduled 
            };

            context.Matches.Add(match);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "League", new { id = model.LeagueId});
        }

        private async Task<List<TeamSimpleViewModel>> GetTeamsAsync(int id)
        {
            var teams = await context.Teams
              .Where(t => t.LeagueId == id)
              .Select(t => new TeamSimpleViewModel()
              {
                  Id = t.Id,
                  Name = t.Name,
              })
              .ToListAsync();

            return teams;
        }

        private async Task<int> GetStadiumId(int homeTeamId)
        {
            var teamWithStadium = await context.Teams
                                   .Include(t => t.Stadium)
                                   .FirstOrDefaultAsync(t => t.Id == homeTeamId);


            return teamWithStadium.StadiumId;
        }
    }
}
