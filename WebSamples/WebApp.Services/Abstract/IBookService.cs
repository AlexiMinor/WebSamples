using WebApp.Core;

namespace WebApp.Services.Abstract;

public interface IBookService
{
    BookModel[] GetBooks();
    BookModel? GetBookById(int id);
}