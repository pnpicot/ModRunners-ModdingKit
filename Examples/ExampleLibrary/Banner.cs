using System.Diagnostics;
using ModRunners.Api;
using ModRunners.Api.Drawing;

namespace ExampleLibrary;

/// <summary>
/// A line of text across the middle of the screen, over a dark band, for a few seconds.
/// </summary>
/// <remarks>
/// It draws with ExampleLibrary's own drawing, kept from its OnLoad, so the mods that call it need nothing of theirs. Once a version of a library is published, the mods built against it rely on its public members: a later version adds new ones, and never changes or removes them.
/// </remarks>
public static class Banner
{
    private const Font TextFont = Font.Kooky30;
    private const float BandHeight = 80;

    private static readonly TimeSpan ShownFor = TimeSpan.FromSeconds(3);
    private static readonly Color White = new(255, 255, 255);
    private static readonly Color Band = new(0, 0, 0, 160);
    private static readonly Stopwatch SinceShown = new();

    private static string _text = string.Empty;

    /// <summary>
    /// Shows a text for three seconds, in place of the one shown.
    /// </summary>
    /// <param name="text"></param>
    public static void Show(string text)
    {
        _text = text;
        SinceShown.Restart();
    }

    internal static void Start(IDrawing drawing) =>
        drawing.Draw += (_, e) => Draw(e.Canvas);

    private static void Draw(ICanvas canvas)
    {
        if (!SinceShown.IsRunning
            || SinceShown.Elapsed > ShownFor
            || canvas.VisibleArea(Layer.Overlay) is not { } window)
            return;

        var size = canvas.MeasureText(_text, default, TextFont);
        var top = window.Y + (window.Height - BandHeight) / 2;

        canvas.FillRectangle(new Rectangle(
            window.X,
            top,
            window.Width,
            BandHeight
        ), Band, Layer.Overlay);

        canvas.DrawText(
            _text,
            new Point(
                window.X + (window.Width - size.Width) / 2,
                top + (BandHeight - size.Height) / 2
            ),
            TextFont,
            White,
            Layer.Overlay
        );
    }
}
