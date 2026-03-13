using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models;

public class Command
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)] 
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string HowTo { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string CommandLine { get; set; } = string.Empty;

    [Required]
    public int PlatformId { get; set; }

    public Platform Platform { get; set; } = null!;
    
    public static Command Create(string name)
        => new Command { Name = name };
}