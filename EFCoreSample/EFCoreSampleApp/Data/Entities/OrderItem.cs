namespace EFCoreSampleApp.Data.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    public string BookId { get; set; }
    public Book Book { get; set; }
}