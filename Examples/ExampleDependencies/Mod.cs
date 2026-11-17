using System.Runtime.CompilerServices;
using ExampleLibrary;
using ModRunners.Api;

namespace ExampleDependencies;

/// <summary>
/// Announces each round of a match with ExampleLibrary's banner when ExampleLibrary is installed, and in the log otherwise.
/// </summary>
/// <remarks>
/// Its manifest lists ExampleLibrary as an optional dependency, and its project references ExampleLibrary's dll without copying it: ExampleLibrary loads from its own folder, before this mod. Without "Optional", the mod would not load without ExampleLibrary, and would call it directly, without checking.
/// </remarks>
public sealed class Mod : IMod
{
    private const string LibraryId = "ExampleLibrary";

    private IModLog _log = null!;
    private bool _hasLibrary;
    private int _round;

    public void OnLoad(IModContext context)
    {
        var match = context.Game.Match;

        _log = context.Log;

        // ExampleLibrary loads before this mod when it is installed, so the answer is already final here.
        _hasLibrary = context.IsModLoaded(LibraryId);

        match.Started += (_, _) => _round = 0;
        match.RoundStarted += (_, _) => Announce($"Round {++_round}");
    }

    private void Announce(string text)
    {
        if (_hasLibrary)
        {
            Library.ShowBanner(text);
        } else
        {
            _log.Write(text);
        }
    }

    /// <summary>
    /// The calls to ExampleLibrary, in a class of their own. .NET loads a dll when it compiles a method that names one of its types, so the methods that run without ExampleLibrary must not name them, even through a small method that the compiler would copy into them, which NoInlining prevents.
    /// </summary>
    private static class Library
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ShowBanner(string text) => Banner.Show(text);
    }
}