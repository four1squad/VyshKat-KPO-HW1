
using VyshKat.App.Interfaces;
using VyshKat.App.Models;
namespace VyshKat.App.Services;

public class ServiceCenter : IServiceCenter
{
    public bool CheckTransport(Transport transport)
    {
        return transport != null && transport.IsWorking;
    }
}
