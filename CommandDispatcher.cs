namespace CoroutineWinNTRepl;

using System;
using System.Threading.Tasks;

/// <summary>
/// Dispatcher that maps messages to commands using the Command pattern.
/// This replaces the traditional switch statement with a more extensible design.
/// </summary>
public class CommandDispatcher
{
    private readonly ClickCommandHandler _clickHandler;
    private readonly KeyCommandHandler _keyHandler;
    private readonly TickCommandHandler _tickHandler;
    private readonly QuitCommandHandler _quitHandler;

    public CommandDispatcher()
    {
        _clickHandler = new ClickCommandHandler();
        _keyHandler = new KeyCommandHandler();
        _tickHandler = new TickCommandHandler();
        _quitHandler = new QuitCommandHandler();
    }

    public bool ShouldQuit => _quitHandler.ShouldQuit;

    /// <summary>
    /// Dispatches a message by creating and executing the appropriate command
    /// </summary>
    /// <param name="message">Message to dispatch</param>
    public async Task DispatchAsync(IAppMessage message)
    {
        ICommand? command = message switch
        {
            Click click => new MessageCommand<Click>(click, _clickHandler),
            Key key => new MessageCommand<Key>(key, _keyHandler),
            Tick tick => new MessageCommand<Tick>(tick, _tickHandler),
            Quit quit => new MessageCommand<Quit>(quit, _quitHandler),
            _ => null
        };

        if (command != null)
        {
            await command.ExecuteAsync();
        }
        else
        {
            // Similar to DispatchMessage when no handler is found
            Console.WriteLine($"Unhandled: {message.GetType().Name}");
        }
    }
}
