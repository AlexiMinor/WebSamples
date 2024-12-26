namespace EFCoreSampleApp.Data.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    
    public Guid ClientId { get; set; }
    public Client Client { get; set; }

    //public Guid AddressId { get; set; }
    //public Address Address{ get; set; }

}