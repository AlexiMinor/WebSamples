namespace WebApp.Services.Samples;

public interface ITransientService
{
    public int X { get; }
    public void Do();
}

public class TransientService: ITransientService
{
    public int X { get; private set; }
    public void Do()
    {
        Console.WriteLine($"Transient - {X}");
        X++;
    }
}

public interface IScopedService
{
    public int X { get; }
    public void Do();
}
public class ScopedService : IScopedService
{
    public int X { get; private set; }
    public void Do()
    {
        Console.WriteLine($"Scoped - {X}");
        X++;
    }
}
public class ScopedService2 : IScopedService
{
    public int X { get; private set; }
    public void Do()
    {
        X++;
    }
}

public interface ISingletonService
{
    public int X { get; }
    public void Do();
}

public class SingletonService : ISingletonService
{
    public int X { get; private set; }
    public void Do()
    {
        Console.WriteLine($"Singleton - {X}");
        X++;
    }
}

public interface ITestService
{
    
    public void Do();
}

public class TestService : ITestService
{
    private readonly ITransientService _trService;
    private readonly IScopedService _scService;
    private readonly ISingletonService _singletonService;

    public TestService(ITransientService tr1Service,
        IScopedService sc1Service,
        ISingletonService singletonService)
    {
        _trService = tr1Service;
        _scService = sc1Service;
        _singletonService = singletonService;
    }

    public void Do()
    {
        _trService.Do();
        _scService.Do();
        _singletonService.Do();
    }
}