using System;
using System.ComponentModel.DataAnnotations;

public class Asset
{
    [Key]
    public int Id { get; set; }
    public string Type { get; set; }    // "Computer" or "Phone"
    public string Brand { get; set; }
    public string ModelName { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int Price { get; set; }
    public DateTime EndOfLife { get; set; }
}