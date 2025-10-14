namespace CoroutineWinNTRepl;

using System.Threading.Tasks;

/// <summary>
/// Command interface following the Command pattern.
/// Each command encapsulates a request as an object.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Executes the command
    /// </summary>
    Task ExecuteAsync();
}

/// <summary>
/// Generic command handler interface that processes specific message types
/// </summary>
/// <typeparam name="TMessage">Type of message to handle</typeparam>
public interface ICommandHandler<in TMessage> where TMessage : IAppMessage
{
    /// <summary>
    /// Handles the specified message
    /// </summary>
    /// <param name="message">Message to handle</param>
    Task HandleAsync(TMessage message);
}

/// <summary>
/// Generic command that wraps a message and its handler
/// </summary>
/// <typeparam name="TMessage">Type of message</typeparam>
public class MessageCommand<TMessage> : ICommand where TMessage : IAppMessage
{
    private readonly TMessage _message;
    private readonly ICommandHandler<TMessage> _handler;

    public MessageCommand(TMessage message, ICommandHandler<TMessage> handler)
    {
        _message = message;
        _handler = handler;
    }

    public Task ExecuteAsync() => _handler.HandleAsync(_message);
}
