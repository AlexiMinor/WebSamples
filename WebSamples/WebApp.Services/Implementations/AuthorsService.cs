using WebApp.Services.Abstract;

namespace WebApp.Services.Implementations;

public class AuthorsService : IAuthorsService
{
    private readonly string _someSecretValue;
    private readonly string _someSecretValue2;


    public AuthorsService()
    {
        _someSecretValue = "someSecretValue";
        _someSecretValue2 = "someSecretValue2";
    }
}