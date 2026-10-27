using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _7_Elephant.Models;

public class Order
{
    public int OrderId { get; set; }

    [StringLength(100)]
    public string? UserEmail { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal? Total { get; set; }

    public string? Address { get; set; }
}
