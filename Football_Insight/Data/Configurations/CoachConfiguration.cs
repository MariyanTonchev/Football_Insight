using Football_Insight.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football_Insight.Data.Configurations
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder) => builder.HasData(
                new Coach
                {
                    Id = 1,
                    FirstName = "Pep",
                    LastName = "Guardiola",
                    DateOfBirth = new DateTime(1971, 1, 18),
                    Salary = 20000M,
                    Trophies = 30,
                    TeamId = 1
                },
                new Coach
                {
                    Id = 2,
                    FirstName = "Zinedine",
                    LastName = "Zidane",
                    DateOfBirth = new DateTime(1972, 6, 23),
                    Salary = 19000M,
                    Trophies = 11,
                    TeamId = 2
                },
                new Coach
                {
                    Id = 3,
                    FirstName = "Jurgen",
                    LastName = "Klopp",
                    DateOfBirth = new DateTime(1967, 6, 16),
                    Salary = 18000M,
                    Trophies = 9,
                    TeamId = 3
                },
                new Coach
                {
                    Id = 4,
                    FirstName = "Carlo",
                    LastName = "Ancelotti",
                    DateOfBirth = new DateTime(1959, 6, 10),
                    Salary = 16000M,
                    Trophies = 20,
                    TeamId = 4
                },
                new Coach
                {
                    Id = 5,
                    FirstName = "Diego",
                    LastName = "Simeone",
                    DateOfBirth = new DateTime(1970, 4, 28),
                    Salary = 14000M,
                    Trophies = 8,
                    TeamId = 5
                },
                new Coach
                {
                    Id = 6,
                    FirstName = "Mauricio",
                    LastName = "Pochettino",
                    DateOfBirth = new DateTime(1972, 3, 2),
                    Salary = 12000M,
                    Trophies = 2,
                    TeamId = 6
                },
                new Coach
                {
                    Id = 7,
                    FirstName = "Mauricio",
                    LastName = "Pochettino",
                    DateOfBirth = new DateTime(1972, 3, 2),
                    Salary = 12000M,
                    Trophies = 2,
                    TeamId = 7
                },
                new Coach
                {
                    Id = 8,
                    FirstName = "Thomas",
                    LastName = "Tuchel",
                    DateOfBirth = new DateTime(1973, 8, 29),
                    Salary = 16000M,
                    Trophies = 6,
                    TeamId = 8
                },
                new Coach
                {
                    Id = 9,
                    FirstName = "Mauricio",
                    LastName = "Pochettino",
                    DateOfBirth = new DateTime(1972, 3, 2),
                    Salary = 12000M,
                    Trophies = 2,
                    TeamId = 9
                },
                new Coach
                {
                    Id = 10,
                    FirstName = "Mauricio",
                    LastName = "Pochettino",
                    DateOfBirth = new DateTime(1972, 3, 2),
                    Salary = 12000M,
                    Trophies = 2,
                    TeamId = 10
                },
                new Coach
                {
                    Id = 11,
                    FirstName = "Mikel",
                    LastName = "Arteta",
                    DateOfBirth = new DateTime(1983, 3, 10),
                    Salary = 16000M,
                    Trophies = 4,
                    TeamId = 11
                }
            );
    }
}
