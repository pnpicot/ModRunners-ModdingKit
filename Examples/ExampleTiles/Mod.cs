using ModRunners.Api;
using ModRunners.Api.Drawing;
using ModRunners.Api.Options;
using ModRunners.Api.Tiles;

namespace ExampleTiles;

/// <summary>
/// Repaints every level: dark blue tiles, a light outline around each group of tiles, and pink marks on the tiles a runner can grapple or wall jump on.
/// </summary>
/// <remarks>
/// Each look of the tiles is a rule for every tile type, with exceptions for some types. Only this game draws the level differently, so the mod is fine in any match.
/// </remarks>
public sealed class Mod : IMod
{
    private const int OutlineThickness = 2;

    private static readonly Color TileColor = new(12, 16, 58);
    private static readonly Color OutlineColor = new(26, 107, 129);
    private static readonly Color MarkColor = new(240, 56, 93);

    private static readonly TileType[] MarkedTypes =
    [
        TileType.GrappleCeiling,
        TileType.WallJumpLeftFace,
        TileType.WallJumpRightFace
    ];

    private ITiles _tiles = null!;
    private IOption<bool> _isOn = null!;

    public void OnLoad(IModContext context)
    {
        _tiles = context.Game.Tiles;
        _isOn = context.Options.OnOff("On", "Repaint the levels", true);
        _isOn.Changed += (_, _) => Apply();

        Apply();
    }

    private void Apply()
    {
        if (_isOn.Value)
        {
            Paint();
        } else
        {
            Restore();
        }
    }

    private void Paint()
    {
        // The rules, for every tile type.
        _tiles.Palette = new TilePalette(TileColor, TilePalette.Default.Secondary);
        _tiles.Outline = new TileOutline(OutlineColor, OutlineThickness);

        // The exceptions: the marked tiles get their own palette, and no outline over their marks.
        foreach (var type in MarkedTypes)
        {
            _tiles.SetPalette(type, new TilePalette(TileColor, MarkColor));
            _tiles.SetOutline(type, TileOutline.None);
        }
    }

    private void Restore()
    {
        _tiles.Palette = TilePalette.Default;
        _tiles.Outline = TileOutline.None;

        foreach (var type in MarkedTypes)
        {
            _tiles.ClearPalette(type);
            _tiles.ClearOutline(type);
        }
    }
}
