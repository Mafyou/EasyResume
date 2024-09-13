using EasyResume.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyResume.API.Context;

public class EasyResumeContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<Person> People { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Compagny> Compagnies { get; set; }
}