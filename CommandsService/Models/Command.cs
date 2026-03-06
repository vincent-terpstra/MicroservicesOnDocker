using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models;

public class Command
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }

    public static Command Create(string name)
        => new Command { Name = name };
}