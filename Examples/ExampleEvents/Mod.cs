using System.Diagnostics;
using ModRunners.Api;
using ModRunners.Api.Drawing;

namespace ExampleEvents;

/// <summary>
/// Follows a match through the game's events, and shows each one in the middle of the screen for a few seconds.
/// </summary>
public sealed class Mod : IMod
{
    private const Font MessageFont = Font.Kooky30;

    private static readonly TimeSpan ShownFor = TimeSpan.FromSeconds(3);
    private static readonly Color White = new(255, 255, 255);

    private readonly Stopwatch _sinceShown = new();

    private IModLog _log = null!;
    private string _message = string.Empty;

    public void OnLoad(IModContext context)
    {
        var game = context.Game;
        var match = game.Match;

        _log = context.Log;

        // Every event is raised on the game's thread, so a handler can use the whole Api.
        game.ScreenChanged += (_, e) => _log.Write($"From {e.Previous} to {e.Current}.");
        match.RoundStarted += (_, _) => Show("GO!");
        match.RunnerOut += (_, e) => Show($"{e.Runner.Slot.Name} is out ({e.Cause})");
        match.Ended += (_, _) => Show("The match is over");

        match.RoundEnded += (_, e) => Show(e.Winner is { } winner
            ? $"{winner.Name} wins the round"
            : "Nobody wins the round");

        context.Drawing.Draw += (_, e) => Draw(e.Canvas);
    }

    private void Show(string message)
    {
        _message = message;

        _sinceShown.Restart();
        _log.Write(message);
    }

    private void Draw(ICanvas canvas)
    {
        if (!_sinceShown.IsRunning
            || _sinceShown.Elapsed > ShownFor
            || canvas.VisibleArea(Layer.Overlay) is not { } window)
            return;

        var size = canvas.MeasureText(_message, default, MessageFont);
        var position = new Point((window.Width - size.Width) / 2, window.Height / 4);

        canvas.DrawText(_message, position, MessageFont, White, Layer.Overlay);
    }
}
