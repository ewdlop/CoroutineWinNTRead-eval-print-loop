namespace CoroutineWinNTRepl;

using System;

/// <summary>
/// Base interface for all application messages (similar to Windows messages)
/// </summary>
public interface IAppMessage { }

/// <summary>
/// Click message representing a mouse click event
/// </summary>
/// <param name="X">X coordinate</param>
/// <param name="Y">Y coordinate</param>
public record Click(int X, int Y) : IAppMessage;

/// <summary>
/// Key message representing a keyboard event
/// </summary>
/// <param name="Ch">Character pressed</param>
public record Key(char Ch) : IAppMessage;

/// <summary>
/// Tick message representing a timer event
/// </summary>
/// <param name="At">Timestamp of the tick</param>
public record Tick(DateTime At) : IAppMessage;

/// <summary>
/// Quit message to signal application shutdown (similar to WM_QUIT)
/// </summary>
public record Quit() : IAppMessage;
