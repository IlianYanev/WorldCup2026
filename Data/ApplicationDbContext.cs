using Microsoft.EntityFrameworkCore;
using WorldCup2026.Models;

namespace WorldCup2026.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany()
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany()
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Stadium)
                .WithMany()
                .HasForeignKey(m => m.StadiumId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>().HasData(
                new Team { Id = 1, Name = "Mexico", GroupLetter = "A" },
                new Team { Id = 2, Name = "South Africa", GroupLetter = "A" },
                new Team { Id = 3, Name = "South Korea", GroupLetter = "A" },
                new Team { Id = 4, Name = "Czech Republic", GroupLetter = "A" },
                new Team { Id = 5, Name = "Canada", GroupLetter = "B" },
                new Team { Id = 6, Name = "Bosnia and Herzegovina", GroupLetter = "B" },
                new Team { Id = 7, Name = "Qatar", GroupLetter = "B" },
                new Team { Id = 8, Name = "Switzerland", GroupLetter = "B" },
                new Team { Id = 9, Name = "Brazil", GroupLetter = "C" },
                new Team { Id = 10, Name = "Morocco", GroupLetter = "C" },
                new Team { Id = 11, Name = "Haiti", GroupLetter = "C" },
                new Team { Id = 12, Name = "Scotland", GroupLetter = "C" },
                new Team { Id = 13, Name = "USA", GroupLetter = "D" },
                new Team { Id = 14, Name = "Paraguay", GroupLetter = "D" },
                new Team { Id = 15, Name = "Australia", GroupLetter = "D" },
                new Team { Id = 16, Name = "Turkey", GroupLetter = "D" },
                new Team { Id = 17, Name = "Germany", GroupLetter = "E" },
                new Team { Id = 18, Name = "Curacao", GroupLetter = "E" },
                new Team { Id = 19, Name = "Ivory Coast", GroupLetter = "E" },
                new Team { Id = 20, Name = "Ecuador", GroupLetter = "E" },
                new Team { Id = 21, Name = "Netherlands", GroupLetter = "F" },
                new Team { Id = 22, Name = "Japan", GroupLetter = "F" },
                new Team { Id = 23, Name = "Sweden", GroupLetter = "F" },
                new Team { Id = 24, Name = "Tunisia", GroupLetter = "F" },
                new Team { Id = 25, Name = "Spain", GroupLetter = "G" },
                new Team { Id = 26, Name = "Cape Verde", GroupLetter = "G" },
                new Team { Id = 27, Name = "Belgium", GroupLetter = "G" },
                new Team { Id = 28, Name = "Egypt", GroupLetter = "G" },
                new Team { Id = 29, Name = "Saudi Arabia", GroupLetter = "H" },
                new Team { Id = 30, Name = "Uruguay", GroupLetter = "H" },
                new Team { Id = 31, Name = "Iran", GroupLetter = "H" },
                new Team { Id = 32, Name = "New Zealand", GroupLetter = "H" },
                new Team { Id = 33, Name = "France", GroupLetter = "I" },
                new Team { Id = 34, Name = "Senegal", GroupLetter = "I" },
                new Team { Id = 35, Name = "Iraq", GroupLetter = "I" },
                new Team { Id = 36, Name = "Norway", GroupLetter = "I" },
                new Team { Id = 37, Name = "Argentina", GroupLetter = "J" },
                new Team { Id = 38, Name = "Algeria", GroupLetter = "J" },
                new Team { Id = 39, Name = "Austria", GroupLetter = "J" },
                new Team { Id = 40, Name = "Jordan", GroupLetter = "J" },
                new Team { Id = 41, Name = "Portugal", GroupLetter = "K" },
                new Team { Id = 42, Name = "DR Congo", GroupLetter = "K" },
                new Team { Id = 43, Name = "Uzbekistan", GroupLetter = "K" },
                new Team { Id = 44, Name = "Colombia", GroupLetter = "K" },
                new Team { Id = 45, Name = "England", GroupLetter = "L" },
                new Team { Id = 46, Name = "Croatia", GroupLetter = "L" },
                new Team { Id = 47, Name = "Ghana", GroupLetter = "L" },
                new Team { Id = 48, Name = "Panama", GroupLetter = "L" }
            );

            modelBuilder.Entity<Stadium>().HasData(
                new Stadium { Id = 1, Name = "Estadio Azteca", City = "Mexico City", Country = "Mexico", Capacity = 87523 },
                new Stadium { Id = 2, Name = "Estadio BBVA", City = "Monterrey", Country = "Mexico", Capacity = 53500 },
                new Stadium { Id = 3, Name = "Estadio Akron", City = "Guadalajara", Country = "Mexico", Capacity = 48071 },
                new Stadium { Id = 4, Name = "BC Place", City = "Vancouver", Country = "Canada", Capacity = 54500 },
                new Stadium { Id = 5, Name = "BMO Field", City = "Toronto", Country = "Canada", Capacity = 30000 },
                new Stadium { Id = 6, Name = "MetLife Stadium", City = "East Rutherford", Country = "USA", Capacity = 82500 },
                new Stadium { Id = 7, Name = "AT&T Stadium", City = "Arlington", Country = "USA", Capacity = 80000 },
                new Stadium { Id = 8, Name = "Arrowhead Stadium", City = "Kansas City", Country = "USA", Capacity = 76416 },
                new Stadium { Id = 9, Name = "NRG Stadium", City = "Houston", Country = "USA", Capacity = 72220 },
                new Stadium { Id = 10, Name = "Mercedes-Benz Stadium", City = "Atlanta", Country = "USA", Capacity = 71000 },
                new Stadium { Id = 11, Name = "SoFi Stadium", City = "Inglewood", Country = "USA", Capacity = 70240 },
                new Stadium { Id = 12, Name = "Lincoln Financial Field", City = "Philadelphia", Country = "USA", Capacity = 69796 },
                new Stadium { Id = 13, Name = "Lumen Field", City = "Seattle", Country = "USA", Capacity = 69000 },
                new Stadium { Id = 14, Name = "Levi's Stadium", City = "Santa Clara", Country = "USA", Capacity = 68500 },
                new Stadium { Id = 15, Name = "Gillette Stadium", City = "Foxborough", Country = "USA", Capacity = 65878 },
                new Stadium { Id = 16, Name = "Hard Rock Stadium", City = "Miami Gardens", Country = "USA", Capacity = 64767 }
            );

            modelBuilder.Entity<Match>().HasData(
                new Match { Id = 1, HomeTeamId = 1, AwayTeamId = 2, StadiumId = 1, KickOffTime = DateTime.Parse("2026-06-11T22:00:00"), Status = "Scheduled" },
                new Match { Id = 2, HomeTeamId = 3, AwayTeamId = 4, StadiumId = 4, KickOffTime = DateTime.Parse("2026-06-12T05:00:00"), Status = "Scheduled" },
                new Match { Id = 3, HomeTeamId = 5, AwayTeamId = 6, StadiumId = 5, KickOffTime = DateTime.Parse("2026-06-12T22:00:00"), Status = "Scheduled" },
                new Match { Id = 4, HomeTeamId = 7, AwayTeamId = 8, StadiumId = 11, KickOffTime = DateTime.Parse("2026-06-13T05:00:00"), Status = "Scheduled" },
                new Match { Id = 5, HomeTeamId = 9, AwayTeamId = 10, StadiumId = 6, KickOffTime = DateTime.Parse("2026-06-13T18:00:00"), Status = "Scheduled" },
                new Match { Id = 6, HomeTeamId = 11, AwayTeamId = 12, StadiumId = 7, KickOffTime = DateTime.Parse("2026-06-13T22:00:00"), Status = "Scheduled" },
                new Match { Id = 7, HomeTeamId = 13, AwayTeamId = 14, StadiumId = 12, KickOffTime = DateTime.Parse("2026-06-14T15:00:00"), Status = "Scheduled" },
                new Match { Id = 8, HomeTeamId = 15, AwayTeamId = 16, StadiumId = 8, KickOffTime = DateTime.Parse("2026-06-14T19:00:00"), Status = "Scheduled" },
                new Match { Id = 9, HomeTeamId = 17, AwayTeamId = 18, StadiumId = 13, KickOffTime = DateTime.Parse("2026-06-14T23:00:00"), Status = "Scheduled" },
                new Match { Id = 10, HomeTeamId = 19, AwayTeamId = 20, StadiumId = 9, KickOffTime = DateTime.Parse("2026-06-15T16:00:00"), Status = "Scheduled" },
                new Match { Id = 11, HomeTeamId = 21, AwayTeamId = 22, StadiumId = 14, KickOffTime = DateTime.Parse("2026-06-15T20:00:00"), Status = "Scheduled" },
                new Match { Id = 12, HomeTeamId = 23, AwayTeamId = 24, StadiumId = 10, KickOffTime = DateTime.Parse("2026-06-15T23:59:00"), Status = "Scheduled" },
                new Match { Id = 13, HomeTeamId = 25, AwayTeamId = 26, StadiumId = 15, KickOffTime = DateTime.Parse("2026-06-16T15:00:00"), Status = "Scheduled" },
                new Match { Id = 14, HomeTeamId = 27, AwayTeamId = 28, StadiumId = 2, KickOffTime = DateTime.Parse("2026-06-16T19:00:00"), Status = "Scheduled" },
                new Match { Id = 15, HomeTeamId = 29, AwayTeamId = 30, StadiumId = 3, KickOffTime = DateTime.Parse("2026-06-16T23:00:00"), Status = "Scheduled" },
                new Match { Id = 16, HomeTeamId = 31, AwayTeamId = 32, StadiumId = 16, KickOffTime = DateTime.Parse("2026-06-17T16:00:00"), Status = "Scheduled" },
                new Match { Id = 17, HomeTeamId = 33, AwayTeamId = 34, StadiumId = 1, KickOffTime = DateTime.Parse("2026-06-17T20:00:00"), Status = "Scheduled" },
                new Match { Id = 18, HomeTeamId = 35, AwayTeamId = 36, StadiumId = 4, KickOffTime = DateTime.Parse("2026-06-17T23:59:00"), Status = "Scheduled" },
                new Match { Id = 19, HomeTeamId = 37, AwayTeamId = 38, StadiumId = 6, KickOffTime = DateTime.Parse("2026-06-18T18:00:00"), Status = "Scheduled" },
                new Match { Id = 20, HomeTeamId = 39, AwayTeamId = 40, StadiumId = 7, KickOffTime = DateTime.Parse("2026-06-18T22:00:00"), Status = "Scheduled" },
                new Match { Id = 21, HomeTeamId = 41, AwayTeamId = 42, StadiumId = 8, KickOffTime = DateTime.Parse("2026-06-18T23:59:00"), Status = "Scheduled" },
                new Match { Id = 22, HomeTeamId = 43, AwayTeamId = 44, StadiumId = 9, KickOffTime = DateTime.Parse("2026-06-19T15:00:00"), Status = "Scheduled" },
                new Match { Id = 23, HomeTeamId = 45, AwayTeamId = 46, StadiumId = 10, KickOffTime = DateTime.Parse("2026-06-19T19:00:00"), Status = "Scheduled" },
                new Match { Id = 24, HomeTeamId = 47, AwayTeamId = 48, StadiumId = 11, KickOffTime = DateTime.Parse("2026-06-19T23:00:00"), Status = "Scheduled" },
                new Match { Id = 25, HomeTeamId = 1, AwayTeamId = 3, StadiumId = 1, KickOffTime = DateTime.Parse("2026-06-20T16:00:00"), Status = "Scheduled" },
                new Match { Id = 26, HomeTeamId = 2, AwayTeamId = 4, StadiumId = 2, KickOffTime = DateTime.Parse("2026-06-20T20:00:00"), Status = "Scheduled" },
                new Match { Id = 27, HomeTeamId = 5, AwayTeamId = 7, StadiumId = 4, KickOffTime = DateTime.Parse("2026-06-20T23:59:00"), Status = "Scheduled" },
                new Match { Id = 28, HomeTeamId = 6, AwayTeamId = 8, StadiumId = 5, KickOffTime = DateTime.Parse("2026-06-21T15:00:00"), Status = "Scheduled" },
                new Match { Id = 29, HomeTeamId = 9, AwayTeamId = 11, StadiumId = 12, KickOffTime = DateTime.Parse("2026-06-21T19:00:00"), Status = "Scheduled" },
                new Match { Id = 30, HomeTeamId = 10, AwayTeamId = 12, StadiumId = 13, KickOffTime = DateTime.Parse("2026-06-21T23:00:00"), Status = "Scheduled" },
                new Match { Id = 31, HomeTeamId = 13, AwayTeamId = 15, StadiumId = 11, KickOffTime = DateTime.Parse("2026-06-22T16:00:00"), Status = "Scheduled" },
                new Match { Id = 32, HomeTeamId = 14, AwayTeamId = 16, StadiumId = 14, KickOffTime = DateTime.Parse("2026-06-22T20:00:00"), Status = "Scheduled" },
                new Match { Id = 33, HomeTeamId = 17, AwayTeamId = 19, StadiumId = 15, KickOffTime = DateTime.Parse("2026-06-22T23:59:00"), Status = "Scheduled" },
                new Match { Id = 34, HomeTeamId = 18, AwayTeamId = 20, StadiumId = 3, KickOffTime = DateTime.Parse("2026-06-23T15:00:00"), Status = "Scheduled" },
                new Match { Id = 35, HomeTeamId = 21, AwayTeamId = 23, StadiumId = 16, KickOffTime = DateTime.Parse("2026-06-23T19:00:00"), Status = "Scheduled" },
                new Match { Id = 36, HomeTeamId = 22, AwayTeamId = 24, StadiumId = 6, KickOffTime = DateTime.Parse("2026-06-23T23:00:00"), Status = "Scheduled" },
                new Match { Id = 37, HomeTeamId = 25, AwayTeamId = 27, StadiumId = 7, KickOffTime = DateTime.Parse("2026-06-24T16:00:00"), Status = "Scheduled" },
                new Match { Id = 38, HomeTeamId = 26, AwayTeamId = 28, StadiumId = 8, KickOffTime = DateTime.Parse("2026-06-24T20:00:00"), Status = "Scheduled" },
                new Match { Id = 39, HomeTeamId = 29, AwayTeamId = 31, StadiumId = 9, KickOffTime = DateTime.Parse("2026-06-24T23:59:00"), Status = "Scheduled" },
                new Match { Id = 40, HomeTeamId = 30, AwayTeamId = 32, StadiumId = 10, KickOffTime = DateTime.Parse("2026-06-25T15:00:00"), Status = "Scheduled" },
                new Match { Id = 41, HomeTeamId = 33, AwayTeamId = 35, StadiumId = 1, KickOffTime = DateTime.Parse("2026-06-25T19:00:00"), Status = "Scheduled" },
                new Match { Id = 42, HomeTeamId = 34, AwayTeamId = 36, StadiumId = 4, KickOffTime = DateTime.Parse("2026-06-25T23:00:00"), Status = "Scheduled" },
                new Match { Id = 43, HomeTeamId = 37, AwayTeamId = 39, StadiumId = 5, KickOffTime = DateTime.Parse("2026-06-26T16:00:00"), Status = "Scheduled" },
                new Match { Id = 44, HomeTeamId = 38, AwayTeamId = 40, StadiumId = 12, KickOffTime = DateTime.Parse("2026-06-26T20:00:00"), Status = "Scheduled" },
                new Match { Id = 45, HomeTeamId = 41, AwayTeamId = 43, StadiumId = 13, KickOffTime = DateTime.Parse("2026-06-26T23:59:00"), Status = "Scheduled" },
                new Match { Id = 46, HomeTeamId = 42, AwayTeamId = 44, StadiumId = 14, KickOffTime = DateTime.Parse("2026-06-27T15:00:00"), Status = "Scheduled" },
                new Match { Id = 47, HomeTeamId = 45, AwayTeamId = 47, StadiumId = 11, KickOffTime = DateTime.Parse("2026-06-27T19:00:00"), Status = "Scheduled" },
                new Match { Id = 48, HomeTeamId = 46, AwayTeamId = 48, StadiumId = 15, KickOffTime = DateTime.Parse("2026-06-27T23:00:00"), Status = "Scheduled" },
                new Match { Id = 49, HomeTeamId = 4, AwayTeamId = 1, StadiumId = 3, KickOffTime = DateTime.Parse("2026-06-28T18:00:00"), Status = "Scheduled" },
                new Match { Id = 50, HomeTeamId = 2, AwayTeamId = 3, StadiumId = 1, KickOffTime = DateTime.Parse("2026-06-28T18:00:00"), Status = "Scheduled" },
                new Match { Id = 51, HomeTeamId = 8, AwayTeamId = 5, StadiumId = 4, KickOffTime = DateTime.Parse("2026-06-28T22:00:00"), Status = "Scheduled" },
                new Match { Id = 52, HomeTeamId = 6, AwayTeamId = 7, StadiumId = 5, KickOffTime = DateTime.Parse("2026-06-28T22:00:00"), Status = "Scheduled" },
                new Match { Id = 53, HomeTeamId = 12, AwayTeamId = 9, StadiumId = 6, KickOffTime = DateTime.Parse("2026-06-29T18:00:00"), Status = "Scheduled" },
                new Match { Id = 54, HomeTeamId = 10, AwayTeamId = 11, StadiumId = 16, KickOffTime = DateTime.Parse("2026-06-29T18:00:00"), Status = "Scheduled" },
                new Match { Id = 55, HomeTeamId = 16, AwayTeamId = 13, StadiumId = 7, KickOffTime = DateTime.Parse("2026-06-29T22:00:00"), Status = "Scheduled" },
                new Match { Id = 56, HomeTeamId = 14, AwayTeamId = 15, StadiumId = 8, KickOffTime = DateTime.Parse("2026-06-29T22:00:00"), Status = "Scheduled" },
                new Match { Id = 57, HomeTeamId = 20, AwayTeamId = 17, StadiumId = 11, KickOffTime = DateTime.Parse("2026-06-30T18:00:00"), Status = "Scheduled" },
                new Match { Id = 58, HomeTeamId = 19, AwayTeamId = 18, StadiumId = 9, KickOffTime = DateTime.Parse("2026-06-30T18:00:00"), Status = "Scheduled" },
                new Match { Id = 59, HomeTeamId = 24, AwayTeamId = 21, StadiumId = 12, KickOffTime = DateTime.Parse("2026-06-30T22:00:00"), Status = "Scheduled" },
                new Match { Id = 60, HomeTeamId = 22, AwayTeamId = 23, StadiumId = 13, KickOffTime = DateTime.Parse("2026-06-30T22:00:00"), Status = "Scheduled" },
                new Match { Id = 61, HomeTeamId = 28, AwayTeamId = 25, StadiumId = 14, KickOffTime = DateTime.Parse("2026-07-01T18:00:00"), Status = "Scheduled" },
                new Match { Id = 62, HomeTeamId = 26, AwayTeamId = 27, StadiumId = 10, KickOffTime = DateTime.Parse("2026-07-01T18:00:00"), Status = "Scheduled" },
                new Match { Id = 63, HomeTeamId = 32, AwayTeamId = 29, StadiumId = 15, KickOffTime = DateTime.Parse("2026-07-01T22:00:00"), Status = "Scheduled" },
                new Match { Id = 64, HomeTeamId = 31, AwayTeamId = 30, StadiumId = 2, KickOffTime = DateTime.Parse("2026-07-01T22:00:00"), Status = "Scheduled" },
                new Match { Id = 65, HomeTeamId = 36, AwayTeamId = 33, StadiumId = 1, KickOffTime = DateTime.Parse("2026-07-02T18:00:00"), Status = "Scheduled" },
                new Match { Id = 66, HomeTeamId = 34, AwayTeamId = 35, StadiumId = 4, KickOffTime = DateTime.Parse("2026-07-02T18:00:00"), Status = "Scheduled" },
                new Match { Id = 67, HomeTeamId = 40, AwayTeamId = 37, StadiumId = 5, KickOffTime = DateTime.Parse("2026-07-02T22:00:00"), Status = "Scheduled" },
                new Match { Id = 68, HomeTeamId = 38, AwayTeamId = 39, StadiumId = 6, KickOffTime = DateTime.Parse("2026-07-02T22:00:00"), Status = "Scheduled" },
                new Match { Id = 69, HomeTeamId = 44, AwayTeamId = 41, StadiumId = 11, KickOffTime = DateTime.Parse("2026-07-03T18:00:00"), Status = "Scheduled" },
                new Match { Id = 70, HomeTeamId = 43, AwayTeamId = 42, StadiumId = 16, KickOffTime = DateTime.Parse("2026-07-03T18:00:00"), Status = "Scheduled" },
                new Match { Id = 71, HomeTeamId = 48, AwayTeamId = 45, StadiumId = 7, KickOffTime = DateTime.Parse("2026-07-03T22:00:00"), Status = "Scheduled" },
                new Match { Id = 72, HomeTeamId = 46, AwayTeamId = 47, StadiumId = 8, KickOffTime = DateTime.Parse("2026-07-03T22:00:00"), Status = "Scheduled" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@worldcup2026.com",
                    //Password is "Admin123!"
                    PasswordHash = "$2a$11$6pD4R7Zp4M1i8gE6fXm9UeZ8G5vK0T4vJ7yL0f6R2z4A5wS6n3B6e",
                    Role = "Admin"
                }
            );
        }
    }
}