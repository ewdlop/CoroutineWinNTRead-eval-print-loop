namespace CoroutineWinNTRepl;

using System;
using System.Threading.Tasks;

/// <summary>
/// Handler for Click messages
/// </summary>
public class ClickCommandHandler : ICommandHandler<Click>
{
    public Task HandleAsync(Click message)
    {
        Console.WriteLine($"Click at ({message.X},{message.Y})");
        return Task.CompletedTask;
    }
}

/// <summary>
/// Handler for Key messages
/// </summary>
public class KeyCommandHandler : ICommandHandler<Key>
{
    public Task HandleAsync(Key message)
    {
        Console.WriteLine($"Key: {message.Ch}");
        return Task.CompletedTask;
    }
}

/// <summary>
/// Handler for Tick messages
/// </summary>
public class TickCommandHandler : ICommandHandler<Tick>
{
    public Task HandleAsync(Tick message)
    {
        Console.WriteLine($"Tick @ {message.At:o}");
        return Task.CompletedTask;
    }
}

/// <summary>
/// Handler for Quit messages
/// </summary>
public class QuitCommandHandler : ICommandHandler<Quit>
{
    public bool ShouldQuit { get; private set; }

    public Task HandleAsync(Quit message)
    {
        Console.WriteLine("Quit received. Exiting loop.");
        ShouldQuit = true;
        return Task.CompletedTask;
    }
}
