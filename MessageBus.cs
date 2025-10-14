using System.Threading.Channels;

namespace CoroutineWinNTRepl;

/// <summary>
/// Message bus for creating channels that transport application messages
/// </summary>
public static class MessageBus
{
    /// <summary>
    /// Creates an unbounded channel for application messages
    /// </summary>
    public static Channel<IAppMessage> Create() =>
        Channel.CreateUnbounded<IAppMessage>(new UnboundedChannelOptions
        {
            SingleReader = false,
            SingleWriter = false
        });
}
