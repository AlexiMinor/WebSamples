﻿namespace EFCoreSampleApp.Data.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    //N.P.
    public List<BookGenre> BookGenres { get; set; }
}