using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models;

public class Platform
{
    [Key, Required]
    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public required string Name { get; set; }
    
    [Required, MaxLength(50)]
    public required string Publisher { get; set; }
    
    [MaxLength(50)]
    public required string Cost { get; set; }
    
    [MaxLength(200)]
    public required string Description { get; set; }


    public static Platform Create(string name, string publisher, string cost, string description)
    {
        return new Platform()
        {
            Name = name,
            Publisher = publisher,
            Cost = cost,
            Description = description
        };
    }
}