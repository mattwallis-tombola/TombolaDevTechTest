using Microsoft.EntityFrameworkCore;
using TombolaDevTechTest.Models;

namespace TombolaDevTechTest;

public class CoffeeDbContext(DbContextOptions<CoffeeDbContext> options) : DbContext(options)
{
    public DbSet<CoffeeBean> CoffeeBeans { get; set; }
    public DbSet<BeanOfTheDayHistory> BeanOfTheDayHistory { get; set; }
    public DbSet<Colour> Colours { get; set; }
    public DbSet<Country> Countries { get; set; }
}

