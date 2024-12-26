using WebApp.Core;
using WebApp.Services.Abstract;

namespace WebApp.Services.Implementations
{
    public class BookService : IBookService

    {
        private readonly Dictionary<int, BookModel> _data;
        private readonly IAuthorsService _authorsService;

        public BookService(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
            _data = new Dictionary<int, BookModel>
            {
                { 1, new BookModel { Title = "Sample Book 1", Author = "John Doe " } },
                { 2, new BookModel { Title = "Sample Book", Author = "John Doe" } },
                { 3, new BookModel { Title = "Sample Book", Author = "John Doe" } },
                { 4, new BookModel { Title = "Sample Book", Author = "John Doe" } },
                { 5, new BookModel { Title = "Sample Book", Author = "John Doe" } },
                { 6, new BookModel { Title = "Sample Book", Author = "John Doe" } },
                { 7, new BookModel { Title = "Sample Book", Author = "John Doe" } },
                { 8, new BookModel { Title = "Sample Book", Author = "John Doe" } },
                { 9, new BookModel { Title = "Sample Book", Author = "John Doe" } }
            };
        }

        public BookModel[] GetBooks()
        {
            return _data.Values.ToArray();
        }

        public BookModel? GetBookById(int id)
        {
            return _data.ContainsKey(id)
                ? _data[id]
                : null;
        }
    }
}
