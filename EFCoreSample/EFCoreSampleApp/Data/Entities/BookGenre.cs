namespace EFCoreSampleApp.Data.Entities;

public class BookGenre
{
    public Guid Id { get; set; }
    
    public double? UserAccuracyRating { get; set; }
    
    public string BookId { get; set; }
    public Book Book{ get; set; }
    
    public Guid GenreId { get; set; }
    public Genre Genre { get; set; }
}