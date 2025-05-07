using System.ComponentModel.DataAnnotations.Schema;

namespace TombolaDevTechTest.Models;

[Table("Countries")]
public sealed class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
