using System.Globalization;
using ModRunners.Api;
using ModRunners.Api.Drawing;
using ModRunners.Api.Runners;
using ModRunners.Api.Screens;

namespace ExampleDrawing;

/// <summary>
/// Draws a logo in the main menu's top right corner, and the speed of each runner played on this computer above its head.
/// </summary>
public sealed class Mod : IMod
{
    private const Font SpeedFont = Font.Kooky22;
    private const float LogoScale = 0.5f;
    private const float Margin = 20;
    private const float GapAboveHead = 10;

    private static readonly Color White = new(255, 255, 255);
    private static readonly Color Backdrop = new(0, 0, 0, 160);

    private IGame _game = null!;
    private IImage _logo = null!;

    public void OnLoad(IModContext context)
    {
        _game = context.Game;

        // Read from the mod's folder, where Logo.png ships next to the dll.
        _logo = context.Drawing.LoadImage("Logo.png");

        // Draw is raised once per frame, and a drawing only lasts that frame, so everything is drawn again each time.
        context.Drawing.Draw += OnDraw;
    }

    private void OnDraw(object? sender, DrawEventArgs e)
    {
        if (_game.Screen == Screen.MainMenu)
            DrawLogo(e.Canvas);

        foreach (var slot in _game.Lobby.Slots)
        {
            if (slot.IsLocal && slot.Runner is { IsOut: false } runner)
                DrawSpeed(e.Canvas, runner);
        }
    }

    private void DrawLogo(ICanvas canvas)
    {
        // The overlay is in pixels of the game's window, so its visible area is the whole window.
        if (canvas.VisibleArea(Layer.Overlay) is not { } window)
            return;

        var width = _logo.Width * LogoScale;
        var height = _logo.Height * LogoScale;

        canvas.DrawImage(
            _logo,
            new Rectangle(window.Width - width - Margin, Margin, width, height),
            Layer.Overlay
        );
    }

    private static void DrawSpeed(ICanvas canvas, IRunner runner)
    {
        var text = ((int)runner.Speed).ToString(CultureInfo.InvariantCulture);
        var size = canvas.MeasureText(text, default, SpeedFont);
        var head = runner.Hitbox;

        // In the level, positions are in level units: the text stays above the runner as the camera moves and zooms.
        var position = new Point(
            head.X + (head.Width - size.Width) / 2,
            head.Y - GapAboveHead - size.Height
        );

        canvas.FillRectangle(
            new Rectangle(position.X - 4, position.Y - 2, size.Width + 8, size.Height + 4),
            Backdrop,
            Layer.InFrontOfRunners
        );

        canvas.DrawText(text, position, SpeedFont, White, Layer.InFrontOfRunners);
    }
}
