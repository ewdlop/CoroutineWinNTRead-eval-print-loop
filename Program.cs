using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CoroutineWinNTRepl;

/// <summary>
/// Main program demonstrating Win32-style message loop with Command pattern
/// </summary>
public class Program
{
    /// <summary>
    /// Message pump that yields messages from the channel reader.
    /// Similar to GetMessage in Win32, this pulls messages from the queue.
    /// </summary>
    static async IAsyncEnumerable<IAppMessage> Pump(
        ChannelReader<IAppMessage> reader,
        [EnumeratorCancellation] CancellationToken ct = default)
    {
        await foreach (var msg in reader.ReadAllAsync(ct))
        {
            yield return msg; // Similar to GetMessage pulling from the queue
        }
    }

    static async Task Main()
    {
        var ch = MessageBus.Create();
        var cts = new CancellationTokenSource();
        var dispatcher = new CommandDispatcher();

        // Producer (simulating event generation)
        // Could be replaced with any stream source
        _ = Task.Run(async () =>
        {
            await ch.Writer.WriteAsync(new Click(100, 200));
            await ch.Writer.WriteAsync(new Key('A'));
            await ch.Writer.WriteAsync(new Tick(DateTime.UtcNow));
            await Task.Delay(50);
            await ch.Writer.WriteAsync(new Quit()); // Similar to PostQuitMessage
            ch.Writer.Complete();
        });

        // Consumer (message loop)
        // Using Command pattern instead of switch statement for better extensibility
        await foreach (var msg in Pump(ch.Reader, cts.Token))
        {
            await dispatcher.DispatchAsync(msg);

            // Check if we should quit (similar to WM_QUIT in Win32)
            if (dispatcher.ShouldQuit)
            {
                break;
            }
        }
    }
}
