using WebApp.Core;
using WebApp.Services.Abstract;

namespace WebApp.Services.Implementations
{
    public class BookService : IBookService

    {
        private readonly Dictionary<int, BookModel> _data;
        private readonly IAuthorsService _authorsService;

        private const string Description =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed in lobortis risus, vel blandit dui. Sed auctor pellentesque justo in venenatis. Cras elit mauris, accumsan at sollicitudin et, viverra ut mi. In ultrices tortor sapien, quis interdum nulla faucibus nec. Nam euismod augue eget urna iaculis bibendum. Etiam augue augue, porttitor sit amet ultrices ac, laoreet ut est. Curabitur laoreet porta enim eu cursus. Mauris tempor dolor metus, nec molestie dui vulputate sit amet.\r\n\r\nIn euismod vulputate nunc, sit amet faucibus lacus congue et. Duis vulputate lorem sit amet libero eleifend, eget vestibulum lacus consectetur. Quisque eget varius ex. Sed iaculis in ipsum nec lobortis. Nunc sit amet mauris mattis, tincidunt turpis eget, luctus ex. Sed malesuada diam ac orci placerat, a laoreet eros commodo. Nam aliquet eleifend lorem sit amet lacinia. Proin tortor quam, vehicula vel justo sed, accumsan tincidunt ligula. Nulla ullamcorper laoreet ante quis ornare. Duis id turpis vitae diam vehicula ornare.\r\n\r\nMorbi aliquet vel justo quis iaculis. Praesent posuere convallis dolor, nec condimentum turpis scelerisque suscipit. Etiam vulputate in purus non aliquet. Pellentesque congue, ex non tincidunt placerat, turpis dolor blandit velit, a auctor dui purus at nunc. Sed eget convallis metus. Suspendisse ut venenatis lectus, a porttitor dolor. Etiam egestas urna ac pellentesque semper. Ut congue odio eget metus rhoncus, ac sagittis purus scelerisque. Vivamus accumsan consectetur metus, ultrices cursus augue elementum at. Aliquam rhoncus lobortis nisl sed pharetra. Curabitur venenatis ligula id eros condimentum semper. Sed vitae iaculis neque. Donec tempor porttitor leo, non eleifend orci pharetra tincidunt. Praesent sed dapibus nunc. Morbi lobortis accumsan sollicitudin. Sed enim neque, egestas lacinia turpis vitae, vehicula auctor nunc.\r\n\r\nIn nisi augue, cursus non enim a, fermentum rhoncus elit. Maecenas et maximus turpis. Maecenas id eros eu dolor porttitor volutpat aliquam semper velit. Nam dictum sagittis sem a molestie. Nulla at pellentesque ex. Aenean pulvinar odio vitae metus hendrerit lacinia. In pulvinar ac risus vel laoreet. Sed consequat elit sed posuere eleifend. Aliquam vel rhoncus dolor. Integer sit amet nulla pulvinar, congue augue luctus, pretium lectus. Etiam ac sollicitudin nisl.\r\n\r\nVivamus at dolor quis libero gravida semper. Vivamus pellentesque magna tortor, id viverra sapien suscipit vitae. Proin finibus iaculis mauris at imperdiet. Praesent quis mauris convallis, rutrum lacus id, porttitor erat. Praesent elit nisi, mattis sit amet placerat nec, pretium et mauris. Ut semper ex ut turpis maximus, vel laoreet tortor rutrum. Praesent ac libero dolor. Fusce sodales volutpat magna vitae pretium. Sed sem augue, cursus eu turpis a, placerat interdum ligula. Maecenas rutrum tellus id accumsan scelerisque. Sed bibendum, risus eu aliquam tincidunt, urna leo pretium nulla, eu fringilla nulla elit ornare libero. Proin non est et magna efficitur mollis eu non felis.";
        public BookService(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
            _data = new Dictionary<int, BookModel>
            {
                { 1, new BookModel { Id = 1,Title = "Sample Book 1",Description = Description, Author = "John Doe " } },
                { 2, new BookModel { Id = 2,Title = "Sample Book 2",Description = Description, Author = "John Doe" } },
                { 3, new BookModel { Id = 3,Title = "Sample Book 3",Description = Description, Author = "John Doe" } },
                { 4, new BookModel { Id = 4,Title = "Sample Book 4",Description = Description, Author = "John Doe" } },
                { 5, new BookModel { Id = 5,Title = "Sample Book 5",Description = Description, Author = "John Doe" } },
                { 6, new BookModel { Id = 6,Title = "Sample Book 6",Description = Description, Author = "John Doe" } },
                { 7, new BookModel { Id = 7,Title = "Sample Book 7",Description = Description, Author = "John Doe" } },
                { 8, new BookModel { Id = 8,Title = "Sample Book 8",Description = Description, Author = "John Doe" } },
                { 9, new BookModel { Id = 9,Title = "Sample Book 9",Description = Description, Author = "John Doe" } }
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
