namespace EFCoreSampleApp.Data.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    //nav prop-s 
    public List<Address> Addresses { get; set; }
    public List<Order> Orders { get; set; }
}