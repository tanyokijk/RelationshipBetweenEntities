using System.Reflection;

namespace Data;
using Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Security;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

public class DataContex : DbContext
{
    public DataContex()
    {
        this.Database.EnsureDeleted();
        this.Database.EnsureCreated();
    }

    public DbSet<Team> Teams => this.Set<Team>();

    public DbSet<Player> Players => this.Set<Player>();

    public DbSet<Model.Match> Matches => this.Set<Model.Match>();

    public DbSet<Goal> Goals => this.Set<Goal>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=standings.sqlite;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>()
            .HasOne(p => p.Team)
            .WithMany(t => t.Players)
            .HasForeignKey(p => p.TeamId);

        modelBuilder.Entity<Model.Match>()
            .HasMany(m => m.Teams)
            .WithMany(t => t.Matches);

        modelBuilder.Entity<Goal>()
            .HasOne(g => g.Player)
            .WithMany(p => p.Goals)
            .HasForeignKey(p => p.PlayerId);

        modelBuilder.Entity<Goal>()
            .HasOne(g => g.Match)
            .WithMany(m => m.Goals)
            .HasForeignKey(p => p.MatchId);
    }
}