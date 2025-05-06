using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TombolaDevTechTest.Models;

[Table("CoffeeBeans")]
public sealed class CoffeeBean
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    [Required]
    public string BeanId { get; set; } = string.Empty;
    [Required]
    public decimal Cost { get; set; }
    [Required]
    public string ImageUrl { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;

    // Foreign key for Countries
    [Required]
    public int CountryId { get; set; }

    // Foreign key for Colours
    [Required]
    public int ColourId { get; set; }
}
