using ModRunners.Api;
using ModRunners.Api.Items;
using ModRunners.Api.Runners;

namespace ExampleItem;

/// <summary>
/// Adds a pistol to the item pickups: three shots, each a small and fast rocket that flies straight ahead and stuns briefly.
/// </summary>
/// <remarks>
/// Every game of a match raises <see cref="IModItem.Used"/> for its own copy of the runner, so each game fires the shot itself and nothing needs to be sent. A game without the mod sees the use but fires nothing.
/// </remarks>
public sealed class Mod : IMod
{
    private const int Shots = 3;
    private const float ShotScale = 0.6f;
    private const float ShotSpeed = 1500;
    private const float ShotExplosionRadius = 40;
    private const float ShotPush = 150;

    private static readonly TimeSpan ShotStun = TimeSpan.FromSeconds(0.35);

    /// <summary>
    /// From the runner's feet to the pistol's muzzle, for a runner facing right.
    /// </summary>
    private static readonly Point Muzzle = new(20, -30);

    private IGame _game = null!;

    public void OnLoad(IModContext context)
    {
        _game = context.Game;

        // Items can only be defined during OnLoad. The id must stay the same in every version of the mod.
        var pistol = context.Items.Define(
            "Pistol",
            "Pistol",
            context.Drawing.LoadImage("Pistol.png"),
            Shots
        );

        // How often a pickup gives it, on the scale of the game's own items (the shockwave is about 15).
        // A lobby option, so the host of a modded lobby decides it for every game.
        var odds = context.Options.Lobby.Number("Odds", "Pistol odds", 15, 0, 40, 1);

        pistol.RollWeight = (float)odds.Value;
        odds.Changed += (_, e) => pistol.RollWeight = (float)e.Current;
        pistol.Used += (_, e) => Fire(e.Runner);
    }

    private void Fire(IRunner runner)
    {
        var side = runner.FacesRight ? 1 : -1;
        var start = new Point(runner.Position.X + side * Muzzle.X, runner.Position.Y + Muzzle.Y);
        var shot = _game.Items.Rockets.Fire(runner.Slot, start, runner.FacesRight ? 0 : 180);

        // A rocket homes in on the runner ahead unless told otherwise.
        shot.HomingStrength = 0;
        shot.HomingGrowth = 0;
        shot.Scale = ShotScale;
        shot.Speed = ShotSpeed;
        shot.ExplosionRadius = ShotExplosionRadius;
        shot.Push = ShotPush;
        shot.StunDuration = ShotStun;
    }
}
