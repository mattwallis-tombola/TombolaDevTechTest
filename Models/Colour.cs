using System.ComponentModel.DataAnnotations.Schema;

namespace TombolaDevTechTest.Models;

[Table("Colours")]
public sealed class Colour
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

}
