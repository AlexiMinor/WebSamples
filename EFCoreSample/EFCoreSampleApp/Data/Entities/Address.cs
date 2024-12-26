namespace EFCoreSampleApp.Data.Entities;

public class Address
{
    public Guid Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    
    //FK
    public Guid ClientId { get; set; }
    //nav property
    public Client Client { get; set; }
}