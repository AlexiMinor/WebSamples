using EFCoreSampleApp.Data;
using EFCoreSampleApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSampleApp;

public class AddManyEntitiesSample
{
    private readonly Author[] _authors;

    //1000, 10k, 100k
    public AddManyEntitiesSample(int length)
    {
        _authors = new Author[length];
        for (int i = 0; i < length; i++)
        {
            _authors[i] = new Author()
            {
                Id = i,
                Name = $"Author {i}"
            };
        }
    }

    public void InsertManyEntities()
    {

    }

    public void InsertManyEntities1()
    {
        foreach (var author in _authors)
        {
            using (var dbContext = new BookStoreContext())
            {
                dbContext.Authors.Add(author);
                dbContext.SaveChanges();
            }
        }
    }

    public void InsertManyEntities2()
    {
        using (var dbContext = new BookStoreContext())
        {
            foreach (var author in _authors)
            {
                dbContext.Authors.Add(author);
                dbContext.SaveChanges();
            }
        }
    }
    public void InsertManyEntities3()
    {
        using (var dbContext = new BookStoreContext())
        {
            foreach (var author in _authors)
            {
                dbContext.Authors.Add(author);

            }
            dbContext.SaveChanges();
        }
    }

    public void InsertManyEntities3()
    {
        using (var dbContext = new BookStoreContext())
        {
            dbContext.Authors.AddRange(_authors);

            dbContext.SaveChanges();
        }
    }

}