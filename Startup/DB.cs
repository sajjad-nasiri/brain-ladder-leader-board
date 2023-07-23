using BrainLadder.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrainLadder.Startup;

public class Db : DbContext
{
    public Db(DbContextOptions <Db> options): base(options) {}
    
    public DbSet <LeaderBoard> LeaderBoards { get; set; }
}