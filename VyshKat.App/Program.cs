using Microsoft.Extensions.DependencyInjection;
using VyshKat.App.ConsoleUI;
using VyshKat.App.Interfaces;
using VyshKat.App.Services;

var services = new ServiceCollection();

services.AddSingleton<IServiceCenter, ServiceCenter>();
services.AddSingleton<FleetService>();
services.AddSingleton<ConsoleMenu>();

using var provider = services.BuildServiceProvider();

var menu = provider.GetRequiredService<ConsoleMenu>();
menu.Run();
