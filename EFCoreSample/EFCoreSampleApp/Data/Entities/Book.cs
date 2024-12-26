using System.ComponentModel.DataAnnotations;

namespace EFCoreSampleApp.Data.Entities;

public class Book
{
    public string Id { get; set; } //ISBN 

    //[Key]
    //public string ISBN { get; set; } //ISBN 
    public string Name { get; set; }
    public string PressName { get; set;}
    public decimal Price { get; set;}

    public List<BookAuthor> BookAuthors{ get; set; }
    public List<BookGenre> BookGenres{ get; set; }
    
}