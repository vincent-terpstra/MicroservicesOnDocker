using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data;

public class CommandsDbContext(DbContextOptions<CommandsDbContext> options) : DbContext(options)
{
    public DbSet<Command> Commands { get; set; }
}