using Football_Insight.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football_Insight.Data.Configurations
{
    public class StadiumConfiguration : IEntityTypeConfiguration<Stadium>
    {
        public void Configure(EntityTypeBuilder<Stadium> builder)
        {
            builder.HasData(
                new Stadium { Id = 1, Name = "Emirates Stadium", Address = "Highbury House, 75 Drayton Park, London", Capacity = 60704, YearBuilt = 2006 },
                new Stadium { Id = 2, Name = "Old Trafford", Address = "Sir Matt Busby Way, Manchester", Capacity = 74879, YearBuilt = 1910 },
                new Stadium { Id = 3, Name = "Camp Nou", Address = "C. d'Aristides Maillol, 12, Barcelona", Capacity = 99354, YearBuilt = 1957 },
                new Stadium { Id = 4, Name = "Wembley Stadium", Address = "London, HA9 0WS", Capacity = 90000, YearBuilt = 2007 },
                new Stadium { Id = 5, Name = "Maracanã", Address = "Av. Pres. Castelo Branco, Rio de Janeiro", Capacity = 78838, YearBuilt = 1950 },
                new Stadium { Id = 6, Name = "San Siro", Address = "Piazzale Angelo Moratti, Milan", Capacity = 80018, YearBuilt = 1926 },
                new Stadium { Id = 7, Name = "Estadio Azteca", Address = "Calz. de Tlalpan 3665, Mexico City", Capacity = 87523, YearBuilt = 1966 }
                );
        }
    }
}
