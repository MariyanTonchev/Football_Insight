using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football_Insight.Data.Configurations
{
    public class StadiumConfiguration : IEntityTypeConfiguration<Stadium>
    {
        public void Configure(EntityTypeBuilder<Stadium> builder)
        {
            builder.HasData(
                new Stadium { Id = 1, Name = "Old Trafford", Address = "Sir Matt Busby Way, Manchester", Capacity = 74879, YearBuilt = 1910 },
                new Stadium { Id = 2, Name = "Santiago Bernabéu", Address = "Av. de Concha Espina, 1, Madrid", Capacity = 81044, YearBuilt = 1947 },
                new Stadium { Id = 3, Name = "Allianz Arena", Address = "Werner-Heisenberg-Allee 25, Munich", Capacity = 75000, YearBuilt = 2005 },
                new Stadium { Id = 4, Name = "San Siro", Address = "Piazzale Angelo Moratti, Milan", Capacity = 80018, YearBuilt = 1926 },
                new Stadium { Id = 5, Name = "Stade de France", Address = "Rue Henri Delaunay, Saint-Denis, Paris", Capacity = 81338, YearBuilt = 1998 },
                new Stadium { Id = 6, Name = "Stamford Bridge", Address = "Fulham Road, Fulham, London SW6 1HS", Capacity = 40343, YearBuilt = 1935 },
                new Stadium { Id = 7, Name = "Camp Nou", Address = "C. d'Aristides Maillol, 12, Barcelona", Capacity = 99354, YearBuilt = 1957 },
                new Stadium { Id = 8, Name = "Signal Iduna Park", Address = "Strobelallee 50, 44139 Dortmund, North Rhine-Westphalia, Germany", Capacity = 81365, YearBuilt = 1974},
                new Stadium { Id = 9, Name = "Emirates Stadium", Address = "Highbury House, 75 Drayton Park, London", Capacity = 60704, YearBuilt = 2006 },
                new Stadium { Id = 10, Name = "Anfield", Address = "Anfield Rd, Liverpool", Capacity = 53394, YearBuilt = 1884 }
                );
        }
    }
}
