using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _7_Elephant.Models;

public class Phone
{
    public int ProductId { get; set; }

    [StringLength(50)]
    public string? Brand { get; set; }

    [StringLength(50)]
    public string? Model { get; set; }

    [Column(TypeName = "money")]
    public decimal? Price { get; set; }

    [Column(TypeName = "money")]
    public decimal? Cost { get; set; }

    public string? Url { get; set; }
}
