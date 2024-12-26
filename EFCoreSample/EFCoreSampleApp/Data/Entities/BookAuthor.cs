namespace EFCoreSampleApp.Data.Entities;

public class BookAuthor
{
    public Guid Id { get; set; }
    
    public string BookId { get; set; }
    public Book Book{ get; set; }
    
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}