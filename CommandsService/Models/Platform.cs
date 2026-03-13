using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Models;

[PrimaryKey(nameof(Platform.Id))]
public class Platform {
    
    [Key]
    public int Id { get; set; } 
    
    [Key]
    public int ExternalId { get; set; }
    
    [Required, MaxLength(50)]
    public string Name { get; set; }  = string.Empty;
    
    public ICollection<Command> Commands { get; set; } = null!;
};