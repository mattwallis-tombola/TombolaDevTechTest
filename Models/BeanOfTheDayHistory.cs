using System.ComponentModel.DataAnnotations.Schema;

namespace TombolaDevTechTest.Models;

[Table("BeanOfTheDayHistory")]
public class BeanOfTheDayHistory
{
    public int Id { get; set; }
    public string BeanId { get; set; } = string.Empty;
    public DateTime DateStamp { get; set; }
}
