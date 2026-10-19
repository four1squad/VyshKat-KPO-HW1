using VyshKat.App.Interfaces;
using VyshKat.App.Models;
namespace VyshKat.Tests;

public class FakeServiceCenter : IServiceCenter
{
    private readonly bool _result;

    public FakeServiceCenter(bool result)
    {
        _result = result;
    }

    public bool CheckTransport(Transport transport)
    {
        return _result;
    }
}
