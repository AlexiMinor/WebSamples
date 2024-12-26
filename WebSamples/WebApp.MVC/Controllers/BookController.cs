using Microsoft.AspNetCore.Mvc;
using WebApp.Services.Abstract;

namespace WebApp.MVC.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;
    
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    public IActionResult Index()
    {
        var books = _bookService.GetBooks();
        return View(books);

    }

    public IActionResult Details(int id)
    {
        var book = _bookService.GetBookById(id);
        if (book != null)
        {
            return View(book);
        }
        return NotFound();
    }

    //public IActionResult AddNew(int id)
    //{

    //}
}