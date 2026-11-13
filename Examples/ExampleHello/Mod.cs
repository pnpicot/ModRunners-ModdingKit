using ModRunners.Api;

namespace ExampleHello;

/// <summary>
/// The smallest mod: ModRunners creates this class when the game starts, then calls <see cref="OnLoad"/> once.
/// </summary>
public sealed class Mod : IMod
{
    public void OnLoad(IModContext context)
    {
        // The line goes to ModRunners/Logs/Runtime.log, after the mod's id.
        context.Log.Write($"Hello from {context.Name} {context.Version}!");
    }
}
