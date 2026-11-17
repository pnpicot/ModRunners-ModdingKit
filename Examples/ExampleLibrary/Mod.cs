using ModRunners.Api;

namespace ExampleLibrary;

/// <summary>
/// A library: on its own it does nothing, and it offers <see cref="Banner"/> to the mods that list ExampleLibrary in their manifest's Dependencies.
/// </summary>
public sealed class Mod : IMod
{
    /// <summary>
    /// Starts the banner. The mods that depend on ExampleLibrary load after it, so the banner is ready before any of them calls it.
    /// </summary>
    public void OnLoad(IModContext context) => Banner.Start(context.Drawing);
}