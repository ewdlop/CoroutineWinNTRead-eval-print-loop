# Command Pattern Implementation

## Overview

This project demonstrates the implementation of the **Command Pattern** in a Win32-style message loop using C# coroutines and channels.

## Design Pattern: Command

The Command pattern encapsulates a request as an object, thereby letting you parameterize clients with different requests, queue or log requests, and support undoable operations.

### Key Components

1. **ICommand** - Command interface with `ExecuteAsync()` method
2. **ICommandHandler<TMessage>** - Generic handler interface for specific message types
3. **MessageCommand<TMessage>** - Concrete command that wraps a message and its handler
4. **CommandDispatcher** - Invoker that creates and executes commands

### Benefits

- **Open/Closed Principle**: Easy to add new commands without modifying existing code
- **Single Responsibility**: Each handler has one reason to change
- **Decoupling**: The invoker (dispatcher) is decoupled from the handlers
- **Testability**: Each component can be tested in isolation

## Message Flow

```
Producer → Channel → Pump → Dispatcher → Command → Handler
```

1. **Producer** writes messages to the channel
2. **Channel** acts as a queue (similar to Win32 message queue)
3. **Pump** pulls messages using `await foreach` (similar to GetMessage)
4. **Dispatcher** creates appropriate commands based on message type
5. **Command** encapsulates the handler call
6. **Handler** processes the message

## Comparison with Traditional Switch

### Before (Switch Statement)
```csharp
switch (msg)
{
    case Click(var x, var y):
        Console.WriteLine($"Click at ({x},{y})");
        break;
    // ... more cases
}
```

**Issues:**
- Adding new message types requires modifying the switch
- All handler logic is in one place
- Hard to test individual handlers

### After (Command Pattern)
```csharp
await dispatcher.DispatchAsync(msg);
```

**Advantages:**
- New handlers are separate classes
- Each handler is independently testable
- No modification to dispatcher when adding new messages (with DI)
- Clear separation of concerns

## File Structure

```
CoroutineWinNTRepl/
├── Messages.cs              # Message type definitions
├── CommandPattern.cs        # Command pattern interfaces
├── CommandHandlers.cs       # Concrete handler implementations
├── CommandDispatcher.cs     # Message-to-command dispatcher
├── MessageBus.cs           # Channel factory
├── Program.cs              # Main entry point with message pump
└── README.md               # Documentation
```

## Extension Points

To add a new message type:

1. Define the message in `Messages.cs`:
   ```csharp
   public record NewMessage(string Data) : IAppMessage;
   ```

2. Create a handler in `CommandHandlers.cs`:
   ```csharp
   public class NewMessageHandler : ICommandHandler<NewMessage>
   {
       public Task HandleAsync(NewMessage message)
       {
           // Handle the message
           return Task.CompletedTask;
       }
   }
   ```

3. Register in `CommandDispatcher.cs`:
   ```csharp
   NewMessage newMsg => new MessageCommand<NewMessage>(newMsg, _newMessageHandler),
   ```

## Further Improvements

- **Dependency Injection**: Use DI container to manage handlers
- **Middleware Pipeline**: Add interceptors for logging, validation
- **Command Queuing**: Store commands for later execution
- **Undo/Redo**: Implement command history
- **Async Commands**: Support long-running operations
