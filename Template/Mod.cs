using ModRunners.Api;

namespace ModRunnersMod;

/// <summary>
/// The mod's entry point: ModRunners creates this class when the game starts, then calls <see cref="OnLoad"/> once.
/// </summary>
public sealed class Mod : IMod
{
    public void OnLoad(IModContext context)
    {
        // The line goes to ModRunners/Logs/Runtime.log, after the mod's id.
        context.Log.Write($"{context.Name} {context.Version} is loaded.");

        // The game is context.Game, drawing on the screen context.Drawing, and the mod's options context.Options.
    }
}
