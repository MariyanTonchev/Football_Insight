using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football_Insight.Data.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            var players = new List<Player> {
                new Player { Id = 1, FirstName = "Andre", LastName = "Onana", DateOfBirth = new DateTime(1990, 11, 7), Position = (int)PlayerPosition.Goalkeeper, TeamId = 1, Salary = 250000m, Price = 15000000m },
                new Player { Id = 2, FirstName = "Harry", LastName = "Maguire", DateOfBirth = new DateTime(1993, 3, 5), Position = (int)PlayerPosition.Defender, TeamId = 1, Salary = 4500000m, Price = 35000000m },
                new Player { Id = 3, FirstName = "Bruno", LastName = "Fernandes", DateOfBirth = new DateTime(1994, 9, 8), Position = (int)PlayerPosition.Midfielder, TeamId = 1, Salary = 6000000m, Price = 75000000m },
                new Player { Id = 4, FirstName = "Marcus", LastName = "Rashford", DateOfBirth = new DateTime(1997, 10, 31), Position = (int)PlayerPosition.Forward, TeamId = 1, Salary = 5000000m, Price = 55000000m },
                new Player { Id = 5, FirstName = "Thibaut", LastName = "Courtois", DateOfBirth = new DateTime(1992, 5, 11), Position = (int)PlayerPosition.Goalkeeper, TeamId = 2, Salary = 5000000m, Price = 50000000m },
                new Player { Id = 6, FirstName = "David", LastName = "Alaba", DateOfBirth = new DateTime(1986, 3, 30), Position = (int)PlayerPosition.Defender, TeamId = 2, Salary = 4500000m, Price = 35000000m },
                new Player { Id = 7, FirstName = "Luka", LastName = "Modric", DateOfBirth = new DateTime(1985, 9, 9), Position = (int)PlayerPosition.Midfielder, TeamId = 2, Salary = 8000000m, Price = 30000000m },
                new Player { Id = 8, FirstName = "Vinicius", LastName = "Junior", DateOfBirth = new DateTime(1987, 12, 19), Position = (int)PlayerPosition.Forward, TeamId = 2, Salary = 5000000m, Price = 80000000m },
                new Player { Id = 9, FirstName = "Manuel", LastName = "Neuer", DateOfBirth = new DateTime(1986, 3, 27), Position = (int)PlayerPosition.Goalkeeper, TeamId = 3, Salary = 7000000m, Price = 20000000m },
                new Player { Id = 10, FirstName = "Lucas", LastName = "Hernandez", DateOfBirth = new DateTime(1996, 2, 14), Position = (int)PlayerPosition.Defender, TeamId = 3, Salary = 4500000m, Price = 60000000m },
                new Player { Id = 11, FirstName = "Joshua", LastName = "Kimmich", DateOfBirth = new DateTime(1995, 2, 8), Position = (int)PlayerPosition.Midfielder, TeamId = 3, Salary = 8000000m, Price = 85000000m },
                new Player { Id = 12, FirstName = "Thomas", LastName = "Muller", DateOfBirth = new DateTime(1989, 9, 13), Position = (int)PlayerPosition.Forward, TeamId = 3, Salary = 7000000m, Price = 40000000m },
                new Player { Id = 13, FirstName = "Mike", LastName = "Maignan", DateOfBirth = new DateTime(1995, 7, 3), Position = (int)PlayerPosition.Goalkeeper, TeamId = 4, Salary = 3000000m, Price = 25000000m },
                new Player { Id = 14, FirstName = "Fikayo", LastName = "Tomori", DateOfBirth = new DateTime(1997, 12, 19), Position = (int)PlayerPosition.Defender, TeamId = 4, Salary = 3000000m, Price = 28000000m },
                new Player { Id = 15, FirstName = "Christian", LastName = "Pulisic", DateOfBirth = new DateTime(2000, 5, 8), Position = (int)PlayerPosition.Midfielder, TeamId = 4, Salary = 5000000m, Price = 50000000m },
                new Player { Id = 16, FirstName = "Olivier", LastName = "Giroud", DateOfBirth = new DateTime(1986, 9, 30), Position = (int)PlayerPosition.Forward, TeamId = 4, Salary = 4000000m, Price = 20000000m },
                new Player { Id = 17, FirstName = "Gianluigi", LastName = "Donnarumma", DateOfBirth = new DateTime(1999, 2, 25), Position = (int)PlayerPosition.Goalkeeper, TeamId = 5, Salary = 4503000m, Price = 10000000m },
                new Player { Id = 18, FirstName = "Marquinhos", LastName = "Correa", DateOfBirth = new DateTime(1994, 5, 14), Position = (int)PlayerPosition.Defender, TeamId = 5, Salary = 4000000m, Price = 20000000m  },
                new Player { Id = 19, FirstName = "Marco", LastName = "Verratti", DateOfBirth = new DateTime(1992, 11, 5), Position = (int)PlayerPosition.Midfielder, TeamId = 5, Salary = 6000000m, Price = 55000000m },
                new Player { Id = 20, FirstName = "Kylian", LastName = "Mbappe", DateOfBirth = new DateTime(1998, 12, 20), Position = (int)PlayerPosition.Forward, TeamId = 5, Salary = 18000000m, Price = 180000000m },
                new Player { Id = 21, FirstName = "Robert", LastName = "Sanchez", DateOfBirth = new DateTime(1992, 3, 1), Position = (int)PlayerPosition.Goalkeeper, TeamId = 6, Salary = 2500000m, Price = 12000000m },
                new Player { Id = 22, FirstName = "Thiago", LastName = "Silva", DateOfBirth = new DateTime(1984, 9, 22), Position = (int)PlayerPosition.Defender, TeamId = 6, Salary = 5000000m, Price = 5000000m },
                new Player { Id = 23, FirstName = "Cole", LastName = "Palmer", DateOfBirth = new DateTime(1991, 3, 29), Position = (int)PlayerPosition.Midfielder, TeamId = 6, Salary = 1000000m, Price = 8000000m },
                new Player { Id = 24, FirstName = "Nicolas", LastName = "Jackson", DateOfBirth = new DateTime(1999, 6, 11), Position = (int)PlayerPosition.Forward, TeamId = 6, Salary = 900000m, Price = 7000000m },
                new Player { Id = 25, FirstName = "Marc-Andre", LastName = "ter Stegen", DateOfBirth = new DateTime(1992, 4, 30), Position = (int)PlayerPosition.Goalkeeper, TeamId = 7, Salary = 7500000m, Price = 45000000m },
                new Player { Id = 26, FirstName = "Eric", LastName = "Garcia", DateOfBirth = new DateTime(1987, 2, 2), Position = (int)PlayerPosition.Defender, TeamId = 7, Salary = 3000000m, Price = 25000000m },
                new Player { Id = 27, FirstName = "Frenkie", LastName = "de Jong", DateOfBirth = new DateTime(1997, 5, 12), Position = (int)PlayerPosition.Midfielder, TeamId = 7, Salary = 6000000m, Price = 80000000m },
                new Player { Id = 28, FirstName = "Robert", LastName = "Lewandowski", DateOfBirth = new DateTime(2002, 10, 31), Position = (int)PlayerPosition.Forward, TeamId = 7, Salary = 10000000m, Price = 90000000m },
                new Player { Id = 29, FirstName = "Gregor", LastName = "Kobel", DateOfBirth = new DateTime(1997, 12, 6), Position = (int)PlayerPosition.Goalkeeper, TeamId = 8, Salary = 2000000m, Price = 15000000m },
                new Player { Id = 30, FirstName = "Mats", LastName = "Hummels", DateOfBirth = new DateTime(1988, 12, 16), Position = (int)PlayerPosition.Defender, TeamId = 8, Salary = 5000000m, Price = 10000000m },
                new Player { Id = 31, FirstName = "Marco", LastName = "Reus", DateOfBirth = new DateTime(2003, 6, 29), Position = (int)PlayerPosition.Midfielder, TeamId = 8, Salary = 7000000m, Price = 30000000m },
                new Player { Id = 32, FirstName = "Donyell", LastName = "Malen", DateOfBirth = new DateTime(1999, 1, 19), Position = (int)PlayerPosition.Forward, TeamId = 8, Salary = 1500000m, Price = 20000000m },
                new Player { Id = 33, FirstName = "Bukayo", LastName = "Saka", DateOfBirth = new DateTime(2001, 9, 5), Position = (int)PlayerPosition.Forward, TeamId = 9, Salary = 3000000m, Price = 35000000m },
                new Player { Id = 34, FirstName = "Martin", LastName = "Odegaard", DateOfBirth = new DateTime(1998, 12, 17), Position = (int)PlayerPosition.Midfielder, TeamId = 9, Salary = 4000000m, Price = 45000000m },
                new Player { Id = 35, FirstName = "Gabriel", LastName = "Magalhaes", DateOfBirth = new DateTime(1997, 12, 19), Position = (int)PlayerPosition.Defender, TeamId = 9, Salary = 2000000m, Price = 25000000m },
                new Player { Id = 36, FirstName = "Aaron", LastName = "Ramsdale", DateOfBirth = new DateTime(1998, 5, 14), Position = (int)PlayerPosition.Goalkeeper, TeamId = 9, Salary = 3000000m, Price = 18000000m },
                new Player { Id = 37, FirstName = "Alisson", LastName = "Becker", DateOfBirth = new DateTime(1992, 10, 2), Position = (int)PlayerPosition.Goalkeeper, TeamId = 10, Salary = 8000000m, Price = 60000000m },
                new Player { Id = 38, FirstName = "Virgil", LastName = "van Dijk", DateOfBirth = new DateTime(1991, 7, 8), Position = (int)PlayerPosition.Defender, TeamId = 10, Salary = 8500000m, Price = 75000000m },
                new Player { Id = 39, FirstName = "Thiago", LastName = "Alcântara", DateOfBirth = new DateTime(1991, 4, 11), Position = (int)PlayerPosition.Midfielder, TeamId = 10, Salary = 7000000m, Price = 40000000m },
                new Player { Id = 40, FirstName = "Mohamed", LastName = "Salah", DateOfBirth = new DateTime(1992, 6, 15), Position = (int)PlayerPosition.Forward, TeamId = 10, Salary = 10000000m, Price = 120000000m }
            };

            builder.HasData(players);
        }
    }
}