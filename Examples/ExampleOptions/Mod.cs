using ModRunners.Api;
using ModRunners.Api.Drawing;
using ModRunners.Api.Input;
using ModRunners.Api.Options;

namespace ExampleOptions;

/// <summary>
/// One option of each kind, which the player edits on the options page: F1, or "ModRunners options" in the game's Options. Together they put a line of text on the screen.
/// </summary>
public sealed class Mod : IMod
{
    private const int MaxTextLength = 40;

    private IOption<bool> _isShown = null!;
    private IOption<Key> _toggleKey = null!;
    private IOption<string> _text = null!;
    private IOption<Color> _color = null!;
    private IOption<Font> _font = null!;
    private IOption<double> _height = null!;

    public void OnLoad(IModContext context)
    {
        var options = context.Options;

        // Options are declared here, in the order the options page lists them. A value is saved under its id, so the label can change in a later version, but the id must not, or players lose what they chose.
        _isShown = options.OnOff("Shown", "Show the text", true);
        _toggleKey = options.Key("ToggleKey", "Show or hide", Key.F4);
        _text = options.Text("Text", "Text", "Hello, runners!", MaxTextLength);
        _color = options.Color("Color", "Color", new Color(255, 200, 0));
        _font = options.Choice("Font", "Font", Font.Kooky30);
        _height = options.Number("Height", "Height on screen (%)", 10, 0, 90, 5);

        // Raised for the player's changes on the options page, and for the mod's own.
        _text.Changed += (_, e) => context.Log.Write($"The text is now \"{e.Current}\".");

        context.Game.Keyboard.KeyPressed += (_, e) =>
        {
            // Setting a value saves it, as if the player had changed it.
            if (e.Key == _toggleKey.Value)
                _isShown.Value = !_isShown.Value;
        };

        context.Drawing.Draw += (_, e) => Draw(e.Canvas);
    }

    private void Draw(ICanvas canvas)
    {
        if (!_isShown.Value || canvas.VisibleArea(Layer.Overlay) is not { } window)
            return;

        var size = canvas.MeasureText(_text.Value, default, _font.Value);

        var position = new Point(
            (window.Width - size.Width) / 2,
            window.Height * (float)_height.Value / 100
        );

        canvas.DrawText(_text.Value, position, _font.Value, _color.Value, Layer.Overlay);
    }
}
